using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Formulas;
using Dependencies;

namespace SS //this was originally Spreadsheet and I changed it to SS
{
    /// <summary>
    /// A spreadsheet consists of an infinite number of named cells.
    /// 
    /// A string s is a valid cell name if and only if it consists of one or more letters, 
    /// followed by a non-zero digit, followed by zero or more digits.
    /// 
    /// For example, "A15", "a15", "XY32", and "BC7" are valid cell names.  On the other hand, 
    /// "Z", "X07", and "hello" are not valid cell names.
    /// 
    /// A spreadsheet contains a unique cell corresponding to each possible cell name.  
    /// In addition to a name, each cell has a contents and a value.  The distinction is
    /// important, and it is important that you understand the distinction and use
    /// the right term when writing code, writing comments, and asking questions.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In an empty spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError.
    /// The value of a Formula, of course, can depend on the values of variables.  The value 
    /// of a Formula variable is the value of the spreadsheet cell it names (if that cell's 
    /// value is a double) or is undefined (otherwise).  If a Formula depends on an undefined
    /// variable or on a division by zero, its value is a FormulaError.  Otherwise, its value
    /// is a double, as specified in Formula.Evaluate.
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        //This is where I save all of the cells
        private Dictionary<string,Cell> cells;

        //This is where I keep track of all of the dependencies
        private DependencyGraph graph;

        /// <summary>
        /// Each cell has a name and contents. The contents could be a string, formula, or double
        /// </summary>
        private struct Cell
        {
            public object Contents { get; set; } //gives public visibility but forces the use of a method to get and set
            public string Name { get; set; } //the get and set are already written right here

            public Cell (string name, object contents)
            {
                //otherwise, if everything is valid, set the cell to the values
                this.Contents = contents;
                this.Name = name;
            }
        }

        /// <summary>
        /// Zero argument constructor that creates an empty spreadsheet
        /// </summary>
        public Spreadsheet ()
        {
            this.cells = new Dictionary<string, Cell>();
            this.graph = new DependencyGraph();
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            //check for null
            if (name == null)
                throw new InvalidNameException();

            //check name validity
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                throw new InvalidNameException();

            //if it's there, return its value
            if (cells.TryGetValue(name, out Cell theCell))
            {
                object contents = theCell.Contents;
                return contents;
            }

            //if the cell isn't in the dictionary, it's empty, so return an empty string
            return "";
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            //return an empty list if there are no non-empty cells
            if (cells == null)
                return new List<string>();

            //otherwise return the cell names
            else return cells.Keys;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, double number)
        {
            //this kind of cell can only have dependents, not dependees

            //check for null
            if (name == null)
                throw new InvalidNameException();

            //check name validity
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                throw new InvalidNameException();

            HashSet<string> dependents = new HashSet<string>();

            //if the cell already exists, change the contents
            if (cells.TryGetValue(name, out Cell theCell))
            {
                cells.Remove(name);
                cells.Add(name, new Cell(name, number));
            }
            
            //otherwise add a new cell with the contents and return no dependents since it's a number
            else
                cells.Add(name, new Cell(name, number));

            //replace the dependees of this cell since it won't contain any cell names
            graph.ReplaceDependees(name, new HashSet<string>());

            //add name to the list of cells that depend on the value in this cell
            dependents.Add(name);

            foreach (string cellName in GetCellsToRecalculate(name))
            {
                dependents.Add(cellName);
            }

            return dependents;
        }

        //get cells to recalculate will throw a circulardependency exception when there is one. So we have to catch it and remove the dependency.

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, string text)
        {
            //this kind of cell can only have dependents, not dependees

            //check for null
            if (name == null)
                throw new InvalidNameException();

            //check name validity
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                throw new InvalidNameException();

            HashSet<string> dependents = new HashSet<string>();

            //if the cell already exists, change the contents
            if (cells.TryGetValue(name, out Cell theCell))
            {
                cells.Remove(name);
                cells.Add(name, new Cell(name, text));
            }

            //otherwise add a new cell with the contents and return no dependents since it's a number
            else
                cells.Add(name, new Cell(name, text));

            //replace the dependees of this cell since it won't contain any cell names
            graph.ReplaceDependees(name, new HashSet<string>());

            //add name to the list of cells that depend on the value in this cell
            dependents.Add(name);

            foreach (string cellName in GetCellsToRecalculate(name))
            {
                dependents.Add(cellName);
            }

            return dependents;
        }

        /// <summary>
        /// Requires that all of the variables in formula are valid cell names.
        /// 
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            //copy the dictionary and dependency graph in case we have to revert later
            DependencyGraph originalGraph = new DependencyGraph(graph);
            object originalCellContents = GetCellContents(name);
           
            //this is how I revert the cell contents
            bool isString = false, isDouble = false, isFormula = false;
            if (originalCellContents is double)
                isDouble = true;
            else if (originalCellContents is Formula)
                isFormula = true;
            else if (originalCellContents is string)
                isString = true;

            //check for null
            if (name == null)
                throw new InvalidNameException();

            //check name validity
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                throw new InvalidNameException();

            HashSet<string> dependents = new HashSet<string>();

            //make sure that every variable in the formula is a valid cell name
            HashSet<string> variables = new HashSet<string>(formula.GetVariables());
            foreach(string variable in variables)
            {
                if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                    throw new InvalidNameException();
            }

            //update dependency graph based on formula with replacedependencies
            graph.ReplaceDependees(name, variables);

            //if the cell already exists, change the contents
            if (cells.TryGetValue(name, out Cell theCell))
            {
                cells.Remove(name);
                cells.Add(name, new Cell(name, formula));
            }

            //otherwise add a new cell with the contents and return no dependents since it's a number
            else
                cells.Add(name, new Cell(name, formula));

            //empty set for dependents
            

            try
            {
                //add name to the list of cells that depend on the value in this cell
                dependents.Add(name);

                foreach (string cellName in GetCellsToRecalculate(name))
                {
                    dependents.Add(cellName);
                }
            }

            catch (CircularException c)
            {
                //revert the graph to what is was before
                this.graph = originalGraph;

                //revert the contents to what is was before
                if (isDouble)
                    SetCellContents(name,(double)originalCellContents);
                if (isString)
                    SetCellContents(name,(string)originalCellContents);
                if (isFormula)
                    SetCellContents(name,(Formula)originalCellContents);

                //rethrow the exception
                throw new CircularException();
            }

            return dependents;
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            //check for null name
            if (name == null)
                throw new ArgumentNullException();

            //check name validity
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                throw new InvalidNameException();

            //return the cells whose values depend on this cell
            return graph.GetDependees(name);
        }
    }
}

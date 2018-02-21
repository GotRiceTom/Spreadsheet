using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Formulas;
using Dependencies;
using System.IO;

// Written by Joe Zachary and Eric Naegle for CS 3500, February 2018

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
        //Fields 

        /// <summary>
        /// This is where I save all of the cells, with their names attached to them
        /// </summary>
        private Dictionary<string,Cell> cells;

        /// <summary>
        /// This is where I keep track of all of the dependencies of the spreadsheet.
        /// </summary>
        private DependencyGraph graph;

        /// <summary>
        /// This is what we use to make sure that the strings used for cell names are what we want them to be.
        /// </summary>
        private Regex isValid;

        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed
        {
            get => throw new NotImplementedException();
            protected set => throw new NotImplementedException();
        }

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
        /// Creates an empty Spreadsheet whose IsValid regular expression accepts every string.
        /// </summary>
        public Spreadsheet ()
        {
            this.cells = new Dictionary<string, Cell>();
            this.graph = new DependencyGraph();

            this.isValid = new Regex("@.*");
        }

        /// Creates an empty Spreadsheet whose IsValid regular expression is provided as the parameter
        public Spreadsheet(Regex isValid)
        {
            this.cells = new Dictionary<string, Cell>();
            this.graph = new DependencyGraph();

            this.isValid = isValid;
        }

        /// Creates a Spreadsheet that is a duplicate of the spreadsheet saved in source.
        ///
        /// See the AbstractSpreadsheet.Save method and Spreadsheet.xsd for the file format 
        /// specification.  
        ///
        /// If there's a problem reading source, throws an IOException.
        ///
        /// Else if the contents of source are not consistent with the schema in Spreadsheet.xsd, 
        /// throws a SpreadsheetReadException.  
        ///
        /// Else if the IsValid string contained in source is not a valid C# regular expression, throws
        /// a SpreadsheetReadException.  (If the exception is not thrown, this regex is referred to
        /// below as oldIsValid.)
        ///
        /// Else if there is a duplicate cell name in the source, throws a SpreadsheetReadException.
        /// (Two cell names are duplicates if they are identical after being converted to upper case.)
        ///
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a 
        /// SpreadsheetReadException.  (Use oldIsValid in place of IsValid in the definition of 
        /// cell name validity.)
        ///
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a
        /// SpreadsheetVersionException.  (Use newIsValid in place of IsValid in the definition of
        /// cell name validity.)
        ///
        /// Else if there's a formula that causes a circular dependency, throws a SpreadsheetReadException. 
        ///
        /// Else, create a Spreadsheet that is a duplicate of the one encoded in source except that
        /// the new Spreadsheet's IsValid regular expression should be newIsValid.
        public Spreadsheet(TextReader source, Regex newIsValid)
        {
            throw new NotImplementedException("You used a constructor that you haven't completed.");
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
            //if there aren't any nonempty cells, return an empty list
            if (cells.Count == 0)
                return new List<string>();

            //HashSet<string> keys = new HashSet<string>();
            //foreach (string name in cells.Keys)
                //keys.Add(name);

            //otherwise return the cell names
            return cells.Keys;
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
        protected override ISet<string> SetCellContents(string name, double number)
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
        protected override ISet<string> SetCellContents(string name, string text)
        {
            //this kind of cell can only have dependents, not dependees

            //check for null
            if (name == null)
                throw new InvalidNameException();
            if (text == null)
                throw new ArgumentNullException();

            //check name validity
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                throw new InvalidNameException();

            HashSet<string> dependents = new HashSet<string>();

            //if the cell already exists, change the contents
            if (cells.TryGetValue(name, out Cell theCell))
            {
                cells.Remove(name);
 
                //don't add a new cell if text is an empty string
                if (text != "")
                    cells.Add(name, new Cell(name, text));
            }

            //otherwise add a new cell with the contents if the text is nonempty
            else if (text != "")
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
        protected override ISet<string> SetCellContents(string name, Formula formula)
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

            //empty set for dependents
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

            try
            {
                //add name to the list of cells that depend on the value in this cell
                dependents.Add(name);

                foreach (string cellName in GetCellsToRecalculate(name))
                {
                    dependents.Add(cellName);
                }
            }

            catch (CircularException)
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
            return graph.GetDependents(name);
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to dest using an XML format.
        /// The XML elements should be structured as follows:
        ///
        /// <spreadsheet IsValid="IsValid regex goes here">
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        /// </spreadsheet>
        ///
        /// The value of the IsValid attribute should be IsValid.ToString()
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.
        /// If the cell contains a string, the string (without surrounding double quotes) should be written as the contents.
        /// If the cell contains a double d, d.ToString() should be written as the contents.
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        ///
        /// If there are any problems writing to dest, the method should throw an IOException.
        /// </summary>
        public override void Save(TextWriter dest)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        ///
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        ///
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor with s => s.ToUpper() as the normalizer and a validator that
        /// checks that s is a valid cell name as defined in the AbstractSpreadsheet
        /// class comment.  There are then three possibilities:
        ///
        ///   (1) If the remainder of content cannot be parsed into a Formula, a
        ///       Formulas.FormulaFormatException is thrown.
        ///
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///
        ///   (3) Otherwise, the contents of the named cell becomes f.
        ///
        /// Otherwise, the contents of the named cell becomes content.
        ///
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        ///
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            throw new NotImplementedException();
        }
    }
}

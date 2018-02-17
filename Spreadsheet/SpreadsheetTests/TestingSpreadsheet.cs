using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using System.Text.RegularExpressions;
using Formulas;
using System.Collections.Generic;

namespace SpreadsheetTests
{
    [TestClass]
    public class TestingSpreadsheet
    {
        /// <summary>
        /// This shouldn't blow up if I've made my constructor properly.
        /// </summary>
        [TestMethod]
        public void Creating_Abstract_Spreadsheet_Doesnt_Blow_Up()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
        }

        /// <summary>
        /// This should blow up because the name of the cell is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Null_Cell_Name()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null,4.0);
        }

        /// <summary>
        /// This is my first time using regex in an assignment so I'm making sure I do it right.
        /// </summary>
        [TestMethod]
        public void Practicing_Regex()
        {
            string name = "AA12345";
            if (!(Regex.IsMatch(name, @"^[A-z]+[1-9][0-9]*$")))
                Assert.Fail();

            string name2 = "AA012345";
            if (Regex.IsMatch(name2, @"^[A-z]+[1-9][0-9]*$"))
                Assert.Fail();

            string name3 = "A1";
            if (!(Regex.IsMatch(name3, @"^[A-z]+[1-9][0-9]*$")))
                Assert.Fail("A1 should work but it isn't.");
        }

        /// <summary>
        /// If a cell is empty, when we try to get its contents it should return an empty string
        /// </summary>
        [TestMethod]
        public void Get_Cell_Contents_Empty_Cell()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            string contents = (string)sheet.GetCellContents("A1");
            if (!(contents == ""))
                Assert.Fail();
        }

        /// <summary>
        /// Setting a cell's contents to 5.0 should return 5.0 when we call getCellContents
        /// </summary>
        [TestMethod]
        public void Get_Cell_Contents_A1_Has_Double()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 5.0);
            if (!((double)sheet.GetCellContents("A1") == 5.0))
                Assert.Fail();
        }

        /// <summary>
        /// Setting a cell's contents to hello should return hello when you use get
        /// </summary>
        [TestMethod]
        public void Get_Cell_Contents_A1_Has_String()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", "hello");
            if (!((string)sheet.GetCellContents("A1") == "hello"))
                Assert.Fail();
        }

        /// <summary>
        /// Setting a cell's contents to a formula should return the same formula string-wise
        /// </summary>
        [TestMethod]
        public void Get_Cell_Contents_A1_Has_Formula_No_Circle()
        {
            Formula correctFormula = new Formula("B1+2");
            string a = correctFormula.ToString();
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", new Formula("B1+2"));
            if (!(sheet.GetCellContents("A1").ToString() == a))
                Assert.Fail();
        }

        /// <summary>
        /// Setting a cell's contents to a formula should return the same formula
        /// </summary>
        [TestMethod]
        public void Get_Cell_Contents_A1_Has_Formula_No_Circle_2()
        {
            Formula correctFormula = new Formula("B1+2");
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", correctFormula);
            if (!(sheet.GetCellContents("A1").Equals(correctFormula)))
                Assert.Fail();
        }

        /// <summary>
        /// If you try to set a circular error it should throw a ccircular exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void Get_Cell_Contents_A1_Has_Formula_With_Circular_Error()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", new Formula("B1+2"));
            sheet.SetCellContents("B1", new Formula("C2"));
            sheet.SetCellContents("C2", new Formula("A1-1"));
        }

        /// <summary>
        /// If you try to get a cell's contents with a bad name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Get_Cell_Contents_Bad_Name()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents("A01");
        }

        /// <summary>
        /// If you try to set a cell's contents with a bad name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Set_Cell_Contents_Bad_Name()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A01","hi");
        }

        /// <summary>
        /// If you try to set a cell's contents with a bad name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Set_Cell_Contents_Null_Name2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, new Formula("B2+2"));
        }

        /// <summary>
        /// If you try to set a cell's contents with a bad name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Set_Cell_Contents_Bad_Name3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("G01", new Formula("B2+2"));
        }

        /// <summary>
        /// If you try to set a cell's contents with a bad name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Set_Cell_Contents_Bad_Name2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, "hi");
        }

        /// <summary>
        /// If you try to set a cell's contents with a bad name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Set_Cell_Contents_Bad_Name4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A01", 5.7);
        }

        /// <summary>
        /// If you try to get a cell's contents with a null name it should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Get_Cell_Contents_Null_Name()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);
        }

        /// <summary>
        /// This makes sure that getnamesofnonemptycells returns an empty set
        /// </summary>
        [TestMethod]
        public void Get_Names_Of_All_Nonempty_Cells_No_Cells()
        {
            HashSet<string> names = new HashSet<string>();
            AbstractSpreadsheet sheet = new Spreadsheet();
            foreach (string name in sheet.GetNamesOfAllNonemptyCells())
            {
                names.Add(name);
            }

            if (names.Count != 0)
                Assert.Fail();
        }

        /// <summary>
        /// This makes sure that getnamesofnonemptycells returns an empty set
        /// </summary>
        [TestMethod]
        public void Get_Names_Of_All_Nonempty_Cells_Multiple_Cells()
        {
            HashSet<string> names = new HashSet<string>();
            AbstractSpreadsheet sheet = new Spreadsheet();

            sheet.SetCellContents("A1", new Formula("B1+2"));
            sheet.SetCellContents("B1", new Formula("C2"));
            sheet.SetCellContents("C2", new Formula("D1-1"));

            foreach (string name in sheet.GetNamesOfAllNonemptyCells())
                names.Add(name);

            List<string> theNames = new List<string>();

            foreach (string name in names)
                theNames.Add(name);

            List<string> theNames2 = new List<string>{"A1","B1","C2"};

            if (theNames.Count != 3)
                Assert.Fail();

            if (theNames2.Count != 3)
                Assert.Fail();

            foreach (string name in theNames)
            {
                if (!(theNames2.Contains(name)))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// Adding a formula that contains a non-valid cell name should throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Formula_With_Non_Valid_Formula_Name()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents(null, new Formula("B+2"));
        }

        /// <summary>
        /// Set cell contents to a number when the cell already contains contents
        /// </summary>
        [TestMethod]
        public void Set_Contents_Already_Has_Contents_Number()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1","B1");
            sheet.SetCellContents("A1", 2.0);

            if ((double)sheet.GetCellContents("A1") != 2.0)
                Assert.Fail();
        }

        /// <summary>
        /// Set cell contents to a number when the cell already contains contents
        /// </summary>
        [TestMethod]
        public void Set_Contents_Already_Has_Contents_Formula()
        {
            Formula theFormula = new Formula("B1+C45");
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", "B1");
            sheet.SetCellContents("A1", theFormula);

            if (sheet.GetCellContents("A1").ToString() != theFormula.ToString())
                Assert.Fail();
        }
    }
}

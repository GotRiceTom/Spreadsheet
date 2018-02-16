using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using System.Text.RegularExpressions;

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

        [TestMethod]
        public void Get_Cell_Contents_Empty_Cell()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            string contents = (string)sheet.GetCellContents("A1");
            if (!(contents == ""))
                Assert.Fail();
        }

        [TestMethod]
        public void Get_Cell_Contents_A1_Has_Double()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetCellContents("A1", 5.0);
            if (!((double)sheet.GetCellContents("A1") == 5.0))
                Assert.Fail();
        }
    }
}

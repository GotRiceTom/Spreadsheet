using System;
using System.Collections.Generic;
using Dependencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DependencyGraphTestCases
{
    /// <summary>
    /// IMPORTANT NOTICE: My code coverage appears low because "Assert.Fail();" is never covered
    /// in these tests since they all pass. However, you'll notice that the code coverage in the 
    /// DependencyGraph class alone is near 100%.
    /// </summary>

    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// This was my first test, creating one dependency and making sure the size is right
        /// </summary>
        [TestMethod]
        public void AddOnce()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (!(testing.Size == 1))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that nothing crazy happens when we remove a dependency that doesn't exist.
        /// </summary>
        [TestMethod]
        public void RemoveWithNothing()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.RemoveDependency("a", "b");
            if (!(testing.Size == 0))
                Assert.Fail();
        }

        [TestMethod]
        public void AddEmptyDependency()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency(null,null);
            if (!(testing.Size == 0))
                Assert.Fail();
        }


        /// <summary>
        /// This makes sure that something that doesn't have dependents returns false
        /// </summary>
        [TestMethod]
        public void HasDependentsFALSE()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (testing.HasDependents("b"))
                Assert.Fail();
        }
        /// <summary>
        /// This makes sure that something that has dependents returns true
        /// </summary>
        [TestMethod]
        public void HasDependentsTRUE()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (!(testing.HasDependents("a")))
                Assert.Fail();
        }


        /// <summary>
        /// This makes sure that null returns false
        /// </summary>
        [TestMethod]
        public void HasDependentsNull()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (testing.HasDependents(null))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure something without dependeees returns false
        /// </summary>
        [TestMethod]
        public void HasDependees()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (testing.HasDependees("a"))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that something with dependees returns true
        /// </summary>
        [TestMethod]
        public void HasDependeesTRUE()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (!(testing.HasDependees("b")))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that a null returns with no dependees
        /// </summary>
        [TestMethod]
        public void HasDependeesNull()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (testing.HasDependees(null))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that a single remove function works with size
        /// </summary>
        [TestMethod]
        public void AddRemoveOnce()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.RemoveDependency("a", "b");
            if (!(testing.Size == 0))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that a duplicate entry doesn't do anything
        /// </summary>
        [TestMethod]
        public void Duplicate()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.AddDependency("a", "b");
            if (!(testing.Size == 1))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that non duplicate entries work
        /// </summary>
        [TestMethod]
        public void NonDuplicate()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.AddDependency("a", "c");
            if (!(testing.Size == 2))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that the has dependency method works after one entry
        /// </summary>
        [TestMethod]
        public void SimpleHasDepents()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            if (!(testing.HasDependents("a")))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that a remove works with hasDependents
        /// </summary>
        [TestMethod]
        public void SimpleHasDepents2()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.RemoveDependency("a", "b");
            if (testing.HasDependents("a"))
                Assert.Fail();
        }

        /// <summary>
        /// Check that null returns no dependents
        /// </summary>
        [TestMethod]
        public void GetDependentsNONE()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.RemoveDependency("a", "b");
            if (!(testing.GetDependents(null) == null))
                Assert.Fail();
        }

        /// <summary>
        /// Checks that null returns no dependees
        /// </summary>
        [TestMethod]
        public void GetDependeesNONE()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.RemoveDependency("a", "b");
            if (!(testing.GetDependees(null) == null))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that removing a dependency doesn't work with null
        /// </summary>
        [TestMethod]
        public void RemoveDependencyWithTNull()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.RemoveDependency("a", null);
            if (testing.Size != 1)
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that removeDependency works
        /// </summary>
        [TestMethod]
        public void CorrectDependents()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.AddDependency("a", "c");
            testing.AddDependency("a", "c");
            testing.AddDependency("a", "d");
            testing.RemoveDependency("a", "c");
            var x = new string[] { "b", "d" };
            var y = testing.GetDependents("a").ToArray<string>();

            if (testing.Size != 2)
                Assert.Fail();

            for (int a = 0; a < y.Length; a++)
            {
                if (!(x[a].Equals(y[a])))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// Make sure that removeDependees works
        /// </summary>
        [TestMethod]
        public void CorrectDependees()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("b", "c");
            testing.AddDependency("a", "c");
            testing.AddDependency("a", "c");
            testing.AddDependency("e", "c");
            testing.RemoveDependency("a", "c");
            var x = new string[] { "b", "e" };
            var y = testing.GetDependees("c").ToArray<string>();

            if (testing.Size != 2)
                Assert.Fail();

            for (int a = 0; a < y.Length; a++)
            {
                if (!(x[a].Equals(y[a])))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// Makes sure that dependees works in a different way
        /// </summary>
        [TestMethod]
        public void CorrectDependees2()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("b", "c");
            testing.AddDependency("a", "e");
            testing.AddDependency("a", "c");
            testing.AddDependency("e", "c");
            testing.RemoveDependency("a", "c");
            var x = new string[] { "b", "e" };
            var y = testing.GetDependees("c").ToArray<string>();

            if (testing.Size != 3)
                Assert.Fail();

            for (int a = 0; a < y.Length; a++)
            {
                if (!(x[a].Equals(y[a])))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// Creates 200,000 dependencies and removes almost all of them in well under a second
        /// </summary>
        [TestMethod]
        public void STRESSTEST_200k()
        {
            DependencyGraph testing = new DependencyGraph();
            for (int a = 0; a < 200000; a++)
                testing.AddDependency("a", a.ToString());

            for (int b = 0; b < 199998; b++)
                testing.RemoveDependency("a", b.ToString());

            if (!(testing.Size == 2))
                Assert.Fail();
        }

        /// <summary>
        /// Makes sure that replace dependence works with size and the actual values
        /// </summary>
        [TestMethod]
        public void ReplaceDependents1()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("b", "d");
            testing.AddDependency("a", "a");
            testing.AddDependency("a", "b");
            testing.AddDependency("a", "c");
            testing.AddDependency("a", "c");
            testing.AddDependency("a", "d");

            string[] replacements = new string[] { "f", "g", "h", "i", "j", "k" };
            testing.ReplaceDependents("a", replacements);

            var y = testing.GetDependents("a").ToArray<string>();

            if (testing.Size != 7)
                Assert.Fail();

            for (int a = 0; a < y.Length; a++)
            {
                if (!(replacements[a].Equals(y[a])))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// Makes sure that replace dependees works with size and actual values
        /// </summary>
        [TestMethod]
        public void ReplaceDependees1()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b");
            testing.AddDependency("a", "a");
            testing.AddDependency("b", "a");
            testing.AddDependency("c", "a");
            testing.AddDependency("d", "a");
            testing.AddDependency("e", "a");
            testing.AddDependency("f", "a");
            testing.AddDependency("g", "a");
            testing.AddDependency("h", "a");

            string[] replacements = new string[] { "f", "g", "h", "i" };
            testing.ReplaceDependees("a", replacements);

            var y = testing.GetDependees("a").ToArray<string>();

            if (testing.Size != 5)
                Assert.Fail();

            for (int a = 0; a < y.Length; a++)
            {
                if (!(replacements[a].Equals(y[a])))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// This tests adding, removing, and replacing dependents and dependees. I check the size, but I also
        /// personally debugged to check the values in each dictionary to check for accuracy.
        /// </summary>
        [TestMethod]
        public void TestingAlmostEverything()
        {
            DependencyGraph testing = new DependencyGraph();
            testing.AddDependency("a", "b"); //1
            testing.AddDependency("b", "c"); //2
            testing.AddDependency("a", "c"); //3
            testing.AddDependency("z", "c"); //4
            testing.AddDependency("a", "b"); //4
            testing.RemoveDependency("a", "b"); //3
            HashSet<string> replacementDependents = new HashSet<string> { "g", "h", "i", "j", "k" }; //7
            testing.ReplaceDependents("a", replacementDependents);
            HashSet<string> replacementDependees = new HashSet<string> { "r", "s", "t", "u", "v" }; //11
            testing.ReplaceDependees("k", replacementDependees);

            if (testing.Size != 11)
                Assert.Fail();


        }
    }
}

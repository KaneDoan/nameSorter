using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace nameSorter.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod] //Tests for names that have more than 3 first names
        public void NameTooLongTest()
        {
            string[] names = { "one two three four", "this name is too long", "i love testing very much" };
            bool findings;
            foreach (string line in names)
            {
                if (line.Count(Char.IsWhiteSpace) >= 3)
                {
                    findings = true;
                }
                else
                {
                    findings = false;
                    Assert.IsTrue(findings);
                }
            }
        }

        [TestMethod] //Tests for if line is blank/there is no name given
        public void NoNameTest()
        {
            string[] names = { " ", " ", " " };
            bool findings;
            foreach (string line in names)
            {
                if (line == null)
                {
                    findings = true;
                }
                else
                {
                    findings = false;
                    Assert.IsTrue(findings);
                }
            }
        }

        [TestMethod] //Tests for if there's a name with only one name
        public void OneNameTest()
        {
            string[] names = { "Medan", "Name", "Testing" };
            bool findings;
            foreach (string line in names)
            {
                if (line.Count(Char.IsWhiteSpace) <= 1)
                {
                    findings = true;
                }
                else
                {
                    findings = false;
                    Assert.IsTrue(findings);
                }
            }
        }
    }
}
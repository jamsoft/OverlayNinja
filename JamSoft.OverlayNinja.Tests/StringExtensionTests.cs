namespace JamSoft.OverlayNinja.Tests
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utils;

    public class StringExtensionTests
    {
        [Test]
        public void Given_Name_Formatted_With_Correct_Number_Of_Spaces()
        {
            var testName = "TestName";
            var formattedName = testName.FormatName(4, false);

            Assert.AreEqual(4, formattedName.Count(x => x.Equals(' ')));
        }

        [Test]
        public void Given_Name_Formatted_Correctly_With_Quotes()
        {
            var testName = "TestName";
            Assert.AreEqual("\"  TestName\"", testName.FormatName(2, true));
        }

        [Test]
        public void Given_Name_Formatted_Correctly_Without_Quotes()
        {
            var testName = "TestName";
            Assert.AreEqual("    TestName", testName.FormatName(4, false));
        }

        [Test]
        public void Given_Name_Throws_Null()
        {
            string testName = null;
            Assert.Throws<NullReferenceException>(() => testName.FormatName(4, false));
        }

        [Test]
        public void Given_Name_LeadingSpaces_Throws_Null()
        {
            string testName = null;
            Assert.Throws<NullReferenceException>(() => testName.CalculateNumberOfLeadingSpaces());
        }

        [Test]
        public void Given_Name_LeadingSpaces_Counts_Correct_Number_Of_Spaces()
        {
            string testName = "  TestName";
            Assert.AreEqual(2, testName.CalculateNumberOfLeadingSpaces());
        }
    }
}

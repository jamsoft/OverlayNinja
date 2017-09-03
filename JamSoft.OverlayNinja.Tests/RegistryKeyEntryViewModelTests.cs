namespace JamSoft.OverlayNinja.Tests
{
    using NUnit.Framework;
    using ViewModels;

    public class RegistryKeyEntryViewModelTests
    {
        [Test]
        public void RegKeyEntry_Given_Name_NameUi_Representation_Is_Set()
        {
            string testName = "TestName";
            var key = new RegistryKeyEntryViewModel { Name = testName };

            Assert.AreEqual(testName, key.Name);
            Assert.AreEqual("\"TestName\"", key.NameUi);
        }

        [Test]
        public void RegKeyEntry_Given_Name_NewName_Representation_IsNull()
        {
            string testName = "TestName";
            var key = new RegistryKeyEntryViewModel {Name = testName};

            Assert.AreEqual(testName, key.Name);
            Assert.Null(key.NewNameUi);
            Assert.Null(key.NewName);
        }

        [Test]
        public void RegKeyEntry_Given_Name_Ui_Representation_Correctly_Formatted()
        {
            string testName = "TestName";
            var key = new RegistryKeyEntryViewModel {Name = testName};

            Assert.AreEqual(testName, key.Name);
            Assert.Null(key.NewNameUi);
            Assert.Null(key.NewName);

            key.SelectedPriority = 2;

            Assert.AreEqual("\"  TestName\"", key.NewNameUi);
        }

        [Test]
        public void RegKeyEntry_Given_Name_WillRename_Is_False()
        {
            string testName = "TestName";
            var key = new RegistryKeyEntryViewModel { Name = testName };

            Assert.False(key.WillRename);
        }

        [Test]
        public void RegKeyEntry_Given_Name_Changed_WillRename_Is_True()
        {
            string testName = "TestName";
            var key = new RegistryKeyEntryViewModel
            {
                Name = testName,
                SelectedPriority = 2
            };

            Assert.True(key.WillRename);
        }

        [Test]
        public void RegKeyEntry_Given_Name_Changed_And_Reverted_WillRename_Is_False()
        {
            string testName = "TestName";
            var key = new RegistryKeyEntryViewModel
            {
                Name = testName,
                SelectedPriority = 2
            };

            Assert.True(key.WillRename);
            Assert.AreEqual("\"  TestName\"", key.NewNameUi);

            key.SelectedPriority = 0;

            Assert.False(key.WillRename);
            Assert.Null(key.NewNameUi);
            Assert.Null(key.NewName);
        }

        [Test]
        public void RegKeyEntry_Given_Name_Calculates_Leading_Spaces()
        {
            string testName = "  TestName";
            var key = new RegistryKeyEntryViewModel
            {
                Name = testName,
                SelectedPriority = 2
            };


            Assert.AreEqual(2, key.NumberOfLeadingSpaces);
        }

        [Test]
        public void RegKeyEntry_Given_Name_Null_WillRename_Is_False()
        {
            var key = new RegistryKeyEntryViewModel
            {
                SelectedPriority = 2
            };
            
            Assert.False(key.WillRename);
        }

        [Test]
        public void RegKeyEntry_Given_Name_Reset_WillRename_Is_False()
        {
            string testName = " TestName";
            var key = new RegistryKeyEntryViewModel
            {
                Name = testName,
                SelectedPriority = 2
            };

            Assert.AreEqual(1, key.NumberOfLeadingSpaces);
            Assert.True(key.WillRename);
            Assert.AreEqual("\"  TestName\"", key.NewNameUi);

            key.SelectedPriority = 0;

            Assert.True(key.WillRename);
            Assert.AreEqual("\"TestName\"", key.NewNameUi);
        }
    }
}

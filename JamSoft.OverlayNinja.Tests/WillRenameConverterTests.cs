namespace JamSoft.OverlayNinja.Tests
{
    using System.Windows.Media;
    using Converters;
    using NUnit.Framework;

    public class WillRenameConverterTests
    {
        [Test]
        public void Converts_False_To_White()
        {
            var converter = new WillRenameBackgroundConverter();
            var brush = (SolidColorBrush)converter.Convert(false, null, null, null);
            Assert.AreEqual(Colors.White, brush.Color);
        }

        [Test]
        public void Converts_True_To_Red()
        {
            var converter = new WillRenameBackgroundConverter();
            var brush = (SolidColorBrush)converter.Convert(true, null, null, null);
            Assert.AreEqual(Colors.Red, brush.Color);
        }
    }
}

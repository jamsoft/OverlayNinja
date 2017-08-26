namespace JamSoft.OverlayNinja.Utils
{
    public static class StringExtensions
    {
        public static string FormatName(this string input, int priority, bool includeQuotes)
        {
            var trimmedInput = input.Trim();
            var formattedInput = trimmedInput.PadLeft(trimmedInput.Length + priority, ' ');
            if (includeQuotes)
            {
                return $"\"{formattedInput}\"";
            }

            return $"{formattedInput}";
        }
    }
}
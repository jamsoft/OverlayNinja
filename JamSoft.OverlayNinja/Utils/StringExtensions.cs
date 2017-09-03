namespace JamSoft.OverlayNinja.Utils
{
    public static class StringExtensions
    {
        /// <summary>
        /// Formats the name.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="includeQuotes">if set to <c>true</c> [include quotes].</param>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates the number of leading spaces.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static int CalculateNumberOfLeadingSpaces(this string input)
        {
            var chars = input.ToCharArray();
            int leadingSpaces = 0;
            foreach (var c in chars)
            {
                if (c == ' ')
                {
                    leadingSpaces++;
                }
                else
                {
                    break;
                }
            }

            return leadingSpaces;
        }
    }
}
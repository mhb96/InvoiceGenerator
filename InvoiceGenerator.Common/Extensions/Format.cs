namespace InvoiceGenerator.Common.Extensions
{
    /// <summary>
    /// The format extensions class
    /// </summary>
    public static class Format
    {
        /// <summary>
        /// Convert to 2 dp money string.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatTo2DpMoneyString(this decimal number)
        {
            return string.Format("{0:n}", number);
        }

        /// <summary>
        /// Convert to 2 dp money string.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatToQuantityString(this decimal number)
        {
            return string.Format("{0:#,##0.##}", number);
        }
    }
}

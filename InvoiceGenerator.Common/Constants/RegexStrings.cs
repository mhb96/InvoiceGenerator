namespace InvoiceGenerator.Common.Constants
{
    public static class RegexStrings
    {
        public const string Username = "^[a-zA-Z0-9]+([a-zA-Z0-9._]*[a-zA-Z0-9]+)?$";
        public const string Name = "^[a-zA-Z]+([a-zA-Z. ]*[a-zA-Z]+)?$";
        public const string Email = @"^(.+[@]+.+)*$";
        public const string ObjectName = "^[ -~]*$";
        public const string Password = "^[a-zA-Z0-9,._!$@#%^&*()]+$";
        public const string ContactNo = "^([+]?[0-9]+([0-9 -]*[0-9]+))?$";
    }
}

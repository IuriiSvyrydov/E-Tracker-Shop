

namespace E_Tracker.Infrastructure.StaticServices;

    public static class NameService
    {
        public static string CharacterRegulatory(string name) =>

            name.Replace("~", "")
                .Replace("!", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("^", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("_", "")
                .Replace("-", "")
                .Replace("'", "")
                .Replace(":", "")
                .Replace(">", "")
                .Replace("?", "")
                .Replace("|", "");

    }


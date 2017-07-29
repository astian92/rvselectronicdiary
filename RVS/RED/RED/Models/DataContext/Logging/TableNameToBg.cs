namespace RED.Models.DataContext.Logging
{
    public static class TableNameToBg
    {
        public static string Get(string tableName)
        {
            string result = string.Empty;

            switch (tableName)
            {
                case "User":
                    result = "Потребител";
                    break;

                case "Role":
                    result = "Роля";
                    break;

                case "Test":
                    result = "Изследване";
                    break;

                case "TestCategory":
                    result = "Категория (изследване)";
                    break;

                default:
                    result = string.Empty;
                    break;
            }

            return result;
        }
    }
}
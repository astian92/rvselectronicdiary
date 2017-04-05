namespace RED.Models
{
    public static class TestTypes
    {
        static TestTypes()
        {
            MKB = "МКБ";
            FZH = "ФЗХ";
        }

        public static string MKB { get; private set; }

        public static string FZH { get; private set; }
    }
}

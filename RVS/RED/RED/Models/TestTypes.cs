namespace RED.Models
{
    public static class TestTypes
    {
        static TestTypes()
        {
            MKB = "МКБ";
            FZH = "ФЗХ";
            ORG = "ОРГ";
            DZM = "ДЗМ";
            PRT = "ПРТ";
        }

        public static string MKB { get; private set; }

        public static string FZH { get; private set; }

        public static string ORG { get; private set; }

        public static string DZM { get; private set; }

        public static string PRT { get; private set; }
    }
}
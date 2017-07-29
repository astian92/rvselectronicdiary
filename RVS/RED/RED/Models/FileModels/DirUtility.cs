namespace RED.Models.FileModels
{
    public static class DirUtility
    {
        public static string CalculateDirectoryName(int diaryNumber)
        {
            int start = -1000;
            int end = 0;

            bool found = false;

            while (!found)
            {
                start += 1000;
                end += 1000;
                found = start < diaryNumber && diaryNumber < end;
            }

            return start.ToString("0000") + "-" + end;
        }
    }
}
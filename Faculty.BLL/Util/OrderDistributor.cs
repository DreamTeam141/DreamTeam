namespace Faculty.BLL.Util
{
    public static class OrderDistributor
    {
        private const string ValueAscending = "asc";
        private const string TextAscending = "Name ascending";
        private const string ValueDescending = "desc";
        private const string TextDescending = "Name descending";
        private const string ValueDuration = "dur";
        private const string TextDuration = "Duration";
        private const string ValueNumberOfStudents = "number";
        private const string TextNumberOfStudents = "Number of students";

        public static string GetValueAscending()
        {
            return ValueAscending;
        }

        public static string GetTextAscending()
        {
            return TextAscending;
        }

        public static string GetValueDescending()
        {
            return ValueDescending;
        }

        public static string GetTextDescending()
        {
            return TextDescending;
        }

        public static string GetValueDuration()
        {
            return ValueDuration;
        }

        public static string GetTextDuration()
        {
            return TextDuration;
        }

        public static string GetValueNumberOfStudents()
        {
            return ValueNumberOfStudents;
        }

        public static string GetTextNumberOfStudents()
        {
            return TextNumberOfStudents;
        }

    }
}
namespace Faculty.BLL.Util
{
    public static class RoleDistributer
    {
        private const string AdminRole = "admin";
        private const string StudentRole = "student";
        private const string TeacherRole = "teacher";
        private const string BlockRole = "block";

        public static string GetAdminRole()
        {
            return AdminRole;
        }

        public static string GetStudentRole()
        {
            return StudentRole;
        }

        public static string GetTeacherRole()
        {
            return TeacherRole;
        }

        public static string GetBlockRole()
        {
            return BlockRole;
        }
    }
}
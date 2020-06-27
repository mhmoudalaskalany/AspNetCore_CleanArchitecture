namespace BackendCore.Common.Extensions
{
    public static class Permission
    {
        public static class Users
        {
            public const string View = "Permission.Users.View";
            public const string Create = "Permission.Users.Create";
            public const string Edit = "Permission.Users.Edit";
            public const string Delete = "Permission.Users.Delete";
        }

        public static class Roles
        {
            public const string View = "Permission.Roles.View";
            public const string Create = "Permission.Roles.Create";
            public const string Edit = "Permission.Roles.Edit";
            public const string Delete = "Permission.Roles.Delete";
        }
        public static class Apps
        {
            public const string View = "Permission.Applications.View";
            public const string Create = "Permission.Applications.Create";
            public const string Edit = "Permission.Applications.Edit";
            public const string Delete = "Permission.Applications.Delete";
        }
        public static class Pages
        {
            public const string View = "Permission.Pages.View";
            public const string Create = "Permission.Pages.Create";
            public const string Edit = "Permission.Pages.Edit";
            public const string Delete = "Permission.Pages.Delete";
        }
        public static class Permissions
        {
            public const string View = "Permission.Permissions.View";
            public const string Create = "Permission.Permissions.Create";
            public const string Edit = "Permission.Permissions.Edit";
            public const string Delete = "Permission.Permissions.Delete";
        }
        public class CustomClaimTypes
        {
            public const string Permission = "permission";
        }
    }
}

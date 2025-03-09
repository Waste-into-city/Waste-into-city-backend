namespace WasteIntoCity.Application
{
    public static class ApiRoutes
    {
        private const string ROOT = "api";

        private const string BASE = $"{ROOT}/{VERSION}";


        public const string VERSION = "v1";


        public static class Users
        {
            public const string CONTROLLER_NAME = "users";

            public const string GET = $$"""{{BASE}}/{{CONTROLLER_NAME}}/{userId}""";
        }

        public static class Identity
        {
            public const string LOGIN = $"{BASE}/identity/login";

            public const string REGISTER = $"{BASE}/identity/register";
        }
    }
}

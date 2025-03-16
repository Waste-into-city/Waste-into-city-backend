namespace WasteIntoCity.Application
{
    public static class ApiRoutes
    {
        private const string ROOT = "api";

        private const string BASE = $"{ROOT}/{VERSION}";

        public const string VERSION = "v1";

        public static class Swagger
        {
            private const string CONTROLLER_NAME = "swagger";

            public const string SWAGGER_ENDPOINT = $"{CONTROLLER_NAME}/{VERSION}/swagger.json";
        }

        public static class Users
        {
            private const string CONTROLLER_NAME = "users";

            public const string GET = $$"""{{BASE}}/{{CONTROLLER_NAME}}/{userId}""";
        }

        public static class Identity
        {
            private const string CONTROLLER_NAME = "identity";

            public const string LOGIN = $"{BASE}/{CONTROLLER_NAME}/login";

            public const string REGISTER = $"{BASE}/{CONTROLLER_NAME}/register";

            public const string REFRESH = $"{BASE}/{CONTROLLER_NAME}/refresh";

            public const string LOGOUT = $"{BASE}/{CONTROLLER_NAME}/logout";
        }

        public static class Works
        {
            private const string CONTROLLER_NAME = "works";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string GET_ALL = $"{BASE}/{CONTROLLER_NAME}/get-all";

            public const string GET_BY_ID = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-by-id/{id}""";

            public const string GET_ALL_OWN_TAKE_PART_IN = $"{BASE}/{CONTROLLER_NAME}/get-all-own-take-part-in";

            public const string UPDATE = $$"""{{BASE}}/{{CONTROLLER_NAME}}/update/{id}""";

            public const string UPDATE_WORK_STATUS = $$"""{{BASE}}/{{CONTROLLER_NAME}}/update-work-status/{id}""";
        }

    }
}

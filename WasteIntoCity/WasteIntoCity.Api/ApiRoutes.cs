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

        public static class Identity
        {
            private const string CONTROLLER_NAME = "identity";

            public const string LOGIN = $"{BASE}/{CONTROLLER_NAME}/login";

            public const string REGISTER = $"{BASE}/{CONTROLLER_NAME}/register";

            public const string REFRESH = $"{BASE}/{CONTROLLER_NAME}/refresh";

            public const string LOGOUT = $"{BASE}/{CONTROLLER_NAME}/logout";

            public const string GET_SELF_USER_INFO = $"{BASE}/{CONTROLLER_NAME}/get-self-user-info";

            public const string GET_USER_INFO = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-user-info/{userId:Guid}""";

            public const string GET_USER_INFO_FOR_ADMIN = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-user-info-for-admin/{userId:Guid}""";
        }

        public static class Works
        {
            private const string CONTROLLER_NAME = "works";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string GET_ALL = $"{BASE}/{CONTROLLER_NAME}/get-all";

            public const string GET_BY_ID = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-by-id/{id:Guid}""";

            public const string GET_ALL_OWN_TAKE_PART_IN = $"{BASE}/{CONTROLLER_NAME}/get-all-own-take-part-in";

            public const string UPDATE = $$"""{{BASE}}/{{CONTROLLER_NAME}}/update/{id:Guid}""";

            public const string UPDATE_WORK_STATUS = $$"""{{BASE}}/{{CONTROLLER_NAME}}/update-work-status/{id:Guid}""";
        }

        public static class WorkApplications
        {
            private const string CONTROLLER_NAME = "work-applications";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string REJECT = $$"""{{BASE}}/{{CONTROLLER_NAME}}/reject/{workApplicationsId}""";

            public const string CONFIRM = $$"""{{BASE}}/{{CONTROLLER_NAME}}/confirm/{workApplicationsId}""";
        }

        public static class Images
        {
            private const string CONTROLLER_NAME = "images";

            public const string UPLOAD = $"{BASE}/{CONTROLLER_NAME}/upload";

            public const string GET_BY_NAME = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-by-name/{name}""";
        }

    }
}

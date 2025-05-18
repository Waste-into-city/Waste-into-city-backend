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

            public const string GET_LEADERBOARD_PAGE_BY_BEST_RANKING = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-leaderboard-page-by-best-ranking""";

            public const string UPDATE_OWN_USER_INFO = $"{BASE}/{CONTROLLER_NAME}/update-own-user-info";
        }

        public static class Works
        {
            private const string CONTROLLER_NAME = "works";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string GET_ALL = $"{BASE}/{CONTROLLER_NAME}/get-all";

            public const string GET_BY_ID = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-by-id/{id:Guid}""";

            public const string GET_ALL_LOOKUP = $"{BASE}/{CONTROLLER_NAME}/get-all-lookup";

            public const string GET_ALL_OWN_TAKE_PART_IN = $"{BASE}/{CONTROLLER_NAME}/get-all-own-take-part-in";

            public const string UPDATE = $$"""{{BASE}}/{{CONTROLLER_NAME}}/update/{id:Guid}""";

            public const string UPDATE_WORK_STATUS = $$"""{{BASE}}/{{CONTROLLER_NAME}}/update-work-status/{id:Guid}""";

            public const string TAKE_PART_IN_FIRST = $$"""{{BASE}}/{{CONTROLLER_NAME}}/take-part-in-first-self/{id:Guid}""";

            public const string TAKE_PART_IN = $$"""{{BASE}}/{{CONTROLLER_NAME}}/take-part-in-self/{id:Guid}""";

            public const string LEAVE_FROM_PARTICIPATION = $$"""{{BASE}}/{{CONTROLLER_NAME}}/leave-from-participation-self/{id:Guid}""";
        }

        public static class WorkApplications
        {
            private const string CONTROLLER_NAME = "work-applications";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string REJECT = $$"""{{BASE}}/{{CONTROLLER_NAME}}/reject/{workApplicationsId}""";

            public const string CONFIRM = $$"""{{BASE}}/{{CONTROLLER_NAME}}/confirm/{workApplicationsId}""";

            public const string GET_FROM_QUEUE = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-from-queue""";
        }

        public static class WorkReportResults
        {
            private const string CONTROLLER_NAME = "work-report-results";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string GET = $$"""{{BASE}}/{{CONTROLLER_NAME}}/{workReportResultId:Guid}""";
        }

        public static class WorkReportComplaints
        {
            private const string CONTROLLER_NAME = "work-report-complains";

            public const string CREATE = $"{BASE}/{CONTROLLER_NAME}/create";

            public const string GET = $$"""{{BASE}}/{{CONTROLLER_NAME}}/{workReportComplaintId:Guid}""";

            public const string CONFIRM = $$"""{{BASE}}/{{CONTROLLER_NAME}}/confirm/{workReportComplaintId:Guid}""";

            public const string REJECT = $$"""{{BASE}}/{{CONTROLLER_NAME}}/reject/{workReportComplaintId:Guid}""";

            public const string GET_FROM_QUEUE = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-from-queue""";
        }

        public static class Images
        {
            private const string CONTROLLER_NAME = "images";

            public const string UPLOAD = $"{BASE}/{CONTROLLER_NAME}/upload";

            public const string GET_BY_NAME = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-by-name/{name}""";

            public const string GET_BY_NAMES = $"{BASE}/{CONTROLLER_NAME}/get-by-names";
        }

        public static class WorkColleagueReports
        {
            private const string CONTROLLER_NAME = "work-colleague-reports";

            public const string CREATE_MARKS = $"{BASE}/{CONTROLLER_NAME}/create-marks";

            public const string GET_ALL_BY_WORKS_ID = $$"""{{BASE}}/{{CONTROLLER_NAME}}/get-all-works-by-id/{worksId:Guid}""";
        }

        public static class AdminPanel
        {
            private const string CONTROLLER_NAME = "admin-panel";

            public const string SAVE_ALL_DATA = $"{BASE}/{CONTROLLER_NAME}/save-all-data";

            public const string GET_ALL_DATA = $"{BASE}/{CONTROLLER_NAME}/get-all-data";
        }
    }
}

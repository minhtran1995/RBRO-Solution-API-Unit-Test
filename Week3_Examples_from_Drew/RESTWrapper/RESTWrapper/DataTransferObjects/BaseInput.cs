namespace RESTWrapper.DataTransferObjects
{
    internal class BaseInput
    {
        public int Version { get; set; }
        public int TimeZoneOffset { get; set; }
        public AuthenticationInfoData Authentication { get; set; }

        public BaseInput(string domain, string userId, string password) : this()
        {
            Authentication = new AuthenticationInfoData()
            {
                domain = domain,
                userID = userId,
                password = password,
                appUuid = "F52191FA-A2BD-4E9A-B36A-7735FF6A022A",
            };
        }

        public BaseInput()
        {
            Version = 2;
            TimeZoneOffset = 0;
        }
    }
}

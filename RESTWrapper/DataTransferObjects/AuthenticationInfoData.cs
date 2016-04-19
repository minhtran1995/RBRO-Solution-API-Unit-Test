namespace RESTWrapper.DataTransferObjects
{
    internal class AuthenticationInfoData
    {
        public string userID { get; set; }
        public string password { get; set; }
        public string domain { get; set; }
        public string appUuid { get; set; }

        public AuthenticationInfoData()
        {
            appUuid = "F52191FA-A2BD-4E9A-B36A-7735FF6A022A";
        }
    }
}

namespace RESTWrapper.DataTransferObjects
{
    internal class AuthenticationInfoData
    {
        public string userID { get; set; }
        public string password { get; set; }
        public string domain { get; set; }
        public string appUuid { get; set; }
    }
}

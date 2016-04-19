using RESTWrapper.DataTransferObjects;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace UnitTests.FakeClasses
{
    internal static class Responses
    {
        internal static HttpResponseMessage GetFakePingResponse()
        {
            return GetFakePingResponse(0);
        }

        internal static HttpResponseMessage GetFakePingResponse(int statusCode)
        {
            BaseOutput output = new BaseOutput();
            SetBaseOutput(statusCode, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<BaseOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }

        internal static HttpResponseMessage GetFakeTopResponse()
        {
            DatabaseData db1 = new DatabaseData()
            {
                Name = "WorkSite9a",
                Description = "Database1",
            };
            TopOutput output = new TopOutput()
            {
                PreferredDatabaseName = "WorkSite9a",
                Databases = new DatabaseData[] { db1 },
            };
            SetBaseOutput(0, output);

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ObjectContent<BaseOutput>(output, new JsonMediaTypeFormatter()),
            };

            return response;
        }

        private static void SetBaseOutput(int statusCode, BaseOutput output)
        {
            output.ServerVersion = 3;
            output.statusCode = statusCode;
            output.statusDetail = (statusCode == 0 ? "Success" : "Failed");
            output.Success = (statusCode == 0 ? 0 : 1);
        }
    }
}

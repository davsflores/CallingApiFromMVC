namespace CallingApiFromMVC.Helper
{
    public class StudentAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5208/");
            return client;
        }
    }
}

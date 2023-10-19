namespace CarLinePickup.API.Models.Response.Common
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Field { get; set; }
    }
}

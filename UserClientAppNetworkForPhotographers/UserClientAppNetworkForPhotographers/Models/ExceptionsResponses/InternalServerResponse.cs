namespace UserClientAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class InternalServerResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public InternalServerResponse(string message)
        {
            Status = StatusCodes.Status500InternalServerError;
            Message = message;
        }
    }
}

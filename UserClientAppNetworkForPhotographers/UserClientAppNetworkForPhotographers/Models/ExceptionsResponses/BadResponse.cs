namespace UserClientAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class BadResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public BadResponse(string message)
        {
            Status = StatusCodes.Status400BadRequest;
            Message = message;
        }
    }
}

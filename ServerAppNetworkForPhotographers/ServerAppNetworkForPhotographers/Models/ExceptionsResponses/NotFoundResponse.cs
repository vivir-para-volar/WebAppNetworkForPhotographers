namespace ServerAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class NotFoundResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public NotFoundResponse(string message)
        {
            Status = StatusCodes.Status404NotFound;
            Message = message;
        }
    }
}

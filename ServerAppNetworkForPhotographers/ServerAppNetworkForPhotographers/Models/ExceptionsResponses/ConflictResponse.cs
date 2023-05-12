namespace ServerAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class ConflictResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public ConflictResponse(string message)
        {
            Status = StatusCodes.Status409Conflict;
            Message = message;
        }
    }
}

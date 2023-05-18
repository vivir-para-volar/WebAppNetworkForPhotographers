namespace UserClientAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class ExceptionResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public ExceptionResponse(string message)
        {
            Status = StatusCodes.Status400BadRequest;
            Message = message;
        }
    }
}

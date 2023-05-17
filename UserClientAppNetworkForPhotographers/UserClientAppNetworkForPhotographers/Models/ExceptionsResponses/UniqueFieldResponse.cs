namespace UserClientAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class UniqueFieldResponse
    {
        public int Status { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }

        public UniqueFieldResponse(string field, string message)
        {
            Status = StatusCodes.Status409Conflict;
            Field = field;
            Message = message;
        }
    }
}

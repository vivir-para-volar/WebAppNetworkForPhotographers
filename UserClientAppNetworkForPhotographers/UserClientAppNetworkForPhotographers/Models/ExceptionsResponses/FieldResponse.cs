namespace UserClientAppNetworkForPhotographers.Models.ExceptionsResponses
{
    public class FieldResponse
    {
        public int Status { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }

        public FieldResponse(string field, string message)
        {
            Status = StatusCodes.Status409Conflict;
            Field = field;
            Message = message;
        }
    }
}

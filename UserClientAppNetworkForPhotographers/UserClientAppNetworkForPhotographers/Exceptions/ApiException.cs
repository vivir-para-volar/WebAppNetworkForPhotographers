namespace UserClientAppNetworkForPhotographers.Exceptions
{
    public class ApiException : Exception
    {
        public override string? Message { get; }
        public int Status { get; }

        public ApiException() { }

        public ApiException(int status, string? message = null)
        {
            Status = status;
            Message = message;
        }

        public Object ToObj()
        {
            return new { status = Status, message = Message };
        }
    }
}

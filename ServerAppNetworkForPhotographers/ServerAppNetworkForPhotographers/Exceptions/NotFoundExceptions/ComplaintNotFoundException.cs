namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class ComplaintNotFoundException : Exception
    {
        public override string Message { get; }

        public ComplaintNotFoundException(int id) : base("")
        {
            Message = $"Complaint with id = {id} not found";
        }
    }
}

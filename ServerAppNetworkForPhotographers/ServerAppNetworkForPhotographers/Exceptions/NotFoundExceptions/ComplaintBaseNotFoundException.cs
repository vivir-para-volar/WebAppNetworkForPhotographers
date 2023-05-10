namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class ComplaintBaseNotFoundException : Exception
    {
        public override string Message { get; }

        public ComplaintBaseNotFoundException(int id) : base("")
        {
            Message = $"ComplaintsBase with id = {id} not found";
        }
    }
}

namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class ComplaintsBaseNotFoundException : Exception
    {
        public override string Message { get; }

        public ComplaintsBaseNotFoundException(int id) : base("")
        {
            Message = $"ComplaintsBase with id = {id} not found";
        }
    }
}

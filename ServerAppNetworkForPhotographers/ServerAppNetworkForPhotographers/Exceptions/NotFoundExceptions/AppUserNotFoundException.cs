namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class AppUserNotFoundException : Exception
    {
        public override string Message { get; }

        public AppUserNotFoundException(string? id) : base("")
        {
            if (id == null)
            {
                Message = $"User not found";
            }

            Message = $"User with id = {id} not found";
        }
    }
}

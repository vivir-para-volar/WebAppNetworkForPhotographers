namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class PhotographerNotFoundException : Exception
    {
        public override string Message { get; }

        public PhotographerNotFoundException(int id) : base("")
        {
            Message = $"Photographer with id = {id} not found";
        }
    }
}

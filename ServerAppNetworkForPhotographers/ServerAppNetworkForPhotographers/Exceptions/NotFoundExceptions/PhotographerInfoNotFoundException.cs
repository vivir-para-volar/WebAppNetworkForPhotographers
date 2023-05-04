namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class PhotographerInfoNotFoundException : Exception
    {
        public override string Message { get; }

        public PhotographerInfoNotFoundException(int id) : base("")
        {
            Message = $"PhotographerInfo with photographerId = {id} not found";
        }
    }
}

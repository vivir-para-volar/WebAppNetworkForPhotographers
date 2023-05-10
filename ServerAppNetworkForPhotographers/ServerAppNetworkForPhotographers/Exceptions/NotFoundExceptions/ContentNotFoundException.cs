namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class ContentNotFoundException : Exception
    {
        public override string Message { get; }

        public ContentNotFoundException(int id) : base("")
        {
            Message = $"Content with id = {id} not found";
        }
    }
}

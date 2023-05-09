namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class CategoryNotFoundException : Exception
    {
        public override string Message { get; }

        public CategoryNotFoundException(int id) : base("")
        {
            Message = $"Category with id = {id} not found";
        }
    }
}

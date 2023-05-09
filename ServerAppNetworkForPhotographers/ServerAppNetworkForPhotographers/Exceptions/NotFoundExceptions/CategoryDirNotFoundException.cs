namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class CategoryDirNotFoundException : Exception
    {
        public override string Message { get; }

        public CategoryDirNotFoundException(int id) : base("")
        {
            Message = $"CategoryDir with id = {id} not found";
        }
    }
}

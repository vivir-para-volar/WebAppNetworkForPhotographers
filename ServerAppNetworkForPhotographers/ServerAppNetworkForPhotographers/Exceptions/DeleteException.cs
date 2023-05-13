namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class DeleteException : Exception
    {
        public override string Message { get; }

        public DeleteException(string childModel) : base("")
        {
            Message = $"Can not delete record (records in table {childModel} refer to it)";
        }
    }
}

namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class DeleteException : Exception
    {
        public override string Message { get; }

        public DeleteException(string childTable) : base("")
        {
            Message = $"Can not delete record (records in table {childTable} refer to it)";
        }
    }
}

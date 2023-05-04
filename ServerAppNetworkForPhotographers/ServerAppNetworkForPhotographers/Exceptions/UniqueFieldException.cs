namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class UniqueFieldException : Exception
    {
        public override string Message { get; }

        public UniqueFieldException(string field, string fieldValue) : base("")
        {
            Message = $"This {field} = \"{fieldValue}\" already exists";
        }
    }
}

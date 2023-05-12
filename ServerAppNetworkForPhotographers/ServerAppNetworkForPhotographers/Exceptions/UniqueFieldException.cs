namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class UniqueFieldException : Exception
    {
        public override string Message { get; }
        public string Field { get; }

        public UniqueFieldException(string field, string fieldValue) : base("")
        {
            Field = field;
            Message = $"This {field} = {fieldValue} already exists";
        }
    }
}

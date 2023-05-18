namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class UniqueFieldException : Exception
    {
        public override string Message { get; }
        public string Field { get; }

        public UniqueFieldException(string field) : base("")
        {
            Field = field;
            Message = "Данное значение уже существует";
        }
    }
}

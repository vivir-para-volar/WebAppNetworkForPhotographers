namespace EmployeeClientAppNetworkForPhotographers.Exceptions
{
    public class FieldException : Exception
    {
        public override string Message { get; }
        public string Field { get; }

        public FieldException(string field) : base("")
        {
            Field = field;
            Message = "Данное значение уже существует";
        }
    }
}

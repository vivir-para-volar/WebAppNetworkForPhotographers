namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class UniqueModelException : Exception
    {
        public override string Message { get; }

        public UniqueModelException(string model) : base("")
        {
            Message = $"This {model} already exists";
        }
    }
}

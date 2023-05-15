using ServerAppNetworkForPhotographers.Models.Data;

namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class UniqueModelException : Exception
    {
        public override string Message { get; }

        public UniqueModelException(string model) : base("")
        {
            Message = $"This {model} already exists";
        }

        public UniqueModelException(int photoId) : base("")
        {
            Message = $"This {nameof(PhotoInfo)} with {nameof(PhotoInfo.PhotoId)} = {photoId} already exists";
        }
    }
}

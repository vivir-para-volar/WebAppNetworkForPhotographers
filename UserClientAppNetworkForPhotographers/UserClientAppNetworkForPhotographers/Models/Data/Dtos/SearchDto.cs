namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos
{
    public class SearchDto
    {
        public string SearchData { get; set; }

        public SearchDto() { }

        public SearchDto(string searchData)
        {
            SearchData = searchData;
        }
    }
}

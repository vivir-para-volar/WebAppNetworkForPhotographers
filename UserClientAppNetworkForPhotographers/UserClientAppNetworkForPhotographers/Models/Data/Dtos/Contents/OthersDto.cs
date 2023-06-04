namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class OthersDto
    {
        public string TypeSorting { get; set; }
        public int CountLikeSorting { get; set; }
        public string PeriodSorting { get; set; }

        public string? TypeContent { get; set; }
        public int[]? CategoriesIds { get; set; }

        public OthersDto() { }

        public OthersDto(string typeSorting)
        {
            TypeSorting = typeSorting;
            CountLikeSorting = 0;
            PeriodSorting = Lists.TypeSorting.PeriodAllTime;
        }

        public OthersDto(string typeSorting, int categoryId)
        {
            TypeSorting = typeSorting;
            CountLikeSorting = 0;
            PeriodSorting = Lists.TypeSorting.PeriodAllTime;

            CategoriesIds = new int[] { categoryId };
        }
    }
}

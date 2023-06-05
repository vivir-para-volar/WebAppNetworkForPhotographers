using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class CategoryDir
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Category> Categories { get; set; }

        public CategoryDir()
        {
            InitLists();
        }

        private void InitLists()
        {
            Categories = new List<Category>();
        }
    }
}

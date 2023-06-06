using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class CategoryDir
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название папки")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
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

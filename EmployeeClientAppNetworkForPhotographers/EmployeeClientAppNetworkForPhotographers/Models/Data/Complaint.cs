using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class Complaint
    {
        public int Id { get; set; }

        [Display(Name = "Доп. описание")]
        public string? Text { get; set; }
        public string Status { get; set; }

        public int ComplaintBaseId { get; set; }
        public int ContentId { get; set; }
        public int PhotographerId { get; set; }

        [Display(Name = "Базовая жалоба")]
        public ComplaintBase ComplaintBase { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }
    }
}

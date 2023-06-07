using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class GetContentWithCountComplaints
    {
        [Display(Name = "Контент")]
        public int ContentId { get; set; }

        public string ContentType { get; set; }

        [Display(Name = "Количество жалоб")]
        public int CountComplaints { get; set; }
    }
}

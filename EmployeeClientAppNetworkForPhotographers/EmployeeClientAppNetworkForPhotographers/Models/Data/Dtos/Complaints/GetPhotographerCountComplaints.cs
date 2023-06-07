using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class GetPhotographerCountComplaints
    {
        public int PhotographerId { get; set; }

        [Display(Name = "Логин пользователя")]
        public string PhotographerUsername { get; set; }

        [Display(Name = "Количество жалоб")]
        public int CountComplaints { get; set; }
    }
}

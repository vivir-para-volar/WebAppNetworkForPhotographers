using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase
{
    public class CreateComplaintBaseDto
    {
        [Required]
        [Display(Name = "Название")]
        [StringLength(256, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 256 символов")]
        public string Name { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class ComplaintBase
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Должно быть длиннее 4 и короче 64 символов")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Complaint> Complaints { get; set; }

        public ComplaintBase()
        {
            InitLists();
        }

        private void InitLists()
        {
            Complaints = new List<Complaint>();
        }
    }
}

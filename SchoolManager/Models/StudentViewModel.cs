using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SchoolManagerDAL;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SchoolManager
{
    public class StudentViewModel
    {
        [ScaffoldColumn(false)]
        [Required]
        public int Id { set; get; }

        [Display(Name = "Имя")]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { set; get; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Фамилия")]
        public string LastName { set; get; }

        [Display(Name = "Отчество")]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Patronymic { set; get; }

        [Display(Name = "Телефон")]
        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "Некорректный телефон")]
        public string Phone { set; get; }

        [Required]
        [Display(Name = "Класс")]
        public string Class { get; set; }

        [Required]
        [Display(Name = "Школа")]
        public string School { get; set; }

        [Display(Name = "Класс")]
        [Required]
        public int ClassId { set; get; }

        [Display(Name = "Школа")]
        [Required]
        public int SchoolId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SchoolManagerBLL
{
    public class StudentDTO
    {
        [Display(Name = "Id")]
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

        [Display(Name = "Id класса")]
        [Required]
        public int ClassId { set; get; }

        public StudentDTO(int id, string firstName, string lastName, string patronymic, string phone, int classId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Phone = phone;
            ClassId = classId;
        }
    }
}

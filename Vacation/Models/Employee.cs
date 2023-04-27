using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacation.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; } = 0;

        [Required]
        [StringLength(50)]
        [DisplayName("First name")]
        public string FirstMidName { get; set; } = default!;

        [Required]
        [StringLength(30)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;

        [NotMapped]
        [DisplayName("Name")]
        public string FullName => (FirstMidName + " " + LastName);

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = default!;

        [Required]
        public bool Admin { get; set; }

        public virtual ICollection<VacationList>? VacationLists { get; set; }

    }
}

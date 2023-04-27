using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Vacation.Models
{
    public class VacationList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VacationListId { get; set; } = 0;

        [Required]
        [DisplayName("Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayName("Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VacCreated { get; set; } = DateTime.Now;

        [NotMapped]     // Dont get saved in db
        [DisplayName("Days off")]
        public int DaysOff => (EndDate - StartDate).Days;

        // ************************ FK **************************************'
        [ForeignKey("Employees")]
        [DisplayName("Empolyee")]
        public int FK_EmployeeId { get; set; }
        public virtual Employee? Employees { get; set; }

        [ForeignKey("VacationTypes")]
        [DisplayName("Type of leave")]
        public int? FK_VacationTypeId { get; set; }

        public virtual VacationType? VacationTypes { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Vacation.Models
{
    public class VacationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VacationTypeId { get; set; } = 0;

        [Required]
        [StringLength(50)]
        [DisplayName("Type of leave")]
        public string VacationTypeName { get; set; } = default!;

        public virtual ICollection<VacationList>? VacationLists { get; set; }
    }
}

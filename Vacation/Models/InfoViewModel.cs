using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vacation.Models
{
    public class InfoViewModel
    {
        public Employee Employee { get; set; }
        public List<VacationList> VacationLists { get; set; }
        public int EmployeeId { get; set; }
        public string? FirstMidName { get; set; }
        public string? LastName { get; set; }
        public string FullName => (FirstMidName + LastName);
        public string? Email { get; set; }
        public int VacationListId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime VacCreated { get; set; }
        public int DaysOff => (EndDate - StartDate).Days;
        public string? VacationTypeName { get; set; }

    }
}

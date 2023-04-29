using Microsoft.EntityFrameworkCore;
using Vacation.Models;

namespace Vacation.Data
{
    public class VacationDbContext : DbContext
    {
        public VacationDbContext(DbContextOptions<VacationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<VacationList> VacationLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // ONLY ACTIVATE IN DEVELOPMENT
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Enable sensitive data logging
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}

        // Seed() - Adds a basic list of employees and types of leave at the start of the first start of the program
        // If there's already any data in the database then it wont add them again, so the data will only be added one time.
        protected internal void Seed()
        {
            // If there's no employees in the db then these will be added
            if (!Employees.Any())
            {
                var empList = new List<Employee>
                {
                    new Employee
                    {
                        FirstMidName = "Kajsa",
                        LastName = "Karlsson",
                        Phone = "076 258 42 72",
                        Email = "Kajsa.K@email.com",
                        Password = "Kajsa1",
                        Admin = true
                    },
                    new Employee
                    {
                        FirstMidName = "Jacob",
                        LastName = "Bengtsson",
                        Phone = "073 582 45 95",
                        Email = "Jacob.B@email.com",
                        Password = "Jacob2",
                        Admin = false
                    },
                    new Employee
                    {
                        FirstMidName = "Ajay",
                        LastName = "Reddy",
                        Phone = "070 573 95 64",
                        Email = "Ajay.R@email.com",
                        Password = "Ajay3",
                        Admin = false
                    },
                    new Employee
                    {
                        FirstMidName = "Zia",
                        LastName = "Lakim",
                        Phone = "073 648 28 50",
                        Email = "Zia.L@email.com",
                        Password = "Zia4",
                        Admin = false
                    },
                    new Employee
                    {
                        FirstMidName = "Mike",
                        LastName = "McMillan",
                        Phone = "070 937 56 18",
                        Email = "Mike.M@email.com",
                        Password = "Mike5",
                        Admin = false
                    }
                };

                Employees.AddRange(empList);
                SaveChanges();
            }

            // If there's no vacation types in the db then these will be added
            if (!VacationTypes.Any())
            {
                var vacList = new List<VacationType>
                {
                    new VacationType {VacationTypeName = "Vacation"},
                    new VacationType {VacationTypeName = "Sick child"},
                    new VacationType {VacationTypeName = "Sick family member"},
                    new VacationType {VacationTypeName = "Self-care"},
                    new VacationType {VacationTypeName = "Other"}
                };

                VacationTypes.AddRange(vacList);
                SaveChanges();
            }
            // Otherwise nothing will be added and nothing will be returned
            else{
                return;
            }
        }
    }
}

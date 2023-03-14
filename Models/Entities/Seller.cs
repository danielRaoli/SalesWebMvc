using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace SalesWebMvc1.Models.Entities
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} size should between {2} and {1}")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage ="{0} Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }


        [Display(Name ="Base Salary" )]
        [Required(ErrorMessage ="{0} Required")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Range(100.0, 5000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }

        [Display(Name ="Department")]
        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public List<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller( string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }
        public void RemoveSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        public double TotalSales(DateTime inital, DateTime final)
        {
            return Sales.Where(s => s.Date >= inital && s.Date <= final).Select(s => s.Amount).Sum();
        }
    }
}

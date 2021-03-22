using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.Models
{
    class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }

        public string HealthInsuranceProvider { get; set; }
        public string InsurancePlanName { get; set; }
        public string InsuranceMonthlyFee { get; set; }
        public DateTime InsuranceStartDate { get; set; }

        public PictureBox EmployeeImage { get; set; }
        public string FileExtension { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.Models
{
    class Employeet
    {
        private string _id;
        private string _name;
        private string _city;
        private string _department;
        private string _gender;

        private string _healthInsuranceProvider;
        private string _insurancePlanName;
        private string _insuranceMonthlyFee; //will be parsed to decimal
        private DateTime _insuranceStartDate;

        private PictureBox _employeeImage;
        private string _fileExtension;

        #region "Required Records" Properties
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }

        }

        public string City
        {
            get { return _city; }
            set { _city = value; }

        }

        public string Department
        {
            get { return _department; }
            set { _department = value; }

        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }

        }
        #endregion

        #region "Health Insurance" Properties
        public string HealthInsuranceProvider
        {
            get { return _healthInsuranceProvider; }
            set { _healthInsuranceProvider = value; }
        }

        public string InsurancePlanName
        {
            get { return _insurancePlanName; }
            set { _insurancePlanName = value; }
        }

        public string InsuranceMonthlyFee
        {
            get { return _insuranceMonthlyFee; }
            set { _insuranceMonthlyFee = value; }
        }

        public DateTime InsuranceStartDate
        {
            get { return _insuranceStartDate; }
            set { _insuranceStartDate = value; }
        }
        #endregion

        #region Employee Image
        public PictureBox EmployeeImage
        {
            get { return _employeeImage; }
            set { _employeeImage = value; }
        }

        public string FileExtension
        {
            get { return _fileExtension; }
            set { _fileExtension = value; }
        }
        #endregion
    }
}

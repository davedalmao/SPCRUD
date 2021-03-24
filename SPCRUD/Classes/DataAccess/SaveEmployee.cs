using SPCRUD.Classes.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.DataAccess
{
    class SaveEmployee
    {
        //try to implement CheckHealthInsFields to the stored procedure
        private readonly string _connectionString;

        private string _id;
        private string _name;
        private string _city;
        private string _department;
        private string _gender;

        private string _healthInsuranceProvider;
        private string _insurancePlanName;
        private string _insuranceMonthlyFee; //will be parsed to decimal
        private DateTime? _insuranceStartDate;

        private PictureBox _employeeImage;
        private string _fileExtension;

        private string _actionType;
        #region Constructor
        public SaveEmployee(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

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

        public DateTime? InsuranceStartDate
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

        #region Insert Or Update
        public string ActionType
        {
            get { return _actionType; }
            set { _actionType = value; }
        }
        #endregion

        public void InsertOrUpdate()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spCreateOrUpdateData", con))
            {
                try
                {
                    CheckImageTag();
                    if (CheckHealthInsFields(_healthInsuranceProvider, _insurancePlanName, _insuranceMonthlyFee))
                    {
                        _healthInsuranceProvider = "";
                        _insurancePlanName = "";
                        _insuranceMonthlyFee = "0.00";
                        _insuranceStartDate = null;
                    }

                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    //Employee Record
                    sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = _id;
                    sqlCmd.Parameters.Add("@employee_name", SqlDbType.NVarChar, 250).Value = _name;
                    sqlCmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = _city;
                    sqlCmd.Parameters.Add("@department", SqlDbType.NVarChar, 50).Value = _department;
                    sqlCmd.Parameters.Add("@gender", SqlDbType.NVarChar, 6).Value = _gender;

                    //Employee Health Insurance Record
                    sqlCmd.Parameters.Add("@health_insurance_provider", SqlDbType.NVarChar, 100).Value = _healthInsuranceProvider;
                    sqlCmd.Parameters.Add("@plan_name", SqlDbType.NVarChar, 100).Value = _insurancePlanName;
                    sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = _insuranceStartDate;
                    sqlCmd.Parameters.Add(new SqlParameter("@monthly_fee", SqlDbType.Decimal)
                    {
                        Precision = 15, //Precision specifies the number of digits used to represent the value of the parameter.
                        Scale = 2, //Scale is used to specify the number of decimal places in the value of the parameter.
                        Value = decimal.Parse(_insuranceMonthlyFee)
                    });

                    //Employee Image and File Extension
                    sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary).Value = ImageOperations.ImageToBytes(_employeeImage.Image, _fileExtension);
                    sqlCmd.Parameters.Add("@file_extension", SqlDbType.NVarChar, 12).Value = _fileExtension;

                    int numRes = sqlCmd.ExecuteNonQuery();
                    string actionType = (_actionType == "Save") ? "Saved" : "Updated";
                    ExecuteNonQueryResult(numRes, _name, actionType);
                }

                catch (SqlException ex)
                {
                    if (ex.Number == 2627)// Violation of unique constraint (Name should be unique)
                    {
                        MessageBox.Show($"{_name} Already Exist !!!");
                        return;
                    }
                    MessageBox.Show($"An SQL error occured while processing data. \nError: { ex.Message }");
                }
                catch (FormatException ex)
                {
                    MessageBox.Show($"Some fields might have a wrong format supplied to them! \nCheck your inputs please. \nError: { ex.Message }");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot INSERT or UPDATE data! \nError: { ex.Message }");
                }
            }
        }

        private void ActionMessage(string name, string actionType)
        {
            if (CheckHealthInsFields(_healthInsuranceProvider, _insurancePlanName, _insuranceMonthlyFee))
            {
                MessageBox.Show($"{name}'s record is { actionType } successfully !!! \nAdd Health Insurance records later on.");
                return;
            }
            MessageBox.Show($"{name}'s record is { actionType } successfully !!!");
            return;
        }

        private void ExecuteNonQueryResult(int numRes, string name, string actionType)
        {
            if (numRes > 0) //if query is successful
            {
                ActionMessage(name, actionType);
                return;
            }
            MessageBox.Show($"{name} Already Exist ssssssssssss!!!");
        }

        private bool CheckHealthInsFields(string healthInsProvider, string insPlanName, string insMonthlyFee)
        {
            if (string.IsNullOrWhiteSpace(healthInsProvider) ||
                string.IsNullOrWhiteSpace(insPlanName) ||
                string.IsNullOrWhiteSpace(insMonthlyFee) ||
                decimal.Parse(insMonthlyFee) < 1)
            {
                return true;
            }
            return false;
        }

        private void CheckImageTag()
        {
            //Employee Image and File Extension
            if (_employeeImage.Tag != null && string.IsNullOrWhiteSpace(_fileExtension)) //if tag has a value (save null)
            {
                _employeeImage.Image = null;
                _fileExtension = null;
            }
        }
    }
}
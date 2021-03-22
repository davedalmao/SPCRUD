using SPCRUD.Classes.Utility;
using SPCRUD.Classes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.DataAccess
{
    class TestSave
    {
        private readonly string _connectionString;
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
        // private static readonly Employeet employee = new Employeet();

        public TestSave(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertOrUpdate(string btnSaveText)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spCreateOrUpdateData", con))
            {
                try
                {
                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    //Employee Record list
                    List<SqlParameter> requiredRecords = RequiredRecords(Id, Name, City, Department, Gender);
                    if (requiredRecords != null)
                    {
                        sqlCmd.Parameters.AddRange(requiredRecords.ToArray());
                    }

                    //Employee Health Insurance Record list
                    List<SqlParameter> healthInsuranceRecords = HealthInsuranceRecords(HealthInsuranceProvider, InsurancePlanName, InsuranceMonthlyFee, InsuranceStartDate);
                    if (healthInsuranceRecords != null)
                    {
                        sqlCmd.Parameters.AddRange(healthInsuranceRecords.ToArray());
                    }

                    //Employee Image and File Extension list
                    List<SqlParameter> image = Image(EmployeeImage, FileExtension);
                    if (image != null)
                    {
                        sqlCmd.Parameters.AddRange(image.ToArray());
                    }

                    int numRes = sqlCmd.ExecuteNonQuery();
                    string ActionType = (btnSaveText == "Save") ? "Saved" : "Updated";
                    if (numRes > 0) //if query is successful
                    {
                        ActionMessage(ActionType);
                        return;
                    }
                    MessageBox.Show($"{Name} Already Exist !!!");
                }

                catch (SqlException ex)
                {
                    if (ex.Number == 2627)// Violation of unique constraint (Name should be unique)
                    {
                        MessageBox.Show($"{Name} Already Exist !!!");
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

        private List<SqlParameter> RequiredRecords(string id, string name, string city, string department, string gender)
        {
            List<SqlParameter> requiredRecords = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@employee_id", SqlDbType = SqlDbType.NVarChar, Value = id},
                new SqlParameter() {ParameterName = "@employee_name", SqlDbType = SqlDbType.NVarChar, Size = 250, Value = name},
                new SqlParameter() {ParameterName = "@city", SqlDbType = SqlDbType.NVarChar, Size = 50, Value = city},
                new SqlParameter() {ParameterName = "@department", SqlDbType = SqlDbType.NVarChar, Size = 50, Value = department},
                new SqlParameter() {ParameterName = "@gender", SqlDbType = SqlDbType.NVarChar, Size = 6, Value = gender}
            };
            return requiredRecords;
        }

        private List<SqlParameter> HealthInsuranceRecords(string healthInsProvider, string insPlanName, string insMonthlyFee, DateTime? insStartDate)
        {
            if (CheckHealthInsFields(HealthInsuranceProvider, InsurancePlanName, InsuranceMonthlyFee))
            {
                insStartDate = null;
                healthInsProvider = "";
                insPlanName = "";
                insMonthlyFee = "0.00";
                decimal.Parse(insMonthlyFee);
            }

            List<SqlParameter> healthInsuranceRecords = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@health_insurance_provider",
                                    SqlDbType = SqlDbType.NVarChar,
                                    Size = 100,
                                    Value = healthInsProvider},

                new SqlParameter() {ParameterName = "@plan_name",
                                    SqlDbType = SqlDbType.NVarChar,
                                    Size = 100,
                                    Value = insPlanName},

                new SqlParameter() {ParameterName = "@monthly_fee",
                                    SqlDbType = SqlDbType.Decimal,
                                    Precision=15,
                                    Scale=2,
                                    Value = string.IsNullOrWhiteSpace(insMonthlyFee)
                                          ? 0
                                          : decimal.Parse(insMonthlyFee)},

                new SqlParameter() { ParameterName = "@insurance_start_date",
                                     SqlDbType = SqlDbType.Date,
                                     Value = insStartDate }
            };

            return healthInsuranceRecords;
        }

        private List<SqlParameter> Image(PictureBox employeeImage, string fileExtension)
        {
            byte[] finalEmployeeImage;

            if (employeeImage.Tag != null && string.IsNullOrWhiteSpace(fileExtension)) //if tag has a value (save null)
            {
                finalEmployeeImage = null;
                fileExtension = null;
            }
            else
            {
                finalEmployeeImage = ImageOperations.ImageToBytes(employeeImage.Image, fileExtension);
            }

            List<SqlParameter> requiredRecords = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@user_image",
                                    SqlDbType = SqlDbType.VarBinary,
                                    Value = finalEmployeeImage},
                new SqlParameter() {ParameterName = "@file_extension",
                                    SqlDbType = SqlDbType.NVarChar,
                                    Size = 12,
                                    Value = fileExtension},
            };
            return requiredRecords;
        }

        private void ActionMessage(string ActionType)
        {
            if (CheckHealthInsFields(HealthInsuranceProvider, InsurancePlanName, InsuranceMonthlyFee))
            {
                MessageBox.Show($"{Name}'s record is { ActionType } successfully !!! \nAdd Health Insurance records later on.");
                return;
            }
            MessageBox.Show($"{Name}'s record is { ActionType } successfully !!!");
            return;
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
    }
}

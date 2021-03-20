using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.Utility.DataAccess
{
    class SaveEmployee
    {
        private string _connectionString;
        private string _healthInsProvider;
        private string _insPlanName;
        private string _insMonthlyFee;

        public SaveEmployee(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string HealthInsProvider
        {
            get { return _healthInsProvider; }
            set { _healthInsProvider = value; }
        }

        public string InsPlanName
        {
            get { return _insPlanName; }
            set { _insPlanName = value; }
        }

        public string InsMonthlyFee
        {
            get { return _insMonthlyFee; }
            set { _insMonthlyFee = value; }
        }

        public void InsertOrUpdate(string id, string name, string city, string department, string gender, string healthInsProvider, string insPlanName, string insMonthlyFee, DateTime insStartDate, PictureBox employeeImage, string fileExtension, string btnSaveText)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spCreateOrUpdateData", con))
            {
                try
                {
                    if (CheckHealthInsFields(HealthInsProvider, InsPlanName, InsMonthlyFee))
                    {
                        healthInsProvider = "";
                        insPlanName = "";
                        insMonthlyFee = "0.00";
                        decimal.Parse(insMonthlyFee);
                        insStartDate = DateTime.Now;
                    }

                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    //Employee Record
                    sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = id;
                    sqlCmd.Parameters.Add("@employee_name", SqlDbType.NVarChar, 250).Value = name;
                    sqlCmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = city;
                    sqlCmd.Parameters.Add("@department", SqlDbType.NVarChar, 50).Value = department;
                    sqlCmd.Parameters.Add("@gender", SqlDbType.NVarChar, 6).Value = gender;

                    //Employee Health Insurance Record
                    sqlCmd.Parameters.Add("@health_insurance_provider", SqlDbType.NVarChar, 100).Value = healthInsProvider;
                    sqlCmd.Parameters.Add("@plan_name", SqlDbType.NVarChar, 100).Value = insPlanName;
                    sqlCmd.Parameters.Add(new SqlParameter("@monthly_fee", SqlDbType.Decimal)
                    {
                        Precision = 15, //Precision specifies the number of digits used to represent the value of the parameter.
                        Scale = 2, //Scale is used to specify the number of decimal places in the value of the parameter.
                        Value = string.IsNullOrWhiteSpace(insMonthlyFee) //add 0 as default value in database
                              ? 0
                              : decimal.Parse(insMonthlyFee)
                    });

                    // Save insurance start date:
                    if (CheckHealthInsFields(HealthInsProvider, InsPlanName, InsMonthlyFee))
                    {
                        sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = insStartDate;
                    }

                    //Employee Image and File Extension
                    if (employeeImage.Tag != null && string.IsNullOrWhiteSpace(fileExtension)) //if tag has a value (save null)
                    {
                        sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary).Value = DBNull.Value;
                        sqlCmd.Parameters.Add("@file_extension", SqlDbType.NVarChar, 12).Value = DBNull.Value;

                    }
                    else
                    {
                        sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary).Value = ImageOperations.ImageToBytes(employeeImage.Image, fileExtension);
                        sqlCmd.Parameters.Add("@file_extension", SqlDbType.NVarChar, 12).Value = fileExtension;
                    }

                    int numRes = sqlCmd.ExecuteNonQuery();
                    string ActionType = (btnSaveText == "Save") ? "Saved" : "Updated";
                    if (numRes > 0) //if query is successful
                    {
                        if (CheckHealthInsFields(HealthInsProvider, InsPlanName, InsMonthlyFee))
                        {
                            MessageBox.Show($"{name}'s record is { ActionType } successfully !!! \nAdd Health Insurance records later on.");
                            return;
                        }
                        MessageBox.Show($"{name}'s record is { ActionType } successfully !!!");
                        return;
                    }
                    MessageBox.Show($"{name} Already Exist !!!");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)// Violation of unique constraint (Name should be unique)
                    {
                        MessageBox.Show($"{name} Already Exist !!!");
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
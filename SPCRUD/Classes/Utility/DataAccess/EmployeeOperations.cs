using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SPCRUD;

namespace SPCRUD.Classes.Utility.DataAccess
{
    class EmployeeOperations
    {
        #region Fields
        private string _connectionString;
        private Color _themeColor = Color.FromArgb(172, 188, 212);

        //Health Insurance Fields
        private string _healthInsProvider;
        private string _insPlanName;
        private string _insMonthlyFee;
        private DateTime _insStartDate;
        #endregion

        #region Constructor
        public EmployeeOperations(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Properties
        //------------------------------ < region Properties > ------------------------------
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

        public DateTime InsStartDate
        {
            get { return _insStartDate; }
            set { _insStartDate = value; }
        }
        //------------------------------ </ region Properties > ------------------------------
        #endregion

        #region Methods
        //------------------------------ < region Methods > ------------------------------
        public void DeleteAllRecords(int selectedRowCount)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spDeleteAllEmployeeRecords", con))
            {
                try
                {
                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("All Employee Records DELETED Successfully!");
                        //RefreshData();
                        return;
                    }

                    if (selectedRowCount > 0)
                    {
                        MessageBox.Show($"Cannot DELETE records!");
                        return;
                    }
                    MessageBox.Show($"No records to DELETE! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot DELETE records! \nError: { ex.Message }");
                }
            }
        } //done

        public void DeleteEmployee(string employeeId, string employeeName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spDeleteData", con))
            {
                try
                {
                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = employeeId;

                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show($"{employeeName}'s Record DELETED Successfully!");
                        //RefreshData();
                        return;
                    }
                    MessageBox.Show($"Cannot DELETE records! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot DELETE {employeeName}'s record! \nError: { ex.Message }");
                }

            }
        } //done

        public void DisplayEmployeeImage(string employeeId, PictureBox employeeImage, Label fileExtension)
        {
            //Display user image
            string sqlQuery = "SELECT user_image, file_extension FROM dbo.Employee_Image WHERE employee_id=@employee_id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
            {
                con.Open();
                sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = employeeId;

                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader.GetValue(0) == null && reader.GetValue(0) == null) //if image is null add border color to tag
                        {
                            employeeImage.Tag = _themeColor;
                        }
                        else
                        {
                            employeeImage.Tag = null;
                            fileExtension.Text = reader.GetValue(1).ToString();
                            employeeImage.Image = ImageOperations.BytesToImage((byte[])(reader.GetValue(0)));
                        }
                        return;
                    }
                    employeeImage.Image = null; //if (!reader.HasRows)
                }
            }
        } //done

        public void DisplayEmployeeRecords(string displayType, DataGridView dgvEmpDetails)
        {//Load/Read Data from database
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spDisplayEmployeeRecords", con))
            {
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@display_type", SqlDbType.NVarChar).Value = displayType;
                    sqlCmd.Connection = con;

                    SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
                    sqlSda.Fill(dt);

                    dgvEmpDetails.AutoGenerateColumns = false;//if true - displays all the records in the database

                    // The property names are the column names in dbo.Employee
                    dgvEmpDetails.Columns[0].DataPropertyName = "employee_id"; // This is Employee Id at the datagridview
                    dgvEmpDetails.Columns[1].DataPropertyName = "employee_name";
                    dgvEmpDetails.Columns[2].DataPropertyName = "city";
                    dgvEmpDetails.Columns[3].DataPropertyName = "department";
                    dgvEmpDetails.Columns[4].DataPropertyName = "gender";

                    // The property names are the column names in dbo.Employee_Health_Insurance
                    dgvEmpDetails.Columns[5].DataPropertyName = "health_insurance_provider";
                    dgvEmpDetails.Columns[6].DataPropertyName = "plan_name";
                    dgvEmpDetails.Columns[7].DataPropertyName = "monthly_fee";
                    dgvEmpDetails.Columns[8].DataPropertyName = "insurance_start_date";

                    // Custom cell format
                    dgvEmpDetails.Columns[7].DefaultCellStyle.Format = "#,##0.00";
                    dgvEmpDetails.Columns[8].DefaultCellStyle.Format = "MMMM dd, yyyy";

                    dgvEmpDetails.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot DISPLAY data in the datagridview! \nError: { ex.Message }");
                }
            }
        } //done

        public void SaveEmployeeRecord(string id, string name, string city, string department, string gender, string healthInsProvider, string insPlanName, string insMonthlyFee, DateTime insStartDate, PictureBox employeeImage, string fileExtension, string btnSaveText)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand sqlCmd = new SqlCommand("spCreateOrUpdateData", con))
            {
                try
                {
                    // If at least one of the health insurance fields is blank, save only the employee record without the health insurance record
                    if (CheckHealthInsFields(HealthInsProvider, InsPlanName, InsMonthlyFee))
                    {
                        ResethHealthInsuranceFields(HealthInsProvider, InsPlanName, InsMonthlyFee, InsStartDate);
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
                        }
                        else
                        {
                            MessageBox.Show($"{name}'s record is { ActionType } successfully !!!");
                        }
                        //RefreshData();
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
            //if true ->  one of the health insurance fields is blank
            if (string.IsNullOrWhiteSpace(healthInsProvider) ||
                string.IsNullOrWhiteSpace(insPlanName) ||
                string.IsNullOrWhiteSpace(insMonthlyFee) ||
                decimal.Parse(insMonthlyFee) < 1)
            {
                return true;
            }
            return false;
        }

        private void ResethHealthInsuranceFields(string empHealthInsProvider, string empInsPlanName, string empInsMonthlyFee, DateTime insStartDate)
        {
            //controls
            empHealthInsProvider = "";
            empInsPlanName = "";
            empInsMonthlyFee = "0.00";
            insStartDate = DateTime.Now;
        }
        //------------------------------ </ region Methods > ------------------------------
        #endregion
    }
}

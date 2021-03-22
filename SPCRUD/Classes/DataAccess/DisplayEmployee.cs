using SPCRUD.Classes.Utility;
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
    class DisplayEmployee
    {
        private readonly string _connectionString;

        public DisplayEmployee(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AllRecords(string displayType, DataGridView dgvEmpDetails) //Load/Read Data from database
        {
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

                    dgvEmpDetails.AutoGenerateColumns = false; //if true - displays all the records in the database

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
        }

        public void Image(string employeeId, PictureBox employeeImage, Label fileExtension) //Display user image
        {

            Color _themeColor = Color.FromArgb(172, 188, 212);
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
        }
    }
}

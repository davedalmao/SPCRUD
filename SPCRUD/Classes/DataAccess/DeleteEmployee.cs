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
    class DeleteEmployee
    {
        private readonly string _connectionString;

        public DeleteEmployee(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AllRecords(int selectedRowCount)
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
        }

        public void SpecificRecord(string employeeId, string employeeName)
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
                        return;
                    }
                    MessageBox.Show($"Cannot DELETE records! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot DELETE {employeeName}'s record! \nError: { ex.Message }");
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD {
    public partial class Form1 : Form {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SystemDatabaseConnection1"].ConnectionString); // This is set in App.config
        SqlCommand sqlCmd;
        string EmployeeId = "";

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //dgvEmp.AutoGenerateColumns = false; // dgvEmp is DataGridView name  
            // dgvEmp.DataSource = FetchEmpDetails();
            FetchEmpDetails();
        }

        #region Functions
        private void FetchEmpDetails() {
            if (con.State == ConnectionState.Closed) {
                con.Open();
            }
            DataTable dtData = new DataTable();
            sqlCmd = new SqlCommand("spCRUD_Operations", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ActionType", "ReadData");
            sqlCmd.Connection = con;

            SqlDataAdapter sqlSda = new SqlDataAdapter(sqlCmd);
            sqlSda.Fill(dtData);
            con.Close();

            dgvEmp.AutoGenerateColumns = false;
            dgvEmp.Columns[0].DataPropertyName = "EmployeeId";
            dgvEmp.Columns[1].DataPropertyName = "Name";
            dgvEmp.Columns[2].DataPropertyName = "City";
            dgvEmp.Columns[3].DataPropertyName = "Department";
            dgvEmp.Columns[4].DataPropertyName = "Gender";
            dgvEmp.DataSource = dtData;
        }

        private void RefreshData() {
            btnSave.Text = "Save";
            textBoxEmp.Text = "";
            textBoxCity.Text = "";
            textBoxDept.Text = "";
            comboBoxGen.SelectedIndex = -1;
            EmployeeId = "";
            btnDelete.Enabled = false;
            //dgvEmp.AutoGenerateColumns = false;
            //dgvEmp.DataSource = FetchEmpDetails();
            FetchEmpDetails();
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBoxEmp.Text)) {
                MessageBox.Show("Enter Employee Name !!!");
                //textBoxEmp.Select();
            } else if (string.IsNullOrWhiteSpace(textBoxCity.Text)) {
                MessageBox.Show("Enter Current City !!!");
                //textBoxCity.Select();
            } else if (string.IsNullOrWhiteSpace(textBoxDept.Text)) {
                MessageBox.Show("Enter Department !!!");
                //textBoxDept.Select();
            } else if (comboBoxGen.SelectedIndex <= -1) {
                MessageBox.Show("Select Gender !!!");
                //comboBoxGen.Select();
            } else {
                try {
                    if (con.State == ConnectionState.Closed) {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Name FROM tblEmployee WHERE Name = @Name", con);
                    sda.SelectCommand.Parameters.AddWithValue("@Name", textBoxEmp.Text); //Parameterized query for SqlDataAdapter
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count >= 1) {
                        MessageBox.Show("Name Already Exists!", "!");
                    } else {
                        using (SqlCommand sqlCmd = new SqlCommand("spCRUD_Operations", con)) {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                            sqlCmd.Parameters.AddWithValue("@Name", textBoxEmp.Text);
                            sqlCmd.Parameters.AddWithValue("@City", textBoxCity.Text);
                            sqlCmd.Parameters.AddWithValue("@Department", textBoxDept.Text);
                            sqlCmd.Parameters.AddWithValue("@Gender", comboBoxGen.Text);
                            sqlCmd.Parameters.AddWithValue("@ActionType", "CreateOrUpdateData");
                            sqlCmd.ExecuteNonQuery();

                            if (btnSave.Text == "Save") {
                                MessageBox.Show("Record Saved Successfully !!!");
                            } else {
                                MessageBox.Show("Record Updated Successfully !!!");
                            }
                            RefreshData();
                        }
                    }

                    /*if (numRes > 0) {
                                                   MessageBox.Show("Record Saved Successfully !!!");
                                                   RefreshData();
                                               } else
                                                   MessageBox.Show("Please Try Again !!! t");*/
                    /*using (SqlCommand command = new SqlCommand("spCRUD_Operations", con)) {
                      command.CommandType = CommandType.StoredProcedure;
                      command.Parameters.AddWithValue("@ActionType", "CheckIfUserExists");
                      command.Parameters.AddWithValue("@Name", textBoxEmp.Text);
                      var returnCode = Convert.ToInt32(command.ExecuteScalar());
                      if (returnCode == 1) {
                          MessageBox.Show("1");
                      } else {
                          MessageBox.Show("0");
                      }
                  }


               int selectedrowindex = dgvEmp.SelectedCells[0].RowIndex;
                 DataGridViewRow selectedRow = dgvEmp.Rows[selectedrowindex];
                 string InitialName = Convert.ToString(selectedRow.Cells["Name"].Value);

                 using (SqlCommand command = new SqlCommand("spCRUD_Operations", con))
                 {
                     command.CommandType = CommandType.StoredProcedure;
                     command.Parameters.AddWithValue("@ActionType", "CheckIfUserExists");
                     command.Parameters.AddWithValue("@Name", InitialName);
                     var returnCode = Convert.ToInt32(command.ExecuteScalar());
                     if (returnCode == 1)
                     {
                         using (SqlCommand sqlCmd = new SqlCommand("spCRUD_Operations", con))
                         {
                             DataTable dtData = new DataTable();
                             sqlCmd.CommandType = CommandType.StoredProcedure;
                             sqlCmd.Parameters.AddWithValue("@ActionType", "UpdateData");
                             sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                             sqlCmd.Parameters.AddWithValue("@Name", textBoxEmp.Text);
                             sqlCmd.Parameters.AddWithValue("@City", textBoxCity.Text);
                             sqlCmd.Parameters.AddWithValue("@Department", textBoxDept.Text);
                             sqlCmd.Parameters.AddWithValue("@Gender", comboBoxGen.Text);
                             int numRes = sqlCmd.ExecuteNonQuery();
                             if (numRes > 0)
                             {
                                 MessageBox.Show("Record Saved Successfully !!!");
                                 RefreshData();
                             }
                             else
                                 MessageBox.Show("Please Try Again !!! t");
                         }
                     }
                     else
                     {
                         using (SqlCommand sqlCmd = new SqlCommand("spCRUD_Operations", con))
                         {
                             DataTable dtData = new DataTable();
                             sqlCmd.CommandType = CommandType.StoredProcedure;
                             sqlCmd.Parameters.AddWithValue("@ActionType", "CreateData");
                             sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                             sqlCmd.Parameters.AddWithValue("@Name", textBoxEmp.Text);
                             sqlCmd.Parameters.AddWithValue("@City", textBoxCity.Text);
                             sqlCmd.Parameters.AddWithValue("@Department", textBoxDept.Text);
                             sqlCmd.Parameters.AddWithValue("@Gender", comboBoxGen.Text);
                             int numRes = sqlCmd.ExecuteNonQuery();
                             if (numRes > 0)
                             {
                                 MessageBox.Show("Record Saved Successfully !!!");
                                 RefreshData();
                             }
                             else
                                 MessageBox.Show("Please Try Again !!! b");
                         }
                     }

                     label1.Text = returnCode.ToString();*/
                    /* if (userCount > 0)
                     {
                         MessageBox.Show("Name Already Exists","!");
                     }
                     else
                     {
                         using (SqlCommand sqlCmd = new SqlCommand("spCRUD_Operations", con))
                         {
                             DataTable dtData = new DataTable();
                             sqlCmd.CommandType = CommandType.StoredProcedure;
                             sqlCmd.Parameters.AddWithValue("@ActionType", "CreateData");
                             sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                             sqlCmd.Parameters.AddWithValue("@Name", textBoxEmp.Text);
                             sqlCmd.Parameters.AddWithValue("@City", textBoxCity.Text);
                             sqlCmd.Parameters.AddWithValue("@Department", textBoxDept.Text);
                             sqlCmd.Parameters.AddWithValue("@Gender", comboBoxGen.Text);
                             int numRes = sqlCmd.ExecuteNonQuery();
                             if (numRes > 0)
                             {
                                 MessageBox.Show("Record Saved Successfully !!!");
                                 RefreshData();
                             }
                             else
                                 MessageBox.Show("Please Try Again !!!");
                         }
                     }
                }*/
                } catch (Exception ex) {
                    MessageBox.Show("Error: " + ex.Message);
                } finally {
                    con.Close();
                }
            }
        }

        private void dgvEmp_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1) {
                /* int selectedrowindex = dgvEmp.SelectedCells[0].RowIndex;
                 DataGridViewRow selectedRow = dgvEmp.Rows[selectedrowindex];
                 string InitialName = Convert.ToString(selectedRow.Cells["EmployeeName"].Value); //@Name ni if this exist dont save 
                 label5.Text = InitialName;*/

                DataGridViewRow row = dgvEmp.Rows[e.RowIndex];
                EmployeeId = row.Cells[0].Value.ToString(); //The Employee ID is determined here
                textBoxEmp.Text = row.Cells[1].Value.ToString();
                textBoxCity.Text = row.Cells[2].Value.ToString();
                textBoxDept.Text = row.Cells[3].Value.ToString();
                comboBoxGen.Text = row.Cells[4].Value.ToString();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            int selectedRowCount = dgvEmp.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount >= 0) {
                try {
                    if (con.State == ConnectionState.Closed) {
                        con.Open();
                    }
                    DataTable dtData = new DataTable();
                    sqlCmd = new SqlCommand("spCRUD_Operations", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    sqlCmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted Successfully !!!");
                    RefreshData();
                } catch (Exception ex) {
                    MessageBox.Show("Error:- " + ex.Message);
                }
            } else {
                MessageBox.Show("Please Select A Record !!!");
            }
            /* con.Open();
             string query = "TRUNCATE TABLE tblEmployee";
             SqlCommand sqlCmd = new SqlCommand(query, con);
             sqlCmd.ExecuteNonQuery();
             con.Close();

             RefreshData();*/
        }

        private void btnClear_Click(object sender, EventArgs e) {
            RefreshData();
        }
    }
}

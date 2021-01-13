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
        static string connectionStringConfig = ConfigurationManager.ConnectionStrings[ "SystemDatabaseConnection1" ].ConnectionString;
        string EmployeeId = "";

        #region Form1
        //--------------- < region Form1 > ---------------
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load( object sender, EventArgs e ) {
            FetchEmpDetails();
        }
        //--------------- </ region Form1 > ---------------
        #endregion

        #region Functions
        //--------------- < region Funtions > ---------------

        private void FetchEmpDetails() {
            //Load/Read Data from database
            using ( SqlConnection con = new SqlConnection( connectionStringConfig ) )
            using ( SqlCommand sqlCmd = new SqlCommand( "spCRUD_Operations", con ) ) {
                try {
                    con.Open();
                    DataTable dt = new DataTable();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue( "@ActionType", "ReadData" );
                    sqlCmd.Connection = con;
                    SqlDataAdapter sqlSda = new SqlDataAdapter( sqlCmd );
                    sqlSda.Fill( dt );

                    dgvEmp.AutoGenerateColumns = false;
                    // The property names are the datagridview headers
                    dgvEmp.Columns[ 0 ].DataPropertyName = "EmployeeId"; // This is Employee Id at the datagridview
                    dgvEmp.Columns[ 1 ].DataPropertyName = "Name";
                    dgvEmp.Columns[ 2 ].DataPropertyName = "City";
                    dgvEmp.Columns[ 3 ].DataPropertyName = "Department";
                    dgvEmp.Columns[ 4 ].DataPropertyName = "Gender";
                    dgvEmp.DataSource = dt;
                } catch ( Exception ex ) {
                    MessageBox.Show( "Error: " + ex.Message );
                }

            }
        }

        private void RefreshData() {
            btnSave.Text = "Save";
            textBoxEmp1.Text = "";
            textBoxCity1.Text = "";
            textBoxDept1.Text = "";
            comboBoxGen1.SelectedIndex = -1;
            EmployeeId = "";
            btnDelete.Enabled = false;
            FetchEmpDetails();
        }
        //--------------- </ region Funtions > ---------------
        #endregion

        #region Save/Update and Delete
        //--------------- < region Save/Update and Delete > ---------------
        private void btnSave_Click( object sender, EventArgs e ) {
            //Save or Update btn
            if ( string.IsNullOrWhiteSpace( textBoxEmp1.Text ) ) {
                MessageBox.Show( "Enter Employee Name !!!" );
            } else if ( string.IsNullOrWhiteSpace( textBoxCity1.Text ) ) {
                MessageBox.Show( "Enter Current City !!!" );
            } else if ( string.IsNullOrWhiteSpace( textBoxDept1.Text ) ) {
                MessageBox.Show( "Enter Department !!!" );
            } else if ( comboBoxGen1.SelectedIndex <= -1 ) {
                MessageBox.Show( "Select Gender !!!" );
            } else {
                using ( SqlConnection con = new SqlConnection( connectionStringConfig ) ) {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter( "SELECT Name FROM tblEmployee WHERE Name = @Name", con );
                    sda.SelectCommand.Parameters.AddWithValue( "@Name", textBoxEmp1.Text ); //Parameterized query for SqlDataAdapter
                    DataTable dt = new DataTable();
                    sda.Fill( dt );

                    if ( dt.Rows.Count >= 1 ) {
                        MessageBox.Show( $"{textBoxEmp1.Text} Already Exists!", "!" );
                    } else {
                        using ( SqlCommand sqlCmd = new SqlCommand( "spCRUD_Operations", con ) ) {
                            try {
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.AddWithValue( "@EmployeeId", EmployeeId );
                                sqlCmd.Parameters.AddWithValue( "@Name", textBoxEmp1.Text );
                                sqlCmd.Parameters.AddWithValue( "@City", textBoxCity1.Text );
                                sqlCmd.Parameters.AddWithValue( "@Department", textBoxDept1.Text );
                                sqlCmd.Parameters.AddWithValue( "@Gender", comboBoxGen1.Text );
                                sqlCmd.Parameters.AddWithValue( "@ActionType", "CreateOrUpdateData" );
                                sqlCmd.ExecuteNonQuery();

                                if ( btnSave.Text == "Save" )
                                    MessageBox.Show( "Record Saved Successfully !!!" );
                                else
                                    MessageBox.Show( "Record Updated Successfully !!!" );

                                RefreshData();
                            } catch ( Exception ex ) {
                                MessageBox.Show( "Error: " + ex.Message );
                            }
                        }
                    }
                }
            }
        }

        private void btnDelete_Click( object sender, EventArgs e ) {
            int selectedRowCount = dgvEmp.Rows.GetRowCount( DataGridViewElementStates.Selected );
            if ( selectedRowCount >= 0 ) {
                using ( SqlConnection con = new SqlConnection( connectionStringConfig ) )
                using ( SqlCommand sqlCmd = new SqlCommand( "spCRUD_Operations", con ) ) {
                    try {
                        con.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue( "@ActionType", "DeleteData" );
                        sqlCmd.Parameters.AddWithValue( "@EmployeeId", EmployeeId );
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show( "Record Deleted Successfully !!!" );
                        RefreshData();
                    } catch ( Exception ex ) {
                        MessageBox.Show( "Error: " + ex.Message );
                    }
                }
            } else {
                MessageBox.Show( "Please Select A Record !!!" );
            }
        }
        //--------------- </ region Save/Update and Delete > ---------------
        #endregion


        private void btnRefresh_Click( object sender, EventArgs e ) {
            RefreshData();
        }

        private void dgvEmp_CellClick( object sender, DataGridViewCellEventArgs e ) {
            if ( e.RowIndex != -1 ) {
                DataGridViewRow row = dgvEmp.Rows[ e.RowIndex ];
                EmployeeId = row.Cells[ 0 ].Value.ToString(); //The Employee ID is determined here
                textBoxEmp1.Text = row.Cells[ 1 ].Value.ToString();
                textBoxCity1.Text = row.Cells[ 2 ].Value.ToString();
                textBoxDept1.Text = row.Cells[ 3 ].Value.ToString();
                comboBoxGen1.Text = row.Cells[ 4 ].Value.ToString();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
    }
}

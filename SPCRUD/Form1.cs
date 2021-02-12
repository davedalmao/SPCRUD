using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SPCRUD {
	//Icon Size: 24 px
	//Icon Color: #ACBCD4
	public partial class Form1 : Form {
		static string connectionStringConfig = ConfigurationManager.ConnectionStrings[ "SystemDatabaseConnectionTemp" ].ConnectionString;
		string EmployeeId = "";

		#region Form1
		//--------------- < region Form1 > ---------------
		public Form1() {
			InitializeComponent();
			SetAddRemoveProgramsIcon();
		}

		private void Form1_Load( object sender, EventArgs e ) {
			FetchEmpDetails( "DisplayAllEmployees" );
		}
		//--------------- </ region Form1 > ---------------
		#endregion

		#region Functions
		//--------------- < region Funtions > ---------------
		private void DeleteEmployee( string deleteType, string employeeID ) {
			using ( SqlConnection con = new SqlConnection( connectionStringConfig ) )
			using ( SqlCommand sqlCmd = new SqlCommand( "spCRUD_Operations", con ) ) {
				try {
					con.Open();
					sqlCmd.CommandType = CommandType.StoredProcedure;
					//cmd.Parameters.Add("@Parameter", SqlDbType.DateTime).Value = MyDateTimeVariable;
					sqlCmd.Parameters.AddWithValue( "@action_type", deleteType );
					sqlCmd.Parameters.AddWithValue( "@employee_id", employeeID );//convert to int
					int numRes = sqlCmd.ExecuteNonQuery();
					if ( numRes > 0 )
						MessageBox.Show( ( employeeID != null ) ? $"{ txtEmpName.Text }'s Record DELETED Successfully!" : "All Employee Records DELETED Successfully!" );
					else
						MessageBox.Show( $"Cannot DELETE records! " );
					RefreshData();
				} catch ( Exception ex ) {
					MessageBox.Show( $"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }" );
				}
			}
		}

		private void FetchEmpDetails( string readType ) {
			//Load/Read Data from database
			using ( SqlConnection con = new SqlConnection( connectionStringConfig ) )
			using ( SqlCommand sqlCmd = new SqlCommand( "spCRUD_Operations", con ) ) {
				try {
					con.Open();
					DataTable dt = new DataTable();
					sqlCmd.CommandType = CommandType.StoredProcedure;
					sqlCmd.Parameters.AddWithValue( "@action_type", readType );
					sqlCmd.Connection = con;
					SqlDataAdapter sqlSda = new SqlDataAdapter( sqlCmd );
					sqlSda.Fill( dt );

					dgvEmpDetails.AutoGenerateColumns = false;//if true displays all the records in the database

					// The property names are the column names in dbo.Employee
					dgvEmpDetails.Columns[ 0 ].DataPropertyName = "employee_id"; // This is Employee Id at the datagridview
					dgvEmpDetails.Columns[ 1 ].DataPropertyName = "employee_name";
					dgvEmpDetails.Columns[ 2 ].DataPropertyName = "city";
					dgvEmpDetails.Columns[ 3 ].DataPropertyName = "department";
					dgvEmpDetails.Columns[ 4 ].DataPropertyName = "gender";

					dgvEmpDetails.Columns[ 5 ].DataPropertyName = "health_insurance_provider";
					dgvEmpDetails.Columns[ 6 ].DataPropertyName = "plan_name";
					dgvEmpDetails.Columns[ 7 ].DataPropertyName = "monthly_fee";
					dgvEmpDetails.Columns[ 7 ].DefaultCellStyle.Format = "#,##0.00";

					dgvEmpDetails.Columns[ 8 ].DataPropertyName = "insurance_start_date";
					dgvEmpDetails.Columns[ 8 ].DefaultCellStyle.Format = "MMMM dd, yyyy";

					dgvEmpDetails.DataSource = dt;
				} catch ( Exception ex ) {
					MessageBox.Show( $"Cannot DISPLAY data in the datagridview! \nError: { ex.Message }" );
				}
			}
		}

		private void RefreshData() {
			RefreshHealthInsuranceFields();
			btnSave.Text = "Save";
			EmployeeId = "";
			txtEmpName.Text = "";
			txtEmpCity.Text = "";
			txtEmpDept.Text = "";
			cboEmpGender.SelectedIndex = -1;
			cboEmpGender.Text = "";
			btnDelete.Enabled = false;
			FetchEmpDetails( "DisplayAllEmployees" );
		}

		private void RefreshHealthInsuranceFields() {
			txtEmpHealthInsuranceProvider.Text = "";
			txtEmpInsurancePlanName.Text = "";
			txtEmpInsuranceMonthlyFee.Text = "0";
			dtpInsuranceStartDate.Value = DateTime.Now;
		}

		private void SetAddRemoveProgramsIcon() {
			//This Icon is seen in control panel (uninstalling the app)
			if ( ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun ) {
				try {
					//The icon located in: (Right click Project -> Properties -> Application (tab) -> Icon)
					var iconSourcePath = Path.Combine( Application.StartupPath, "briefcase-4-fill.ico" );

					if ( !File.Exists( iconSourcePath ) )
						return;

					var myUninstallKey = Registry.CurrentUser.OpenSubKey( @"Software\Microsoft\Windows\CurrentVersion\Uninstall" );
					if ( myUninstallKey == null )
						return;

					var mySubKeyNames = myUninstallKey.GetSubKeyNames();
					foreach ( var subkeyName in mySubKeyNames ) {
						var myKey = myUninstallKey.OpenSubKey( subkeyName, true );
						var myValue = myKey.GetValue( "DisplayName" );
						if ( myValue != null && myValue.ToString() == "SP CRUD" ) { // same as in 'Product name:' field (Located in: Right click Project -> Properties -> Publish (tab) -> Options -> Description)
							myKey.SetValue( "DisplayIcon", iconSourcePath );
							break;
						}
					}
				} catch ( Exception ex ) {
					MessageBox.Show( "Error: " + ex.Message );
				}
			}
		}
		//--------------- </ region Funtions > ---------------
		#endregion

		#region Button Click
		//--------------- < region Buttons > ---------------
		private void btnDelete_Click( object sender, EventArgs e ) {
			int selectedRowCount = dgvEmpDetails.Rows.GetRowCount( DataGridViewElementStates.Selected );
			try {

				if ( selectedRowCount >= 0 ) {
					DialogResult dialog = MessageBox.Show( $"Do you want to DELETE { txtEmpName.Text }'s record?", "Continue Process?", MessageBoxButtons.YesNo );
					if ( dialog == DialogResult.Yes ) {
						DeleteEmployee( "DeleteData", EmployeeId );
					}
				} else {
					MessageBox.Show( "Please Select A Record !!!" );
				}
			} catch ( Exception ex ) {
				MessageBox.Show( $"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }" );
			}
		}

		private void btnDeleteAllRecords_Click( object sender, EventArgs e ) {
			DialogResult dialog = MessageBox.Show( "Do you want to DELETE ALL Employee Records?", "Continue Process?", MessageBoxButtons.YesNo );
			if ( dialog == DialogResult.Yes ) {
				DeleteEmployee( "DeleteAllData", null );
			}
		}

		private void btnDisplayAllEmployees_Click( object sender, EventArgs e ) {
			FetchEmpDetails( "DisplayAllEmployees" );
		}

		private void btnRefresh_Click( object sender, EventArgs e ) {
			RefreshData();
		}

		private void btnSave_Click( object sender, EventArgs e ) {
			//Save or Update btn
			if ( string.IsNullOrWhiteSpace( txtEmpName.Text ) ) {
				MessageBox.Show( "Enter Employee Name !!!" );
			} else if ( string.IsNullOrWhiteSpace( txtEmpCity.Text ) ) {
				MessageBox.Show( "Enter Current City !!!" );
			} else if ( string.IsNullOrWhiteSpace( txtEmpDept.Text ) ) {
				MessageBox.Show( "Enter Department !!!" );
			} else if ( cboEmpGender.SelectedIndex <= -1 ) {
				MessageBox.Show( "Select Gender !!!" );
			} else {
				// If at least one of the health insurance fields is not blank, save only the employee record without the health insurance record
				if ( string.IsNullOrWhiteSpace( txtEmpHealthInsuranceProvider.Text ) ||
					 string.IsNullOrWhiteSpace( txtEmpInsurancePlanName.Text ) ||
					 string.IsNullOrWhiteSpace( txtEmpInsuranceMonthlyFee.Text ) ||
					 float.Parse( txtEmpInsuranceMonthlyFee.Text ) < 1 ) {
					RefreshHealthInsuranceFields();
				}

				using ( SqlConnection con = new SqlConnection( connectionStringConfig ) )
				using ( SqlCommand sqlCmd = new SqlCommand( "spCRUD_Operations", con ) ) {
					try {
						con.Open();
						sqlCmd.CommandType = CommandType.StoredProcedure;
						sqlCmd.Parameters.AddWithValue( "@employee_id", EmployeeId );//convert to int
						sqlCmd.Parameters.AddWithValue( "@employee_name", txtEmpName.Text );
						sqlCmd.Parameters.AddWithValue( "@city", txtEmpCity.Text );
						sqlCmd.Parameters.AddWithValue( "@department", txtEmpDept.Text );
						sqlCmd.Parameters.AddWithValue( "@gender", cboEmpGender.Text );

						sqlCmd.Parameters.AddWithValue( "@health_insurance_provider", txtEmpHealthInsuranceProvider.Text );
						sqlCmd.Parameters.AddWithValue( "@plan_name", txtEmpInsurancePlanName.Text );
						sqlCmd.Parameters.AddWithValue( "@monthly_fee", string.IsNullOrWhiteSpace( txtEmpInsuranceMonthlyFee.Text ) ? 0 : float.Parse( txtEmpInsuranceMonthlyFee.Text ) ); //add 0 as default value in database

						// Save insurance start date:
						if ( string.IsNullOrWhiteSpace( txtEmpHealthInsuranceProvider.Text ) ||
							 string.IsNullOrWhiteSpace( txtEmpInsurancePlanName.Text ) ||
							 string.IsNullOrWhiteSpace( txtEmpInsuranceMonthlyFee.Text ) ||
							 float.Parse( txtEmpInsuranceMonthlyFee.Text ) < 1 ) {
							sqlCmd.Parameters.AddWithValue( "@insurance_start_date", SqlDbType.Date ).Value = DBNull.Value;
						} else {
							sqlCmd.Parameters.AddWithValue( "@insurance_start_date", SqlDbType.Date ).Value = dtpInsuranceStartDate.Value.Date.ToString( "yyyyMMdd" );
						}

						sqlCmd.Parameters.AddWithValue( "@action_type", "CreateOrUpdateData" );
						int numRes = sqlCmd.ExecuteNonQuery();
						//ExecuteNonQuery returns 0 if the query's where clause doesnt match any row in the table
						string ActionType = ( btnSave.Text == "Save" ) ? "Saved" : "Updated";
						if ( numRes > 0 ) {
							if ( string.IsNullOrWhiteSpace( txtEmpHealthInsuranceProvider.Text ) ||
								 string.IsNullOrWhiteSpace( txtEmpInsurancePlanName.Text ) ||
								 string.IsNullOrWhiteSpace( txtEmpInsuranceMonthlyFee.Text ) ||
								 float.Parse( txtEmpInsuranceMonthlyFee.Text ) < 1 ) {
								MessageBox.Show( $"{ txtEmpName.Text }'s record is { ActionType } successfully !!! \nAdd Health Insurance records later on." );
							} else {
								MessageBox.Show( $"{ txtEmpName.Text }'s record is { ActionType } successfully !!!" );
							}
							RefreshData();
						} else
							MessageBox.Show( $"{txtEmpName.Text} Already Exist!!!" );
					} catch ( SqlException ex ) {
						//To always have a guaranteed "Unique Value" in sql: Use UNIQUE CONSTRAINT or Primary Key
						if ( ex.Number == 2627 )  // Violation of unique constraint (Name should be unique)
							MessageBox.Show( $"{txtEmpName.Text} Already Exist!!!" );
						else
							MessageBox.Show( $"An SQL error occured while processing data. \nError: { ex.Message }" );
					} catch ( Exception ex ) {
						MessageBox.Show( $"Cannot INSERT or UPDATE data! \nError: { ex.Message }" );
					}
				}
			}
		}

		private void btnSortEmployees_Click( object sender, EventArgs e ) {
			if ( btnSortEmployees.Text == "Employees Without Healh Insurance" ) {
				FetchEmpDetails( "WithoutHealthInsuranceRecords" );
				btnSortEmployees.Values.Image = Properties.Resources.emotion_happy_fill;
			} else {
				FetchEmpDetails( "WithHealthInsuranceRecords" );
				btnSortEmployees.Values.Image = Properties.Resources.emotion_unhappy_fill;
			}

			btnSortEmployees.Text = ( btnSortEmployees.Text == "Employees Without Healh Insurance" ) ? "Employees With Healh Insurance" : "Employees Without Healh Insurance";
		}
		//--------------- < /region Buttons > ---------------
		#endregion

		#region DataGridView
		//--------------- < region DataGridView > ---------------
		private void dgvEmpDetails_CellClick( object sender, DataGridViewCellEventArgs e ) {
			try {
				if ( e.RowIndex != -1 ) {
					//check if one row index is null
					DataGridViewRow row = dgvEmpDetails.Rows[ e.RowIndex ];
					//the ? new .Value would assign null to the Text property of the textboxes in case the cell value is null 
					EmployeeId = row.Cells[ 0 ].Value?.ToString(); //The Employee ID is determined here
					txtEmpName.Text = row.Cells[ 1 ].Value?.ToString();
					txtEmpCity.Text = row.Cells[ 2 ].Value?.ToString();
					txtEmpDept.Text = row.Cells[ 3 ].Value?.ToString();
					cboEmpGender.Text = row.Cells[ 4 ].Value?.ToString();
					txtEmpHealthInsuranceProvider.Text = row.Cells[ 5 ].Value?.ToString();
					txtEmpInsurancePlanName.Text = row.Cells[ 6 ].Value?.ToString();
					txtEmpInsuranceMonthlyFee.Text = Convert.ToDecimal( row.Cells[ 7 ].Value ).ToString( "#,##0.00" );

					var cellValue = dgvEmpDetails.Rows[ e.RowIndex ].Cells[ 8 ].Value;
					if ( cellValue == null || cellValue == DBNull.Value
					 || String.IsNullOrWhiteSpace( cellValue.ToString() ) ) {
						dtpInsuranceStartDate.Value = DateTime.Now;
					} else {
						dtpInsuranceStartDate.Value = DateTime.Parse( row.Cells[ 8 ].Value?.ToString() );
					}

					btnSave.Text = "Update";
					btnDelete.Enabled = true;
				}
			} catch ( Exception ex ) {
				MessageBox.Show( $"Something is wrong with the selected record! \nError: { ex.Message }" );
			}
		}
		//--------------- < /region DataGridView > ---------------
		#endregion
	}
}

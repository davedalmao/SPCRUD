using Microsoft.Win32;
using SPCRUD.Classes.Utility;
using SPCRUD.Classes.Utility.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace SPCRUD
{
    //Icon Size: 24 px
    //Icon Color: #ACBCD4
    public partial class Form1 : Form
    {
        //change to "SystemDatabaseConnection"(the final connectionString) when app is ready to be deployed
        static string connectionStringConfig = ConfigurationManager.ConnectionStrings["SystemDatabaseConnectionTemp"].ConnectionString;
        string EmployeeId = "";
        readonly Color themeColor = Color.FromArgb(172, 188, 212);

        #region Form1
        //------------------------------ < region Form1 > ------------------------------
        public Form1()
        {
            InitializeComponent();
            AddRemoveProgramsIcon.SetAddRemoveProgramsIcon();
            pictureBox1.Image = Properties.Resources.default_Employee_Image;
            pictureBox1.Image = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
            employeeOperation.DisplayEmployeeRecords("DisplayAllEmployees", dgvEmpDetails);
        }
        //------------------------------ </ region Form1 > ------------------------------
        #endregion

        #region Methods
        //------------------------------ < region Methods > ------------------------------
        private void RefreshData()
        {
            ResethHealthInsuranceFields();
            btnSave.Text = "Save";
            EmployeeId = "";
            txtEmpName.Text = "";
            txtEmpCity.Text = "";
            txtEmpDept.Text = "";
            cboEmpGender.SelectedIndex = -1;
            cboEmpGender.Text = "";
            btnDelete.Enabled = false;
            pictureBox1.Image = null;
            lblFileExtension.Text = "";
            txtEmpName.Focus();

            EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
            employeeOperation.DisplayEmployeeRecords("DisplayAllEmployees", dgvEmpDetails);
        }

        private bool CheckHealthInsuranceFields()
        {
            //if true ->  one of the health insurance fields is blank
            if (string.IsNullOrWhiteSpace(txtEmpHealthInsuranceProvider.Text) ||
                string.IsNullOrWhiteSpace(txtEmpInsurancePlanName.Text) ||
                string.IsNullOrWhiteSpace(txtEmpInsuranceMonthlyFee.Text) ||
                float.Parse(txtEmpInsuranceMonthlyFee.Text) < 1)
            {
                return true;
            }
            return false;
        }

        private void ResethHealthInsuranceFields()
        {
            txtEmpHealthInsuranceProvider.Text = "";
            txtEmpInsurancePlanName.Text = "";
            txtEmpInsuranceMonthlyFee.Text = "0.00";
            dtpInsuranceStartDate.Value = DateTime.Now;
        }

        private void DeleteAllRecords()
        {
            int selectedRowCount = dgvEmpDetails.Rows.GetRowCount(DataGridViewElementStates.Selected);
            using (SqlConnection con = new SqlConnection(connectionStringConfig))
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
                        RefreshData();
                        return;
                    }

                    if (selectedRowCount > 0)
                    {
                        MessageBox.Show($"Cannot DELETE records! ");
                        return;
                    }
                    MessageBox.Show($"No records to DELETE! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }");
                }
            }
        } //done

        private void DeleteEmployee()
        {
            using (SqlConnection con = new SqlConnection(connectionStringConfig))
            using (SqlCommand sqlCmd = new SqlCommand("spDeleteData", con))
            {
                try
                {
                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = EmployeeId;

                    int numRes = sqlCmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show($"{ txtEmpName.Text }'s Record DELETED Successfully!");
                        RefreshData();
                        return;
                    }
                    MessageBox.Show($"Cannot DELETE records! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }");
                }

            }
        } //done

        /* private void DisplayEmployeeImge()
         {
             //Display user image
             string sqlQuery = "SELECT user_image, file_extension FROM dbo.Employee_Image WHERE employee_id=@employee_id";
             using (SqlConnection con = new SqlConnection(connectionStringConfig))
             using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, con))
             {
                 con.Open();
                 sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = EmployeeId;

                 using (SqlDataReader reader = sqlCmd.ExecuteReader())
                 {
                     if (reader.HasRows)
                     {
                         reader.Read();
                         if (reader.GetValue(0) == null && reader.GetValue(0) == null) //if image is null add border color to tag
                         {
                             pictureBox1.Tag = themeColor;
                         }
                         else
                         {
                             pictureBox1.Tag = null;
                             lblFileExtension.Text = reader.GetValue(1).ToString();
                             pictureBox1.Image = ImageOperations.BytesToImage((byte[])(reader.GetValue(0)));
                         }
                         return;
                     }
                     pictureBox1.Image = null; //if (!reader.HasRows)
                 }
             }
         }*/ //done

        /*private void DisplayEmployeeRecords(string displayType)
        {//Load/Read Data from database
            using (SqlConnection con = new SqlConnection(connectionStringConfig))
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

                    dgvEmpDetails.AutoGenerateColumns = false;//if true displays all the records in the database

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
        }*/ //done

        /*private void SaveEmployeeRecord()
        {
            using (SqlConnection con = new SqlConnection(connectionStringConfig))
            using (SqlCommand sqlCmd = new SqlCommand("spCreateOrUpdateData", con))
            {
                try
                {
                    // If at least one of the health insurance fields is blank, save only the employee record without the health insurance record
                    if (CheckHealthInsuranceFields()) { ResethHealthInsuranceFields(); }

                    con.Open();
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    //Employee Record
                    sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = EmployeeId;
                    sqlCmd.Parameters.Add("@employee_name", SqlDbType.NVarChar, 250).Value = txtEmpName.Text;
                    sqlCmd.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = txtEmpCity.Text;
                    sqlCmd.Parameters.Add("@department", SqlDbType.NVarChar, 50).Value = txtEmpDept.Text;
                    sqlCmd.Parameters.Add("@gender", SqlDbType.NVarChar, 6).Value = cboEmpGender.Text;

                    //Employee Health Insurance Record
                    sqlCmd.Parameters.Add("@health_insurance_provider", SqlDbType.NVarChar, 100).Value = txtEmpHealthInsuranceProvider.Text;
                    sqlCmd.Parameters.Add("@plan_name", SqlDbType.NVarChar, 100).Value = txtEmpInsurancePlanName.Text;
                    sqlCmd.Parameters.Add(new SqlParameter("@monthly_fee", SqlDbType.Decimal)
                    {
                        Precision = 15, //Precision specifies the number of digits used to represent the value of the parameter.
                        Scale = 2, //Scale is used to specify the number of decimal places in the value of the parameter.
                        Value = string.IsNullOrWhiteSpace(txtEmpInsuranceMonthlyFee.Text) //add 0 as default value in database
                              ? 0
                              : decimal.Parse(txtEmpInsuranceMonthlyFee.Text)
                    });

                    // Save insurance start date:
                    if (CheckHealthInsuranceFields())
                    {
                        sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = DBNull.Value;
                    }
                    else
                    {
                        sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = dtpInsuranceStartDate.Value.Date;
                    }

                    //Employee Image and File Extension
                    if (pictureBox1.Tag != null && string.IsNullOrWhiteSpace(lblFileExtension.Text)) //if tag has a value (save null)
                    {
                        sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary).Value = DBNull.Value;
                        sqlCmd.Parameters.Add("@file_extension", SqlDbType.NVarChar, 12).Value = DBNull.Value;

                    }
                    else
                    {
                        sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary).Value = ImageOperations.ImageToBytes(pictureBox1.Image, lblFileExtension.Text);
                        sqlCmd.Parameters.Add("@file_extension", SqlDbType.NVarChar, 12).Value = lblFileExtension.Text;
                    }

                    int numRes = sqlCmd.ExecuteNonQuery();
                    string ActionType = (btnSave.Text == "Save") ? "Saved" : "Updated";
                    if (numRes > 0) //if query is successful
                    {
                        if (CheckHealthInsuranceFields())
                        {
                            MessageBox.Show($"{ txtEmpName.Text }'s record is { ActionType } successfully !!! \nAdd Health Insurance records later on.");
                        }
                        else
                        {
                            MessageBox.Show($"{ txtEmpName.Text }'s record is { ActionType } successfully !!!");
                        }
                        RefreshData();
                        return;
                    }

                    MessageBox.Show($"{txtEmpName.Text} Already Exist !!!");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)// Violation of unique constraint (Name should be unique)
                    {
                        MessageBox.Show($"{txtEmpName.Text} Already Exist !!!");
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
        }*/
        //------------------------------ </ region Methods > ------------------------------
        #endregion

        #region Button Click
        //------------------------------ < region Buttons > ------------------------------
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Title = "Select image for [user]";
                openFile.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
                openFile.Multiselect = false;

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    try //image validation
                    {
                        Bitmap bmp = new Bitmap(openFile.FileName); //to validate the image
                        string imageFilePath = openFile.FileName;
                        string imageFileName = openFile.SafeFileName;

                        if (bmp != null) //if image is valid
                        {
                            pictureBox1.Image = Image.FromFile(imageFilePath); //display image selected from file explorer
                            pictureBox1.Image.RotateFlip(ImageOperations.Rotate(bmp)); //display image in proper orientation
                            lblFileExtension.Text = Path.GetExtension(openFile.SafeFileName); //file extension
                            txtEmpName.Focus();
                            bmp.Dispose();
                            pictureBox1.Tag = null;
                        }
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("The specified image file is invalid.");
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("The path to image is invalid.");
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dgvEmpDetails.Rows.GetRowCount(DataGridViewElementStates.Selected);
            string employeeName = txtEmpName.Text;
            try
            {
                if (selectedRowCount >= 0)
                {
                    DialogResult dialog = MessageBox.Show($"Do you want to DELETE { txtEmpName.Text }'s record?", "Continue Process?", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        //DeleteEmployee();
                        EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
                        employeeOperation.DeleteEmployee(EmployeeId, employeeName);
                        RefreshData();
                        return;
                    }
                }

                MessageBox.Show("Please Select A Record !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }");
            }
        } //done

        private void btnDeleteAllRecords_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dgvEmpDetails.Rows.GetRowCount(DataGridViewElementStates.Selected);
            string messageHeader = "Continue Process?";
            string messageContent = "Do you want to DELETE ALL Employee Records?";

            DialogResult dialog = MessageBox.Show(messageContent, messageHeader, MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                //DeleteAllRecords();
                EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
                employeeOperation.DeleteAllRecords(selectedRowCount);
                RefreshData();
            }

        } //done

        private void btnDisplayAllEmployees_Click(object sender, EventArgs e)
        {
            EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
            employeeOperation.DisplayEmployeeRecords("DisplayAllEmployees", dgvEmpDetails);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtEmpName.Text;
            string city = txtEmpCity.Text;
            string department = txtEmpDept.Text;
            string gender = cboEmpGender.Text;

            string healthInsProvider = txtEmpHealthInsuranceProvider.Text;
            string insPlanName = txtEmpInsurancePlanName.Text;
            string insMonthlyFee = txtEmpInsuranceMonthlyFee.Text;
            DateTime insStartDate = dtpInsuranceStartDate.Value.Date;

            PictureBox employeeImage = pictureBox1;
            string fileExtension = lblFileExtension.Text;

            string btnSaveText = btnSave.Text;

            //Save or Update btn
            if (string.IsNullOrWhiteSpace(txtEmpName.Text))
            {
                MessageBox.Show("Enter Employee Name !!!");
            }
            else if (string.IsNullOrWhiteSpace(txtEmpCity.Text))
            {
                MessageBox.Show("Enter Current City !!!");
            }
            else if (string.IsNullOrWhiteSpace(txtEmpDept.Text))
            {
                MessageBox.Show("Enter Department !!!");
            }
            else if (cboEmpGender.SelectedIndex <= -1)
            {
                MessageBox.Show("Select Gender !!!");
            }
            else
            {
                //SaveEmployeeRecord();
                SaveEmployee saveEmployee = new SaveEmployee(ConnectionString.config)
                {
                    HealthInsProvider = healthInsProvider,
                    InsPlanName = insPlanName,
                    InsMonthlyFee = insMonthlyFee
                };
                saveEmployee.InsertOrUpdate(EmployeeId, name, city, department, gender,
                                            healthInsProvider, insPlanName, insMonthlyFee, insStartDate,
                                            employeeImage, fileExtension,
                                            btnSaveText);
                RefreshData();
            }
        }

        private void btnSortEmployees_Click(object sender, EventArgs e)
        {
            if (btnSortEmployees.Text == "Employees Without Healh Insurance")
            {

                EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
                employeeOperation.DisplayEmployeeRecords("DisplayEmployeesWithoutHealthInsuranceRecords", dgvEmpDetails);
                btnSortEmployees.Values.Image = Properties.Resources.emotion_happy_fill;
            }
            else
            {

                EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
                employeeOperation.DisplayEmployeeRecords("DisplayEmployeesWithHealthInsuranceRecords", dgvEmpDetails);
                btnSortEmployees.Values.Image = Properties.Resources.emotion_unhappy_fill;
            }
            btnSortEmployees.Text = (btnSortEmployees.Text == "Employees Without Healh Insurance") ? "Employees With Healh Insurance" : "Employees Without Healh Insurance";
        }
        //------------------------------ < /region Buttons > ------------------------------
        #endregion

        #region DataGridView
        //------------------------------ < region DataGridView > ------------------------------
        private void dgvEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow row = dgvEmpDetails.Rows[e.RowIndex];
                    //the ? new .Value would assign null to the Text property of the textboxes in case the cell value is null 
                    EmployeeId = row.Cells[0].Value?.ToString(); //The Employee ID is determined here (the variable EmployeeId will be assigned a new value that is in the dgv)
                    txtEmpName.Text = row.Cells[1].Value?.ToString();
                    txtEmpCity.Text = row.Cells[2].Value?.ToString();
                    txtEmpDept.Text = row.Cells[3].Value?.ToString();
                    cboEmpGender.Text = row.Cells[4].Value?.ToString();
                    txtEmpHealthInsuranceProvider.Text = row.Cells[5].Value?.ToString();
                    txtEmpInsurancePlanName.Text = row.Cells[6].Value?.ToString();
                    txtEmpInsuranceMonthlyFee.Text = Convert.ToDecimal(row.Cells[7].Value).ToString("#,##0.00");

                    //Displaying the date in dateTimePicker
                    var cellValue = dgvEmpDetails.Rows[e.RowIndex].Cells[8].Value;
                    if (cellValue == null || cellValue == DBNull.Value
                        || String.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dtpInsuranceStartDate.Value = DateTime.Now;
                    }
                    else
                    {
                        dtpInsuranceStartDate.Value = DateTime.Parse(row.Cells[8].Value?.ToString());
                    }
                    btnSave.Text = "Update";
                    btnDelete.Enabled = true;

                    EmployeeOperations employeeOperation = new EmployeeOperations(ConnectionString.config);
                    employeeOperation.DisplayEmployeeImage(EmployeeId, pictureBox1, lblFileExtension);
                    // DisplayEmployeeImge();
                }
            }
            catch (InvalidCastException)
            {
                //MessageBox.Show($"This record has no image! \nError: { ex.Message }");
                //if record has null image (it will throw InvalidCastException)
                pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something is wrong with the selected record! \nError: { ex.GetType().FullName }");
            }
        }
        //------------------------------ < /region DataGridView > ------------------------------
        #endregion

        #region PictureBox
        //------------------------------ < region PictureBox > ------------------------------
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //if image is not present in the picturebox -> paint its border
            if (pictureBox1.Image == null)
            {
                pictureBox1.Tag = themeColor;
                pictureBox1.Image = Properties.Resources.default_Employee_Image;
            }

            if (pictureBox1.Tag != null) //if tag has a value -> paint the border (this happens if image form db is null)
            {
                ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, themeColor, ButtonBorderStyle.Solid);
            }
        }
        //------------------------------ </ region PictureBox > ------------------------------
        #endregion
    }
}

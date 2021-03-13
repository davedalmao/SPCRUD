using Microsoft.Win32;
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
        static string connectionStringConfig = ConfigurationManager.ConnectionStrings["SystemDatabaseConnectionTemp"].ConnectionString;
        string EmployeeId = "";
        // string imageLocation = "";

        #region Form1
        //--------------- < region Form1 > ---------------
        public Form1()
        {
            InitializeComponent();
            SetAddRemoveProgramsIcon();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayEmployeeRecords("DisplayAllEmployees");
        }
        //--------------- </ region Form1 > ---------------
        #endregion

        #region Functions
        //--------------- < region Funtions > ---------------



        private void DisplayEmployeeRecords(string displayType)
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
        }

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
            DisplayEmployeeRecords("DisplayAllEmployees");

            txtEmpName.Focus();
        }

        private void ResethHealthInsuranceFields()
        {
            txtEmpHealthInsuranceProvider.Text = "";
            txtEmpInsurancePlanName.Text = "";
            txtEmpInsuranceMonthlyFee.Text = "0.00";
            dtpInsuranceStartDate.Value = DateTime.Now;
        }

        //Change Image to Correct Orientation When displaying to PictureBox
        public static RotateFlipType Rotate(Image bmp)
        {
            const int OrientationId = 0x0112;
            PropertyItem pi = bmp.PropertyItems.Select(x => x).FirstOrDefault(x => x.Id == OrientationId);
            if (pi == null)
                return RotateFlipType.RotateNoneFlipNone;

            byte o = pi.Value[0];

            //Orientations
            if (o == 2) //TopRight
                return RotateFlipType.RotateNoneFlipX;
            if (o == 3) //BottomRight
                return RotateFlipType.RotateNoneFlipXY;
            if (o == 4) //BottomLeft
                return RotateFlipType.RotateNoneFlipY;
            if (o == 5) //LeftTop
                return RotateFlipType.Rotate90FlipX;
            if (o == 6) //RightTop
                return RotateFlipType.Rotate90FlipNone;
            if (o == 7) //RightBottom
                return RotateFlipType.Rotate90FlipY;
            if (o == 8) //LeftBottom
                return RotateFlipType.Rotate90FlipXY;

            return RotateFlipType.RotateNoneFlipNone; //TopLeft (what the image looks by default) [or] Unknown
        }

        private void SetAddRemoveProgramsIcon()
        {
            //This Icon is seen in control panel (uninstalling the app)
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                try
                {
                    //The icon located in: (Right click Project -> Properties -> Application (tab) -> Icon)
                    var iconSourcePath = Path.Combine(Application.StartupPath, "briefcase-4-fill.ico");
                    if (!File.Exists(iconSourcePath)) { return; }

                    var myUninstallKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                    if (myUninstallKey == null) { return; }

                    var mySubKeyNames = myUninstallKey.GetSubKeyNames();
                    foreach (var subkeyName in mySubKeyNames)
                    {
                        var myKey = myUninstallKey.OpenSubKey(subkeyName, true);
                        var myValue = myKey.GetValue("DisplayName");
                        if (myValue != null && myValue.ToString() == "SP CRUD")
                        { // same as in 'Product name:' field (Located in: Right click Project -> Properties -> Publish (tab) -> Options -> Description)
                            myKey.SetValue("DisplayIcon", iconSourcePath);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Add Remove Programs Icon Error! \nError: " + ex.Message);
                }
            }
        }
        //--------------- </ region Funtions > ---------------
        #endregion

        #region Button Click
        //--------------- < region Buttons > ---------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dgvEmpDetails.Rows.GetRowCount(DataGridViewElementStates.Selected);
            try
            {
                if (selectedRowCount >= 0)
                {
                    DialogResult dialog = MessageBox.Show($"Do you want to DELETE { txtEmpName.Text }'s record?", "Continue Process?", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        //DeleteEmployee( "DeleteData", EmployeeId );
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
                                }
                                else
                                    MessageBox.Show($"Cannot DELETE records! ");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select A Record !!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }");
            }
        }

        private void btnDeleteAllRecords_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to DELETE ALL Employee Records?", "Continue Process?", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                //DeleteEmployee( "DeleteAllData", null );
                using (SqlConnection con = new SqlConnection(connectionStringConfig))
                using (SqlCommand sqlCmd = new SqlCommand("spDeleteAllEmployeeRecords", con))
                {
                    try
                    {
                        con.Open();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        int numRes = sqlCmd.ExecuteNonQuery();
                        if (numRes > 0)
                            MessageBox.Show("All Employee Records DELETED Successfully!");
                        else
                            MessageBox.Show($"Cannot DELETE records! ");
                        RefreshData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Cannot DELETE { txtEmpName.Text }'s record! \nError: { ex.Message }");
                    }
                }
            }
        }

        private void btnDisplayAllEmployees_Click(object sender, EventArgs e)
        {
            DisplayEmployeeRecords("DisplayAllEmployees");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                using (SqlConnection con = new SqlConnection(connectionStringConfig))
                using (SqlCommand sqlCmd = new SqlCommand("spCreateOrUpdateData", con))
                {
                    try
                    {
                        // If at least one of the health insurance fields is blank, save only the employee record without the health insurance record
                        if (string.IsNullOrWhiteSpace(txtEmpHealthInsuranceProvider.Text) ||
                             string.IsNullOrWhiteSpace(txtEmpInsurancePlanName.Text) ||
                             string.IsNullOrWhiteSpace(txtEmpInsuranceMonthlyFee.Text) ||
                             float.Parse(txtEmpInsuranceMonthlyFee.Text) < 1)
                        {
                            ResethHealthInsuranceFields();
                        }

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
                            Value = string.IsNullOrWhiteSpace(txtEmpInsuranceMonthlyFee.Text) ? 0 : decimal.Parse(txtEmpInsuranceMonthlyFee.Text) //add 0 as default value in database
                        });
                        // Save insurance start date:
                        if (string.IsNullOrWhiteSpace(txtEmpHealthInsuranceProvider.Text) ||
                             string.IsNullOrWhiteSpace(txtEmpInsurancePlanName.Text) ||
                             string.IsNullOrWhiteSpace(txtEmpInsuranceMonthlyFee.Text) ||
                             float.Parse(txtEmpInsuranceMonthlyFee.Text) < 1)
                        {
                            sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@insurance_start_date", SqlDbType.Date).Value = dtpInsuranceStartDate.Value.Date;
                        }




                        //Employee Image 
                        sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary, 8000).Value = ImageConverter.ConvertImageToByteArray(pictureBox1.Image);//error here
                        //sqlCmd.Parameters.Add("@file_extension", SqlDbType.VarChar, 12).Value = lblFileExtension.Text;

                        int numRes = sqlCmd.ExecuteNonQuery();
                        string ActionType = (btnSave.Text == "Save") ? "Saved" : "Updated";
                        if (numRes > 0)
                        {
                            if (string.IsNullOrWhiteSpace(txtEmpHealthInsuranceProvider.Text) ||
                                 string.IsNullOrWhiteSpace(txtEmpInsurancePlanName.Text) ||
                                 string.IsNullOrWhiteSpace(txtEmpInsuranceMonthlyFee.Text) ||
                                 float.Parse(txtEmpInsuranceMonthlyFee.Text) < 1)
                            {
                                MessageBox.Show($"{ txtEmpName.Text }'s record is { ActionType } successfully !!! \nAdd Health Insurance records later on.");
                            }
                            else
                            {
                                MessageBox.Show($"{ txtEmpName.Text }'s record is { ActionType } successfully !!!");
                            }
                            RefreshData();
                        }
                        else
                            MessageBox.Show($"{txtEmpName.Text} Already Exist !!!");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)// Violation of unique constraint (Name should be unique)
                        {
                            MessageBox.Show($"{txtEmpName.Text} Already Exist !!!");
                        }
                        else
                            MessageBox.Show($"An SQL error occured while processing data. \nError: { ex.Message }");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Cannot INSERT or UPDATE data! \nError: { ex.Message }");
                    }
                }
            }
        }

        private void btnSortEmployees_Click(object sender, EventArgs e)
        {
            if (btnSortEmployees.Text == "Employees Without Healh Insurance")
            {
                DisplayEmployeeRecords("DisplayEmployeesWithoutHealthInsuranceRecords");
                btnSortEmployees.Values.Image = Properties.Resources.emotion_happy_fill;
            }
            else
            {
                DisplayEmployeeRecords("DisplayEmployeesWithHealthInsuranceRecords");
                btnSortEmployees.Values.Image = Properties.Resources.emotion_unhappy_fill;
            }
            btnSortEmployees.Text = (btnSortEmployees.Text == "Employees Without Healh Insurance") ? "Employees With Healh Insurance" : "Employees Without Healh Insurance";
        }
        //--------------- < /region Buttons > ---------------
        #endregion

        #region DataGridView
        //--------------- < region DataGridView > ---------------
        private void dgvEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataGridViewRow row = dgvEmpDetails.Rows[e.RowIndex];
                    //the ? new .Value would assign null to the Text property of the textboxes in case the cell value is null 
                    EmployeeId = row.Cells[0].Value?.ToString(); //The Employee ID is determined here (That's why we declared EmployeeId as string so it can be displayed in the dataGridView)
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

                    //Display user image
                    using (SqlConnection con = new SqlConnection(connectionStringConfig))
                    using (SqlCommand sqlCmd = new SqlCommand("SELECT user_image FROM dbo.Employee_Image WHERE employee_id=@employee_id", con))
                    {
                        con.Open();
                        sqlCmd.Parameters.Add("@employee_id", SqlDbType.NVarChar).Value = EmployeeId;

                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                pictureBox1.Image = ImageConverter.ConvertByteArrayToImage((byte[])(reader.GetValue(0)));
                                //lblFileExtension.Text = reader.GetValue(1).ToString();
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }
                        }
                    }

                    btnSave.Text = "Update";
                    btnDelete.Enabled = true;
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
        //--------------- < /region DataGridView > ---------------
        #endregion

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
                        Bitmap bmp = new Bitmap(openFile.FileName);//to validate the image
                        string imageFilePath = openFile.FileName;
                        string imageFileName = openFile.SafeFileName;


                        if (bmp != null)//if image is valid
                        {
                            //imageLocation = imageFilePath;
                            //lblFileExtension.Text = Path.GetExtension(imageFileName);//file extension
                            //pictureBox1.Load(filePath);//display selected image file
                            pictureBox1.Image = Image.FromFile(imageFilePath);
                            pictureBox1.Image.RotateFlip(Rotate(bmp));//display image in proper orientation
                            bmp.Dispose();
                        }

                        //using (Bitmap bmp = new Bitmap(filePath))
                        //{
                        //    if (bmp != null)
                        //    {
                        //        img = new Bitmap(bmp);
                        //        pictureBox1.Image = img;
                        //        pictureBox1.Image.RotateFlip(Rotate(bmp));
                        //        lblFileExtension.Text = Path.GetExtension(fileName);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("The path to image is invalid.");
                        //    }
                        //}
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //pictureBox border color
            Color themeColor = Color.FromArgb(172, 188, 212);

            //if image is not present in the picturebox -> paint its border
            if (pictureBox1.Image == null)
            {
                ControlPaint.DrawBorder(e.Graphics, pictureBox1.ClientRectangle, themeColor, ButtonBorderStyle.Solid);
            }
        }
    }
}

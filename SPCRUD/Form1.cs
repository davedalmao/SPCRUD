using Microsoft.Win32;
using SPCRUD.Classes.DataAccess;
using SPCRUD.Classes.Models;
using SPCRUD.Classes.Utility;
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
        string EmployeeId = "";

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
            DisplayEmployee displayEmployee = new DisplayEmployee(ConnectionString.config);
            displayEmployee.AllRecords("DisplayAllEmployees", dgvEmpDetails);

        }
        //------------------------------ </ region Form1 > ------------------------------
        #endregion

        #region Methods
        //------------------------------ < region Methods > ------------------------------
        private void RefreshData()
        {
            txtEmpHealthInsuranceProvider.Text = "";
            txtEmpInsurancePlanName.Text = "";
            txtEmpInsuranceMonthlyFee.Text = "0.00";
            dtpInsuranceStartDate.Value = DateTime.Now;
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

            DisplayEmployee displayEmployee = new DisplayEmployee(ConnectionString.config);
            displayEmployee.AllRecords("DisplayAllEmployees", dgvEmpDetails);
        }
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
            string messageHeader = "Continue Process?";
            string messageContent = $"Do you want to DELETE { employeeName }'s record?";

            try
            {
                if (selectedRowCount >= 0)
                {
                    DialogResult dialog = MessageBox.Show(messageContent, messageHeader, MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        DeleteEmployee deleteEmployee = new DeleteEmployee(ConnectionString.config);
                        deleteEmployee.SpecificRecord(EmployeeId, employeeName);
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
        }

        private void btnDeleteAllRecords_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dgvEmpDetails.Rows.GetRowCount(DataGridViewElementStates.Selected);
            string messageHeader = "Continue Process?";
            string messageContent = "Do you want to DELETE ALL Employee Records?";

            DialogResult dialog = MessageBox.Show(messageContent, messageHeader, MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                DeleteEmployee deleteEmployee = new DeleteEmployee(ConnectionString.config);
                deleteEmployee.AllRecords(selectedRowCount);
                RefreshData();
            }

        }

        private void btnDisplayAllEmployees_Click(object sender, EventArgs e)
        {
            DisplayEmployee displayEmployee = new DisplayEmployee(ConnectionString.config);
            displayEmployee.AllRecords("DisplayAllEmployees", dgvEmpDetails);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Required Employee Records
            string name = txtEmpName.Text;
            string city = txtEmpCity.Text;
            string department = txtEmpDept.Text;
            string gender = cboEmpGender.Text;

            //Employee Health Insurance Records
            string healthInsProvider = txtEmpHealthInsuranceProvider.Text;
            string insPlanName = txtEmpInsurancePlanName.Text;
            string insMonthlyFee = txtEmpInsuranceMonthlyFee.Text;
            DateTime insStartDate = dtpInsuranceStartDate.Value.Date;

            //Employee Image
            PictureBox employeeImage = pictureBox1;
            string fileExtension = lblFileExtension.Text;

            //Action Type
            string actionType = btnSave.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Enter Employee Name !!!");
            }
            else if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Enter Current City !!!");
            }
            else if (string.IsNullOrWhiteSpace(department))
            {
                MessageBox.Show("Enter Department !!!");
            }
            else if (cboEmpGender.SelectedIndex <= -1)
            {
                MessageBox.Show("Select Gender !!!");
            }
            else
            {
                //TestSave t = new TestSave(ConnectionString.config)
                //{
                //    Id = EmployeeId,
                //    Name = name,
                //    City = city,
                //    Department = department,
                //    Gender = gender,

                //    HealthInsuranceProvider = healthInsProvider,
                //    InsurancePlanName = insPlanName,
                //    InsuranceMonthlyFee = insMonthlyFee,
                //    InsuranceStartDate = insStartDate,

                //    EmployeeImage = employeeImage,
                //    FileExtension = fileExtension
                //};
                //t.InsertOrUpdate(btnSaveText);
                SaveEmployee saveEmployee = new SaveEmployee(ConnectionString.config)
                {
                    Id = EmployeeId,
                    Name = name,
                    City = city,
                    Department = department,
                    Gender = gender,

                    HealthInsuranceProvider = healthInsProvider,
                    InsurancePlanName = insPlanName,
                    InsuranceMonthlyFee = insMonthlyFee,
                    InsuranceStartDate = insStartDate,

                    EmployeeImage = employeeImage,
                    FileExtension = fileExtension,

                    ActionType = actionType
                };
                saveEmployee.InsertOrUpdate();
                //check if InsertOrUpdate, if not, dont refresh
                RefreshData();
            }
        }

        private void btnSortEmployees_Click(object sender, EventArgs e)
        {
            if (btnSortEmployees.Text == "Employees Without Healh Insurance")
            {
                DisplayEmployee withoutHealthInsurance = new DisplayEmployee(ConnectionString.config);
                withoutHealthInsurance.AllRecords("DisplayEmployeesWithoutHealthInsuranceRecords", dgvEmpDetails);

                btnSortEmployees.Text = "Employees With Healh Insurance";
                btnSortEmployees.Values.Image = Properties.Resources.emotion_happy_fill;
                return;
            }

            DisplayEmployee withHealthInsurance = new DisplayEmployee(ConnectionString.config);
            withHealthInsurance.AllRecords("DisplayEmployeesWithHealthInsuranceRecords", dgvEmpDetails);

            btnSortEmployees.Text = "Employees Without Healh Insurance";
            btnSortEmployees.Values.Image = Properties.Resources.emotion_unhappy_fill;
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
                    EmployeeId = row.Cells[0].Value?.ToString(); //The Employee ID is determined here (the variable EmployeeId will be assigned a new value that is in this dgv cell)
                    txtEmpName.Text = row.Cells[1].Value?.ToString();
                    txtEmpCity.Text = row.Cells[2].Value?.ToString();
                    txtEmpDept.Text = row.Cells[3].Value?.ToString();
                    cboEmpGender.Text = row.Cells[4].Value?.ToString();
                    txtEmpHealthInsuranceProvider.Text = row.Cells[5].Value?.ToString();
                    txtEmpInsurancePlanName.Text = row.Cells[6].Value?.ToString();
                    txtEmpInsuranceMonthlyFee.Text = Convert.ToDecimal(row.Cells[7].Value).ToString("#,##0.00");

                    //Displaying the date in dateTimePicker
                    var cellValue = dgvEmpDetails.Rows[e.RowIndex].Cells[8].Value;
                    if (cellValue == null || cellValue == DBNull.Value || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dtpInsuranceStartDate.Value = DateTime.Now;
                    }
                    else
                    {
                        dtpInsuranceStartDate.Value = DateTime.Parse(row.Cells[8].Value?.ToString());
                    }
                    btnSave.Text = "Update";
                    btnDelete.Enabled = true;

                    //Display Employee Image
                    DisplayEmployee displayEmployee = new DisplayEmployee(ConnectionString.config);
                    displayEmployee.Image(EmployeeId, pictureBox1, lblFileExtension);
                }
            }
            catch (InvalidCastException)
            {
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
            Color themeColor = Color.FromArgb(172, 188, 212);
            if (pictureBox1.Image == null) //if image is not present in the picturebox -> paint its border
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

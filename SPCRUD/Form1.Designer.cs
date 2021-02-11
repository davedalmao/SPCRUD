
namespace SPCRUD {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.btnRefresh = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.btnDelete = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.dgvEmpDetails = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
			this.EmpId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.txtEmpName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.txtEmpCity = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.txtEmpDept = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.cboEmpGender = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
			this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.dtpInsuranceStartDate = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
			this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.txtEmpInsuranceMonthlyFee = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.txtEmpInsurancePlanName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel10 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.txtEmpHealthInsuranceProvider = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.kryptonLabel11 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
			this.btnDisplayAllEmployees = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.btnSortEmployees = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.btnDeleteAllRecords = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			((System.ComponentModel.ISupportInitialize)(this.dgvEmpDetails)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboEmpGender)).BeginInit();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(813, 223);
			this.btnSave.Name = "btnSave";
			this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.btnSave.Size = new System.Drawing.Size(105, 45);
			this.btnSave.StateCommon.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
			this.btnSave.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopRight;
			this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.TabIndex = 13;
			this.btnSave.Values.Image = global::SPCRUD.Properties.Resources.menu_add_line;
			this.btnSave.Values.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(935, 223);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.btnRefresh.Size = new System.Drawing.Size(105, 45);
			this.btnRefresh.StateCommon.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
			this.btnRefresh.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopRight;
			this.btnRefresh.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRefresh.TabIndex = 14;
			this.btnRefresh.Values.Image = global::SPCRUD.Properties.Resources.refresh_line__1_;
			this.btnRefresh.Values.Text = "Refresh";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(1054, 223);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.btnDelete.Size = new System.Drawing.Size(105, 45);
			this.btnDelete.StateCommon.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
			this.btnDelete.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopRight;
			this.btnDelete.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.TabIndex = 15;
			this.btnDelete.Values.Image = global::SPCRUD.Properties.Resources.delete_bin_5_line__1_;
			this.btnDelete.Values.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// dgvEmpDetails
			// 
			this.dgvEmpDetails.AllowUserToAddRows = false;
			this.dgvEmpDetails.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dgvEmpDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvEmpDetails.ColumnHeadersHeight = 40;
			this.dgvEmpDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvEmpDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmpId,
            this.EmployeeName,
            this.City,
            this.Department,
            this.Gender,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
			this.dgvEmpDetails.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dgvEmpDetails.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Sheet;
			this.dgvEmpDetails.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet;
			this.dgvEmpDetails.GridStyles.StyleColumn = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
			this.dgvEmpDetails.GridStyles.StyleDataCells = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
			this.dgvEmpDetails.GridStyles.StyleRow = ComponentFactory.Krypton.Toolkit.GridStyle.Sheet;
			this.dgvEmpDetails.Location = new System.Drawing.Point(28, 282);
			this.dgvEmpDetails.MultiSelect = false;
			this.dgvEmpDetails.Name = "dgvEmpDetails";
			this.dgvEmpDetails.ReadOnly = true;
			this.dgvEmpDetails.RowHeadersVisible = false;
			this.dgvEmpDetails.RowHeadersWidth = 50;
			this.dgvEmpDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dgvEmpDetails.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvEmpDetails.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dgvEmpDetails.RowTemplate.Height = 25;
			this.dgvEmpDetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvEmpDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvEmpDetails.Size = new System.Drawing.Size(1131, 242);
			this.dgvEmpDetails.TabIndex = 16;
			this.dgvEmpDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmp_CellClick);
			this.dgvEmpDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEmp_CellFormatting);
			// 
			// EmpId
			// 
			this.EmpId.HeaderText = "Employee Id";
			this.EmpId.Name = "EmpId";
			this.EmpId.ReadOnly = true;
			// 
			// EmployeeName
			// 
			this.EmployeeName.HeaderText = "Name";
			this.EmployeeName.Name = "EmployeeName";
			this.EmployeeName.ReadOnly = true;
			// 
			// City
			// 
			this.City.HeaderText = "City";
			this.City.Name = "City";
			this.City.ReadOnly = true;
			// 
			// Department
			// 
			this.Department.HeaderText = "Department";
			this.Department.Name = "Department";
			this.Department.ReadOnly = true;
			// 
			// Gender
			// 
			this.Gender.HeaderText = "Gender";
			this.Gender.Name = "Gender";
			this.Gender.ReadOnly = true;
			// 
			// Column1
			// 
			this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column1.HeaderText = "Health Insurance Provider";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Insurance Plan Name";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 135;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Monthly Fee";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Insurance Start Date";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.Column4.Width = 136;
			// 
			// txtEmpName
			// 
			this.txtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmpName.Location = new System.Drawing.Point(197, 27);
			this.txtEmpName.Name = "txtEmpName";
			this.txtEmpName.Size = new System.Drawing.Size(346, 29);
			this.txtEmpName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpName.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.txtEmpName.TabIndex = 17;
			// 
			// kryptonLabel1
			// 
			this.kryptonLabel1.Location = new System.Drawing.Point(28, 30);
			this.kryptonLabel1.Name = "kryptonLabel1";
			this.kryptonLabel1.Size = new System.Drawing.Size(167, 26);
			this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel1.TabIndex = 21;
			this.kryptonLabel1.Values.Image = global::SPCRUD.Properties.Resources.user_3_fill__2_;
			this.kryptonLabel1.Values.Text = "Employee Name :";
			// 
			// kryptonLabel2
			// 
			this.kryptonLabel2.Location = new System.Drawing.Point(28, 74);
			this.kryptonLabel2.Name = "kryptonLabel2";
			this.kryptonLabel2.Size = new System.Drawing.Size(134, 26);
			this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel2.TabIndex = 23;
			this.kryptonLabel2.Values.Image = global::SPCRUD.Properties.Resources.community_fill;
			this.kryptonLabel2.Values.Text = "Current City :";
			// 
			// txtEmpCity
			// 
			this.txtEmpCity.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmpCity.Location = new System.Drawing.Point(197, 71);
			this.txtEmpCity.Name = "txtEmpCity";
			this.txtEmpCity.Size = new System.Drawing.Size(346, 29);
			this.txtEmpCity.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpCity.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.txtEmpCity.TabIndex = 22;
			// 
			// kryptonLabel3
			// 
			this.kryptonLabel3.Location = new System.Drawing.Point(28, 118);
			this.kryptonLabel3.Name = "kryptonLabel3";
			this.kryptonLabel3.Size = new System.Drawing.Size(132, 26);
			this.kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel3.TabIndex = 25;
			this.kryptonLabel3.Values.Image = global::SPCRUD.Properties.Resources.honour_fill;
			this.kryptonLabel3.Values.Text = "Department :";
			// 
			// txtEmpDept
			// 
			this.txtEmpDept.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmpDept.Location = new System.Drawing.Point(197, 115);
			this.txtEmpDept.Name = "txtEmpDept";
			this.txtEmpDept.Size = new System.Drawing.Size(346, 29);
			this.txtEmpDept.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpDept.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.txtEmpDept.TabIndex = 24;
			// 
			// kryptonLabel4
			// 
			this.kryptonLabel4.Location = new System.Drawing.Point(30, 168);
			this.kryptonLabel4.Name = "kryptonLabel4";
			this.kryptonLabel4.Size = new System.Drawing.Size(101, 26);
			this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel4.TabIndex = 26;
			this.kryptonLabel4.Values.Image = global::SPCRUD.Properties.Resources.parent_fill;
			this.kryptonLabel4.Values.Text = "Gender :";
			// 
			// cboEmpGender
			// 
			this.cboEmpGender.DropDownWidth = 334;
			this.cboEmpGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
			this.cboEmpGender.ItemStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Command;
			this.cboEmpGender.Location = new System.Drawing.Point(197, 165);
			this.cboEmpGender.Name = "cboEmpGender";
			this.cboEmpGender.Size = new System.Drawing.Size(346, 29);
			this.cboEmpGender.StateActive.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboEmpGender.StateActive.ComboBox.Content.Padding = new System.Windows.Forms.Padding(50, 1, 1, 1);
			this.cboEmpGender.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboEmpGender.StateCommon.ComboBox.Content.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
			this.cboEmpGender.StateCommon.Item.Content.Padding = new System.Windows.Forms.Padding(10, 3, 3, 3);
			this.cboEmpGender.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboEmpGender.StateNormal.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboEmpGender.StateNormal.ComboBox.Content.Padding = new System.Windows.Forms.Padding(50, 5, 5, 5);
			this.cboEmpGender.TabIndex = 27;
			// 
			// kryptonLabel5
			// 
			this.kryptonLabel5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
			this.kryptonLabel5.Location = new System.Drawing.Point(26, 244);
			this.kryptonLabel5.Name = "kryptonLabel5";
			this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
			this.kryptonLabel5.Size = new System.Drawing.Size(115, 30);
			this.kryptonLabel5.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(149)))), ((int)(((byte)(188)))));
			this.kryptonLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel5.TabIndex = 28;
			this.kryptonLabel5.Values.Text = "Employees";
			// 
			// kryptonLabel7
			// 
			this.kryptonLabel7.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
			this.kryptonLabel7.Location = new System.Drawing.Point(49, 195);
			this.kryptonLabel7.Name = "kryptonLabel7";
			this.kryptonLabel7.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
			this.kryptonLabel7.Size = new System.Drawing.Size(6, 4);
			this.kryptonLabel7.StateCommon.ShortText.Color1 = System.Drawing.Color.DarkSlateGray;
			this.kryptonLabel7.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel7.TabIndex = 30;
			this.kryptonLabel7.Values.Text = "";
			// 
			// dtpInsuranceStartDate
			// 
			this.dtpInsuranceStartDate.Location = new System.Drawing.Point(813, 161);
			this.dtpInsuranceStartDate.Name = "dtpInsuranceStartDate";
			this.dtpInsuranceStartDate.Size = new System.Drawing.Size(346, 30);
			this.dtpInsuranceStartDate.StateActive.Content.Padding = new System.Windows.Forms.Padding(4);
			this.dtpInsuranceStartDate.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpInsuranceStartDate.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.dtpInsuranceStartDate.StateNormal.Content.Padding = new System.Windows.Forms.Padding(4);
			this.dtpInsuranceStartDate.TabIndex = 31;
			// 
			// kryptonLabel8
			// 
			this.kryptonLabel8.Location = new System.Drawing.Point(577, 118);
			this.kryptonLabel8.Name = "kryptonLabel8";
			this.kryptonLabel8.Size = new System.Drawing.Size(132, 26);
			this.kryptonLabel8.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel8.TabIndex = 37;
			this.kryptonLabel8.Values.Image = global::SPCRUD.Properties.Resources.secure_payment_fill;
			this.kryptonLabel8.Values.Text = "Monthly Fee:";
			// 
			// txtEmpInsuranceMonthlyFee
			// 
			this.txtEmpInsuranceMonthlyFee.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmpInsuranceMonthlyFee.Location = new System.Drawing.Point(813, 115);
			this.txtEmpInsuranceMonthlyFee.Name = "txtEmpInsuranceMonthlyFee";
			this.txtEmpInsuranceMonthlyFee.Size = new System.Drawing.Size(346, 30);
			this.txtEmpInsuranceMonthlyFee.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpInsuranceMonthlyFee.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.txtEmpInsuranceMonthlyFee.TabIndex = 36;
			this.txtEmpInsuranceMonthlyFee.Text = "0";
			// 
			// kryptonLabel9
			// 
			this.kryptonLabel9.Location = new System.Drawing.Point(577, 74);
			this.kryptonLabel9.Name = "kryptonLabel9";
			this.kryptonLabel9.Size = new System.Drawing.Size(200, 26);
			this.kryptonLabel9.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel9.TabIndex = 35;
			this.kryptonLabel9.Values.Image = global::SPCRUD.Properties.Resources.health_book_fill;
			this.kryptonLabel9.Values.Text = "Insurance Plan Name:";
			// 
			// txtEmpInsurancePlanName
			// 
			this.txtEmpInsurancePlanName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmpInsurancePlanName.Location = new System.Drawing.Point(813, 71);
			this.txtEmpInsurancePlanName.Name = "txtEmpInsurancePlanName";
			this.txtEmpInsurancePlanName.Size = new System.Drawing.Size(346, 29);
			this.txtEmpInsurancePlanName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpInsurancePlanName.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.txtEmpInsurancePlanName.TabIndex = 34;
			// 
			// kryptonLabel10
			// 
			this.kryptonLabel10.Location = new System.Drawing.Point(577, 30);
			this.kryptonLabel10.Name = "kryptonLabel10";
			this.kryptonLabel10.Size = new System.Drawing.Size(236, 26);
			this.kryptonLabel10.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel10.TabIndex = 33;
			this.kryptonLabel10.Values.Image = global::SPCRUD.Properties.Resources.hospital_fill;
			this.kryptonLabel10.Values.Text = "Health Inusrance Provider :";
			// 
			// txtEmpHealthInsuranceProvider
			// 
			this.txtEmpHealthInsuranceProvider.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtEmpHealthInsuranceProvider.Location = new System.Drawing.Point(813, 27);
			this.txtEmpHealthInsuranceProvider.Name = "txtEmpHealthInsuranceProvider";
			this.txtEmpHealthInsuranceProvider.Size = new System.Drawing.Size(346, 29);
			this.txtEmpHealthInsuranceProvider.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpHealthInsuranceProvider.StateCommon.Content.Padding = new System.Windows.Forms.Padding(4);
			this.txtEmpHealthInsuranceProvider.TabIndex = 32;
			// 
			// kryptonLabel11
			// 
			this.kryptonLabel11.Location = new System.Drawing.Point(577, 165);
			this.kryptonLabel11.Name = "kryptonLabel11";
			this.kryptonLabel11.Size = new System.Drawing.Size(192, 26);
			this.kryptonLabel11.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.kryptonLabel11.TabIndex = 38;
			this.kryptonLabel11.Values.Image = global::SPCRUD.Properties.Resources.calendar_event_fill;
			this.kryptonLabel11.Values.Text = "Insurance Start Date:";
			// 
			// btnDisplayAllEmployees
			// 
			this.btnDisplayAllEmployees.Location = new System.Drawing.Point(714, 537);
			this.btnDisplayAllEmployees.Name = "btnDisplayAllEmployees";
			this.btnDisplayAllEmployees.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.btnDisplayAllEmployees.Size = new System.Drawing.Size(217, 45);
			this.btnDisplayAllEmployees.StateCommon.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
			this.btnDisplayAllEmployees.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopRight;
			this.btnDisplayAllEmployees.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDisplayAllEmployees.TabIndex = 39;
			this.btnDisplayAllEmployees.Values.Image = global::SPCRUD.Properties.Resources.team_fill;
			this.btnDisplayAllEmployees.Values.Text = "Display All Employees";
			this.btnDisplayAllEmployees.Click += new System.EventHandler(this.btnDisplayAllEmployees_Click);
			// 
			// btnSortEmployees
			// 
			this.btnSortEmployees.Location = new System.Drawing.Point(392, 537);
			this.btnSortEmployees.Name = "btnSortEmployees";
			this.btnSortEmployees.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.btnSortEmployees.Size = new System.Drawing.Size(311, 45);
			this.btnSortEmployees.StateCommon.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
			this.btnSortEmployees.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopRight;
			this.btnSortEmployees.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSortEmployees.TabIndex = 42;
			this.btnSortEmployees.Values.Image = global::SPCRUD.Properties.Resources.emotion_happy_fill;
			this.btnSortEmployees.Values.Text = "Employees With Healh Insurance";
			this.btnSortEmployees.Click += new System.EventHandler(this.btnSortEmployees_Click);
			// 
			// btnDeleteAllRecords
			// 
			this.btnDeleteAllRecords.Location = new System.Drawing.Point(942, 537);
			this.btnDeleteAllRecords.Name = "btnDeleteAllRecords";
			this.btnDeleteAllRecords.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.btnDeleteAllRecords.Size = new System.Drawing.Size(217, 45);
			this.btnDeleteAllRecords.StateCommon.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
			this.btnDeleteAllRecords.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopRight;
			this.btnDeleteAllRecords.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDeleteAllRecords.TabIndex = 43;
			this.btnDeleteAllRecords.Values.Image = global::SPCRUD.Properties.Resources.delete_bin_2_line;
			this.btnDeleteAllRecords.Values.Text = "Delete All Employees";
			this.btnDeleteAllRecords.Click += new System.EventHandler(this.btnDeleteAllRecords_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1190, 603);
			this.Controls.Add(this.btnDeleteAllRecords);
			this.Controls.Add(this.btnSortEmployees);
			this.Controls.Add(this.btnDisplayAllEmployees);
			this.Controls.Add(this.kryptonLabel11);
			this.Controls.Add(this.kryptonLabel8);
			this.Controls.Add(this.txtEmpInsuranceMonthlyFee);
			this.Controls.Add(this.kryptonLabel9);
			this.Controls.Add(this.txtEmpInsurancePlanName);
			this.Controls.Add(this.kryptonLabel10);
			this.Controls.Add(this.txtEmpHealthInsuranceProvider);
			this.Controls.Add(this.dtpInsuranceStartDate);
			this.Controls.Add(this.kryptonLabel7);
			this.Controls.Add(this.kryptonLabel5);
			this.Controls.Add(this.cboEmpGender);
			this.Controls.Add(this.kryptonLabel4);
			this.Controls.Add(this.kryptonLabel3);
			this.Controls.Add(this.txtEmpDept);
			this.Controls.Add(this.kryptonLabel2);
			this.Controls.Add(this.txtEmpCity);
			this.Controls.Add(this.kryptonLabel1);
			this.Controls.Add(this.txtEmpName);
			this.Controls.Add(this.dgvEmpDetails);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.btnSave);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stored Procedures CRUD";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvEmpDetails)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboEmpGender)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnRefresh;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnDelete;
		private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvEmpDetails;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmpName;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmpCity;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmpDept;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
		private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboEmpGender;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
		private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker dtpInsuranceStartDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn EmpId;
		private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
		private System.Windows.Forms.DataGridViewTextBoxColumn City;
		private System.Windows.Forms.DataGridViewTextBoxColumn Department;
		private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmpInsuranceMonthlyFee;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmpInsurancePlanName;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel10;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmpHealthInsuranceProvider;
		private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel11;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnDisplayAllEmployees;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnSortEmployees;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnDeleteAllRecords;
	}
}


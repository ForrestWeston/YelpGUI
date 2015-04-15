namespace YelpGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AttributeList = new System.Windows.Forms.CheckedListBox();
            this.SubCategoryList = new System.Windows.Forms.CheckedListBox();
            this.MainCategoryList = new System.Windows.Forms.CheckedListBox();
            this.BusinessGridView = new System.Windows.Forms.DataGridView();
            this.DayOfWeekCombo = new System.Windows.Forms.ComboBox();
            this.FromCombo = new System.Windows.Forms.ComboBox();
            this.ToCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SearchForCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BusinessGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.3125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.6875F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 224F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 806F));
            this.tableLayoutPanel1.Controls.Add(this.AttributeList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SubCategoryList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.MainCategoryList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BusinessGridView, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 43);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1414, 658);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // AttributeList
            // 
            this.AttributeList.FormattingEnabled = true;
            this.AttributeList.Location = new System.Drawing.Point(387, 3);
            this.AttributeList.Name = "AttributeList";
            this.AttributeList.Size = new System.Drawing.Size(212, 649);
            this.AttributeList.TabIndex = 7;
            this.AttributeList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.AttributeList_ItemCheck);
            // 
            // SubCategoryList
            // 
            this.SubCategoryList.FormattingEnabled = true;
            this.SubCategoryList.Location = new System.Drawing.Point(177, 3);
            this.SubCategoryList.Name = "SubCategoryList";
            this.SubCategoryList.Size = new System.Drawing.Size(204, 649);
            this.SubCategoryList.TabIndex = 6;
            this.SubCategoryList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SubCategoryList_ItemCheck);
            // 
            // MainCategoryList
            // 
            this.MainCategoryList.FormattingEnabled = true;
            this.MainCategoryList.Location = new System.Drawing.Point(3, 3);
            this.MainCategoryList.Name = "MainCategoryList";
            this.MainCategoryList.Size = new System.Drawing.Size(168, 649);
            this.MainCategoryList.TabIndex = 5;
            this.MainCategoryList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MainCategoryList_ItemCheck);
            // 
            // BusinessGridView
            // 
            this.BusinessGridView.AllowUserToAddRows = false;
            this.BusinessGridView.AllowUserToDeleteRows = false;
            this.BusinessGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BusinessGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BusinessGridView.Location = new System.Drawing.Point(611, 3);
            this.BusinessGridView.Name = "BusinessGridView";
            this.BusinessGridView.ReadOnly = true;
            this.BusinessGridView.Size = new System.Drawing.Size(800, 652);
            this.BusinessGridView.TabIndex = 8;
            // 
            // DayOfWeekCombo
            // 
            this.DayOfWeekCombo.FormattingEnabled = true;
            this.DayOfWeekCombo.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.DayOfWeekCombo.Location = new System.Drawing.Point(15, 728);
            this.DayOfWeekCombo.Name = "DayOfWeekCombo";
            this.DayOfWeekCombo.Size = new System.Drawing.Size(168, 21);
            this.DayOfWeekCombo.TabIndex = 2;
            this.DayOfWeekCombo.Text = "(Select Day)";
            // 
            // FromCombo
            // 
            this.FromCombo.FormattingEnabled = true;
            this.FromCombo.Items.AddRange(new object[] {
            "12:00AM",
            "1:00AM",
            "2:00AM",
            "3:00AM",
            "4:00AM",
            "5:00AM",
            "6:00AM",
            "7:00AM",
            "8:00AM",
            "9:00AM",
            "10:00AM",
            "11:00AM",
            "12:00PM",
            "1:00PM",
            "2:00PM",
            "3:00PM",
            "4:00PM",
            "5:00PM",
            "6:00PM",
            "7:00PM",
            "8:00PM",
            "9:00PM",
            "10:00PM",
            "11:00PM"});
            this.FromCombo.Location = new System.Drawing.Point(189, 728);
            this.FromCombo.Name = "FromCombo";
            this.FromCombo.Size = new System.Drawing.Size(98, 21);
            this.FromCombo.TabIndex = 3;
            this.FromCombo.Text = "(Open Time)";
            // 
            // ToCombo
            // 
            this.ToCombo.FormattingEnabled = true;
            this.ToCombo.Items.AddRange(new object[] {
            "12:00AM",
            "1:00AM",
            "2:00AM",
            "3:00AM",
            "4:00AM",
            "5:00AM",
            "6:00AM",
            "7:00AM",
            "8:00AM",
            "9:00AM",
            "10:00AM",
            "11:00AM",
            "12:00PM",
            "1:00PM",
            "2:00PM",
            "3:00PM",
            "4:00PM",
            "5:00PM",
            "6:00PM",
            "7:00PM",
            "8:00PM",
            "9:00PM",
            "10:00PM",
            "11:00PM"});
            this.ToCombo.Location = new System.Drawing.Point(295, 728);
            this.ToCombo.Name = "ToCombo";
            this.ToCombo.Size = new System.Drawing.Size(98, 21);
            this.ToCombo.TabIndex = 4;
            this.ToCombo.Text = "(Close Time)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 702);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Day of Week";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 702);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 702);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "To";
            // 
            // SearchForCombo
            // 
            this.SearchForCombo.FormattingEnabled = true;
            this.SearchForCombo.Items.AddRange(new object[] {
            "ALL",
            "ANY"});
            this.SearchForCombo.Location = new System.Drawing.Point(399, 728);
            this.SearchForCombo.Name = "SearchForCombo";
            this.SearchForCombo.Size = new System.Drawing.Size(212, 21);
            this.SearchForCombo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 707);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Search For";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(623, 706);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1438, 761);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SearchForCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToCombo);
            this.Controls.Add(this.FromCombo);
            this.Controls.Add(this.DayOfWeekCombo);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BusinessGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView BusinessGridView;
        private System.Windows.Forms.CheckedListBox AttributeList;
        private System.Windows.Forms.CheckedListBox SubCategoryList;
        private System.Windows.Forms.CheckedListBox MainCategoryList;
        private System.Windows.Forms.ComboBox DayOfWeekCombo;
        private System.Windows.Forms.ComboBox FromCombo;
        private System.Windows.Forms.ComboBox ToCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox SearchForCombo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
    }
}


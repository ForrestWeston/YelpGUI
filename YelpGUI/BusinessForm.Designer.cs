namespace YelpGUI
{
    partial class BusinessForm
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
            this.BusinessReviewGridView = new System.Windows.Forms.DataGridView();
            this.CloseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BusinessReviewGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BusinessReviewGridView
            // 
            this.BusinessReviewGridView.AllowUserToAddRows = false;
            this.BusinessReviewGridView.AllowUserToDeleteRows = false;
            this.BusinessReviewGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.BusinessReviewGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BusinessReviewGridView.Location = new System.Drawing.Point(12, 12);
            this.BusinessReviewGridView.Name = "BusinessReviewGridView";
            this.BusinessReviewGridView.ReadOnly = true;
            this.BusinessReviewGridView.Size = new System.Drawing.Size(882, 452);
            this.BusinessReviewGridView.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(704, 470);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(190, 48);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BusinessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 530);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BusinessReviewGridView);
            this.Name = "BusinessForm";
            this.Text = "BusinessForm";
            ((System.ComponentModel.ISupportInitialize)(this.BusinessReviewGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView BusinessReviewGridView;
        private System.Windows.Forms.Button CloseButton;
    }
}
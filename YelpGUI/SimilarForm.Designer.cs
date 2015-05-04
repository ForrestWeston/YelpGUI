namespace YelpGUI
{
    partial class SimilarForm
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
            this.SimilarGridView = new System.Windows.Forms.DataGridView();
            this.ReviewButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SimilarGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SimilarGridView
            // 
            this.SimilarGridView.AllowUserToAddRows = false;
            this.SimilarGridView.AllowUserToDeleteRows = false;
            this.SimilarGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SimilarGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SimilarGridView.Location = new System.Drawing.Point(13, 13);
            this.SimilarGridView.Name = "SimilarGridView";
            this.SimilarGridView.ReadOnly = true;
            this.SimilarGridView.Size = new System.Drawing.Size(837, 468);
            this.SimilarGridView.TabIndex = 0;
            // 
            // ReviewButton
            // 
            this.ReviewButton.Location = new System.Drawing.Point(774, 491);
            this.ReviewButton.Name = "ReviewButton";
            this.ReviewButton.Size = new System.Drawing.Size(75, 23);
            this.ReviewButton.TabIndex = 1;
            this.ReviewButton.Text = "Reviews";
            this.ReviewButton.UseVisualStyleBackColor = true;
            this.ReviewButton.Click += new System.EventHandler(this.ReviewButton_Click);
            // 
            // SimilarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 526);
            this.Controls.Add(this.ReviewButton);
            this.Controls.Add(this.SimilarGridView);
            this.Name = "SimilarForm";
            this.Text = "SimilarForm";
            ((System.ComponentModel.ISupportInitialize)(this.SimilarGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView SimilarGridView;
        private System.Windows.Forms.Button ReviewButton;
    }
}
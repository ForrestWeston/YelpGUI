using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YelpGUI
{
    public partial class BusinessForm : Form
    {
        MySqlConnector _mydb;
        public BusinessForm()
        {
            InitializeComponent();
            _mydb = new MySqlConnector();
        }

        public void BusinessForm_PopulateGridView(string businessId, object sender, EventArgs e)
        {
            BusinessReviewGridView.ClearSelection();
            string qStr = "SELECT R.review_date, R.stars, R.review_text, U.name, R.votes_useful " +
                          "FROM review R JOIN user U ON R.user_id = U.ID " +
                          "WHERE R.business_id = '" + businessId + "'";
            BusinessReviewGridView.DataSource = _mydb.FillTable(qStr);


        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

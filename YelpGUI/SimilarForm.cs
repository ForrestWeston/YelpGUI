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
    public partial class SimilarForm : Form
    {
        MySqlConnector _mydb;
        public SimilarForm()
        {
            InitializeComponent();
            _mydb = new MySqlConnector();
        }

        public void SimilarForm_PopulateGridView(string subCatId, string attId, int subCount, int attCount, object sender, EventArgs e)
        {
            SimilarGridView.ClearSelection();
            string qStr = "SELECT distinct name, city, state, stars, ID FROM BusinessCatAtt " +
                          "WHERE category_id IN ( " + subCatId + " ) AND attribute_id IN (" + attId + ") " +
                          "GROUP BY ID HAVING COUNT(category_id) >= " + subCount + " AND COUNT(attribute_id) >= " + attCount + ";";
            SimilarGridView.DataSource = _mydb.FillTable(qStr);
            
            SimilarGridView.Columns[4].Visible = false;
        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {
            if (SimilarGridView.CurrentRow == null)
                return;
            string selectedBusinessID = SimilarGridView[SimilarGridView.CurrentCell.ColumnIndex + 4, SimilarGridView.CurrentCell.RowIndex].Value.ToString();
            BusinessForm BusForm = new BusinessForm();
            BusForm.BusinessForm_PopulateGridView(selectedBusinessID, sender, e);
            BusForm.Show();

        }
    }
}

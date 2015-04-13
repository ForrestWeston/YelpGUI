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
    public partial class Form1 : Form
    {
        MySqlConnector _mydb;
        List<String> MainCategoryCheckedItems = new List<String>();
        List<String> SubCategoryCheckedItems = new List<String>();
        
        public Form1()
        {
            InitializeComponent();
            _mydb = new MySqlConnector();

            string qStr = "SELECT DISTINCT cat_name FROM category WHERE is_main = True ORDER BY cat_name";
            List<String> qResult = _mydb.SQLSELECTExec(qStr, "cat_name");

            //copy query to Listbox cList
            for (int i = 0; i < qResult.Count(); i++)
            {
                MainCategoryList.Items.Add(qResult[i]);
            }
        }

        private void MainCategoryList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(e.NewValue == CheckState.Checked)
            {
                var checkedItem = MainCategoryList.SelectedItem;
                MainCategoryCheckedItems.Add(checkedItem.ToString());
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                var checkedItem = MainCategoryList.SelectedItem;
                MainCategoryCheckedItems.Remove(checkedItem.ToString());
            }
            SubCategoryList_Update(sender, e);
        }

        private void SubCategoryList_Update(object sender, ItemCheckEventArgs e)
        {
            SubCategoryList.Items.Clear();
            string qStr = "SELECT DISTINCT cat_name FROM category WHERE is_main = False ORDER BY cat_name";
            List<String> qResult = _mydb.SQLSELECTExec(qStr, "cat_name");
            //copy query to Listbox cList
            for (int i = 0; i < qResult.Count(); i++)
            {
                SubCategoryList.Items.Add(qResult[i]);
            }
        }



    
    }
}

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
        List<String> AttributeCheckedItems = new List<String>();

        Dictionary<string, int> CategoryLookUp = new Dictionary<string, int>();
        Dictionary<string, int> AttributeLookUp = new Dictionary<string, int>();

        String MainCategoryQuerryIDs;
        String SubCategoryQuerryIDs;
        String AttributeQuerryIDs;



        /*TODO:
         * Create a function to map Category name to the correct id in the DB        DONE
         * Create a function for atribute that does the same                         DONE
         *  *   * this can be acomplished buy querying the db and building the set from that, might want to make a stored procedure for that...
         * For selecting multipule IDs use 'WHERE table.ID IN (list, of, id's, here)
         *  *   * Clean that ugly code up in itatalize it will cause problems later because of lack of organization 
         *  ISSUE: when you deselect an item in the main category it clears all of the checks from the subcategory list
        */

        public Form1()
        {
            InitializeComponent();
            _mydb = new MySqlConnector();

            InitDropDownLists();
            BuildHashCategory();
            BuildAttributeHash();
            PopulateMainCategory();
            SearchButton.Enabled = false;

        }

        #region Form Events

        private void MainCategoryList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                var checkedItem = MainCategoryList.SelectedItem;
                MainCategoryCheckedItems.Add(checkedItem.ToString());
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                var checkedItem = MainCategoryList.SelectedItem;
                MainCategoryCheckedItems.Remove(checkedItem.ToString());
            }

            //if there is nothing selected there is no reason to call this
            if (MainCategoryCheckedItems.Count > 0)
                SubCategoryList_Update();
            //we need to clear the subcategory 
            else
            {
                SubCategoryList.Items.Clear();
                AttributeList.Items.Clear();
                BusinessGridView.DataSource = null;
            }
        }

        private void SubCategoryList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                var checkedItem = SubCategoryList.SelectedItem;
                SubCategoryCheckedItems.Add(checkedItem.ToString());
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                var checkedItem = SubCategoryList.SelectedItem;
                SubCategoryCheckedItems.Remove(checkedItem.ToString());
            }

            //if there is nothing selected there is no reason to call this
            if (SubCategoryCheckedItems.Count > 0)
                AttributeList_Update();

            //we need to clear the subcategory 
            else
            {
                AttributeList.Items.Clear();
                BusinessGridView.ClearSelection();

            }
        }

        private void AttributeList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                var checkedItem = AttributeList.SelectedItem;
                AttributeCheckedItems.Add(checkedItem.ToString());
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                var checkedItem = AttributeList.SelectedItem;
                AttributeCheckedItems.Remove(checkedItem.ToString());
            }

            if (AttributeCheckedItems.Count > 0)
            {
                SearchButton.Enabled = true;
                BusinessDataGridView_Update();
            }
            else
            {
                BusinessGridView.ClearSelection();
                SearchButton.Enabled = false;
                return;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            BusinessDataGridView_Refine();
        }

        private void BusinessGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string selectedBusinessID = BusinessGridView[BusinessGridView.CurrentCell.ColumnIndex + 4, BusinessGridView.CurrentCell.RowIndex].Value.ToString();
            BusinessForm BusForm = new BusinessForm();
            BusForm.BusinessForm_PopulateGridView(selectedBusinessID, sender, e);
            BusForm.Show();
            
          
        }

        #endregion

        #region Form Updates

        private void SubCategoryList_Update()
        {
            SubCategoryList.Items.Clear();

            StringBuilder whereClause = new StringBuilder();

            foreach (var item in MainCategoryCheckedItems)
            {
                int itemID = 0;
                CategoryLookUp.TryGetValue(item.ToString(), out itemID);
                whereClause.Append(itemID.ToString() + ", ");
            }
            whereClause.Remove(whereClause.Length - 2, 2);
            MainCategoryQuerryIDs = whereClause.ToString();

            string qStr = @"SELECT name
                          FROM category C
                          WHERE C.is_main = False AND C.ID IN
                            (SELECT DISTINCT category_id
                            FROM business_Category BC
                            WHERE BC.business_id IN
                                (SELECT DISTINCT business_id
                                FROM business_Category BC
                                WHERE BC.category_id IN (" + whereClause.ToString() + ")));";

            List<String> qResult = _mydb.SQLSELECTExec(qStr, "name");
            //copy query to Listbox cList
            for (int i = 0; i < qResult.Count(); i++)
            {
                SubCategoryList.Items.Add(qResult[i]);
            }

        }

        private void AttributeList_Update()
        {
            AttributeList.Items.Clear();

            StringBuilder whereClause = new StringBuilder();

            foreach (var item in SubCategoryCheckedItems)
            {
                int itemID = 0;
                CategoryLookUp.TryGetValue(item.ToString(), out itemID);
                whereClause.Append(itemID.ToString() + ", ");
            }
            whereClause.Remove(whereClause.Length - 2, 2);
            SubCategoryQuerryIDs = whereClause.ToString();

            string qStr = @"SELECT name 
                          FROM attribute A 
                          WHERE A.ID IN 
                            (SELECT DISTINCT attribute_id 
                            FROM business_Attribute BA
                            WHERE BA.business_id IN
                                (SELECT DISTINCT business_id 
                                FROM business_Attribute BA 
                                WHERE BA.attribute_id IN (" + whereClause.ToString() + ")));";

            List<String> qResult = _mydb.SQLSELECTExec(qStr, "name");
            //copy query to Listbox cList
            for (int i = 0; i < qResult.Count(); i++)
            {
                AttributeList.Items.Add(qResult[i]);
            }
        }

        private void BusinessDataGridView_Update()
        {
            BusinessGridView.ClearSelection();
            StringBuilder whereClause = new StringBuilder();

            foreach (var item in AttributeCheckedItems)
            {
                int itemID = 0;
                AttributeLookUp.TryGetValue(item.ToString(), out itemID);
                whereClause.Append(itemID.ToString() + ", ");
            }
            whereClause.Remove(whereClause.Length - 2, 2);
            AttributeQuerryIDs = whereClause.ToString();

            string MainCatID = MainCategoryQuerryIDs.ToString();
            string SubCatID = SubCategoryQuerryIDs.ToString();
            string AttID = AttributeQuerryIDs.ToString();
            int CatCount = (MainCategoryCheckedItems.Count() + SubCategoryCheckedItems.Count());
            int AttCount = AttributeCheckedItems.Count();

            string qStr = "SELECT distinct name, city, state, stars, ID FROM BusinessCatAtt " +
                          "WHERE category_id IN ( " + MainCatID + "," + SubCatID + " ) AND attribute_id IN (" + AttID + ") " +
                          "GROUP BY ID HAVING COUNT(category_id) >= " + CatCount + " AND COUNT(attribute_id) >= " + AttCount + ";";


            BusinessGridView.DataSource = _mydb.FillTable(qStr);
            BusinessGridView.Columns[4].Visible = false;


        }

        private void BusinessDataGridView_Refine()
        {
            BusinessGridView.ClearSelection();
            StringBuilder whereClause = new StringBuilder();
            StringBuilder InnerJoin = new StringBuilder();

            foreach (var item in AttributeCheckedItems)
            {
                int itemID = 0;
                AttributeLookUp.TryGetValue(item.ToString(), out itemID);
                whereClause.Append(itemID.ToString() + ", ");
            }
            whereClause.Remove(whereClause.Length - 2, 2);
            AttributeQuerryIDs = whereClause.ToString();

            string MainCatID = MainCategoryQuerryIDs.ToString();
            string SubCatID = SubCategoryQuerryIDs.ToString();
            string AttID = AttributeQuerryIDs.ToString();
            int CatCount = (MainCategoryCheckedItems.Count() + SubCategoryCheckedItems.Count());
            int AttCount = AttributeCheckedItems.Count();

            InnerJoin.Append("INNER JOIN business_Hour ON ID = business_Hour.business_id");

            if (DayOfWeekCombo.Text != string.Empty)
                InnerJoin.Append(" AND business_Hour.day_of_week ='" + DayOfWeekCombo.Text + "'");
            if (FromCombo.Text != string.Empty)
                InnerJoin.Append(" AND business_Hour.open_time >='" + FromCombo.Text + "'");
            if (ToCombo.Text != string.Empty)
                InnerJoin.Append(" AND business_Hour.close_time <='" + ToCombo.Text + "'");
            if (SearchForCombo.Text == "ANY")
                AttCount = 0;

            string qStr = "SELECT distinct name, city, state, stars, ID FROM BusinessCatAtt " +
                           InnerJoin.ToString() + " " +
                          "WHERE category_id IN ( " + MainCatID + "," + SubCatID + " ) AND attribute_id IN (" + AttID + ") " +
                          "GROUP BY ID HAVING COUNT(category_id) >= " + CatCount + " AND COUNT(attribute_id) >= " + AttCount + ";";

            BusinessGridView.DataSource = _mydb.FillTable(qStr);
            BusinessGridView.Columns[4].Visible = false;
        }

        #endregion

        #region Helpers


        private void BuildHashCategory()
        {
            string qStr = "SELECT name FROM category;";
            List<String> qResult = _mydb.SQLSELECTExec(qStr, "name");
            for (int i = 0; i < qResult.Count(); i++)
                CategoryLookUp.Add(qResult[i], (i + 1));//1 + i becasue sql keys start counting at one not zero

        }

        private void BuildAttributeHash()
        {
            string qStr = "SELECT name FROM attribute;";
            List<String> qResult = _mydb.SQLSELECTExec(qStr, "name");
            for (int i = 0; i < qResult.Count(); i++)
                AttributeLookUp.Add(qResult[i], (i + 1));

        }

        private void InitDropDownLists()
        {
            DayOfWeekCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            FromCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            ToCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            SearchForCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void PopulateMainCategory()
        {
            string qStr = "SELECT DISTINCT name FROM category WHERE is_main = True ORDER BY name";
            List<String> qResult = _mydb.SQLSELECTExec(qStr, "name");

            //copy query to Listbox cList
            for (int i = 0; i < qResult.Count(); i++)
            {
                MainCategoryList.Items.Add(qResult[i]);
            }
        }

        #endregion








    }
}

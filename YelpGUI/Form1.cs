using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Maps;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System.Windows.Controls.Primitives;


namespace YelpGUI
{
    public partial class Form1 : Form
    {
        MySqlConnector _mydb;
        MapContainer mapCon = new MapContainer();
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
            ReviewsButton.Enabled = false;
            SimilarButton.Enabled = false;

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
                ReviewsButton.Enabled = true;
                SimilarButton.Enabled = true;
                BusinessDataGridView_Update();
            }
            else
            {
                BusinessGridView.ClearSelection();
                ReviewsButton.Enabled = false;
                SimilarButton.Enabled = false;
                SearchButton.Enabled = false;
                return;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            BusinessDataGridView_Refine();
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

            if (DayOfWeekCombo.Text != string.Empty || FromCombo.Text != string.Empty || ToCombo.Text != string.Empty)
                InnerJoin.Append("INNER JOIN business_Hour ON ID = business_Hour.business_id");

            if (DayOfWeekCombo.Text != string.Empty)
                InnerJoin.Append(" AND business_Hour.day_of_week ='" + DayOfWeekCombo.Text + "'");
            if (FromCombo.Text != string.Empty)
                InnerJoin.Append(" AND business_Hour.open_time >='" + FromCombo.Text + "'");
            if (ToCombo.Text != string.Empty)
                InnerJoin.Append(" AND business_Hour.close_time <='" + ToCombo.Text + "'");
            if (SearchForCombo.Text == "ANY")
                AttCount = 1;

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

        private void ReviewsButton_Click(object sender, EventArgs e)
        {
            string selectedBusinessID = BusinessGridView[BusinessGridView.CurrentCell.ColumnIndex + 4, BusinessGridView.CurrentCell.RowIndex].Value.ToString();
            BusinessForm BusForm = new BusinessForm();
            BusForm.BusinessForm_PopulateGridView(selectedBusinessID, sender, e);
            BusForm.Show();
        }

        private void SimilarButton_Click(object sender, EventArgs e)
        {
            
            //string selectedBusinessID = BusinessGridView[BusinessGridView.CurrentCell.ColumnIndex + 4, BusinessGridView.CurrentCell.RowIndex].Value.ToString();
            SimilarForm SimForm = new SimilarForm();
            SimForm.SimilarForm_PopulateGridView(SubCategoryQuerryIDs, AttributeQuerryIDs, SubCategoryCheckedItems.Count(),  AttributeCheckedItems.Count(), sender, e);
            SimForm.Show();
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            string selectedBusinessID = BusinessGridView[BusinessGridView.CurrentCell.ColumnIndex + 4, BusinessGridView.CurrentCell.RowIndex].Value.ToString();
            string qStr = "SELECT latitude, longitude, name, stars FROM Business WHERE ID = '" + selectedBusinessID + "';";
            List<String> qResultLatitude = _mydb.SQLSELECTExec(qStr, "latitude");
            List<String> qResultLongitude = _mydb.SQLSELECTExec(qStr, "longitude");
            List<String> qResultName = _mydb.SQLSELECTExec(qStr, "name");
            List<String> qResultStars = _mydb.SQLSELECTExec(qStr, "stars");
            double lat = Convert.ToDouble(qResultLatitude[0].ToString());
            double lon = Convert.ToDouble(qResultLongitude[0].ToString());
            mapCon.Show();
            mapCon.addPin(lat, lon, qResultName[0].ToString(), qResultStars[0].ToString(), selectedBusinessID);
            
        }

        private void FindNearBy(object sender, EventArgs e)
        {
            string selectedBusinessID = BusinessGridView[BusinessGridView.CurrentCell.ColumnIndex + 4, BusinessGridView.CurrentCell.RowIndex].Value.ToString();
            string qStr = "SELECT latitude, longitude, name, stars FROM Business WHERE ID = '" + selectedBusinessID + "';";
            List<String> qResultLatitude = _mydb.SQLSELECTExec(qStr, "latitude");
            List<String> qResultLongitude = _mydb.SQLSELECTExec(qStr, "longitude");
            List<String> qResultName = _mydb.SQLSELECTExec(qStr, "name");
            List<String> qResultStars = _mydb.SQLSELECTExec(qStr, "stars");
            double lat = Convert.ToDouble(qResultLatitude[0].ToString());
            double lon = Convert.ToDouble(qResultLongitude[0].ToString());
            string locQStr = "SELECT ID, ( 3959 * acos( cos( radians(37) ) * cos( radians( " + lat + " ) ) * " +
                             "cos( radians( "+lon+" ) - radians(-122) ) + sin( radians(37) ) * " +
                             "sin( radians( " + lat + " ) ) ) ) AS distance FROM Business HAVING " +
                             "distance < 5000 ORDER BY distance LIMIT 0 , 20;";
            List<String> qNearByResult = _mydb.SQLSELECTExec(locQStr, "ID");
            foreach (var business in qNearByResult)
            {
                string query = "SELECT latitude, longitude, name, stars FROM Business WHERE ID = '" + business.ToString() + "';";
                List<String> qLatitude = _mydb.SQLSELECTExec(query, "latitude");
                List<String> qLongitude = _mydb.SQLSELECTExec(query, "longitude");
                List<String> qtName = _mydb.SQLSELECTExec(query, "name");
                List<String> qStars = _mydb.SQLSELECTExec(query, "stars");
                double latitude = Convert.ToDouble(qLatitude[0].ToString());
                double longitude = Convert.ToDouble(qLongitude[0].ToString());
                mapCon.addPin(latitude, longitude, qtName[0].ToString(), qStars[0].ToString(), business.ToString());
            }
            mapCon.Show();
        }








    }
}

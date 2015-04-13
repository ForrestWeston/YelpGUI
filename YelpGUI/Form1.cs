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
        public Form1()
        {
            InitializeComponent();
            _mydb = new MySqlConnector();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string qStr = "SELECT DISTINCT cat_name FROM category ORDER BY cat_name";
            List<String> qResult = _mydb.SQLSELECTExec(qStr, "cat_name");

            //copy query to Listbox cList
            for (int i = 0; i < qResult.Count(); i++)
            {
                cList.Items.Add(qResult[i]);
            }
        }
    }
}

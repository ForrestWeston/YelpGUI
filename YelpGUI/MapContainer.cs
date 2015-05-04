using Microsoft.Maps.MapControl.WPF;
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
    public partial class MapContainer : Form
    {
        public MapContainer()
        {
            InitializeComponent();
            MyMapUserControl.Map.CredentialsProvider = new ApplicationIdCredentialsProvider("Ap29TI6ZXw42NFfZczK2D9Wxx0DZeFmT2QeDASKj-xY0lariAngGUsSdokjMLXUP");
            
        }

        public void addPin(double lat, double lon, string name, string stars, string Business_id)
        {
            Pushpin myPin = new Pushpin();
            myPin.Location = new Location(lat, lon);
            myPin.Content = stars;
            myPin.ToolTip = name;
            myPin.Tag = Business_id;
            myPin.MouseLeftButtonDown += myPin_MouseDoubleClick;
            this.MyMapUserControl.Map.Children.Add(myPin);
        }

        void myPin_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BusinessForm reviewForm = new BusinessForm();
            var selectedPin = (Pushpin)sender;
            reviewForm.BusinessForm_PopulateGridView(selectedPin.Tag.ToString(), sender, e);
            reviewForm.Show();

        }
    }
}

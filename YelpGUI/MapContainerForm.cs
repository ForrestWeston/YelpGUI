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
using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.WPF.Design;

namespace YelpGUI
{
    public partial class MapContainerForm : Form
    {
        MapControl myMap;
        public MapContainerForm()
        {
            InitializeComponent();
            myMap = new MapControl();
            myMap.Map.CredentialsProvider = new ApplicationIdCredentialsProvider("Ap29TI6ZXw42NFfZczK2D9Wxx0DZeFmT2QeDASKj-xY0lariAngGUsSdokjMLXUP");
           
        }

        public void addPins(double lat, double lon)
        {
            Pushpin myPin = new Pushpin();
            myPin.Location = new Location(lat, lon);
            myMap.Map.Children.Add(myPin);

        }



        
    }
}

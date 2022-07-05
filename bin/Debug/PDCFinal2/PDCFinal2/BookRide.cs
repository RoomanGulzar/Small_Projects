using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDCFinal2
{
    public partial class BookRide : Form
    {
        public BookRide()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.ShowCenter = false;
        }
        public BookRide(string name, string from , string to , string rider,int dist)
        {

            InitializeComponent();
            GMapProviders.GoogleMap.ApiKey = "AIzaSyC6W6EONqh_N_n1pHcyJ9yHR1ZAzHJA0oU";
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.Zoom = 18;
            tbName.Text = name;
            tbFrom.Text = from;
            tbTo.Text = to;
            tbRider.Text = rider;
            tbCost.Text = $"{dist * 10}";
            gMapControl1.MapProvider = GMapProviders.GoogleMap;






            double flat = Convert.ToDouble(from.Split(',')[0]);
            double flong = Convert.ToDouble(from.Split(',')[1]);
            double tlat = Convert.ToDouble(to.Split(',')[0]);
            double tlong = Convert.ToDouble(to.Split(',')[1]);
             
            gMapControl1.Position = new PointLatLng(flat, flong);

            string overlayIDfrom = name;
            string overlayIDto = rider;
            gMapControl1.MinZoom = 4;
            gMapControl1.MaxZoom = 24;


            PointLatLng pointsfrom = new PointLatLng(flat, flong);
            PointLatLng pointsto = new PointLatLng(tlat, tlong);
            GMapMarker markerfrom;
            GMapMarker markerto;

            markerfrom = new GMarkerGoogle(pointsfrom, GMarkerGoogleType.red_dot);
            markerto = new GMarkerGoogle(pointsto, GMarkerGoogleType.green_pushpin);


            GMapOverlay Overlaymarkersfrom = new GMapOverlay($"{overlayIDfrom}");
            GMapOverlay Overlaymarkersto = new GMapOverlay($"{overlayIDto}");

            Overlaymarkersfrom.Markers.Add(markerfrom);
            Overlaymarkersto.Markers.Add(markerto);




            PointLatLng start = new PointLatLng(
                             flat, flong
                             );
            PointLatLng end = new PointLatLng(
              tlat, tlong
                );


            var route = GoogleMapProvider.Instance.GetRoute(start, end, true, false, 18);
            var r = new GMapRoute(route.Points, "My Route")
            {
                Stroke = new Pen(Color.Red, 5)
            };
            var routes = new GMapOverlay("routes");
            routes.Routes.Add(r);
            gMapControl1.Overlays.Add(routes);
            // cover map with Overlay


            


            GMapOverlay ToRemovefrom = gMapControl1.Overlays.Where(x => x.Id == overlayIDfrom).FirstOrDefault();
            GMapOverlay ToRemoveto = gMapControl1.Overlays.Where(x => x.Id == overlayIDto).FirstOrDefault();
            if (ToRemovefrom != null)
                gMapControl1.Overlays.Remove(ToRemovefrom);
            if (ToRemoveto != null)
                gMapControl1.Overlays.Remove(ToRemoveto);
            gMapControl1.Overlays.Add(Overlaymarkersfrom);
            gMapControl1.Overlays.Add(Overlaymarkersto);






            gMapControl1.ShowCenter = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Have a Good ride");
        }
    }
}

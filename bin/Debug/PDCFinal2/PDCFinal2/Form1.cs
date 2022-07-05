using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using GMap;
using GMap.NET;
using System.IO;
using GMap.NET.WindowsForms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Net;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;

namespace PDCFinal2
{
    public partial class Form1 : Form
    {
        static double zoomlevel = 0;

        List<PointLatLng> _points;
        public TcpClient c;
        public BinaryFormatter bf;
        public static string lats = null;
        public static string longs = null;


        static string constr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PdcFinal;Data Source=DESKTOP-B7GKLQR";
        SqlConnection con = new SqlConnection(constr);

        public Form1()
        {

            bf = new BinaryFormatter();
            _points = new List<PointLatLng>();


            InitializeComponent();
            GMapProviders.GoogleMap.ApiKey = "AIzaSyC6W6EONqh_N_n1pHcyJ9yHR1ZAzHJA0oU";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbIpAdd.Text != string.Empty && tbName.Text != string.Empty)
                {
                    //tbName.Text = LocationDetails.Name;


                    label3.Hide();
                    tbIpAdd.Hide();
                    label2.Hide();
                    btnConnect.Hide();
                    System.Drawing.Size s = new Size(30, 30);
                    gMapControl1.MinimumSize = s;
                    gMapControl1.Zoom += 12;

                    RideDetails objRD = new RideDetails();
                    if (objRD.ShowDialog() == DialogResult.OK)
                    {
                        c = new TcpClient();
                        c.Connect(IPAddress.Parse(tbIpAdd.Text), 9265);
                        // MessageBox.Show("connected");
                        // bf.Serialize(c.GetStream(), tbName.Text);
                        bf.Serialize(c.GetStream(), tbName.Text + ":" + tblocation.Text + ":" + LocationDetails.cate);
                        LocationDetails.riderFlag = true;
                        tbName.Text = LocationDetails.Name;
                        if (c.Connected)
                        {
                            new Thread(() => Read()).Start();
                        }
                        // bf.Serialize(c.GetStream(), $"{LocationDetails.cate}");
                    }
                }



                // panel1.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*  private void Write()
          {
              bf = new BinaryFormatter();
              bf.Serialize(c.GetStream(), tblocation.Text);


          }*/
        private void Read()
        {
            try
            {
                while (true)
                {

                    string msg = (string)bf.Deserialize(c.GetStream());
                    if (!msg.Equals(","))
                    {
                        string[] NameLocCate = msg.Split(':');
                        //tblocation.Text = latlong[1];
                        int c = dataGridView1.Rows.Count - 2;
                        Boolean isExist = true;//g g datagrid me exist kerta hy ye 
                        while (c >= 0 && isExist)
                        {
                            //check if it is in dgv
                            if (dataGridView1.Rows[c].Cells[0].Value.ToString().Equals(NameLocCate[0]))
                            {
                                dataGridView1.Rows[c].Cells[1].Value = NameLocCate[1];
                                isExist = false; // lo g is ka kam ho gya update
                            }
                            c--;
                        }
                        if (isExist && RiderWatchCustomerOrViceVersa(NameLocCate[2]))
                            dataGridView1.Rows.Add(NameLocCate[0], NameLocCate[1], NameLocCate[2]); //wo datagrid me exist nai kerta tha ... is lia add ker dia


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private bool RiderWatchCustomerOrViceVersa(string cmsg)
        {
            //////              rider.Equals(customer)          return true;
            ///              customer.Equals(rider)             return true;
            if (!LocationDetails.cate.Equals(cmsg))
            {
                return true;
            }
            return false;
        }
        private void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (tblocation.Text.Contains("Loading..."))
                {
                    //tblocation.BackColor = Color.Red;
                }

                else
                {
                    gMapControl1.DragButton = MouseButtons.Left;

                    double lat = Convert.ToDouble(tblocation.Text.Split(',')[0]);
                    double longt = Convert.ToDouble(tblocation.Text.Split(',')[1]);
                    gMapControl1.Position = new GMap.NET.PointLatLng(lat, longt);


                    gMapControl1.MinZoom = 4;
                    gMapControl1.MaxZoom = 24;


                    PointLatLng points = new PointLatLng(lat, longt);
                    GMapMarker marker;

                    marker = new GMarkerGoogle(points, GMarkerGoogleType.red_dot);

                    GMapOverlay markers = new GMapOverlay(tbName.Text);
                    GMapOverlay ToRemove = gMapControl1.Overlays.Where(x => x.Id == tbName.Text).FirstOrDefault();
                    if (ToRemove != null)
                        gMapControl1.Overlays.Remove(ToRemove);

                    markers.Markers.Add(marker);

                    // cover map with Overlay
                    gMapControl1.Overlays.Add(markers);
                    gMapControl1.Zoom = 18;
                    if (c != null)
                        bf.Serialize(c.GetStream(), $"{tbName.Text} :{tblocation.Text}: {LocationDetails.cate}");

                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            try
            {

                gMapControl1.MapProvider = GMapProviders.GoogleMap;
                gMapControl1.ShowCenter = false;

                /*CLocation obj = new CLocation();
                obj.GetLocationEvent();*/
                CheckForIllegalCrossThreadCalls = false;



                new Thread(() =>
                {
                    while (true)
                    {
                        CLocation obj1 = new CLocation();
                        obj1.GetLocationEvent();
                        while (lats == null && longs == null)
                        {
                            //wait until location comes
                        }
                        Thread.Sleep(2000);
                        string loc = $"{ lats },{ longs}";
                        LocationDetails.myLat = Convert.ToDouble(lats);
                        LocationDetails.myLong = Convert.ToDouble(longs);


                        //  MessageBox.Show($"In Form_load Thread latlong :{msg}");
                        tblocation.Enabled = true;
                        btnLocation.Enabled = true;
                        if (!loc.Equals(tblocation.Text))
                        {
                            btnLocation.PerformClick();
                            tblocation.Text = loc;
                        }
                        // bf.Serialize(c.GetStream(), tblocation.Text);

                        // new Thread(() => Read()).Start();
                        Thread.Sleep(3000);
                    }

                }).Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                zoomlevel -= 2;
                gMapControl1.Zoom = zoomlevel;
                MessageBox.Show($"ZoomLevel={zoomlevel} where gmapZoom is {gMapControl1.Zoom.ToString()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                //zoomlevel += 2;
                MessageBox.Show($"ZoomLevel={zoomlevel} where gmapZoom is {gMapControl1.Zoom + 1}");
                gMapControl1.Zoom += 2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            try
            {
                //zoomlevel += 2;
                gMapControl1.Zoom -= 1;
                // MessageBox.Show($"ZoomLevel={zoomlevel} where gmapZoom is {gMapControl1.Zoom.ToString()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    double lat = Convert.ToDouble(dataGridView1.SelectedCells[0].Value.ToString().Split(',')[0]);
                    double longt = Convert.ToDouble(dataGridView1.SelectedCells[0].Value.ToString().Split(',')[1]);
                    gMapControl1.Position = new PointLatLng(lat, longt);
                    string overlayID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    gMapControl1.MinZoom = 4;
                    gMapControl1.MaxZoom = 24;


                    PointLatLng points = new PointLatLng(lat, longt);
                    GMapMarker marker;

                    marker = new GMarkerGoogle(points, GMarkerGoogleType.green_pushpin);


                    GMapOverlay Overlaymarkers = new GMapOverlay($"{overlayID}");

                    Overlaymarkers.Markers.Add(marker);




                    PointLatLng start = new PointLatLng(
                                     lat, longt
                                     );
                    PointLatLng end = new PointLatLng(
                        Convert.ToDouble(tblocation.Text.Split(',')[0]),
                        Convert.ToDouble(tblocation.Text.Split(',')[1])
                        );
                    

                    try
                    {
                        var route = GoogleMapProvider.Instance.GetRoute(start, end, true, false, 18);
                        var r = new GMapRoute(route.Points, "My Route")
                        {
                            Stroke = new Pen(Color.Red, 5)
                        };
                        var routes = new GMapOverlay("routes");
                        routes.Routes.Add(r);
                        gMapControl1.Overlays.Add(routes);
                        // cover map with Overlay


                        lblDistance.Text = route.Distance + "km";



                        GMapOverlay ToRemove = gMapControl1.Overlays.Where(x => x.Id == overlayID).FirstOrDefault();
                        if (ToRemove != null)
                            gMapControl1.Overlays.Remove(ToRemove);
                        gMapControl1.Overlays.Add(Overlaymarkers);
                        if (LocationDetails.cate.Equals("customer") && MessageBox.Show($"Do You Want to have ride ?\ntotal Distance {route.Distance} km\nFare {route.Distance * 10} ", "get Ride", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {

                            BookRide obj = new BookRide(tbName.Text, tblocation.Text, $"{lat},{longt}", overlayID, Convert.ToInt32(lblDistance.Text.Split('k')[0]));
                            obj.ShowDialog();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Application.Exit();

            else if (e.CloseReason == CloseReason.WindowsShutDown)
                Application.Exit();

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if(cbCategory.SelectedIndex == 1 && !LocationDetails.riderFlag)
            {
                RideDetails obj = new RideDetails();
                if(obj.ShowDialog() == DialogResult.OK)
                {
                    LocationDetails.riderFlag = true;
                    cbCategory.Enabled = false;
                    bf.Serialize(c.GetStream(), "Rider");
                }
                
            }*/
        }

        private void gMapControl1_OnPositionChanged(PointLatLng point)
        {
            /*// MessageBox.Show($"Difference between previous and new :{point.Lat - Convert.ToDouble(tblocation.Text.Split(',')[0])}");
             tblocation.Text = $"{point.Lat},{point.Lng}";
             GMapMarker marker;

             marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);

             GMapOverlay markers = new GMapOverlay(tbName.Text);
             GMapOverlay ToRemove = gMapControl1.Overlays.Where(x => x.Id == tbName.Text).FirstOrDefault();
             if (ToRemove != null)
                 gMapControl1.Overlays.Remove(ToRemove);

             markers.Markers.Add(marker);

             // cover map with Overlay
             gMapControl1.Overlays.Add(markers);
             gMapControl1.Zoom = 18;
             if (c != null)
                 bf.Serialize(c.GetStream(), $"{tbName.Text} :{tblocation.Text}: {LocationDetails.cate}");
 */
        }
    }




    class CLocation
    {
        GeoCoordinateWatcher watcher;

        public void GetLocationEvent()
        {
            this.watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            bool started = this.watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            if (!started)
            {
                MessageBox.Show("GeoCoordinateWatcher timed out on start.");
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {

            lock (this)
            {
                Form1.lats = e.Position.Location.Latitude.ToString();
                Form1.longs = e.Position.Location.Longitude.ToString();
                //string msg = Form1.lats + "," + Form1.longs;
                //MessageBox.Show(msg);


                Monitor.PulseAll(this);
            }
        }
    }
}

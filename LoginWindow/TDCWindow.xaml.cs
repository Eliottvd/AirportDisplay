using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

using ClassLibrary;
namespace AirportWindows
{
    /// <summary>
    /// Interaction logic for TDCWindow.xaml
    /// </summary>
    public partial class TDCWindow : Window
    {
        private OCvol<VolProgramme> _listVolProg;
        private OCvol<VolProgramme> _listVolDG;
        private TimeSpan _mtn;
        private Timer _timer1;
        private FlightAndAirportManager _fnaManager;


        public TDCWindow(FlightAndAirportManager fnaM)
        {
            OCvol<VolProgramme> ListVolTmp;
            FnaManager = fnaM;
            InitializeComponent();
            ListVolProg = new OCvol<VolProgramme>();
            ListVolTmp = new OCvol<VolProgramme>();
            ListVolDG = new OCvol<VolProgramme>();
            Mtn = new TimeSpan(0, 0, 0);
            ListVolTmp.Load(FnaManager.GetVolProgSavingPath());
            foreach(VolProgramme vProg in ListVolTmp)
            {
                if (vProg.VolGen.AeroportDepart.CodeAeroport == FnaManager.Code)
                {
                    ListVolProg.Add(vProg);
                }
            }
            dataGridSimulation.DataContext = ListVolDG;
        }

        public OCvol<VolProgramme> ListVolProg { get => _listVolProg; set => _listVolProg = value; }
        public TimeSpan Mtn { get => _mtn; set => _mtn = value; }
        public Timer Timer1 { get => _timer1; set => _timer1 = value; }
        public FlightAndAirportManager FnaManager { get => _fnaManager; set => _fnaManager = value; }
        public OCvol<VolProgramme> ListVolDG { get => _listVolDG; set => _listVolDG = value; }

        private void ButtonMenuStart_Click(object sender, RoutedEventArgs e)
        {
            DebutSimulWindow dsWin = new DebutSimulWindow();
            dsWin.EventDebut += MWEventDebut;
            dsWin.ShowDialog();
            lblAfficheDate.Content = Mtn.ToString();

            Timer1 = new Timer
            {
                Interval = 200
            };
            Timer1.Tick += Running;
            Timer1.Start();
            DPTimeChoice.Visibility = Visibility.Visible;
        }

        private void MWEventDebut(DateTime date)
        {
            Mtn = date.TimeOfDay;
            foreach(VolProgramme vProg in ListVolProg)
            {
                if(date.ToShortDateString() == vProg.DateDepart.ToShortDateString())
                {
                    ListVolDG.Add(vProg);
                }
            }
        }

        private void Running(object sender, EventArgs e)
        {
            Mtn = Mtn.Add(new TimeSpan(0,1,0));
            foreach (VolProgramme vProg in ListVolProg)
            {
                if ((vProg.Statut = vProg.CalculStatut(Mtn)) == "FAR AWAY")
                    ListVolDG.Remove(vProg);
                
            }
            lblAfficheDate.Content = Mtn.ToString();
        }

        private void SliderTimeChoice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(Timer1 != null)
            Timer1.Interval = 1000 / (Convert.ToInt32(SliderTimeChoice.Value));
        }
    }
}

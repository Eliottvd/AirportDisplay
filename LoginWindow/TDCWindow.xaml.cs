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
using ClassLibrary;
namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for TDCWindow.xaml
    /// </summary>
    public partial class TDCWindow : Window
    {
        private OCvol<VolProgramme> _listVolProg;
        private TimeSpan _mtn;

        public TDCWindow(FlightAndAirportManager fnaManager)
        {
            OCvol<VolProgramme> ListVolTmp;
            InitializeComponent();
            ListVolProg = new OCvol<VolProgramme>();
            ListVolTmp = new OCvol<VolProgramme>();
            Mtn = new TimeSpan(0, 0, 0);
            
            ListVolTmp.Load(fnaManager.getVolProgSavingPath());
            foreach(VolProgramme vProg in ListVolTmp)
            {
                if (vProg.VolGen.AeroportDepart.CodeAeroport == fnaManager.Code)
                    ListVolProg.Add(vProg);
            }
            dataGridSimulation.DataContext = ListVolProg;

        }

        public OCvol<VolProgramme> ListVolProg { get => _listVolProg; set => _listVolProg = value; }
        public TimeSpan Mtn { get => _mtn; set => _mtn = value; }
    }
}

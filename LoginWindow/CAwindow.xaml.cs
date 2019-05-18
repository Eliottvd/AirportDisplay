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
using System.Windows.Media.Effects;
using System.Drawing;

using ClassLibrary;
namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for CAwindow.xaml
    /// </summary>
    public partial class CAwindow : Window
    {
        #region variables
        private CompagnieAerienne _ca;
        private OCvol<VolGenerique> _listVolsGeneriques;
        private FlightAndAirportManager _fnaManager;
        private string _workSpace;
        #endregion

        #region constructeur
        public CAwindow(FlightAndAirportManager f)
        {
            InitializeComponent();
            fnaManager = f;
            //WorkSpace = fnaManager.Workspace;
            CA = new CompagnieAerienne("SN", "Brussels Airlines", "Belgique" ,"SN.png");
            CA.Load(fnaManager.DosFiles+"\\"+fnaManager.Code+".xml");
            lblNomCompagnie.Content = CA.FullName;
            lblLocalisationC.Content = CA.Localisation;
            //imgCA.Source = CA.LogoPath + "\\" + CA.Code + ".png"; 

            listVolsGeneriques = new OCvol<VolGenerique>();

            listVolsGeneriques.Add(new VolGenerique("1", "OLE Oleye", "DBI Dubai", new TimeSpan(), new TimeSpan()));
            DataGridVolsGeneriques.DataContext = listVolsGeneriques;

        }
        #endregion

        #region proprietes
        public CompagnieAerienne CA
        {
            set { _ca = value; }
            get { return _ca; }
        }

        public FlightAndAirportManager fnaManager
        {
            set { _fnaManager = value; }
            get { return _fnaManager; }
        }

        public OCvol<VolGenerique> listVolsGeneriques
        {
            set { _listVolsGeneriques = value; }
            get { return _listVolsGeneriques; }
        }

        public string workSpace
        {
            set { _workSpace = value; }
            get { return _workSpace; }
        }

        #endregion

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            newVolgenWindow addWin = new newVolgenWindow();
            addWin.Valider += ModVolGen;
            this.Effect = new BlurEffect();
            addWin.ShowDialog();
            this.Effect = null;
        }

        public void ModVolGen(bool ajout, VolGenerique vol)
        {
            listVolsGeneriques.Add(vol);
            listVolsGeneriques.Sort();
        }
    }
}

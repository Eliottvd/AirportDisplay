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
using System.IO;


using ClassLibrary;
namespace AirportWindows
{
    /// <summary>
    /// Interaction logic for CAwindow.xaml
    /// </summary>
    public partial class CAwindow : Window
    {
        #region variables
        private CompagnieAerienne _ca;
        private OCvol<VolGenerique> _listVolsGeneriques;
        private OCvol<VolProgramme> _listVolsProgrammes;
        private FlightAndAirportManager _fnaManager;
        #endregion

        #region constructeur
        public CAwindow(FlightAndAirportManager f)
        {
            InitializeComponent();
            FnaManager = f;
            CA = new CompagnieAerienne();
            CA.Load(FnaManager.GetCASavingPath());
            lblNomCompagnie.Content = CA.FullName;
            lblLocalisationC.Content = CA.Localisation;
            imgCA.DataContext = CA;
            //imgCA.Source = "D:\\OneDrive - Enseignement de la Province de Liège\\Ecole\\BLOC2\\Q2\\C#\\Labo\\Phase3\\Phase3\\LoginWindow\\data\\SN.png"; 

            ListVolsGeneriques = new OCvol<VolGenerique>();
            ListVolsProgrammes = new OCvol<VolProgramme>();
            try
            {
                ListVolsGeneriques.Load(FnaManager.GetVolGenSavingPath());
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("Pas de vols génériques trouvés", "Bienvenue");
            }
            try
            {
                ListVolsProgrammes.Load(FnaManager.GetVolProgSavingPath());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Pas de vols programmés trouvés", "Bienvenue");
            }
            DataGridVolsGeneriques.DataContext = ListVolsGeneriques;
            DataGridVolsProgrammes.DataContext = ListVolsProgrammes;

        }
        #endregion

        #region proprietes
        public CompagnieAerienne CA
        {
            set { _ca = value; }
            get { return _ca; }
        }

        public FlightAndAirportManager FnaManager
        {
            set { _fnaManager = value; }
            get { return _fnaManager; }
        }

        public OCvol<VolGenerique> ListVolsGeneriques
        {
            set { _listVolsGeneriques = value; }
            get { return _listVolsGeneriques; }
        }

        public OCvol<VolProgramme> ListVolsProgrammes { get => _listVolsProgrammes; set => _listVolsProgrammes = value; }


        #endregion

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            NewVolgenWindow addWin = new NewVolgenWindow(FnaManager.Code);
            addWin.Valider += ModVolGen;
            this.Effect = new BlurEffect();
            addWin.ShowDialog();
            this.Effect = null;
        }

        public void ModVolGen(bool ajout, VolGenerique vol)
        {
            ListVolsGeneriques.Add(vol);
            ListVolsGeneriques.Sort();
        }

        private void ButtonMenuSave_Click(object sender, RoutedEventArgs e)
        {
            ListVolsGeneriques.Save(FnaManager.GetVolGenSavingPath());
            MessageBox.Show("Vos vols ont bien été sauvegardés", "Opération réussie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonMenuLoad_Click(object sender, RoutedEventArgs e)
        {
            ListVolsGeneriques.Load(FnaManager.GetVolGenSavingPath());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Voulez-vous sauvez les vols génériques ?",
                "Fermeture de l'application", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if(boxResult == MessageBoxResult.Yes)
            {
                ListVolsGeneriques.Save(FnaManager.GetVolGenSavingPath());
            }
            else if(boxResult == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            ListVolsProgrammes.Save(FnaManager.GetVolProgSavingPath());
        }

        private void DataGridVolsGeneriques_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back)
            {
                ListVolsGeneriques.Remove((VolGenerique)DataGridVolsGeneriques.SelectedItem);
            }
        }

        private void ButtonVolGenToVolProg_Click(object sender, RoutedEventArgs e)
        {
            if (dtPckr.SelectedDate == null) 
                MessageBox.Show("Veuillez entrer un date", "Information manquante", MessageBoxButton.OK, MessageBoxImage.Information);
            else if(DataGridVolsGeneriques.SelectedItem == null)
                MessageBox.Show("Veuillez choisir un ou plusieurs vols génériques", "Information manquante"
                    , MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                foreach(VolGenerique vGen in DataGridVolsGeneriques.SelectedItems)
                {
                    DateTime date = (DateTime)dtPckr.SelectedDate;
                    DateTime datedep = new DateTime(date.Ticks + vGen.HeureDepart.Ticks);
                    DateTime datearr = new DateTime(date.Ticks + vGen.HeureArrivee.Ticks);
                    if (vGen.NextDay)
                        datearr = datearr.AddDays(1);
                        
                    ListVolsProgrammes.Add(new VolProgramme(vGen, datedep, datearr, 100));
                }
                ListVolsProgrammes.Sort();
            }

        }
    }
}

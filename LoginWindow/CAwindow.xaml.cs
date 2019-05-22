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
        private OCvol<VolProgramme> _listVolsProgrammes;
        private FlightAndAirportManager _fnaManager;
        #endregion

        #region constructeur
        public CAwindow(FlightAndAirportManager f)
        {
            InitializeComponent();
            fnaManager = f;
            CA = new CompagnieAerienne();
            //CA.Save(fnaManager.DosFiles + "\\" + fnaManager.Code + ".xml");
            CA.Load(fnaManager.getCASavingPath());
            lblNomCompagnie.Content = CA.FullName;
            lblLocalisationC.Content = CA.Localisation;
            //imgCA.Source = CA.LogoPath + "\\" + CA.Code + ".png"; 

            listVolsGeneriques = new OCvol<VolGenerique>();
            listVolsProgrammes = new OCvol<VolProgramme>();
            try
            {
                listVolsGeneriques.Load(fnaManager.getVolGenSavingPath());
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("Pas de vols génériques trouvés", "Bienvenue");
            }
            try
            {
                listVolsProgrammes.Load(fnaManager.getVolProgSavingPath());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Pas de vols programmés trouvés", "Bienvenue");
            }
            DataGridVolsGeneriques.DataContext = listVolsGeneriques;
            DataGridVolsProgrammes.DataContext = listVolsProgrammes;

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

        public OCvol<VolProgramme> listVolsProgrammes { get => _listVolsProgrammes; set => _listVolsProgrammes = value; }


        #endregion

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            newVolgenWindow addWin = new newVolgenWindow(fnaManager.Code);
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

        private void ButtonMenuSave_Click(object sender, RoutedEventArgs e)
        {
            listVolsGeneriques.Save(fnaManager.getVolGenSavingPath());
            MessageBox.Show("Vos vols ont bien été sauvegardés", "Opération réussie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonMenuLoad_Click(object sender, RoutedEventArgs e)
        {
            listVolsGeneriques.Load(fnaManager.getVolGenSavingPath());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Voulez-vous sauvez les vols génériques ?",
                "Fermeture de l'application", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if(boxResult == MessageBoxResult.Yes)
            {
                listVolsGeneriques.Save(fnaManager.getVolGenSavingPath());
            }
            else if(boxResult == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            listVolsProgrammes.Save(fnaManager.getVolProgSavingPath());
        }

        private void DataGridVolsGeneriques_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back)
            {
                listVolsGeneriques.Remove((VolGenerique)DataGridVolsGeneriques.SelectedItem);
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
                        
                    listVolsProgrammes.Add(new VolProgramme(vGen, datedep, datearr, 100));
                }
                listVolsProgrammes.Sort();
            }

        }
    }
}

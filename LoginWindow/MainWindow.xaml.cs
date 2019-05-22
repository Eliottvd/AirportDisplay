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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ClassLibrary;

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightAndAirportManager _fnaManager;
        private string _login, _mdp, _code;
        private string _imgpath; 
        public MainWindow()
        {
            InitializeComponent();
            fnaManager = new FlightAndAirportManager();
            _imgpath = null;
        }

        private void ButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            fnaManager.Login = txtLogin.Text;
            fnaManager.Pass = txtMdp.Password;
            fnaManager.Code = txtCode.Text;
            if (!fnaManager.connexion())
                MessageBox.Show("Mauvais identifiants/Mot de passe", "Erreur de login", MessageBoxButton.OK, MessageBoxImage.Hand);
            else
            {
                lancerWindow();
            }

        }

        private void ButtonNouveauUser_Click(object sender, RoutedEventArgs e)
        {
            gridConnexion.Visibility = Visibility.Hidden;
            gridCreation.Visibility = Visibility.Visible;
            this.Height = 275;
        }

        private void ButtonRetourLogin_Click(object sender, RoutedEventArgs e)
        {
            gridCreation.Visibility = Visibility.Hidden;
            gridConnexion.Visibility = Visibility.Visible;
            this.Height = 245;
        }

        private void ButtonCreation_Click(object sender, RoutedEventArgs e)
        {
            fnaManager.Login = txtLoginCreation.Text;
            fnaManager.Pass = txtMdpCreation.Password;
            fnaManager.Code = txtCodeCreation.Text;
            if(!txtMdpCreation.Password.Equals(txtMdpConfirmation.Password))
            {
                MessageBox.Show("Les 2 mot de passes ne correspondent pas", "Erreur de création", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (txtLoginCreation.Text == "" || txtMdpCreation.Password == "" || txtCodeCreation.Text == "")
            {
                MessageBox.Show("Veuillez compléter tous les champs", "Erreur de création", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if ((!fnaManager.creation()))
            {
                MessageBox.Show("Login déjà existant", "Erreur de création", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if(fnaManager.Code.Length == 2)
            {
                if (fnaManager.isCompanyCreated())
                {
                    MessageBox.Show("Login crée !");
                }
                else
                {
                    MessageBox.Show("La compagnie n'existe pas encore, veuillez encoder ses données", "Nouvelle compagnie", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    gridCreation.Visibility = Visibility.Hidden;
                    gridCompagnie.Visibility = Visibility.Visible;
                    this.Height = 245;
                }
            }
            else
            {
                lancerWindow();
            }
        }

        private void ButtonValiderCA_Click(object sender, RoutedEventArgs e)
        {
            if(txtLocalisation.Text == "" || txtNomComplet.Text == "" || Imgpath == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur de création", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                new CompagnieAerienne(fnaManager.Code, txtNomComplet.Text, txtLocalisation.Text, Imgpath).Save(fnaManager.getCASavingPath());
                lancerWindow();
            }
        }

        private void lancerWindow()
        {
            if(fnaManager.Code.Length == 2)
            {
                CAwindow caWin = new CAwindow(fnaManager);
                caWin.Show();
            }
            else
            {
                TDCWindow tdcWin = new TDCWindow(fnaManager);
                tdcWin.Show();
            }
            
            this.Close();
        }

        private void MenuOption_Click(object sender, RoutedEventArgs e)
        {
            if(gridConnexion.Visibility != Visibility.Visible)
            {
                MessageBox.Show("Cette fonctionnalitée n'est disponible que depuis la fenêtre de connexion", "Accès refusé", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }    
            else if(txtLogin.Text != "admin" ||txtMdp.Password != "admin")
            {
                MessageBox.Show("Veuillez entrer les identifiants spéciaux pour accéder à la fenêtre d'option", "Accès refusé"
                    , MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                OptionWindow optWin = new OptionWindow(fnaManager);
                optWin.Show();
            }
            
        }

        public FlightAndAirportManager fnaManager
        {
            set { _fnaManager = value; }
            get { return _fnaManager; }
        }

        public string login
        {
            set { _login = value; }
            get { return _login; }
        }


        private void TxtPath_GotFocus(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            Imgpath = dialog.SelectedPath;
            txtPath.Text = Imgpath;
        }

        public string mdp   
        {
            set { _mdp = value; }
            get { return _mdp; }
        }

        private void TxtCode_LostFocus(object sender, RoutedEventArgs e)
        {
            txtCode.Text = txtCode.Text.ToUpper();
        }

        private void TxtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) //si on appuie sur enter
            {
                ButtonConnexion_Click(sender, new RoutedEventArgs());
            }
        }

        private void TxtCodeCreation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) //si on appuie sur enter
            {
                ButtonCreation_Click(sender, new RoutedEventArgs());
            }
        }

        private void TxtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) //si on appuie sur enter
            {
                ButtonConnexion_Click(sender, new RoutedEventArgs());
            }
        }

        public string code
        {
            set { _code = value; }
            get { return _code; }
        }

        public string Imgpath { get => _imgpath; set => _imgpath = value; }
    }
}

﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            fnaManager = new FlightAndAirportManager();

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
                CAwindow caWin = new CAwindow(fnaManager);
                caWin.Show();
                this.Close();
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
            this.Height = 225;
        }

        private void ButtonCreation_Click(object sender, RoutedEventArgs e)
        {
            fnaManager.Login = txtLogin.Text;
            fnaManager.Pass = txtMdp.Password;
            fnaManager.Code = txtCode.Text;
            if (txtLogin.Text == "" || txtMdp.Password == "" || txtCode.Text == "")
            {
                MessageBox.Show("Veuillez compléter tous les champs", "Erreur de création", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if ((!fnaManager.creation()))
            {
                MessageBox.Show("Login déjà existant", "Erreur de création", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                CompagnieAerienne ca = new CompagnieAerienne(fnaManager.Code, txtNomComplet.Text, txtLocalisation.Text, "d:\\");

            }
        }

        private void MenuOption_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ceci est la fenêtre option");
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        public string mdp   
        {
            set { _mdp = value; }
            get { return _mdp; }
        }

        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
    }
}
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

namespace LoginWindow
{
    /// <summary>
    /// Interaction logic for debutSimulWindow.xaml
    /// </summary>
    public partial class DebutSimulWindow : Window
    {
        public delegate void dateDebut(DateTime dateDebut);
        public event dateDebut EventDebut;
        public DebutSimulWindow()
        {
            InitializeComponent();
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            if (datePckr.Value == null)
                MessageBox.Show("Veuillez entrer une date", "Erreur de lancement", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                EventDebut?.Invoke((DateTime)datePckr.Value);
                Close();
            }
        }
    }
}

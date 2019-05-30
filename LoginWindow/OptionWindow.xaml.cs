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

namespace AirportWindows
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : Window
    {
        #region variables
        private FlightAndAirportManager _fnaManager;
        private string _imgPath;
        private string _dataPath;
        #endregion

        #region constructeur
        public OptionWindow(FlightAndAirportManager f)
        {
            InitializeComponent();
            FnaManager = f;
            txtData.Text = FnaManager.DosFiles;
            txtLogo.Text = FnaManager.DosImg;
            ImgPath = FnaManager.DosImg;
            DataPath = FnaManager.DosFiles;
        }
        #endregion

        #region propriétés
        public FlightAndAirportManager FnaManager { get => _fnaManager; set => _fnaManager = value; }
        public string ImgPath { get => _imgPath; set => _imgPath = value; }
        public string DataPath { get => _dataPath; set => _dataPath = value; }

        private void TxtData_GotFocus(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                SelectedPath = DataPath
            };
            dialog.ShowDialog();
            DataPath = dialog.SelectedPath;
            txtData.Text = DataPath;
        }

        private void TxtLogo_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                SelectedPath = ImgPath
            };
            dialog.ShowDialog();
            ImgPath = dialog.SelectedPath;
            txtLogo.Text = ImgPath;
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            FnaManager.DosImg = ImgPath;
            FnaManager.DosFiles = DataPath;
            FnaManager.SaveRegistryParameters();
            this.Close();
        }
        #endregion

        private void TxtLogo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) //si on appuie sur enter
            {
                Valider_Click(sender, new RoutedEventArgs());
            }
        }
    }
}

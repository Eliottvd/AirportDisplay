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
            fnaManager = f;
            txtData.Text = fnaManager.DosFiles;
            txtLogo.Text = fnaManager.DosImg;
        }
        #endregion

        #region propriétés
        public FlightAndAirportManager fnaManager { get => _fnaManager; set => _fnaManager = value; }
        public string ImgPath { get => _imgPath; set => _imgPath = value; }
        public string DataPath { get => _dataPath; set => _dataPath = value; }

        private void TxtData_GotFocus(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            DataPath = dialog.SelectedPath;
            txtData.Text = DataPath;
        }

        private void TxtLogo_GotFocus(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            ImgPath = dialog.SelectedPath;
            txtLogo.Text = ImgPath;
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            fnaManager.DosImg = ImgPath;
            fnaManager.DosFiles = DataPath;
            fnaManager.SaveRegistryParameters();
            this.Close();
        }
        #endregion
    }
}

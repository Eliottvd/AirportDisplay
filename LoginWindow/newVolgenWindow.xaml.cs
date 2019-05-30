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
using System.Collections.ObjectModel;
using ClassLibrary;

namespace AirportWindows
{
    /// <summary>
    /// Interaction logic for newVolgenWindow.xaml
    /// </summary>
    public partial class NewVolgenWindow : Window
    {
        public NewVolgenWindow(string codeCA)
        {
            int i, j;
            InitializeComponent();
            ObservableCollection<Aeroport> listAeroport = new ObservableCollection<Aeroport>
            {
                Aeroport.GetAerBRU(),
                Aeroport.GetAerJFK(),
                Aeroport.GetAerLIS()
            };
            cbAerArr.DataContext = listAeroport;
            cbAerDep.DataContext = listAeroport;
            for (i = 0, j = 0; i < 24; i++, j += 5) 
            {
                cbHeureDep.Items.Add(i + "H");
                if (i < 12)
                    cbMinDep.Items.Add(j);
            }
            tbNumvol.Text = codeCA + "xxx";
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            int i, j, x = 0, y;
            bool nextDay = false;
            i = Convert.ToInt32(cbHeureDep.SelectedIndex);
            j = Convert.ToInt32(cbMinDep.SelectedIndex);
            j *= 5;
            TimeSpan hDep = new TimeSpan(i, j, 0);
            TimeSpan hArr;
            int duree = Convert.ToInt32(tbDuree.Text);
            y = duree;
            while (duree > 59)
            {
                x++;
                y = duree % 60;
                duree -= 60;
            }
            if((i+x)>23)
            {
                nextDay = true;
                hArr = new TimeSpan(i + x - 24, j + y, 0);
            }
            else
                hArr = new TimeSpan(i + x, j + y, 0);

            
            VolGenerique newVol = new VolGenerique(tbNumvol.Text, (Aeroport)cbAerDep.SelectedItem, (Aeroport)cbAerArr.SelectedItem, hDep, hArr, nextDay);
            Valider(true, newVol);
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public delegate void ModVolGen(bool ajout, VolGenerique vol);
        public event ModVolGen Valider;
    }
}

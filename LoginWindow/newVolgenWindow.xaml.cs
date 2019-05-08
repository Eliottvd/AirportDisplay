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
    /// Interaction logic for newVolgenWindow.xaml
    /// </summary>
    public partial class newVolgenWindow : Window
    {
        public newVolgenWindow()
        {
            int i, j;
            InitializeComponent();
            cbAerDep.Items.Add("BRU");
            cbAerDep.Items.Add("JFK");
            cbAerArr.Items.Add("BRU");
            cbAerArr.Items.Add("JFK");
            for (i = 0, j = 0; i < 24; i++, j += 5) 
            {
                cbHeureDep.Items.Add(i + "H");
                if (i < 12)
                    cbMinDep.Items.Add(j);
            }
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            int i, j, x = 0, y;
            i = Convert.ToInt32(cbHeureDep.SelectedIndex);
            j = Convert.ToInt32(cbMinDep.SelectedIndex);
            j *= 5;
            TimeSpan hDep = new TimeSpan(i, j, 0);
            int duree = Convert.ToInt32(tbDuree.Text);
            y = duree;
            while (duree > 59)
            {
                x++;
                y = duree % 60;
                duree -= 60;
            }
            TimeSpan hArr = new TimeSpan(i + x, j + y, 0);

            VolGenerique newVol = new VolGenerique(tbNumvol.Text, cbAerDep.SelectedItem.ToString(), cbAerArr.SelectedItem.ToString(), hDep, hArr);
            Valider(true, newVol);
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public delegate void ModVolGen(bool ajout, VolGenerique vol);
        public event ModVolGen Valider;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace ClassLibrary
{
    public class VolProgramme : IComparable, INotifyPropertyChanged
    {
        #region Variables + accesseurs
        private VolGenerique _volgen;
        private DateTime _datedepart;
        private DateTime _datearrivee;
        private int _nbrplaces;
        private string _statut;

        public VolGenerique VolGen { get => _volgen; set => _volgen = value; }
        public DateTime DateDepart { get => _datedepart; set => _datedepart = value; }
        public DateTime DateArrivee { get => _datearrivee; set => _datearrivee = value; }
        public int NbrPlaces { get => _nbrplaces; set => _nbrplaces = value; }
        public string Statut
        {
            get
            {
                return _statut;
            }
            set
            {
                if(value != _statut)
                {
                    _statut = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region constructeur
        VolProgramme() { }

        public VolProgramme(VolGenerique vg, DateTime dDep, DateTime dArr, int nPlaces)
        {
            VolGen = vg;
            DateDepart = dDep;
            DateArrivee = dArr;
            NbrPlaces = nPlaces;
            Statut = "";
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            VolProgramme vProg = (VolProgramme)obj;
            return DateDepart.CompareTo(vProg.DateDepart);
        }

        public string Heure
        {
            get
            {
                return DateDepart.Hour + ":" + DateDepart.Minute;
            }
        }


        public String CalculStatut(TimeSpan mtn)
        {
            if(VolGen.HeureDepart-mtn < new TimeSpan(0,30,0))
            {
                if (VolGen.HeureDepart - mtn < new TimeSpan(0, 10, 0))
                {
                    if (VolGen.HeureDepart - mtn < new TimeSpan(0, 5, 0))
                    {
                        if ((VolGen.HeureDepart + new TimeSpan(0, 30, 0)) < mtn)
                            return "FAR AWAY";
                        if (VolGen.HeureDepart < mtn)
                            return "AIRBORNE";
                        return "GATE CLOSED";
                    }
                    else
                        return "LAST CALL";
                }
                else
                {
                    return "BOARDING";
                }
            }
            return "SCHEDULED";
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "Statut")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

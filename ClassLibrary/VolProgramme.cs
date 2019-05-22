using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class VolProgramme : IComparable
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


        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            VolProgramme vProg = (VolProgramme)obj;
            return DateDepart.CompareTo(vProg.DateDepart);
        }

        public string heure
        {
            get
            {
                return DateDepart.Hour + ":" + DateDepart.Minute;
            }
        }

        public string Statut { get => Statut1; set => Statut1 = value; }
        public string Statut1 { get => _statut; set => _statut = value; }

        public void majStatut(TimeSpan mtn)
        {
            if(VolGen.HeureDepart-mtn < new TimeSpan(0,30,0))
            {
                if (VolGen.HeureDepart - mtn < new TimeSpan(0, 10, 0))
                {
                    if (VolGen.HeureDepart - mtn < new TimeSpan(0, 5, 0))
                    {
                        if (VolGen.HeureDepart == mtn)
                            Statut = "AIRBORNE";
                        Statut = "GATE CLOSED";
                    }
                    else
                        Statut = "LAST CALL";
                }
                else
                    Statut = "BOARDING";
            }
            Statut = "SCHEDULED";
        }
    }
}

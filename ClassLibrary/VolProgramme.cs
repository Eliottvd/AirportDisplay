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
        }
        #endregion


        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            VolProgramme vProg = (VolProgramme)obj;
            return DateDepart.CompareTo(vProg.DateDepart);
        }
    }
}

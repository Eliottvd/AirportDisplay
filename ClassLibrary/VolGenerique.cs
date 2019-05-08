using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class VolGenerique
    {
        #region variables
        string _numvol;
        string _aeroportdepart;
        string _aeroportarrivee;
        TimeSpan _heuredepart;
        TimeSpan _heurearrivee;
        #endregion

        #region constructeur
        public VolGenerique(string n, string d, string a, TimeSpan hd, TimeSpan ha)
        {
            _numvol = n;
            _aeroportdepart = d;
            _aeroportarrivee = a;
            _heuredepart = hd;
            _heurearrivee = ha;

        }
        #endregion

        #region propriétés
        public string NumVol
        {
            set { _numvol = value; }
            get { return _numvol; }
        }

        public string AeroportDepart
        {
            set { _aeroportdepart = value; }
            get { return _aeroportdepart; }
        }

        public string AeroportArrivee
        {
            set { _aeroportarrivee = value; }
            get { return _aeroportarrivee; }
        }

        public TimeSpan HeureDepart
        {
            set { _heuredepart = value; }
            get { return _heuredepart; }
        }

        public TimeSpan HeureArrivee
        {
            set { _heurearrivee = value; }
            get { return _heurearrivee; }
        }

        #endregion
    }
}

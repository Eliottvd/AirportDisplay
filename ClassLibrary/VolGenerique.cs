using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ClassLibrary
{
    public class VolGenerique : IComparable
    {
        #region variables
        string _numvol;
        Aeroport _aeroportdepart;
        Aeroport _aeroportarrivee;
        TimeSpan _heuredepart;
        TimeSpan _heurearrivee;
        bool _nextDay;
        #endregion

        #region constructeur
        public VolGenerique() {
            _nextDay = false;
        }

        public VolGenerique(string n, Aeroport d, Aeroport a, TimeSpan hd, TimeSpan ha, bool nd)
        {
            _numvol = n;
            _aeroportdepart = d;
            _aeroportarrivee = a;
            _heuredepart = hd;
            _heurearrivee = ha;
            _nextDay = nd;
        }

        #endregion

        #region propriétés
        public string NumVol
        {
            set { _numvol = value; }
            get { return _numvol; }
        }

        public Aeroport AeroportDepart
        {
            set { _aeroportdepart = value; }
            get { return _aeroportdepart; }
        }

        public Aeroport AeroportArrivee
        {
            set { _aeroportarrivee = value; }
            get { return _aeroportarrivee; }
        }
        [XmlIgnore]
        public TimeSpan HeureDepart
        {
            set { _heuredepart = value; }
            get { return _heuredepart; }
        }

        [XmlElement("HeureDepart")]
        public long ticksHdep
        {
            get { return HeureDepart.Ticks; }
            set { HeureDepart = new TimeSpan(value); }
        }

        [XmlIgnore]
        
        public TimeSpan HeureArrivee
        {
            set { _heurearrivee = value; }
            get { return _heurearrivee; }
        }

        [XmlElement("HeureArrivee")]
        public long ticksHarr
        {
            get { return HeureArrivee.Ticks; }
            set { HeureArrivee = new TimeSpan(value); }
        }

        public TimeSpan Duree
        {
            get
            {
                if (NextDay)
                    return HeureArrivee - HeureDepart + new TimeSpan(24, 0, 0);
                return HeureArrivee - HeureDepart;
            }
        }

        public string AffHeureArr
        {
            get
            {
                if (_nextDay)
                    return HeureArrivee.ToString() + " +1";
                return HeureArrivee.ToString();
            }
        }
        public bool NextDay { get => _nextDay; set => _nextDay = value; }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            VolGenerique vGen = (VolGenerique)obj;
            return HeureDepart.CompareTo(vGen.HeureDepart);
        }
        #endregion
    }
}

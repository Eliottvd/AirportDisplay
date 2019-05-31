using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace ClassLibrary
{
    public class VolGenerique : IComparable, INotifyPropertyChanged
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
            set
            {
                if(_numvol != value)
                {
                    _numvol = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _numvol; }
        }

        public Aeroport AeroportDepart
        {
            set
            {
                if (_aeroportdepart != value)
                {
                    _aeroportdepart = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _aeroportdepart; }
        }

        public Aeroport AeroportArrivee
        {
            set
            {
                if (_aeroportarrivee != value)
                {
                    _aeroportarrivee = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _aeroportarrivee; }
        }
        [XmlIgnore]
        public TimeSpan HeureDepart
        {
            set
            {
                if (_heuredepart != value)
                {
                    _heuredepart = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _heuredepart; }
        }

        [XmlElement("HeureDepart")]
        public long TicksHdep
        {
            get { return HeureDepart.Ticks; }
            set { HeureDepart = new TimeSpan(value); }
        }

        [XmlIgnore]
        
        public TimeSpan HeureArrivee
        {
            set
            {
                if (_heurearrivee != value)
                {
                    _heurearrivee = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _heurearrivee; }
        }

        [XmlElement("HeureArrivee")]
        public long TicksHarr
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
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            VolGenerique vGen = (VolGenerique)obj;
            return HeureDepart.CompareTo(vGen.HeureDepart);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ClassLibrary
{
    public class Aeroport 
    {
        #region variables + propriétés correspondantes
        private string _nomaeroport;
        private string _localisation;
        private string _codearoport;

        public string NomAeroport { get => _nomaeroport; set => _nomaeroport = value; }
        public string Localisation { get => _localisation; set => _localisation = value; }
        public string CodeAeroport { get => _codearoport; set => _codearoport = value; }
        #endregion

        #region constructeur
        public Aeroport()
        {

        }

        public Aeroport(string n, string l, string c)
        {
            NomAeroport = n;
            Localisation = l;
            if (c.Length != 3)
            {
                throw new ArgumentException("Le code aéroport doit être composé de 3 caractères");
            }
            else
                CodeAeroport = c;
        }
        #endregion

        public string NomAAfficher
        {
            get{ return CodeAeroport + " " + NomAeroport;}
            
        }
       
        private static readonly Aeroport aerBRU = new Aeroport("Zaventem", "Bruxelles", "BRU");
        private static readonly Aeroport aerJFK = new Aeroport("John F. Kennedy", "New-York", "JFK");
        private static readonly Aeroport aerLIS = new Aeroport("Lisbonne", "Lisbonne", "LIS");

        public static Aeroport GetAerBRU() { return aerBRU; }
        public static Aeroport GetAerJFK() { return aerJFK; }
        public static Aeroport GetAerLIS() { return aerLIS; }

        public void Load(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(Aeroport));
            using (Stream fStream = File.OpenRead(path))
            {
                Aeroport tmp = (Aeroport)xmlFormat.Deserialize(fStream);
                NomAeroport = tmp.NomAeroport;
                Localisation = tmp.Localisation;
                CodeAeroport = tmp.CodeAeroport;
            }
        }

        public void Save(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(Aeroport));
            Console.WriteLine(path);
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this);
            }
        }
    }
}

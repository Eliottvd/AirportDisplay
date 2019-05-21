using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ClassLibrary
{
    public class CompagnieAerienne
    {
        #region variables
        string _code;
        string _fullname;
        string _localisation;
        string _logopath;
        #endregion

        #region constructeur
        public CompagnieAerienne()
        {

        }

        public CompagnieAerienne(string c, string f, string loc, string log)
        {
            _code = c;
            _fullname = f;
            _localisation = loc;
            _logopath = log;
        }
        #endregion

        #region propriétés
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        
        public string FullName
        {
            set { _fullname = value; }
            get { return _fullname; }
        }

        public string Localisation
        {
            set { _localisation = value; }
            get { return _localisation; }
        }

        public string LogoPath
        {
            set { _logopath = value; }
            get { return _logopath; }
        }


        public void Load(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(CompagnieAerienne));
            using (Stream fStream = File.OpenRead(path))
            {
                CompagnieAerienne tmp = (CompagnieAerienne)xmlFormat.Deserialize(fStream);
                FullName = tmp.FullName;
                Localisation = tmp.Localisation;
                LogoPath = tmp.LogoPath;
                Code = tmp.Code;
            }
        }

        public void Save(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(CompagnieAerienne));
            Console.WriteLine(path);
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this);
            }
        }
        #endregion

    }
}

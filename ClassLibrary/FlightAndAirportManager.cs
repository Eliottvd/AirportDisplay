using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ClassLibrary
{
    public class FlightAndAirportManager
    {
        #region variables 
        string _login;
        string _pass;
        string _code;
        string _nomcomplet;
        string _dosimg;
        string _dosfiles;
        #endregion

        #region constructeur 
        public FlightAndAirportManager()
        {
            _login = null;
            _pass = null;
            _code = null;
            _nomcomplet = null;
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software");
            rk = rk.CreateSubKey("HEPL");
            _dosimg = rk.GetValue("imgpath").ToString();
            _dosfiles = rk.GetValue("datapath").ToString();
        }

        #endregion

        #region propriétés 
        public string Login
        {
            set { _login = value; }
            get { return _login; }
        }

        public string Pass
        {
            set { _pass = value; }
            get { return _pass; }
        }

        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        public string NomComplet
        {
            set { _nomcomplet = value; }
            get { return _nomcomplet; }
        }

        public string DosImg
        {
            set { _dosimg = value; }
            get { return _dosimg; }
        }

        public string DosFiles
        {
            set { _dosfiles = value; }
            get { return _dosfiles; }
        }

        void LoadregistryParameters()
        {

        }

        void SaveRegistryParameters(string img, string data)
        {
            img = "C:\\Users\\Eliott\\Pictures";
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software");
            RegistryKey HEPL = rk.CreateSubKey("HEPL");
            RegistryKey image = HEPL;
            //if(image.GetValue("path")==null)
            //{
                image.SetValue("imgpath", img);
            //}
            RegistryKey Data = HEPL;
            Data.SetValue("datapath", Data);

            HEPL.CreateSubKey("CodesCA");
            HEPL.CreateSubKey("CodesAeroports");

        }

        public bool connexion()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software");
            rk = rk.CreateSubKey("HEPL");

            if ( Code == null) //Si on appuie sur connexion directement
                return false;
            switch (Code.Length)
            {
                case 2:
                    rk = rk.CreateSubKey("codeCA");
                    break;
                case 3:
                    rk = rk.CreateSubKey("codeAeroport");
                    break;
                default: return false; //mauvais code
            }

            rk = rk.CreateSubKey(Code);

            if ((string)rk.GetValue(Login) != Pass)
                return false;
            else
                return true;      
        }

        public bool creation()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software");
            rk = rk.CreateSubKey("HEPL");

            switch (Code.Length)
            {
                case 2:
                    rk = rk.CreateSubKey("codeCA");
                    //TO DO : charger donnée compagnie ! 
                    break;
                case 3:
                    rk = rk.CreateSubKey("codeAeroport");
                    //TO DO : charger donnée aeroport ! 
                    break;

            }

            rk = rk.CreateSubKey(Code);

            if ((string)rk.GetValue(Login) != null)
                return false;
            else
            {
                rk.SetValue(Login, Pass);
                return true;
            }
        }
        #endregion
    }
}

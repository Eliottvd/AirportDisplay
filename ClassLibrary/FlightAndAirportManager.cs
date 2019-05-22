using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;

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
            this.LoadregistryParameters();
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

        public void LoadregistryParameters()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software");
            rk = rk.CreateSubKey("HEPL");
            this.DosImg = rk.GetValue("imgpath").ToString();
            this.DosFiles = rk.GetValue("datapath").ToString();
        }

        public void SaveRegistryParameters()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey("Software");
            rk = rk.CreateSubKey("HEPL");
            rk.SetValue("imgpath", this.DosImg);
            rk.SetValue("datapath", this.DosFiles);
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

        public bool isCompanyCreated()
        {
            Stream fStream;
            try
            {
                fStream = File.OpenRead(this.getCASavingPath());
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            return true;
        }

        public string getCASavingPath()
        {
            return this.DosFiles + "\\" + this.Code + ".xml";
        }

        public string getVolGenSavingPath()
        {
            return this.DosFiles + "\\" + this.Code + "VOL.xml";
        }

        public string getVolProgSavingPath()
        {
            return this.DosFiles + "\\" + "volsPrgs.xml";
        }
        #endregion
    }
}

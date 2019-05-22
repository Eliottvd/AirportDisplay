using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class OCvol<T> : ObservableCollection<T>
    {
        public void Save(string s)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            using (Stream fStream = new FileStream(s, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this.ToList());
            }
        }

        public void Load(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            Clear();
            using (Stream fStream = File.OpenRead(path))
            {
                ((List<T>)xmlFormat.Deserialize(fStream)).ForEach(item => Add(item));
            }
        }

        public void Sort()
        {
            var tmp = new List<T>(this);
            tmp.Sort();
            Clear();
            foreach (T vol in tmp)
            {
                Add(vol);
            }
        }
    }
}

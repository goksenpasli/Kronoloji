using Extensions;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Kronoloji
{
    public class ViewModel : InpcBase
    {
        public static readonly string xmldatapath = Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath) + @"\Data.xml";

        public ViewModel()
        {
            Veri = new Veri();
            Veriler = new Veriler
            {
                Veri = VerileriYükle()
            };
            Ekle = new RelayCommand<object>(parameter => {
                Veri veri = new Veri()
                {
                    Açıklama = Veri.Açıklama,
                    Resim = Veri.Resim,
                    Tarih = Veri.Tarih
                };
                Veriler.Veri.Add(veri);
                Serialize(Veriler);
            }, parameter => Veri.Resim!=null && !string.IsNullOrWhiteSpace(Veri.Açıklama));

            ResimEkle = new RelayCommand<object>(parameter => {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Multiselect = false,
                    Filter = "Resim Dosyaları (*.jpg;*.jpeg;*.tif;*.tiff;*.png)|*.jpg;*.jpeg;*.tif;*.tiff;*.png"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(openFileDialog.FileName);
                    File.Copy(openFileDialog.FileName, $"{Path.GetDirectoryName(xmldatapath)}\\{filename}");
                    Veri.Resim = filename;
                }
            }, parameter => true);
        }

        public ICommand Ekle
        {
            get;
        }

        public ICommand ResimEkle
        {
            get;
        }

        public Veri Veri
        {
            get;
            set;
        }

        public Veriler Veriler
        {
            get;
            set;
        }

        public static T DeSerialize<T>(string xmldatapath) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader stream = new StreamReader(xmldatapath))
            {
                return serializer.Deserialize(stream) as T;
            }
        }

        public static void Serialize<T>(T dataToSerialize) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter stream = new StreamWriter(xmldatapath))
            {
                serializer.Serialize(stream, dataToSerialize);
            }
        }

        public static ObservableCollection<Veri> VerileriYükle()
        {
            if (File.Exists(xmldatapath))
            {
                return DeSerialize<Veriler>(xmldatapath).Veri;
            }
            _ = Directory.CreateDirectory(Path.GetDirectoryName(xmldatapath));
            return new ObservableCollection<Veri>();
        }
    }
}
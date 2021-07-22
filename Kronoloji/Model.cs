using Extensions;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Kronoloji
{
    [XmlRoot(ElementName = "Veri")]
    public class Veri : InpcBase
    {
        private string açıklama;
        private DateTime tarih=DateTime.Today;
        private string resim;

        [XmlAttribute(AttributeName = "Açıklama")]
        public string Açıklama
        {
            get { return açıklama; }

            set
            {
                if (açıklama != value)
                {
                    açıklama = value;
                    OnPropertyChanged(nameof(Açıklama));
                }
            }
        }

        [XmlAttribute(AttributeName = "Tarih")]
        public DateTime Tarih
        {
            get { return tarih; }

            set
            {
                if (tarih != value)
                {
                    tarih = value;
                    OnPropertyChanged(nameof(Tarih));
                }
            }
        }

        [XmlAttribute(AttributeName = "Resim")]
        public string Resim
        {
            get { return resim; }

            set
            {
                if (resim != value)
                {
                    resim = value;
                    OnPropertyChanged(nameof(Resim));
                }
            }
        }
    }

    [XmlRoot(ElementName = "Veriler")]
    public class Veriler : InpcBase
    {
        private ObservableCollection<Veri> veri=new ObservableCollection<Veri>();

        [XmlElement(ElementName = "Veri")]
        public ObservableCollection<Veri> Veri
        {
            get { return veri; }

            set
            {
                if (veri != value)
                {
                    veri = value;
                    OnPropertyChanged(nameof(Veri));
                }
            }
        }
    }
}

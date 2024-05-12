using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MusteriTakipWithElasticSearch.Models
{

    public class Musteri: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private int musterino { get; set; }
        private string musteriadi { get; set; } = string.Empty;
        private string musterisoyadi { get; set; } = string.Empty;
        private string musteritel { get; set; } = string.Empty;
        private string musterieposta { get; set; } = string.Empty;

        [Key]
        public int MusteriNo
        {
            get { return musterino; }
            set { musterino = value; OnPropertyChanged("MusteriNo"); }
        }

        [StringLength(50)]
        public string MusteriAdi
        {
            get { return musteriadi; }
            set { musteriadi = value; OnPropertyChanged("MusteriAdi"); }
        }
        [StringLength(50)]
        public string MusteriSoyadi
        {
            get { return musterisoyadi; }
            set { musterisoyadi = value; OnPropertyChanged("MusteriSoyadi"); }
        }

        [StringLength(50)]
        public string MusteriTel
        {
            get { return musteritel; }
            set { musteritel = value; OnPropertyChanged("MusteriTel"); }
        }

        [StringLength(50)]
        public string MusteriEposta
        {
            get { return musterieposta; }
            set { musterieposta = value; OnPropertyChanged("MusteriEposta"); }
        }


    }
}

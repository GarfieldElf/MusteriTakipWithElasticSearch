using MusteriTakipWithElasticSearch.Models;
using MusteriTakipWithElasticSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MusteriTakipWithElasticSearch.Views
{
   /// <summary>
    /// MüşteriGüncelleme.xaml etkileşim mantığı
    /// </summary>
    public partial class MüşteriGüncelleme : Window
    {
        MusteriViewModel mvm;
        int musterino;
        public MüşteriGüncelleme(){}
        public MüşteriGüncelleme(Musteri passedmusteri) : this() 
        {
            InitializeComponent();
            mvm = new MusteriViewModel();

            Musteri getmusteri = passedmusteri;
            musterino = getmusteri.MusteriNo;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            mvm.MusteriGuncelle(TextBox_MusteriAdi.Text, TextBox_MusteriSoyadi.Text, TextBox_MusteriTelefon.Text, TextBox_MusteriEposta.Text,musterino);
            Close();
        }
    }
}

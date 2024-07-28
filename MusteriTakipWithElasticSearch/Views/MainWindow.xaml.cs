using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Data.SqlClient;
using MusteriTakipWithElasticSearch.Models;
using MusteriTakipWithElasticSearch.ViewModels;
using MusteriTakipWithElasticSearch.Views;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using System.Windows;
using MusteriTakipWithElasticSearch.Elastic;


namespace MusteriTakipWithElasticSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MusteriViewModel mvm = new MusteriViewModel();
        ElasticConnection elastic = new ElasticConnection();
   
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MusteriViewModel();

            //-----------------------------------------
            var result = elastic.CreateConnection();
            elastic.ElasticSearchQuery(result);
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }
        private void ResetControls()
        {
            TextBox_MusteriAdi.Text = string.Empty;
            TextBox_MusteriSoyadi.Text = string.Empty;
            TextBox_MusteriTelefon.Text = string.Empty;
            TextBox_MusteriEposta.Text = string.Empty;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Musteri? m = musteridatagrid.SelectedItem as Musteri;
            MüşteriGüncelleme MG = new MüşteriGüncelleme(m) { DataContext = m };
            MG.Show();
        }
        private void Button_Save_File_Click(object sender, RoutedEventArgs e)
        {
            var cikti = this.musteridatagrid.ItemsSource;
            var json = JsonConvert.SerializeObject(cikti, Formatting.Indented);

            string fname = @"C:\Users\pc\Desktop\Dosyalar\MusteriBilgileri.JSON";
            File.WriteAllText(fname, json);
        }
    }
}
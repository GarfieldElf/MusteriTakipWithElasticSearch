using MusteriTakipWithElasticSearch.Models;
using MusteriTakipWithElasticSearch.ViewModels;
using MusteriTakipWithElasticSearch.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusteriTakipWithElasticSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MusteriViewModel mvm = new MusteriViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MusteriViewModel();
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
            Musteri m = musteridatagrid.SelectedItem as Musteri;
            //string madi = m.MusteriAdi;
            //string msoyadi = m.MusteriSoyadi;
            //MessageBox.Show(madi + " " + msoyadi);
            MüşteriGüncelleme MG = new MüşteriGüncelleme(m)
            {
                DataContext = m
            };
            MG.Show();
        }
    }
}
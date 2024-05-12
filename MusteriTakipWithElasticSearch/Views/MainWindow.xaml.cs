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

        public List<Musteri>? Musterilerim { get; set; }
        MusteriViewModel mvm = new MusteriViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MusteriViewModel();

            //using (MusteriDbContext _context = new MusteriDbContext())
            //{
            //    Musterilerim = _context.Musteriler.ToList();
            //}

            //MusteriList.ItemsSource = Musterilerim;

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
            MüşteriGüncelleme MG = new MüşteriGüncelleme { Owner = this, DataContext = this.musteridatagrid.SelectedItem};
            MG.Show();
           
        }
    }
}
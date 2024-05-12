using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MusteriTakipWithElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace MusteriTakipWithElasticSearch.ViewModels
{
    public class MusteriViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Musteri> _musteriler;
        private Musteri _selectedMusteri;
        public string musteriadi { get; set; }
        public string musterisoyadi { get; set; }
        public string musteritel { get; set; }
        public string musterieposta { get; set; }

        public ObservableCollection<Musteri> Musterilerr
        {
            get { return _musteriler; }
            set
            {
                _musteriler = value;
                OnPropertyChanged();
            }
        }

        public Musteri SelectedMusteri
        {
            get { return _selectedMusteri; }
            set
            {
                _selectedMusteri = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }

        public MusteriViewModel()
        {
            Musterilerr = new ObservableCollection<Musteri>();
            MusterileriGetir();
            AddCommand = new RelayCommand((s) => true, MusteriEkle);
            DeleteCommand = new RelayCommand((s) => true, MusteriSil);
            UpdateCommand = new RelayCommand((s) => true, MusteriEditle);
        }

        private void MusteriEditle(object parameter)
        {
                //var employeeToUpdate = parameter as Musteri;
               
                 SelectedMusteri = parameter as Musteri;
        }

        private void MusteriEkle(object parameter)
        {
            using (MusteriDbContext _context = new MusteriDbContext())
            {
                var yeniMusteri = new Musteri
                {
                    MusteriAdi = musteriadi,
                    MusteriSoyadi = musterisoyadi,
                    MusteriTel = musteritel,
                    MusteriEposta = musterieposta
                };

                if (yeniMusteri.MusteriAdi != null && yeniMusteri.MusteriSoyadi != null)
                {
                    _context.Musteriler.Add(yeniMusteri);
                    _context.SaveChanges();

                    Musterilerr.Add(yeniMusteri);
                }

            }
        }
        public void MusterileriGetir()
        {
            using (MusteriDbContext _context = new MusteriDbContext())
            {
                Musterilerr.Clear(); // Clear existing items

                foreach (var employee in _context.Musteriler)
                {
                    Musterilerr.Add(employee);
                }
            }

        }

        private void MusteriSil(object parameter)
        {
            using (MusteriDbContext _context = new MusteriDbContext())
            {
                var employeeToDelete = parameter as Musteri;

                if (employeeToDelete != null)
                {
                    //db contextten silinecek datayı çıkar ve degisiklikleri kaydet
                    _context.Musteriler.Remove(employeeToDelete);
                    _context.SaveChanges();
                    // UIdaki listeyi güncelle
                    Musterilerr.Remove(employeeToDelete);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
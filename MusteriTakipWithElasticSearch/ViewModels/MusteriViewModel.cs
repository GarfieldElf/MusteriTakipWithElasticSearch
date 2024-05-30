using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MusteriTakipWithElasticSearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace MusteriTakipWithElasticSearch.ViewModels
{
    public class MusteriViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Musteri> _musteriler;
        private Musteri _selectedMusteri;
        private string _searchMusteri;

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

        public string SearchMusteri
        {
            get { return _searchMusteri; }
            set
            {
                _searchMusteri = value;
                OnPropertyChanged();
                Mustericollection.Filter = MusteriAra;
            }
        }
        private ICollectionView _mustericollection;
        public ICollectionView Mustericollection
        {
            get { return _mustericollection; }
            set
            {
                _mustericollection = value;
                OnPropertyChanged();
            }
        }
        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }
        public MusteriViewModel()
        {
            Musterilerr = new ObservableCollection<Musteri>();
            MusterileriGetir();
            Mustericollection = CollectionViewSource.GetDefaultView(Musterilerr);
            AddCommand = new RelayCommand((s) => true, MusteriEkle);
            DeleteCommand = new RelayCommand((s) => true, MusteriSil);
        }
        private bool MusteriAra(object parameter)
        {
            if (!string.IsNullOrEmpty(SearchMusteri))
            {
                var musteri = parameter as Musteri;
                return musteri != null && musteri.MusteriAdi.StartsWith(SearchMusteri, StringComparison.CurrentCultureIgnoreCase);
            }
            return true;
        }

        public void MusteriGuncelle(string musteriad, string musterisoyadi, string musterinumara, string musterieposta, int getno)
        {
            using (MusteriDbContext _context = new MusteriDbContext())
            {
                var musteri = _context.Musteriler.FirstOrDefault(x => x.MusteriNo == getno);
                if (musteri != null)
                {
                    musteri.MusteriAdi = musteriad;
                    musteri.MusteriSoyadi = musterisoyadi;
                    musteri.MusteriTel = musterinumara;
                    musteri.MusteriEposta = musterieposta;
                    _context.SaveChanges();
                }
            }
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

                if (!string.IsNullOrEmpty(yeniMusteri.MusteriAdi) && !string.IsNullOrEmpty(yeniMusteri.MusteriSoyadi))
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
                Musterilerr.Clear();

                foreach (var musteri in _context.Musteriler)
                {
                    Musterilerr.Add(musteri);
                }
            }
        }
        private void MusteriSil(object parameter)
        {
            using (MusteriDbContext _context = new MusteriDbContext())
            {
                var silinecekmusteri = parameter as Musteri;

                if (silinecekmusteri != null)
                {
                    //db contextten silinecek datayı çıkar ve degisiklikleri kaydet
                    _context.Musteriler.Remove(silinecekmusteri);
                    _context.SaveChanges();
                    // UIdaki listeyi güncelle
                    Musterilerr.Remove(silinecekmusteri);
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
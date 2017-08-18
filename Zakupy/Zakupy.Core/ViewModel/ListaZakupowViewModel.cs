using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Zakupy.Core.DataBase;

namespace Zakupy.Core.ViewModel
{
    public class ListaZakupowViewModel : MvxViewModel
    {
        private readonly IZakupyDb _db;
        public ICommand DodajProdukCmd { get; private set; } 
        public ICommand UsunProduktCmd { get; private set; }

        public ListaZakupowViewModel(IZakupyDb db)
        {
            _db = db;
            DodajProdukCmd = new MvxCommand<DodajProdukt>(DodajProduktMethod);
            UsunProduktCmd = new MvxCommand<ZakupyViewModel>(UsunProduktMethod);            
        }

        private void UsunProduktMethod(ZakupyViewModel obj)
        {
            _db.UsunElement(obj.Id);
            WczytajListe();
        }

        private void DodajProduktMethod(DodajProdukt produkt)
        {
            double? cena = produkt.Cena;

            _db.DodajElement(produkt.NazwaProduktu, cena);
            WczytajListe();
        }

        private double? _cenaOgolna;
        public double? CenaOgolna
        {
            get { return _cenaOgolna; }
            set
            {
                _cenaOgolna = value;
                RaisePropertyChanged(() => CenaOgolna);
            }
        }


        public void Init()
        {
            WczytajListe();            
        }

        private void WczytajListe()
        {
            List<ZakupyViewModel> zakupyList = new List<ZakupyViewModel>();
            var zakupy = _db.PobierzZakupy();
            CenaOgolna = 0;

            try
            {
                foreach (ZakupyModel item in zakupy)
                {
                    var element = new ZakupyViewModel();
                    element.Id = item.Id;
                    element.NazwaProduktu = item.NazwaProduktu;
                    element.Cena = item.Cena;

                    CenaOgolna += item.Cena.HasValue ? item.Cena.Value : 0;

                    zakupyList.Add(element);
                }

                ListaZakupow = new ObservableCollection<ZakupyViewModel>(zakupyList);
            }
            catch(Exception ex)
            {
                //ignore
            }
            
        }

        private ObservableCollection<ZakupyViewModel> _listaZakupow;
        public ObservableCollection<ZakupyViewModel> ListaZakupow
        {
            get { return _listaZakupow; }
            set
            {
                _listaZakupow = value;
                RaisePropertyChanged(() => ListaZakupow);
            }
        }
    }
}

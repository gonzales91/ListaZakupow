using MvvmCross.Core.ViewModels;

namespace Zakupy.Core.ViewModel
{
    public class ZakupyViewModel : MvxViewModel
    {
        public int Id { get; set; }

        private string _nazwProduktu;
        public string NazwaProduktu
        {
            get { return _nazwProduktu; }
            set
            {
                _nazwProduktu = value;
                RaisePropertyChanged(() => NazwaProduktu);
            }
        }

        private double? _cena;
        public double? Cena
        {
            get { return _cena; }
            set
            {
                _cena = value;
                RaisePropertyChanged(() => Cena);
            }
        }
    }
}


using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Views;
using Zakupy.Core.ViewModel;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Views;
using Zakupy.Droid.View.dialogs;
using Android.Content;
using MvvmCross.Platform.Converters;
using System;
using System.Globalization;
using Android.Icu.Text;

namespace Zakupy.Droid.View
{
    [Activity(Label = "Zakupy", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class ListaZakupowView : MvxActivity<ListaZakupowViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListaZakupowView);

            var listView = FindViewById<MvxListView>(Resource.Id.listaZakupow);
            listView.ItemTemplateId = Resource.Layout.ZakupyView;
            listView.Adapter = new ItemAdapter(this);
            var cenaOgolna = FindViewById<TextView>(Resource.Id.textCenaOgolna);

            var set = this.CreateBindingSet<ListaZakupowView, ListaZakupowViewModel>();
            set.Bind(listView).For(v => v.ItemsSource).To(vm => vm.ListaZakupow);
            set.Bind(cenaOgolna).For(v => v.Text).To(vm => vm.CenaOgolna).WithConversion(new ConvertCena()).TwoWay();
            set.Apply();
        }

        public class ConvertCena : MvxValueConverter<double?, string>
        {
            protected override string Convert(double? value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == 0 || !value.HasValue)
                    return "brak";

                //return value + " zł";
                DecimalFormat format = new DecimalFormat("0.00");
                string formatted = format.Format(value);
                return formatted + " zł";
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DodajMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.dodaj:
                    var dodajDIalog = new AddDialog(produkt => 
                    {
                        if(produkt.NazwaProduktu == "" || produkt.NazwaProduktu == null)
                        {
                            var alert = new AlertDialog.Builder(this)
                            .SetMessage("Nazwa produktu nie może być pusta")
                            .SetPositiveButton("OK", (s,a)=> { })
                            .Show();
                        }
                        else
                            ViewModel.DodajProdukCmd.Execute(produkt);
                    });
                    dodajDIalog.Show(FragmentManager, null);
                    return true;                    
            }

            return true;
        }

        public class ItemAdapter : MvxAdapter
        {
            private readonly ListaZakupowView _context;
            public ItemAdapter(ListaZakupowView context) : base(context, (IMvxAndroidBindingContext)context.BindingContext)
            {
                _context = context;
            }

            protected override IMvxListItemView CreateBindableView(object dataContext, int templateId)
            {
                var result = new ListItemAdapter(Context, BindingContext.LayoutInflaterHolder, dataContext, templateId);
                var nazwaProduktu = result.FindViewById<TextView>(Resource.Id.textNazwa);
                var cena = result.FindViewById<TextView>(Resource.Id.textCena);
                var delete = result.FindViewById<ImageView>(Resource.Id.imageDelete);

                var set = result.CreateBindingSet<ListItemAdapter, ZakupyViewModel>();
                set.Bind(nazwaProduktu).For(v => v.Text).To(vm => vm.NazwaProduktu);
                set.Bind(cena).For(v => v.Text).To(vm => vm.Cena).WithConversion(new ConvertCena());
                set.Apply();

                delete.Click += (s, a) =>
                {
                    _context.ViewModel.UsunProduktCmd.Execute(result.ViewModel);
                };

                return result;
            }
        }

        public class ListItemAdapter : MvxListItemView
        {
            public ListItemAdapter(Context context, IMvxLayoutInflaterHolder layoutInflater, object dataContext, int templateId) : base(context, layoutInflater, dataContext, templateId)
            {
            }

            public ZakupyViewModel ViewModel
            {
                get { return DataContext as ZakupyViewModel; }
            }
        }
    }
}
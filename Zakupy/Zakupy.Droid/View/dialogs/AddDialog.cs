using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Zakupy.Core;
using Android.Content;

namespace Zakupy.Droid.View.dialogs
{
    public class AddDialog : DialogFragment
    {
        public Action<DodajProdukt> _pordukt;

        public AddDialog(Action<DodajProdukt> pordukt)
        {
            _pordukt = pordukt;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {

            LinearLayout layout = new LinearLayout(Activity);
            layout.Orientation = Orientation.Vertical;

            EditText nazwaProduktu = new EditText(Activity);
            nazwaProduktu.Hint = "wpisz nazwe produktu";
            nazwaProduktu.SetBackgroundResource(Android.Resource.Drawable.EditText);

            EditText cena = new EditText(Activity);
            cena.Hint = "podaj cene";
            cena.SetBackgroundResource(Android.Resource.Drawable.EditText);
            cena.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;

            layout.AddView(nazwaProduktu);
            layout.AddView(cena);

            var builder = new AlertDialog.Builder(Activity);
            
            builder.SetTitle("Dodaj produkt");                        
            builder.SetView(layout);
            builder.SetPositiveButton("Zapisz", (sender, args) =>
            {
                var produkt = new DodajProdukt();
                produkt.NazwaProduktu = nazwaProduktu.Text;

                if (String.IsNullOrEmpty(cena.Text))
                    produkt.Cena = null;
                else
                    produkt.Cena = Math.Floor(Double.Parse(cena.Text.Replace(".", ",")) * 100) / 100;

                _pordukt(produkt);
            });

            nazwaProduktu.TextChanged += (s, a) =>
            {
                if(!String.IsNullOrEmpty(a.Text.ToString()))
                {
                    
                }
            };             
            
            var dialog = builder.Create();
            return dialog;
        }        
    }
}
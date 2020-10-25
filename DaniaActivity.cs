using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Konkurs
{
    [Activity(Label = "DaniaActivity")]
    public class DaniaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Przepis);
            
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = Intent.GetStringExtra("title");
            SetActionBar(toolbar);

            TextView textPrzepis = FindViewById<TextView>(Resource.Id.textPrzepis);
            textPrzepis.Text = Intent.GetStringExtra("przepis");

            Button button = FindViewById<Button>(Resource.Id.KcalButton);
            button.Click += (sender, e) =>
            {
                var dane = Application.Context.GetSharedPreferences("Login", FileCreationMode.Private);
                if (dane.GetInt("Kcal", 0) <= Intent.GetIntExtra("Lkcal",0))
                {
                    dane.Edit().PutInt("Kcal", 0);
                }
                else 
                {
                    int _kcal = dane.GetInt("Kcal", 0);
                    _kcal -= Intent.GetIntExtra("Lkcal", 0);
                    dane.Edit().PutInt("Kcal", _kcal);
                }

                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };


        }
    }

}
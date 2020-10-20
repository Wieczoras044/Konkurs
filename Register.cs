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
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.register_view);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Rejestracja";
            SetActionBar(toolbar);

            EditText _imie = FindViewById<EditText>(Resource.Id.imie);
            EditText _wiek = FindViewById<EditText>(Resource.Id.wiek);
            EditText _waga = FindViewById<EditText>(Resource.Id.waga);
            EditText _wzrost = FindViewById<EditText>(Resource.Id.wzrost);

            Button button = FindViewById<Button>(Resource.Id.submit_button);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.gender, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            button.Click += (sender, e) =>
            {
                if (_imie.Text.Length != 0)
                {
                    if (_wiek.Text.ToString() != "")
                    {
                        if (_waga.Text.ToString() != "")
                        {
                            if (_wzrost.Text.ToString() != "")
                            {
                                var _login = Application.Context.GetSharedPreferences("Login", FileCreationMode.Private).Edit();
                                _login.PutString("Name", _imie.Text).Apply();
                                _login.PutString("Weight", _waga.Text).Apply();
                                _login.PutString("Height", _wzrost.Text).Apply();
                                _login.PutString("Age", _wiek.Text).Apply();
                                var _register = Application.Context.GetSharedPreferences("isRegister", FileCreationMode.Private);
                                _register.Edit().PutBoolean("Register", true).Apply();
                                var intent = new Intent(this, typeof(MainActivity));
                                StartActivity(intent);
                                Finish();
                            }
                            else Android.Widget.Toast.MakeText(this, "Podaj swój wzrost", ToastLength.Short).Show();
                        }
                        else Android.Widget.Toast.MakeText(this, "Podaj swoją wage!", ToastLength.Short).Show();
                    }
                    else Android.Widget.Toast.MakeText(this, "Podaj wiek!", ToastLength.Short).Show();
                }
                else Android.Widget.Toast.MakeText(this, "Podaj Imię!", ToastLength.Short).Show();
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var _login = Application.Context.GetSharedPreferences("Login", FileCreationMode.Private).Edit();
            _login.PutString("Gender", spinner.GetItemAtPosition(e.Position).ToString()).Apply();
        }
    }
}
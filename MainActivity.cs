using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content;
using System;

namespace Konkurs
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            var variable = Application.Context.GetSharedPreferences("isRegister", FileCreationMode.Private);
            bool register = variable.GetBoolean("Register", false);
            if (!register)
            {
                var intent = new Intent(this, typeof(Register));
                StartActivity(intent);
                Finish();
            }

            SetContentView(Resource.Layout.activity_main);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Witaj " + Application.Context.GetSharedPreferences("Login", FileCreationMode.Private).GetString("Name", null) + "!";
            SetActionBar(toolbar);

            var dane = Application.Context.GetSharedPreferences("Login",FileCreationMode.Private);
            ProfileView profile = new ProfileView();
            string _gender = dane.GetString("Gender", null);
            int _waga = int.Parse(dane.GetString("Weight", "0"));
            int _wzrost = int.Parse(dane.GetString("Height", "0"));
            int _wiek = int.Parse(dane.GetString("Age", "0"));
            profile.Profile(_waga, _wzrost, _wiek, _gender);
            TextView _plec = FindViewById<TextView>(Resource.Id.gender_text);
            _plec.Text = "Płeć: " + profile.plec;
            TextView wagaPoczatkowa = FindViewById<TextView>(Resource.Id.wagaPoczatkowa);
            wagaPoczatkowa.Text = "Waga początkowa:" + profile.waga_poczatkowa.ToString() + "kg";
            TextView wzrost = FindViewById<TextView>(Resource.Id.Wzrost);
            wzrost.Text = "Wzrost: " + profile.wzrost.ToString() +"cm";
            TextView BMI = FindViewById<TextView>(Resource.Id.BMI);
            BMI.Text = "BMI: " + profile.LiczBMI(profile.waga_poczatkowa, profile.wzrost).ToString() ;
            TextView wiek = FindViewById<TextView>(Resource.Id.wiek);
            wiek.Text = "Wiek:  " + profile.wiek.ToString();
            Spinner spinner = FindViewById<Spinner>(Resource.Id.level_spiner);

            string _stopinAktywnosci = dane.GetString("Activity", "Niski");
            ProgressBar CalorieProgressBar = FindViewById<ProgressBar>(Resource.Id.CalorieBar);
            TextView calorie = FindViewById<TextView>(Resource.Id.calorieText);
            

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ActivityLevelSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.level_spiner, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Button dietaButton = FindViewById<Button>(Resource.Id.dieta_button);
            bool test = dane.GetBoolean("LiczKcal", false);
            if (test)
            {
                string kcal = profile.LiczZapotrzebowanie(_gender, _waga, _wzrost, _wiek, _stopinAktywnosci).ToString();
                dane.Edit().PutInt("Kcal", int.Parse(kcal));
                calorie.Text = kcal;
            }
            else
            {
                calorie.Text = dane.GetString("Kcal", "Błąd");
            }

            dietaButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(DietaActivity));
                StartActivity(intent);
            };
        }

        private void spinner_ActivityLevelSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var dane = Application.Context.GetSharedPreferences("Login", FileCreationMode.Private);
            dane.Edit().PutBoolean("LiczKcal", true);
            dane.Edit().PutString(spinner.GetItemAtPosition(e.Position).ToString(),"Niski");
            TextView calorie = FindViewById<TextView>(Resource.Id.calorieText);
            ProfileView profile = new ProfileView();
            string _gender = dane.GetString("Gender", null);
            int _waga = int.Parse(dane.GetString("Weight", "0"));
            int _wzrost = int.Parse(dane.GetString("Height", "0"));
            int _wiek = int.Parse(dane.GetString("Age", "0"));
            calorie.Text = profile.LiczZapotrzebowanie(_gender, _waga, _wzrost, _wiek, spinner.GetItemAtPosition(e.Position).ToString()).ToString();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
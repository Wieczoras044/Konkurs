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
    class ProfileView
    {
        public string plec;
        public int waga_poczatkowa, wzrost, wiek;

        public double BMI, Calorie;
        
        public void Profile(int waga_poczatkowa, int wzrost, int wiek, string plec)
        {
            this.waga_poczatkowa = waga_poczatkowa;
            this.wzrost = wzrost;
            this.wiek = wiek;
            this.plec = plec;
        }

        public double LiczBMI(int waga, double wzrost) 
        {
            wzrost = wzrost / 100;
            BMI = (waga / Math.Pow(wzrost,2));

            return Math.Round(BMI,2);
        }

        public double LiczZapotrzebowanie(string plec,int masa_ciala, int wzrost, int wiek, string stopie_aktywnosci) 
        {

            if(plec == "Kobieta") 
            {
                switch (stopie_aktywnosci)
                {
                    case "Niski":
                        Calorie = (655 + (9.6 * masa_ciala) + (1.8 * wzrost) - (4.7 * wiek)) * 1.4;
                        break;
                    case "Średni":
                        Calorie = (655 + (9.6 * masa_ciala) + (1.8 * wzrost) - (4.7 * wiek)) * 1.7;
                        break;
                    case "Wysoki":
                        Calorie = (655 + (9.6 * masa_ciala) + (1.8 * wzrost) - (4.7 * wiek)) * 2.2;
                        break;
                }
            }
            else 
            {
                switch (stopie_aktywnosci)
                {
                    case "Niski":
                        Calorie = (66 + (13.7 * masa_ciala) + (5 * wzrost) - (6.76 * wiek)) * 1.4;
                        break;
                    case "Średni":
                        Calorie = (66 + (13.7 * masa_ciala) + (5 * wzrost) - (6.76 * wiek)) * 1.7;
                        break;
                    case "Wysoki":
                        Calorie = (66 + (13.7 * masa_ciala) + (5 * wzrost) - (6.76 * wiek)) * 2.2;
                        break;
                }
            }

            return Math.Round(Calorie,0);
        }
    }
}
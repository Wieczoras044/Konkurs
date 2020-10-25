using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.ObjectModel;
using Android.Support.V7.Widget;
using Android.Support.V7.RecyclerView.Extensions;

namespace Konkurs
{
    [Activity(Label = "DietaActivity")]
    public class DietaActivity : ListActivity
    {
        static readonly string[] posilki = new String[] {
            "Śniadanie","Launch","Obiad","Deser","Kolacja"
        };

        static readonly string[] sniadanie = new String[] {
            "OWSIANKA Z JABŁKIEM I MASŁEM ORZECHOWYM ( 500Kcal )","PŁATKI JAGLANE Z BANANEM I MIGDAŁAMI ( 450Kcal )","JAGLANKA Z KIWI ( 400Kcal )"
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.posilki_button, posilki);

            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                ListView listView = (ListView)sender;
                switch (listView.GetItemAtPosition(args.Position).ToString())
                {
                    case ("Śniadanie"):
                            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.posilki_button, sniadanie);
                            ListView.TextFilterEnabled = true;
                        ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
                        {
                            
                            ListView listView = (ListView)sender;
                            switch (listView.GetItemAtPosition(args.Position).ToString())
                            {
                                case ("OWSIANKA Z JABŁKIEM I MASŁEM ORZECHOWYM ( 500Kcal )"):
                                    var intent = new Intent(this, typeof(DaniaActivity));
                                    intent.PutExtra("title", "OWSIANKA Z JABŁKIEM I MASŁEM ORZECHOWYM");
                                    intent.PutExtra("przepis", "Tu będzie przepis");
                                    intent.PutExtra("Lkcal", 550);
                                    StartActivity(intent);
                                    break;
                            }

                        };
                        break;
                    case ("Launch"):
                        break;
                    case ("Obiad"):
                        break;
                    case ("Deser"):
                        break;
                    case ("Kolacja"):
                        break;
                }
            };
        }
    }

}
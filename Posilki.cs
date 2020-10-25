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
    class Posilki
    {
        private int imageResource;
        private string posilek;

        public Posilki(int imageResource, string posilek) 
        {
            this.imageResource = imageResource;
            this.posilek = posilek;
        }

        public int getImageResource()
        {
            return imageResource;
        }
        public string getPosilekText() 
        {
            return posilek;
        }
    }
}
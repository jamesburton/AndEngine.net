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

namespace andengine.util.progress
{
    public interface IProgressListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pProgress">Value between 0 and 100</param>
        void OnProgressChanged(int pProgress);
    }
}
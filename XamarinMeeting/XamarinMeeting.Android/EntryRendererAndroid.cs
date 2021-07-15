using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinMeeting;
using XamarinMeeting.Droid;
using static Android.OS.DropBoxManager;
using Entry = Xamarin.Forms.Entry;

[assembly:  ExportRenderer(typeof(MyEntry),typeof(EntryRendererAndroid))]
namespace XamarinMeeting.Droid
{

    public  class EntryRendererAndroid:EntryRenderer
    {
      public EntryRendererAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control!= null)
            {
                //Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                 var nativeEditText = (global::Android.Widget.EditText)Control;
                 var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                 shape.Paint.Color = Xamarin.Forms.Color.Black.ToAndroid();
                 shape.Paint.SetStyle(Paint.Style.Stroke);
                 nativeEditText.Background = shape;

               
                   /* GradientDrawable gd = new GradientDrawable();
                    //this line sets the bordercolor
                    gd.SetColor(global::Android.Graphics.Color.Transparent);
                    this.Control.SetBackgroundDrawable(gd);
                    this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                    Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));*/
  
            }



        }



    }
}
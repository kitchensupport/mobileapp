using System;
using Android;
using Android.App;
using Android.Content;
//using Java.Lang;

[assembly: Xamarin.Forms.Dependency(typeof(KitchenSupport.Droid.localData_droid))]
namespace KitchenSupport.Droid
{
    public class localData_droid : Java.Lang.Object, localDataInterface
    {
        public localData_droid()
        {
        }

        public void save(string key, string value)
        {
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            //save user's properties for a specific key
            prefEditor.PutString(key, value);
            prefEditor.Commit();
        }

        public string load(string key)
        {
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            return prefs.GetString(key, null);
        }
    }
}
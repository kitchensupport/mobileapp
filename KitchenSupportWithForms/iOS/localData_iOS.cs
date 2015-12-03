using System;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(KitchenSupport.iOS.localData_iOS))]
namespace KitchenSupport.iOS
{
    public class localData_iOS : localDataInterface
    {
        public localData_iOS()
        {
        }

        public void save(string key, string value)
        {
            var prefs = NSUserDefaults.StandardUserDefaults;
            //save user's properties for a specific keys
            prefs.SetValueForKey(new NSString(value), new NSString(key));
        }

        public string load(string key)
        {
            var prefs = NSUserDefaults.StandardUserDefaults;
            return prefs.StringForKey(key);
        }
    }
}
using System;

namespace KitchenSupport
{
    public interface localDataInterface
    {
        void save(string key, string value);
        string load(string key);
    }
}
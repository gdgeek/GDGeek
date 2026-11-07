using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GDGeek {

    public  class PlayerPrefsHelper {


        public static void Clear(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
            }
        }
        public static void SaveJson<T>(string key, T value)
        {
            if (value == null)
                PlayerPrefs.DeleteKey(key);
            else
                PlayerPrefs.SetString(key, JsonUtility.ToJson(value));

            PlayerPrefs.Save();
        }

        public static T LoadJson<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
                return default;

            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }


        
    }
}
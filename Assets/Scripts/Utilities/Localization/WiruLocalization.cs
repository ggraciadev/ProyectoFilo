using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace WiruLib {
    public class WiruLocalization : Singleton<WiruLocalization>
    {
        public enum Language { Eng, Spa, Cat, Jap, Fre };
        Language currentLanguage;
        public Language CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
                ChangeLanguage();
            }
        }
        public Dictionary<string, List<string>> map;

        public void Init_WiruLocalization()
        {
            //ParseLanguageCSVToJSON(LoadCSV("Localization"));
            CurrentLanguage = Language.Spa;
        }

        public static string LoadCSV(string path)
        {
            TextAsset file = Resources.Load(path) as TextAsset;
            if (file == null)
            {
                Debug.LogError("Error while Loading: " + path);
                return "";
            }
            else
            {
                return file.text;
            }
        }

        public static void SaveJSON(string path)
        {
            System.IO.StreamWriter file = new StreamWriter("Assets/Resources/" + path + ".json", false);
            file.Write(CSVToJSON.CSVToJson(LoadCSV(path)));
            file.Close();
        }

        public static void LoadLocalization()
        {

        }

        public static void GenerateJSON(string path)
        {
            SaveJSON(path);
        }

        public string[] GetTermData(string key)
        {
            string[] ret = new string[0];
            ret = map[key].ToArray();
            return ret;
        }

        public void ChangeLanguage()
        {
            WiruLocalizeText[] temp = GameObject.FindObjectsOfType<WiruLocalizeText>();
            foreach (WiruLocalizeText word in temp)
            {
                word.ChangeLanguage(currentLanguage);
            }
        }

        
    }
}

class Map
{
    public Dictionary<string, List<string>> map;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace WiruLib {
    public class WiruLocalization : Singleton<WiruLocalization>
    {
        public enum Language { Eng, Spa, Cat, End};
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
        public Dictionary<string, string[]> map;

        public void Init_WiruLocalization()
        {
            CurrentLanguage = Language.Spa;
            map = new Dictionary<string, string[]>();
            LoadJSON();
        }

        public void LoadJSON()
        {
            TextAsset json = Resources.Load<TextAsset>("Localization/Localization_JSON");
            Cjt_Map tempCjt = JsonUtility.FromJson<Cjt_Map>("{\"maps\":" + json.text + "}");

            Map[] allWords = tempCjt.maps;

            foreach(Map m in allWords)
            {
                string[] temp = new string[(int)Language.End];
                temp[(int)Language.Eng] = m.Eng;
                temp[(int)Language.Spa] = m.Spa;
                temp[(int)Language.Cat] = m.Cat;

                map.Add(m.Key, temp);
            }
        }

        public static string LoadCSV(string path)
        {
            TextAsset file = Resources.Load<TextAsset>(path);
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
            System.IO.StreamWriter file = new StreamWriter("Assets/Resources/" + path + "_JSON.json", false);
            file.Write(CSVToJSON.CSVToJson(LoadCSV(path)));
            file.Close();
        }

        public static void GenerateJSON(string path)
        {
            SaveJSON(path);
        }

        public string GetTerm(string key)
        {
            string ret = "";
            ret = map[key][(int)currentLanguage];
            return ret;
        }

        public string[] GetTermData(string key)
        {
            return map[key];
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

[System.Serializable]
class Cjt_Map
{
    public Map[] maps;
}

[System.Serializable]
class Map
{
    public string Key;
    public string Eng;
    public string Spa;
    public string Cat;
}

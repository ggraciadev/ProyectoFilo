using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public static List<List<string>> gameLocalization;
        public static Dictionary<string, int> keyPosition;

        public void Init_WiruLocalization()
        {
            ParseLanguageCSVToJSON(LoadCSV("Localization"));
            CurrentLanguage = Language.Spa;
        }

        public static void ParseLanguageCSVToJSON(string csv)
        {
            if (gameLocalization != null)
            {
                gameLocalization.Clear();
                keyPosition.Clear();
            }
            else
            {
                gameLocalization = new List<List<string>>();
                keyPosition = new Dictionary<string, int>();
            }

            int currentIteratorX = 0;
            int currentIteratorY = 0;
            string temp = "";

            gameLocalization.Add(new List<string>());

            for (int i = 0; i < csv.Length; i++)
            {
                if (csv[i] == '\n')
                {
                    currentIteratorY++;
                    currentIteratorX = 0;
                    temp = "";
                    gameLocalization.Add(new List<string>());
                }
                else if (csv[i] == ',')
                {
                    gameLocalization[currentIteratorY].Add(temp);
                    
                    if(currentIteratorX++ == 0 && !keyPosition.ContainsKey(temp))
                    {
                        keyPosition.Add(temp, currentIteratorY);
                    }
                    temp = "";
                }
                else if (csv[i] != '\r')
                {
                    temp += csv[i];
                }
            }
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

        public static void LoadLocalization(string path)
        {
            ParseLanguageCSVToJSON(LoadCSV(path));
        }

        public string[] GetTermData(string key)
        {
            string[] ret = new string[0];
            ret = gameLocalization[keyPosition[key]].ToArray();
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

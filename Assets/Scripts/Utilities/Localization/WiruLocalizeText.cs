using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WiruLib {
    public class WiruLocalizeText : MonoBehaviour
    {
        [SerializeField] string key;
        TMPro.TextMeshProUGUI text;
        string[] term;
        string translatedTerm;

        private void Start()
        {
            text = GetComponent<TMPro.TextMeshProUGUI>();
            SetKey(key);
        }

        public void InitWiruLocalizeText(string[] data)
        {
            key = data[0];
            term = new string[data.Length - 1];
            for (int i = 0; i < term.Length; i++)
            {
                term[i] = data[i + 1];
            }
            ChangeLanguage(WiruLocalization.Instance.CurrentLanguage);
        }

        public void ChangeLanguage(WiruLocalization.Language lang)
        {
            if (term != null)
            {
                translatedTerm = term[(int)lang];
                text.text = translatedTerm;
            }
        }

        public void SetKey(string key)
        {
            InitWiruLocalizeText(WiruLocalization.Instance.GetTermData(key));
        }
    }
}

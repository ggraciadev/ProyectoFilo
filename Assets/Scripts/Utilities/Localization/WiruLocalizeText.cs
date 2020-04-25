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

        private void Awake()
        {
            text = GetComponent<TMPro.TextMeshProUGUI>();
        }

        private void Start()
        {
        }

        public void InitWiruLocalizeText(string[] data)
        {
            term = data;
            ChangeLanguage(WiruLocalization.Instance.CurrentLanguage);
        }

        public void ChangeLanguage(WiruLocalization.Language lang)
        {
            if (term != null)
            {
                text.text = term[(int)lang];
            }
        }

        public void SetKey(string _key)
        {
            key = _key;
            InitWiruLocalizeText(WiruLocalization.Instance.GetTermData(key));
        }

        public string GetKey()
        {
            return key;
        }
    }
}

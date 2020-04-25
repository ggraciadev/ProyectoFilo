using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    static bool initialized;
    // Start is called before the first frame update
    void Awake()
    {
        if(!initialized)
        {
            MainManager.Instance.Init_MainManager();
            WiruLib.WiruLocalization.Instance.Init_WiruLocalization();
            initialized = true;
        }
    }
}

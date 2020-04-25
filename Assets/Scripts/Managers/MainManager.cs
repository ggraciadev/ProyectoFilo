using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : WiruLib.Singleton<MainManager>
{
    public string asignatura;
    public string tema;
    
    public void Init_MainManager()
    {
        asignatura = "Filo";
        tema = "*";
    }
}

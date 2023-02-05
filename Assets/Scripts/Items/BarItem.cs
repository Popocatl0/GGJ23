using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Update slider value
/// </summary>
public class BarItem : MonoBehaviour
{
    public string ID;
    public Image[] fillBar;

    public void UpdateBar(int value, int maxVal){
        //fillBar.value = value/maxVal;
        for (int i = 0; i < fillBar.Length; i++){
            fillBar[i].enabled = i < value;
        }
    }
}

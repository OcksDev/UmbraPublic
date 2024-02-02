using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class I_Sorters : MonoBehaviour
{
    public Image[] shites;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var item in shites)
        {
            item.color = new Color32(111,111,111,255);
        }
        shites[Gamer.Instance.SortType].color = Gamer.Instance.main_colors[1];
    }
}

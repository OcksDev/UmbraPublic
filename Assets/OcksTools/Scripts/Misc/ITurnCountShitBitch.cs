using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ITurnCountShitBitch : MonoBehaviour
{
    public Color32[] colors;
    public Image[] balls;
    public void UpdateColors(bool isplayerturn)
    {
        var col1 = colors[isplayerturn?0:2];
        var col2 = colors[isplayerturn?1:3];
        balls[0].color = col1;
        balls[1].color = col1;
        balls[2].color = col2;
        balls[3].color = col2;
    }
}

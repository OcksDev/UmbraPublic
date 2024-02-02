using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringBallsInYourJaw : MonoBehaviour
{
    public string HoverClarifyText = "";
    public float scale = 2f;
    public int type = 0;
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform=  GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool can = true;
        var g = Gamer.Instance;
        switch (type)
        {
            case 1:
                if (g.checks[13])
                {
                    can = false;
                }
                break;
        }
        if(can && RandomFunctions.Instance.CheckInsideRect(rectTransform, g.MousePosMainUnScaled, g.cam, scale))
        {
            g.hoveringonmyballsinyourjaw = HoverClarifyText;
        }
    }
}

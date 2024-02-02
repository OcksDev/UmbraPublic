using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenuShit : MonoBehaviour
{
    public RectTransform[] buttons;
    public Image[] paths;
    public Image[] alwayson;
    public Image[] whitetocolor;
    public Image[] miscy;
    public TextMeshProUGUI titt;

    //Default setup to make this a singleton
    public static PlayMenuShit instance;
    public static PlayMenuShit Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance == null) instance = this;
    }
    private void OnEnable()
    {
        if (Gamer.Instance != null)
        {
            j();
        }
    }
    void FixedUpdate()
    {
        j();
    }
    public void j()
    {
        int hover = -1;
        int x = 0;
        foreach (var rectum in buttons)
        {
            if (RandomFunctions.Instance.CheckInsideRect(rectum, Gamer.Instance.MousePosMainUnScaled, Gamer.Instance.cam, 1.2f))
            {
                hover = x;
                break;
            }
            x++;
        }
        x = 0;
        SetPaths(hover);
    }
    public void SetPaths(int hover)
    {
        foreach (var im in alwayson)
        {
            im.color = Gamer.Instance.main_colors[1];
        }
        foreach (var im in whitetocolor)
        {
            im.color = Gamer.Instance.main_colors[3];
        }
        titt.color = Gamer.Instance.main_colors[1];
        var g = Gamer.Instance;
        foreach (var path in paths)
        {
            path.color = g.main_colors[0];
        }
        foreach (var butt in buttons)
        {
            var e = butt.GetComponent<TextMeshProUGUI>();
            if (e != null)
            {
                e.color = new Color32(56, 56, 56, 255);
            }
        }

        if (hover > -1)
        {
            var e = buttons[hover].GetComponent<TextMeshProUGUI>();
            if (e != null) e.color = g.main_colors[1];
            paths[0].color = g.main_colors[1];
            paths[9].color = g.main_colors[1];
            paths[hover + 6].color = g.main_colors[1];
            paths[hover + 3].color = g.main_colors[1];
            paths[hover + 12].color = g.main_colors[1];
            if (hover < 1)
            {
                paths[1].color = g.main_colors[1];
                paths[10].color = g.main_colors[1];
            }
            if (hover > 1)
            {
                paths[2].color = g.main_colors[1];
                paths[11].color = g.main_colors[1];
            }

        }
    }

}

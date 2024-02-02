using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu2Shit : MonoBehaviour
{
    public RectTransform[] buttons;
    public Image[] paths;
    public Image[] alwayson;
    public Image[] whitetocolor;
    public Image[] miscy;
    public TextMeshProUGUI titt;
    public Button startetertr;

    public ToggleThings[] titties;
    public SliderBoi[] scrolling_titties;

    public GameObject SingleHost;
    public GameObject clint;


    //Default setup to make this a singleton
    public static PlayMenu2Shit instance;
    public static PlayMenu2Shit Instance
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
            if (MainMenuShit.Instance.gaydaring) titt.text = "Gaming";
            foreach (var tit in titties)
            {
                tit.SetVal();
                tit.UpdateValues();
            }
            foreach (var tit in scrolling_titties)
            {
                tit.SetVals();
            }

        }
    }
    void FixedUpdate()
    {
        j();
    }
    public void j()
    {

        SingleHost.SetActive(!Gamer.Instance.Multiplayer || NetworkManager.Singleton.IsHost); ;
        clint.SetActive(Gamer.Instance.Multiplayer && !NetworkManager.Singleton.IsHost);


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

        if (Gamer.Instance.Multiplayer)
        {
            startetertr.interactable = Gamer.Instance.miscrefs[46].transform.childCount > 1 && NetworkManager.Singleton.IsHost;
        }
        else
        {
            startetertr.interactable = true;
        }

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
    }

}

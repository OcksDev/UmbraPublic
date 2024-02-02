using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderBoi : MonoBehaviour
{
    public int Type;
    private Slider sl;
    public TextMeshProUGUI text;
    public Image SlideBody;
    public Image SliderBG;
    public Image SliderHandle;

    private void Start()
    {
        sl = GetComponent<Slider>();
    }

    public void SetVals()
    {
        sl = GetComponent<Slider>();
        switch (Type)
        {
            case 0:
                sl.value = SoundSystem.Instance.MasterVolume;
                break;
            case 1:
                sl.value = SoundSystem.Instance.SFXVolume;
                break;
            case 2:
                sl.value = SoundSystem.Instance.MusicVolume;
                break;
            case 3:
                sl.value = Gamer.Instance.HandSizeSel;
                break;
            case 4:
                sl.value = Gamer.Instance.Highlights * 20;
                break;
            case 5:
                sl.value = Gamer.Instance.Lowlights * 20;
                break;
        }

        var c = Gamer.Instance.main_colors[1];
        SliderHandle.color = c;
        text.color = c;

        c.r = (byte)(0.8f * c.r);
        c.g = (byte)(0.8f * c.g);
        c.b = (byte)(0.8f * c.b);
        SlideBody.color = c;
        c.r = (byte)(0.5f * c.r);
        c.g = (byte)(0.5f * c.g);
        c.b = (byte)(0.5f * c.b);
        SliderBG.color = c;
    }

    public void CHangeVal()
    {
        if (SoundSystem.Instance != null)
        {
            switch (Type)
            {
                case 0:
                    SoundSystem.Instance.MasterVolume = sl.value;
                    break;
                case 1:
                    SoundSystem.Instance.SFXVolume = sl.value;
                    break;
                case 2:
                    SoundSystem.Instance.MusicVolume = sl.value;
                    break;
                case 3:
                    Gamer.Instance.HandSizeSel = (int)sl.value;
                    break;
                case 4:
                    Gamer.Instance.Highlights = sl.value / 20;
                    Debug.Log(Gamer.Instance.Highlights);
                    Gamer.Instance.UpdateShaders();
                    break;
                case 5:
                    Gamer.Instance.Lowlights = sl.value / 20;
                    Debug.Log(Gamer.Instance.Lowlights);
                    Gamer.Instance.UpdateShaders();
                    break;
            }
        }
    }
}

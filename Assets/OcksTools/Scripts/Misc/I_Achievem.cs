using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class I_Achievem : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public int BallSex = -1;
    public Image BG;
    public Color[] cowor = new Color[2];
    public void Clickity()
    {
        Gamer.Instance.AchievemtCrossover = BallSex;
        Gamer.Instance.AchievemtColorCrossover = BG.color;
        Gamer.Instance.ToggleAchiOverlay();
    }
}

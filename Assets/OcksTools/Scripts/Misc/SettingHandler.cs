using UnityEngine;

public class SettingHandler : MonoBehaviour
{
    public SliderBoi[] slids;

    public void SetVals()
    {
        foreach (var slid in slids)
        {
            slid.SetVals();
        }
    }
}

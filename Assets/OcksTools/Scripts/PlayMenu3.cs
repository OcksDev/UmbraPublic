using Unity.Netcode;
using UnityEngine;

public class PlayMenu3 : MonoBehaviour
{
    public GameObject SingleHost;
    public GameObject clint;
    public GameObject single;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (Gamer.Instance != null)
        {
            SingleHost.SetActive(!Gamer.Instance.Multiplayer || NetworkManager.Singleton.IsHost); ;
            clint.SetActive(Gamer.Instance.Multiplayer && !NetworkManager.Singleton.IsHost);
            single.SetActive(!Gamer.Instance.Multiplayer);
        }
    }
}

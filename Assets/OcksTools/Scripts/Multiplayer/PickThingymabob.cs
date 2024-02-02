using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PickThingymabob : MonoBehaviour
{
    private RelayMoment relay;
    public void GoinGame()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void GoinGameE()
    {
        var p = GetComponent<TMP_InputField>();
        GoinGameE2(p.text);
    }
    public async void GoinGameE2(string code)
    {
        Gamer.Instance.miscrefs[47].SetActive(true);
        relay = RelayMoment.Instance;
        Debug.Log("Attempt Join");
        int i = await relay.JoinRelay(code);
        if (i == 1)
        {
            Debug.Log("Joined Goodly");
            NetworkManager.Singleton.StartClient();
            if(Gamer.Instance.miscrefs[46].transform.childCount > 2)
            {
                Gamer.Instance.MainMenu();
            }
            else
            {
                Gamer.Instance.StartCoroutine(Gamer.Instance.WaitForConn());
                Gamer.Instance.ToPlay2(true);
            }
        }
        else
        {
            Gamer.Instance.miscrefs[47].SetActive(false);
        }
    }

    public async void MakeGame()
    {
        Gamer.Instance.miscrefs[47].SetActive(true);
        var x = await MakeGame2();
        if (x != "Error")
        {
            relay = RelayMoment.Instance;
            var p = Instantiate(relay.ServerGamerObject, relay.transform.position, relay.transform.rotation, relay.transform);
            p.GetComponent<NetworkObject>().Spawn();
            Gamer.Instance.Multiplayer = true;
            Gamer.Instance.StartCoroutine(Gamer.Instance.WaitForConn());
            Gamer.Instance.UpdateMenus();
            Gamer.Instance.ToPlay2(true);
        }
        else
        {
            Gamer.Instance.miscrefs[47].SetActive(false);
        }
    }
    public async Task<string> MakeGame2()
    {
        relay = RelayMoment.Instance;
        int i = await relay.CreateRelay();
        if (i == 1)
        {
            Debug.Log(relay.Join_Code);

            NetworkManager.Singleton.StartHost();

            Gamer.Instance.Multiplayer = true;
            Gamer.Instance.UpdateMenus();
            return relay.Join_Code;
        }
        else
        {
            return "Error";
        }
    }
}

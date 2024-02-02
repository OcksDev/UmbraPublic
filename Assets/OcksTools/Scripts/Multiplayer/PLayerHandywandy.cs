using System.Collections;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PLayerHandywandy : NetworkBehaviour
{
    public string Username = "";
    public int DisplayType = 0;
    public string DisplayData = "";
    private NetworkObject no;
    public NetworkVariable<FixedString128Bytes> n_name = new NetworkVariable<FixedString128Bytes>("~fuckmewhyareyoulookingatmybarenakedasshole~", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> IsReady = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<FixedString128Bytes> n_displayd = new NetworkVariable<FixedString128Bytes>("~fuckmewhyareyoulookingatmybarenakedasshole~", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> n_displayt = new NetworkVariable<int>(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);



    public no nono;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = Gamer.Instance.miscrefs[46].transform;
        no = GetComponent<NetworkObject>();
        Gamer.Instance.AllHandJobs.Add(this);
        if (no.IsOwner)
        {
            Gamer.Instance.LocalHandJob = this;
            Username = FileSystem.Instance.UniversalData["Username"];
            n_name.Value = Username;
            DisplayType = 0;
            n_displayt.Value = DisplayType;
            var e = Gamer.Instance.Titles[Gamer.Instance.SelectedTitle];
            DisplayData = e == ""?e:$"\"{e}\"";
            n_displayd.Value = DisplayData;
            StartCoroutine(SetDescription());
        }
        else
        {
            for (int i = 0; i < 1; i++) StartCoroutine(WaitForData(0));
        }
    }

    public IEnumerator WaitForData(int i)
    {
        switch (i)
        {
            case 0:
                yield return new WaitUntil(() => { return n_name.Value.ToString() != "~fuckmewhyareyoulookingatmybarenakedasshole~"; });
                Username = n_name.Value.ToString();
                break;
            case 1:
                yield return new WaitUntil(() => { return n_displayd.Value.ToString() != "~fuckmewhyareyoulookingatmybarenakedasshole~"; });
                DisplayData = n_displayd.Value.ToString();
                SetDescription();
                break;
            case 2:
                yield return new WaitUntil(() => { return n_displayt.Value != -1; });
                DisplayType = n_displayt.Value;
                break;
        }
        yield return null;
    }


    public IEnumerator SetDescription()
    {
        yield return new WaitForFixedUpdate();
        nono.texty_2.text = DisplayData;
    }
}

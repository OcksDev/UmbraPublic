using Unity.Netcode;
using UnityEngine;

public class ServerGamer : NetworkBehaviour
{
    public static ServerGamer instance;

    // public NetworkVariable<int> PlayerNum = new NetworkVariable<int>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    // FixedString128Bytes
    public static ServerGamer Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (Instance == null) instance = this;
    }



    [ServerRpc(RequireOwnership = false)]
    public void SpawnObjectServerRpc(int refe, Vector3 pos, Quaternion rot, string id, string data, bool isreal = false)
    {
        SpawnObjectClientRpc(refe, pos, rot, id, data, isreal);
    }

    [ClientRpc]
    public void SpawnObjectClientRpc(int refe, Vector3 pos, Quaternion rot, string id, string data = "", bool isreal = false)
    {
        if (id == RandomFunctions.Instance.ClientID) return;

        RandomFunctions.Instance.SpawnObject(refe, gameObject, pos, rot, false, data, isreal);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendChatMessageServerRpc(string id, string message, string hex)
    {
        RecieveChatMessageClientRpc(id, message, hex);
    }

    [ClientRpc]
    public void RecieveChatMessageClientRpc(string id, string message, string hex)
    {
        if (id == RandomFunctions.Instance.ClientID) return;

        ChatLol.Instance.WriteChat(message, hex);
    }

    [ServerRpc(RequireOwnership = false)]
    public void MessageServerRpc(string id, string message, int type)
    {
        FuckMySexyMeatBallsMessageClientRpc(id, message, type);
    }

    [ClientRpc]
    public void FuckMySexyMeatBallsMessageClientRpc(string id, string message, int type)
    {
        if (Gamer.Instance.DevMode) Debug.Log("Recieved Data: " + message);
        if (id == RandomFunctions.Instance.ClientID) return;
        switch (type)
        {
            case 0:
                //to play 3
                Gamer.Instance.DataHandover = message;
                Gamer.Instance.ToPlay3();

                break;
            case 1:
                //to play 3
                Gamer.Instance.DataHandover = message;
                Gamer.Instance.ToGame();

                break;
            case 2:
                // cardhandindex, score, data
                Gamer.Instance.MultiMoveData = message;
                Gamer.Instance.MutliMoveForward = true;
                break;
            case 3:
                // back to play 2 lol
                Gamer.Instance.BackToPlay2();
                break;
        }
    }

    /*
    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc()
    {
        StartGameClientRpc();
    }
    [ClientRpc]
    public void StartGameClientRpc()
    {
        Gamer.Instance.StartGame();
    }
    */
}

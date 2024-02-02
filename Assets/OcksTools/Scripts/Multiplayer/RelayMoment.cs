using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayMoment : MonoBehaviour
{
    public string Join_Code = "";
    public GameObject ServerGamerObject;

    //Default setup to make this a singleton
    public static RelayMoment instance;
    public static RelayMoment Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance == null) instance = this;
    }
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Shitted fardly");
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async Task<int> CreateRelay()
    {
        try
        {
            //MAX CONNECTIONS IS SET HERE   VERY IMPORTANT
            Allocation allo = await RelayService.Instance.CreateAllocationAsync(2);

            Join_Code = await RelayService.Instance.GetJoinCodeAsync(allo.AllocationId);
            Debug.Log(Join_Code);

            RelayServerData rsd = new RelayServerData(allo, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(rsd);

            NetworkManager.Singleton.StartHost();
            return 1;
        }
        catch
        {
            Debug.Log("SHID FUKED");
        }
        return 0;
    }

    public async Task<int> JoinRelay(string joinC)
    {
        try
        {
            JoinAllocation ja = await RelayService.Instance.JoinAllocationAsync(joinC);
            Debug.Log("A1");

            RelayServerData rsd = new RelayServerData(ja, "dtls");
            Debug.Log("A2");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(rsd);
            Debug.Log("A3");

            NetworkManager.Singleton.StartClient();
            Debug.Log("A4");

            Join_Code = joinC.ToUpper();
            return 1;
        }
        catch
        {
            Debug.Log("HAH STUIPDIUD");
        }
        return 0;
    }

    public void EndConnection()
    {
        NetworkManager.Singleton.Shutdown();
    }
}

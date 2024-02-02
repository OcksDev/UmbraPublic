
using System;
using System.Collections.Generic;
using System.Linq;
//using Unity.Netcode;
using UnityEngine;

public class RandomFunctions : MonoBehaviour
{
    public GameObject[] SpawnRefs = new GameObject[1];
    public string GameVer = "DevBuild";
    public string ClientID;

    /* Welcome to Random Functions, your one stop shop of random functions
     * 
     * This is the hub of all the useful or widely used functions that i can't be bothered to qrite 50000 times,
     * so ya this place doesn't have much of a real function other than to store a bunch of other actually useful things.
     * 
     * Any function not marked as public is meant to be copied/pasted into a different script for usage
     */



    //Default setup to make this a singleton
    public static RandomFunctions instance;
    public static RandomFunctions Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        ClientID = GenerateID();
        if (Instance == null) instance = this;
    }

    private void OnApplicationQuit()
    {
        //save game or something idk man
    }

    public void Close()
    {
        Application.Quit();
    }
    public string ListToString(List<string> eee, string split = ", ")
    {
        return String.Join(split, eee);
    }

    public List<string> StringToList(string eee, string split = ", ")
    {
        return eee.Split(split).ToList();
    }
    public GameObject SpawnObject(int refe, GameObject parent, Vector3 pos, Quaternion rot, bool SendToEveryone = false, string data = "", bool isreal = true)
    {
        //object parenting does not work over multiplayer, I plan to add a fix using Tags but for now I am leaving.
        List<string> dadalol = StringToList(data);
        if (isreal) dadalol.Insert(0, GenerateObjectID());
        bool realsend = false;
        switch (refe)
        {
            case 0:
                break;
        }
        var f = Instantiate(SpawnRefs[refe], pos, rot, parent.transform);
        var d = f.GetComponent<SpawnData>();
        if (d != null)
        {
            //Requires objects to have SpawnData.cs to allow for data sending
            d.Data = dadalol;
            d.IsReal = isreal;
        }
        if (SendToEveryone)
        {
            // This code works, its just commented out by default because it requires Ocks Tools Multiplayer to be added
            //used for syncing the spawn of a local gameobject over the network instead of being a networked object

            //known issue: object parent is not preserved when spawning a local object over multiplayer

            ServerGamer.Instance.SpawnObjectServerRpc(refe, pos, rot, ClientID, ListToString(dadalol), realsend);
        }
        if (realsend) {/* this statement just removes the warning for the boolean not being used */};
        return f;
    }

    public float SpreadCalc(int index, int max, float spread, bool fix = false)
    {
        // a spread calculation used to spread out objects over an angle
        int i = max;
        int j = index;
        float k = spread;
        k /= 2;
        float p = j * spread;
        p += fix ? k : -k;
        p -= i * spread / 2;
        return p;
    }
    public void SpreadCalcArc(int index, int max, float total_arc, int buffer = 2, bool fix = false)
    {
        //untested, should allow for slightly more complex arcs
        // should work the same as SpreadCalc(), except that it expands up to a point first
        buffer = Math.Clamp(buffer, 2, 1000000);
        float f = (total_arc * (buffer - 1));
        if (max > 1) f /= (max - 1);
        float spread = f;
        SpreadCalc(index, max, spread, fix);
    }

    public string GenerateObjectID()
    {
        //a more secrure method of making gameobject ids for the Tags system
        string e = GenerateID();
        e = e + Tags.dict.Count.ToString();
        return e;
    }

    public int ArrayWrap(int index, int length)
    {
        return Mod(index, length);
    }

    /*
     * Screen.currentResolution.refreshRate
     * Application.targetFrameRate = 60
     * FPS: (int)(1.0f / Time.smoothDeltaTime)
     */

    private Quaternion RotateTowards(GameObject target, float max_angle_change)
    {
        Vector3 a = target.transform.position;
        var b = Quaternion.LookRotation((a - transform.position).normalized);
        return Quaternion.RotateTowards(transform.rotation, b, max_angle_change);
    }

    public string NumToRead(string number, int style = 0)
    {
        //converts a raw string of numbers into a much nicer format of your choosing
        /* style values:
         * default/0 - Shorthand form (50.00M, 2.00B, 5.00Qa)
         * 1 - Scientific form (5.00E4, 20.00E75)
         */
        string n = number;
        if (number.Contains("."))
        {
            number = number.Substring(0, number.IndexOf("."));
        }
        string final = "";

        string boner = "";
        if (number.Contains("-"))
        {
            boner = "-";
            number = number.Substring(1);
        }
        if (number.Length > 3)
        {
            switch (style)
            {
                case 0:

                    int baller = (number.Length - 1) / 3;
                    int bbbb = baller;
                    baller--;
                    int baller2 = baller;
                    baller2 /= 10;
                    int baller3 = baller2;
                    baller3 /= 10;
                    baller3 *= 10;
                    baller2 *= 10;
                    baller -= baller2;
                    baller2 /= 10;
                    baller2 -= baller3;
                    baller3 /= 10;
                    List<string> bingle = new List<string>()
                    {
                       "","K","M","B","T","Qa","Qn","Sx","Sp","Oc","No",
                    };
                    List<string> bingle2 = new List<string>()
                    {
                       "","De","Vt","Tg","Qa","Qn","Sx","Sp","Oc","No",
                    };
                    List<string> bingle3 = new List<string>()
                    {
                        "","Ce"
                    };
                    if (baller2 > 0)
                    {
                        bingle[1] = "U";
                        bingle[2] = "D";
                        bingle.RemoveAt(3);
                    }
                    else
                    {
                        baller++;
                    }
                    if (baller3 > 1)
                    {
                        bingle3[1] = bingle3[1] + baller3;

                        baller3 = 1;
                    }
                    final = bingle[baller] + bingle2[baller2] + bingle3[Math.Clamp(baller3, 0, 1)];
                    int g = bbbb * 3;
                    string n2 = number.Substring(number.Length - g, 2);
                    string n1 = number.Substring(0, number.Length - g);
                    n = boner + n1 + "." + n2 + final;
                    break;
                case 1:
                    //scientifi
                    string gamerrr = (number.Length - 1).ToString();
                    string n22 = number.Substring(1, 3);
                    string n11 = number.Substring(0, 1);
                    n = boner + n11 + "." + n22 + "E" + gamerrr;
                    break;
            }
        }

        return n;
    }

    public string TimeToRead(string ine)
    {
        var g = System.Numerics.BigInteger.Parse(ine);
        string outp = "";
        bool fall = false;
        if ((g / 31536000) > 0)
        {
            outp += (g / 31536000) + "y ";
            g %= 31536000;
            fall = true;
        }
        if ((g / 604800) > 0 || fall)
        {
            outp += (g / 604800) + "w ";
            g %= 604800;
            fall = true;
        }
        if ((g / 86400) > 0 || fall)
        {
            outp += (g / 86400) + "d ";
            g %= 86400;
            fall = true;
        }
        if ((g / 3600) > 0 || fall)
        {
            outp += (g / 3600) + "h ";
            g %= 3600;
            fall = true;
        }
        if ((g / 60) > 0 || fall)
        {
            outp += (g / 60) + "m ";
            g %= 60;
        }
        outp += g.ToString() + "s";

        return outp;
    }


    public long GetUnixTime(int type = -1)
    {
        //returns the curret unix time
        /* Type values:
         * default - seconds
         * 0 - miliseconds
         * 1 - seconds
         * 2 - minutes
         * 3 - hours
         * 4 - days
         * 1 - seconds
         */
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        var ah = (System.DateTime.UtcNow - epochStart);
        long cur_time = -1;
        switch (type)
        {
            case 0:
                cur_time = (long)ah.TotalMilliseconds;
                break;
            case 1:
                cur_time = (long)ah.TotalSeconds;
                break;
            case 2:
                cur_time = (long)ah.TotalMinutes;
                break;
            case 3:
                cur_time = (long)ah.TotalHours;
                break;
            case 4:
                cur_time = (long)ah.TotalDays;
                break;
            default:
                cur_time = (long)ah.TotalSeconds;
                break;
        }
        return cur_time;
    }
    public void DisconnectFromMatch()
    {
        //NetworkManager.Singleton.Shutdown();
    }
    public float Dist(Vector3 p1, Vector3 p2)
    {
        float distance = Mathf.Sqrt(
                Mathf.Pow(p2.x - p1.x, 2f) +
                Mathf.Pow(p2.y - p1.y, 2f) +
                Mathf.Pow(p2.z - p1.z, 2f));
        return distance;
    }

    public bool CheckInsideRect(RectTransform area, Vector3 mp, Camera cam, float extrascaler = 1f)
    {
        float scaler = cam.pixelHeight / 750f;
        var r = area.rect;
        bool g = false;
        var gg = cam.WorldToScreenPoint(area.position);
        r.min *= scaler * extrascaler;
        r.max *= scaler * extrascaler;
        if ((gg.x + r.min.x) < mp.x && (gg.y + r.min.y) < mp.y)
        {
            if ((gg.x + r.max.x) > mp.x && (gg.y + r.max.y) > mp.y)
            {
                g = true;
            }
        }
        return g;
    }

    public int Mod(int r, int max)
    {
        return ((r % max) + max) % max;
    }
    private Quaternion PointAtPoint(Vector3 start_location, Vector3 location)
    {
        Quaternion _lookRotation =
            Quaternion.LookRotation((location - start_location).normalized);
        return _lookRotation;
    }
    private Quaternion RotateLock(Quaternion start_rot, Quaternion target, float max_speed)
    {
        return Quaternion.RotateTowards(start_rot, target, max_speed);
    }

    private Quaternion Point2D(float offset2, float spread)
    {
        //returns the rotation required to make the current gameobject point at the mouse, untested in 3D.
        var offset = UnityEngine.Random.Range(-spread, spread);
        offset += offset2;
        //Debug.Log(offset);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var sex = Quaternion.Euler(0f, 0f, rotation_z + offset);
        return sex;
    }

    private Quaternion PointAtPoint2D(Vector3 location, float spread)
    {
        // a different version of PointAtPoint with some extra shtuff
        //returns the rotation the gameobject requires to point at a specific location
        var offset = UnityEngine.Random.Range(-spread, spread);

        //Debug.Log(offset);
        Vector3 difference = location - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var sex = Quaternion.Euler(0f, 0f, rotation_z + offset);
        return sex;
    }
    public string GenerateID()
    {
        //generates an id
        List<string> bpp = new List<string>()
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","0","1","2","3","4","5","6","7","8","9","a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
        };

        string e = "";
        for (int i = 0; i < 15; i++)
        {
            e = e + bpp[UnityEngine.Random.Range(0, bpp.Count)];
        }
        return e;
    }
}

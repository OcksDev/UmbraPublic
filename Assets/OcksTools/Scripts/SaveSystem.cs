using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public int SaveFile = 0;
    private string UniqueGamePrefix = "gad";
    private int test = 0;

    public bool UseFileSystem = true;
    public Dictionary<string, string> ProfileData = new Dictionary<string, string>();
    public List<string> codes = new List<string>();
    public Dictionary<int, int> ShopData = new Dictionary<int, int>();

    public static SaveSystem Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance == null) instance = this;
    }
    private void Start()
    {
        TimeTime = RandomFunctions.Instance.GetUnixTime();
        LoadGame();
    }

    public long TimeTime = 0;
    public void ChangeFile(int i = 0)
    {
        SaveFile = i;
        LoadGame(i);
    }

    private int filenum = -1;
    public void LoadGame(int i = -1)
    {
        /* Input Modes:
         * -1 = Load whatever was the last used file (if being used for the first time it defaults to 0)
         * Any Other Value = Load the data of a specific file
         */

        var g = Gamer.Instance;

        InputManager.AssembleTheCodes();
        List<string> list = new List<string>();



        if (i == -1)
        {
            var xx = PlayerPrefs.GetInt("SaveFile", -1);
            if (xx != -1)
            {
                SaveFile = xx;
            }
            i = SaveFile;
        }
        filenum = i;
        var s = SoundSystem.Instance;
        GameFilePath = $"{FileSystem.Instance.GameDirectory}\\{GameDataFileName}_{filenum}.txt";
        FileSystem.Instance.WriteFile(GameFilePath, "", false);

        int x = 0;
        UpdateUniversalData();



        string def2 = "Guest" + UnityEngine.Random.Range(0, 9999);

        if (!FileSystem.Instance.UniversalData.ContainsKey("Username"))
        {
            var pd = $"{FileSystem.Instance.UniversalDirectory}\\Player_Data.txt";
            SaveString("Username", def2, pd);
            UpdateUniversalData();
        }


        var ghghgg = PlayerPrefs.GetString("keybinds", "fuck");
        list = StringToList(ghghgg);
        if (ghghgg != "fuck")
        {
            foreach (var a in list)
            {
                try
                {
                    var sseexx = StringToList(a, "<K>");
                    InputManager.gamekeys[sseexx[0]] = InputManager.namekeys[sseexx[1]];
                    x++;
                }
                catch
                {
                }
            }
        }
        x = 0;

        g.Highlights = float.Parse(LoadString("Highlights", "0.5"));
        g.Lowlights = float.Parse(LoadString("Lowlights", "0.5"));

        g.XP = long.Parse(LoadString("XP", "0"));
        g.LastCheckedLevel = int.Parse(LoadString("LVLC", "1"));

        var sex = LoadString("Achievements", "");
        list.Clear();
        if (sex != "")
        {
            g.Achievements = FileSystem.Instance.StringToBoolArray(sex);
        }

        g.CanBeBoozaled = "true" == PlayerPrefs.GetString("boz", "false");


        g.GetUnlockedCards();

        ValidateCodes();
        sex = LoadString("Saved_Decks", "");
        g.LoadedDecks = new List<List<string>> { 
            //defauly/perm decks
            new List<string>() { "Classic", "1, 1, 1, 1, 2, 2, 2, 4, 4, 4, 1, 3, 3, 3, 5, 5, 5, 14, 14, 15, 8, 8, 9, 7, 10, 10, 10, 12, 11" },
        };
        if (sex == "")
        {
            var f = new List<List<string>>
            {
                new List<string>() { "Unnamed", "1" }
            };
            foreach (var a in f)
            {
                g.LoadedDecks.Add(a);
            }
        }
        else
        {
            var k2 = StringToList(sex, " |IAmDoingThisToStopPeopleFromCrashing| ");
            var k1 = new List<List<string>>();
            foreach (var k in k2)
            {
                k1.Add(StringToList(k, " ~IAmDoingThisToStopPeopleFromCrashing~ "));
            }
            foreach (var a in k1)
            {
                var e = a;
                e[1] = g.CleanDeck(e[1]);
                g.LoadedDecks.Add(e);
            }
        }

        GetShopData();


        sex = LoadString("Profile_Data", "");
        var ee = new Dictionary<string, string>()
            {
                { "Games Played", "0" },
                { "Games Won", "0" },
                { "Games Lost", "0" },
                { "PVP Games Played", "0" },
                { "PVP Games Won", "0" },
                { "PVP Games Lost", "0" },
                { "Title", "0" },
                { "Cards Played", "0" },
                { "Time Played", "0" },
                { "Game Time Played", "0" },
                { "Secrets", "0" },
            };
        if (sex == "")
        {
            ProfileData = ee;
        }
        else
        {
            ProfileData = StringToDictionary(sex);
            foreach (var kk in ee)
            {
                if (!ProfileData.ContainsKey(kk.Key))
                {
                    ProfileData.Add(kk.Key, kk.Value);
                }
            }
        }
        if (ProfileData.ContainsKey("Title"))
        {
            g.SelectedTitle = int.Parse(ProfileData["Title"]);
        }
        else
        {
            g.SelectedTitle = 0;
        }


        if (s != null)
        {
            s.MasterVolume = float.Parse(LoadString("Master_Volume", "1"));
            s.SFXVolume = float.Parse(LoadString("SFX_Volume", "1"));
            s.MusicVolume = float.Parse(LoadString("Music_Volume", "1"));
        }

        test = PlayerPrefs.GetInt(Prefix(i) + "test_num", 0);
        //ConsoleLol.Instance.ConsoleLog(Prefix(i) + "test_num");

        g.UpdateShaders();

        g.miscrefs[4].GetComponent<MainMenuShit>().FardStart();
        SaveFile = i;
    }
    public void UpdateUniversalData()
    {
        var data = StringToList(FileSystem.Instance.ReadFile($"{FileSystem.Instance.UniversalDirectory}\\Player_Data.txt"), Environment.NewLine);
        Dictionary<string, string> sexymuscleactionmangerardbutler = new Dictionary<string, string>();
        foreach (var k in data)
        {
            if(k.IndexOf(": ") > -1)sexymuscleactionmangerardbutler.Add(k.Substring(0, k.IndexOf(": ")), k.Substring(k.IndexOf(": ") + 2));
        }

        FileSystem.Instance.UniversalData = sexymuscleactionmangerardbutler;

    }

    public void SaveGame(int i = -1)
    {
        /* Input Modes:
         * -1 = Save whatever is the currently selected file (by default is 0)
         * Any Other Value = Save curent data to a specfic file
         */
        var g = Gamer.Instance;

        List<string> list = new List<string>();
        if (i == -1)
        {
            PlayerPrefs.SetInt("SaveFile", SaveFile);
            i = SaveFile;
        }
        filenum = i;
        var s = SoundSystem.Instance;

        //"name ~ 1,1,2,3,4 | name2 ~ 2,3,4,5"

        var pd = $"{FileSystem.Instance.UniversalDirectory}\\Player_Data.txt";
        SaveString("Username", FileSystem.Instance.UniversalData["Username"], pd);

        var k1 = new List<string>();
        string k2 = "";
        //z = 1 to skip the first deck becuase Classic should not be modify-able
        for (int z = 1; z < g.LoadedDecks.Count; z++)
        {
            k1.Add(ListToString(g.LoadedDecks[z], " ~IAmDoingThisToStopPeopleFromCrashing~ "));
        }
        k2 = ListToString(k1, " |IAmDoingThisToStopPeopleFromCrashing| ");
        SaveString("Saved_Decks", k2);
        var iiii = RandomFunctions.Instance.GetUnixTime();



        SaveString("Highlights", g.Highlights.ToString());
        SaveString("Lowlights", g.Lowlights.ToString());
        SaveString("XP", g.XP.ToString());
        SaveString("LVLC", g.LastCheckedLevel.ToString());

        SaveSystem.Instance.ProfileData["Time Played"] = (long.Parse(SaveSystem.Instance.ProfileData["Time Played"]) + (iiii - TimeTime)).ToString();


        TimeTime = iiii;

        SaveString("Profile_Data", DictionaryToString(ProfileData));

        SaveString("Achievements", FileSystem.Instance.BoolArrayToString(g.Achievements));

        PlayerPrefs.SetString("boz", "true");

        list.Clear();
        foreach (var a in InputManager.gamekeys)
        {
            list.Add(a.Key + "<K>" + InputManager.keynames[a.Value]);
        }
        PlayerPrefs.SetString("keybinds", ListToString(list));
        //PlayerPrefs.SetInt("UnitySelectMonitor", index); // sets the monitor that unity uses

        if (s != null)
        {
            SaveString("Master_Volume", s.MasterVolume.ToString());
            SaveString("SFX_Volume", s.SFXVolume.ToString());
            SaveString("Music_Volume", s.MusicVolume.ToString());
        }
        SaveString("Codes", ListToString(codes));

        PlayerPrefs.SetInt(Prefix(i) + "test_num", test);
    }
    public List<string> valid_codes = new List<string>();

    public void ValidateCodes()
    {
        var g = Gamer.Instance;
        g.secretplayerstuff = new bool[20];
        SetValidCodes();
        codes = StringToList(LoadString("Codes", ""));
        foreach (var code in codes)
        {
            int i = 0;
            foreach (var c in valid_codes)
            {
                if (code.ToLower() == valid_codes[i].ToLower())
                {
                    g.secretplayerstuff[i] = true;
                }
                i++;
            }
        }
    }

    public void GetShopData()
    {
        var sex = LoadString("Shop_Data", "");
        if (sex != "")
        {
            var gsex = StringToDictionary(sex);
            ShopData.Clear();
            foreach (var e in gsex)
            {
                ShopData.Add(int.Parse(e.Key), int.Parse(e.Value));
            }
        }
    }

    public string GameDataFileName = "Game_Data";
    public void SaveString(string key, string data = "", string filepath = "")
    {
        if (UseFileSystem)
        {
            var f = FileSystem.Instance;
            GameFilePath = $"{f.GameDirectory}\\{GameDataFileName}_{filenum}.txt";
            if (filepath != "")
            {
                GameFilePath = filepath;
            }
            if (!File.Exists(GameFilePath)) f.WriteFile(GameFilePath, "", false);

            var s = StringToList(f.ReadFile(GameFilePath), Environment.NewLine);
            Dictionary<string, string> data2 = new Dictionary<string, string>();
            foreach (var k in s)
            {
                if (k.Contains(": "))
                {
                    data2.Add(k.Substring(0, k.IndexOf(": ")), k.Substring(k.IndexOf(": ") + 2));
                }
            }


            if (data2.ContainsKey(key))
            {
                data2[key] = data;
            }
            else
            {
                data2.Add(key, data);
            }
            f.WriteFile(GameFilePath, DictionaryToString(data2, Environment.NewLine, ": "), true);
        }
        else
        {
            PlayerPrefs.SetString(key, data);
        }
    }
    public string GameFilePath = "";

    public string LoadString(string key, string defaul = "", string filepath = "")
    {
        if (UseFileSystem)
        {
            var f = FileSystem.Instance;
            GameFilePath = $"{f.GameDirectory}\\{GameDataFileName}_{filenum}.txt";
            if (filepath != "")
            {
                GameFilePath = filepath;
            }
            if (File.Exists(GameFilePath))
            {
                var s = StringToList(f.ReadFile(GameFilePath), Environment.NewLine);
                Dictionary<string, string> data = new Dictionary<string, string>();
                foreach (var k in s)
                {
                    if(k.IndexOf(": ") > -1)data.Add(k.Substring(0, k.IndexOf(": ")), k.Substring(k.IndexOf(": ") + 2));
                }


                if (data.ContainsKey(key))
                {
                    return data[key];
                }
                else
                {
                    return defaul;
                }
            }
            else
            {
                return defaul;
            }
        }
        else
        {
            return PlayerPrefs.GetString(key, defaul);
        }
    }

    public string Prefix(int file)
    {
        if (file == -1) file = SaveFile;
        return UniqueGamePrefix + "#" + file + "_";
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public int BoolToInt(bool a)
    {
        return a ? 1 : 0;
    }
    public bool IntToBool(int a)
    {
        return a == 1;
    }

    public string ListToString(List<string> eee, string split = ", ")
    {
        return String.Join(split, eee);
    }

    public List<string> StringToList(string eee, string split = ", ")
    {
        return eee.Split(split).ToList();
    }

    public string DictionaryToString(Dictionary<string, string> dic, string splitter = ", ", string splitter2 = "<K>")
    {
        List<string> list = new List<string>();
        foreach (var a in dic)
        {
            list.Add(a.Key + splitter2 + a.Value);
        }
        return ListToString(list, splitter);
    }
    public Dictionary<string, string> StringToDictionary(string e, string splitter = ", ", string splitter2 = "<K>")
    {
        var dic = new Dictionary<string, string>();
        var list = StringToList(e, splitter);
        foreach (var a in list)
        {
            try
            {
                var sseexx = StringToList(a, splitter2);
                if (dic.ContainsKey(sseexx[0]))
                {
                    dic[sseexx[0]] = dic[sseexx[1]];
                }
                else
                {
                    dic.Add(sseexx[0], sseexx[1]);
                }
            }
            catch
            {
            }
        }
        return dic;
    }

    public void SetValidCodes()
    {
        valid_codes = new List<string>()
        {
            "sourcegear420",
            "bpm2007",
            "sex",
            "mc gaming time",
            "baller",
"sexballsfuck0658020283656009017230980493560237602817219685186277jghgkfjd24387269384729638716987qwertyuiop[][]lkjhgfdsaxcvbnm(eatmyass)69",
"fork726948723968719643lift69420certifiededed===+noballs+l+ratioed",
        };
    }
}

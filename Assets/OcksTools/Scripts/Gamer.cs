
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Gamer : MonoBehaviour
{
    public const int ArraySize = 25;
    public Card[,] turf = new Card[ArraySize, ArraySize];
    public Card[,] enemyturf = new Card[ArraySize, ArraySize];

    public bool DevMode = false;
    public bool CanBeBoozaled;

    public GameObject[] miscrefs;
    public bool Multiplayer;
    public string GameState = "Main Menu";
    public bool GameOver = false;
    public bool[] checks = new bool[50];
    public int Turncount = 0;
    public int AIDifficulty = 0;

    public float gameVer = 1f;
    public float Health = 100f;
    public float Enemyhealth = 100f;
    public float MaxHealth = 100f;
    public float MaxEnemyhealth = 100f;
    public int PLayerEnergy = 0;
    public int AIEnergy = 0;
    public int EnergyPerRound = 5;
    public bool PlayerWon = false;
    public int HeavyPlayer = -1;
    public int HeavyAi = -1;
    public float TimeBetweenCards = 0;
    public int CardStreak = 0;
    [SerializeField]
    public Card PlayerLastCard = null;
    public Card AILastCard = null;
    public Card LastCardTrue = null;
    public Card AILastCardTrue = null;
    public int SameCardStreak = 0;
    public int SweepCount = 0;

    public bool AllowIllegalImmigration = false;
    public bool Junkyard = false;
    public bool DoubleEnergy = false;
    public int HandSizeSel = 7;

    public int Level = 0;
    public int LastCheckedLevel = 0;
    public long XP = 0;
    public long RemainingXP = 0;
    public long ThreshholdXP = 0;
    public List<List<string>> LoadedDecks = new List<List<string>>();

    public int SelectedDeck = 0;
    public GameObject Placer;
    public Card PlacerCard;
    public bool PlacerState = false;
    public bool DiscardState = false;
    public bool FreezeState = false;
    public float PlacerHeight = 1;
    public float PlacerWidth = 1;
    public float PlacerCooldown = 0f;
    public Color32[] colors;
    public int PlayerOnFire = -1;
    public int AIOnFire = -1;
    public bool FeebleOverload = false;
    public bool HoleOverload = false;
    public bool BanishDiscardOverload = false;

    public PLayerHandywandy LocalHandJob;
    public List<PLayerHandywandy> AllHandJobs;

    public string[] CardNames;
    public string[] CardDescriptions;
    public string[] CardLongDescriptions;
    public Sprite[] CardImages;
    public bool[] CardUnlockedWithlevels;
    public int[] CardLevelUnlock;

    public string[] AchievementNames;
    public string[] AchievementDesc;
    public string[] AchievementReward;
    public bool[] Achievements = new bool[69];

    public Color[] AttributeColor;
    public string[] AttributeName;
    public string[] AttributeDesc;

    public List<string> Titles = new List<string>();

    public Sprite[] Construct_Images;

    public Vector3 MousePos = new Vector3(0, 0, 0);
    public Vector3 MousePosMain = new Vector3(0, 0, 0);
    public Vector3 MousePosMainUnScaled = new Vector3(0, 0, 0);

    public long GameTimeTime = -1;
    public long TimeUntilShopReset = -1;

    public Deck dick;
    public Deck AIDeck;
    public Camera cam;
    public Camera cam2;
    private SpriteRenderer pplooker;
    private TextMeshProUGUI homie;
    public Color32[] homie_colors;
    public Color32[] main_colors;
    public Color32[] diff_colors;
    private RectTransform rectum;
    private Button buttsex;
    private TextMeshProUGUI dolphin;
    public string MultiMoveData = "";
    public bool MutliMoveForward = false;
    public string hoveringonmyballsinyourjaw = "";


    public int DisplayType = -1;
    public string DisplayData = "";

    public bool IsPlayerTurn = true;
    public bool IsPlayerTurnWait = true;
    public List<Card> PlayerCons = new List<Card>();
    public List<Card> AICons = new List<Card>();

    public Material[] mats = null;

    public float Highlights = 0.5f;
    public float Lowlights = 0.5f;


    public bool[] secretplayerstuff = new bool[10];
    public Volume volume;
    public bool[] CardUnlocked;

    public Dictionary<int, List<int>> ConstructSizes = new Dictionary<int, List<int>>();

    public static Gamer instance;
    public static Gamer Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance == null) instance = this;
    }


    // Start is called before the first frame update


    void Start()
    {
        cam = miscrefs[0].GetComponent<Camera>();
        cam2 = miscrefs[16].GetComponent<Camera>();

        //default game params

        SelectedDeck = 0;
        SelectedDeckDos = 0;

        AiDiffSelect = 0;
        AllowIllegalImmigration = false;
        Junkyard = false;
        HandSizeSel = 7;

        pplooker = Placer.GetComponent<SpriteRenderer>();
        MainMenu();
    }
    public void ClearMap()
    {
        foreach (var con in miscrefs[39].GetComponentsInChildren<SpawnData>())
        {
            Destroy(con.gameObject);
        }
        foreach (var pr in miscrefs[13].GetComponentsInChildren<SpawnData>())
        {
            Destroy(pr.gameObject);
        }
        foreach (var con in miscrefs[42].GetComponentsInChildren<SpawnData>())
        {
            Destroy(con.gameObject);
        }
    }
    public int HandSizelol;
    public void StartGame()
    {
        GameState = "Game";
        dick = new Deck();
        AIDeck = new Deck();
        Health = 100f;
        Enemyhealth = 100f;
        MaxHealth = 100f;
        MaxEnemyhealth = 100f;
        Turncount = 1;
        GameOver = false;
        //AllowIllegalImmigration = false;
        PlacerCard = null;
        CardPlayed = null;
        PlacerState = false;
        DiscardState = false;
        AIDifficulty = AiDiffSelect - 1;
        EnergyPerRound = 5;
        if (DoubleEnergy)
        {
            EnergyPerRound = 10;
        }
        PlayerLastCard = null;
        AILastCard = null;
        IsPlayerTurn = true;
        PLayerEnergy = EnergyPerRound;
        AIEnergy = 0;
        youtookdamage = false;

        SameCardStreak = 0;
        CardStreak = 0;
        LastCardTrue = null;
        SweepCount = 0;

        foreach (var con in PlayerCons)
        {
            if (con.Construct != null) Destroy(con.Construct);
            if (con.Display != null) Destroy(con.Display);
        }
        var ps = miscrefs[18].GetComponentsInChildren<CardDisplay>();
        foreach (var card in ps)
        {
            Destroy(card.gameObject);
        }

        foreach (var con in AICons)
        {
            if (con.Construct != null) Destroy(con.Construct);
        }
        turf = new Card[ArraySize, ArraySize];
        enemyturf = new Card[ArraySize, ArraySize];

        PlayerCons.Clear();
        AICons.Clear();
        dick.ClearHand();
        AIDeck.ClearHand();

        miscrefs[65].SetActive(false);
        miscrefs[66].SetActive(false);


        AssembleGameData();

        dick.LoadDeck(LoadedDecks[SelectedDeck][1], true);

        if (!Multiplayer)
        {
            AIDeck.LoadDeck(LoadedDecks[SelectedDeckDos][1], true);
            //AIDeck.LoadDeck("1, 14, 3", true);
        }
        int handsize = HandSizeSel;
        HandSizelol = handsize;
        //player hand draw
        DrawCards(handsize);

        //ai hand draw
        supposed_ai_dick = handsize + AIDifficulty;
        if (!Multiplayer)
        {
            for (int i = 0; i < supposed_ai_dick; i++)
            {
                AIDeck.Hand.Add(AIDeck.DrawCard());
            }
        }
        miscrefs[1].transform.position = new Vector3(-MapX, -Mathf.Ceil((float)MapY / 2) + 1, 0);
        miscrefs[2].transform.position = new Vector3(1, -Mathf.Ceil((float)MapY / 2) + 1, 0);
        ClearMap();
        camseize = (((float)MapX * 2) + 5) / 17 * 5.07f;
        var p = (((float)MapY) + 6) / 5 * 2.5f;
        if (p > camseize) camseize = p;
        miscrefs[16].GetComponent<Camera>().orthographicSize = camseize;
        miscrefs[20].transform.localScale = new Vector3(MapX, MapY, 1);
        miscrefs[21].transform.localScale = new Vector3(MapX, MapY, 1);
        miscrefs[20].transform.localPosition = new Vector3((miscrefs[1]).transform.position.x + ((float)MapX / 2) - 0.5f, 0, 0);
        miscrefs[21].transform.localPosition = new Vector3((miscrefs[3]).transform.position.x + ((float)MapX / 2) - 0.5f, 0, 0);

        if (Multiplayer && !NetworkManager.Singleton.IsHost)
        {
            PLayerEnergy = 0;
            EndTurnCheck();
        }


        miscrefs[94].GetComponent<ITurnCountShitBitch>().UpdateColors(IsPlayerTurn);

    }

    public void AddCardTohand()
    {
        Card p = null;
        if(determined_draw_cards.Count > 0)
        {
            p = new Card(determined_draw_cards[0]);
            determined_draw_cards.RemoveAt(0);
        }
        else
        {
            p = dick.DrawCard();
        }
        if (p != null)
        {
            p.IsPlayerControlled = true;
            dick.Hand.Add(p);
            var g = RandomFunctions.Instance.SpawnObject(1, miscrefs[18], new Vector3(17.5f, -10, 0), transform.rotation, false, "");
            g.GetComponent<SpawnData>().card = p;
            p.Display = g;
        }
    }

    public const float DefaultDelay = 0.1f;
    public const float DefaultStartDelay = 0f;
    public List<int> determined_draw_cards = new List<int>();
    public void DrawCards(int amount, float delay = DefaultDelay, float start_delay = DefaultStartDelay)
    {
        StartCoroutine(DrawCardsCor(amount, delay, start_delay));
    }
    public bool DrawingCards = false;
    public IEnumerator DrawCardsCor(int amount, float delay = 0.2f, float start_delay = 0f)
    {
        DrawingCards = true;
        if (start_delay > 0) yield return new WaitForSeconds(start_delay);
        for (int i = 0; i < amount; i++)
        {
            AddCardTohand();
            if (delay > 0) yield return new WaitForSeconds(delay);
        }
        DrawingCards = false;
        determined_draw_cards.Clear();
        yield return null;
    }

    public IEnumerator WaitForEndDrawCardsShit()
    {
        if (IsPlayerTurn)
        {
            if (DrawingCards)
            {
                yield return new WaitUntil(() => { return !DrawingCards; });
            }
            bool shitted = false;
            foreach (var card in dick.Hand)
            {
                if (CanPlayCard(card))
                {
                    shitted = true;
                    break;
                }
            }
            if (!shitted)
            {
                CardPlayed = null;
                PlacerCard = null;
                EndTurnCheck(false, false, true);
            }
            yield return null;
        }
    }


    public void Skip()
    {
        CardPlayed = null;
        PlacerCard = null;
        EndTurnCheck(true);
    }

    // Update Update Update Update Update Update Update Update Update is called once per frame
    void Update()
    {


        /* dev keybinds
        if (InputManager.IsKeyDown(KeyCode.Space))
        {
            GetAchievement(0);
            GetAchievement(1);
            GetAchievement(2);
        }
        if (InputManager.IsKeyDown(KeyCode.W))
        {
            XP += 1000000;
            GetLevel();
        }
        if (InputManager.IsKeyDown(KeyCode.A))
        {
            PlayerWon = true;
            ToEnd();
        }
        */



        PlacerCooldown -= Time.deltaTime;
        if (InputManager.IsKeyDown(InputManager.gamekeys["close_menu"]))
        {
            if (PlacerState)
            {
                PlacerState = false;
                PlacerCard = null;
            }
            else if (DiscardState)
            {
                DiscardState = false;
                PlacerCard = null;
            }
            else if (FreezeState)
            {
                FreezeState = false;
                PlacerCard = null;
            }
            else if (checks[14])
            {
                TogglePickerMenu();
            }
            else if (checks[1])
            {
                ToggleSettingsMenu();
            }
            else if (checks[9])
            {
                ToggleProfileMenu();
            }
            else if (checks[11])
            {
                ToggleAchiOverlay();
            }
            else if (checks[13])
            {
                ToggleLogbookOvergay();
            }
            else if (checks[12])
            {
                ToggleLogbook();
            }
            else if (checks[8])
            {
                ToggleCreditsMenu();
            }
            else if (GameState == "Game")
            {
                TogglePauseMenu();
            }
            else if (checks[2])
            {
                MainMenu();
            }
            else if (checks[6])
            {
                MainMenu();
            }
            else if (checks[10])
            {
                MainMenu();
            }
            else if (GameState == "Main Menu")
            {
                ToggleSettingsMenu();
            }
        }
        /*
        if (InputManager.IsKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < PlayerCons.Count;)
            {
                var f = PlayerCons[0];
                if (f != null)
                {
                    f.Health = -1;
                    f.CheckDie();
                }
                else
                {
                    PlayerCons.RemoveAt(0);
                }
            }
        }*/

        if (PlacerState && InputManager.IsKeyDown(InputManager.gamekeys["shoot"]) && PlacerCooldown < 0)
        {
            var p1 = MousePos;
            p1.z = 0;
            var p2 = Placer.transform.position;
            p2.z = 0;
            if (Dist(p1, p2) <= 1f)
                PlacePlacer();
        }

        if (GameState == "Game")
        {
            if (InputManager.IsKeyDown(InputManager.gamekeys["1"]) && dick.Hand.Count >= 1) dick.Hand[1 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["2"]) && dick.Hand.Count >= 2) dick.Hand[2 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["3"]) && dick.Hand.Count >= 3) dick.Hand[3 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["4"]) && dick.Hand.Count >= 4) dick.Hand[4 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["5"]) && dick.Hand.Count >= 5) dick.Hand[5 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["6"]) && dick.Hand.Count >= 6) dick.Hand[6 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["7"]) && dick.Hand.Count >= 7) dick.Hand[7 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["8"]) && dick.Hand.Count >= 8) dick.Hand[8 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["9"]) && dick.Hand.Count >= 9) dick.Hand[9 - 1].Display.GetComponent<CardDisplay>().Clickity();
            if (InputManager.IsKeyDown(InputManager.gamekeys["0"]) && dick.Hand.Count >= 10) dick.Hand[10 - 1].Display.GetComponent<CardDisplay>().Clickity();
        }


    }
    public bool AchieveAvailable = true;
    public IEnumerator ShexMyAchievement(Notification notif)
    {
        skipitdy = false;
        var index = notif.Data;
        var type = notif.Type;
        AchieveAvailable = false;
        miscrefs[70].SetActive(true);
        miscrefs[70].transform.position = miscrefs[74].transform.position;

        miscrefs[71].SetActive(true);
        miscrefs[72].SetActive(true);
        miscrefs[80].SetActive(false);
        miscrefs[81].SetActive(false);

        switch (type)
        {
            case 1:
                miscrefs[71].SetActive(false);
                miscrefs[72].SetActive(false);
                miscrefs[80].SetActive(true);
                miscrefs[81].SetActive(true);
                miscrefs[81].GetComponent<TextMeshProUGUI>().text = (index - 1) + "   >>>   " + index;
                break;
            default:
                miscrefs[71].GetComponent<TextMeshProUGUI>().text = $"Achievement Unlocked: {AchievementNames[index]}";
                miscrefs[72].GetComponent<TextMeshProUGUI>().text = AchievementDesc[index];
                break;
        }


        float steps = 50;
        for (int i = 0; i < steps; i++)
        {
            miscrefs[70].transform.position = Vector3.Lerp(miscrefs[70].transform.position, miscrefs[73].transform.position, 0.1f);
            yield return new WaitForFixedUpdate();
            if (skipitdy) goto skipballs;
        }
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForFixedUpdate();
            if (skipitdy) goto skipballs;
        }
        for (int i = 0; i < 30; i++)
        {
            miscrefs[70].transform.position = Vector3.Lerp(miscrefs[70].transform.position, miscrefs[74].transform.position, 0.1f);
            yield return new WaitForFixedUpdate();
            if (skipitdy) goto skipballs;
        }

    skipballs:;

        miscrefs[70].SetActive(false);
        AchieveAvailable = true;
    }
    bool skipitdy = false;
    public void SkipAcheieve()
    {
        skipitdy = true;
    }

    public IEnumerator WinLevelUpAnim(long StartXp, long EndXP)
    {
        GetLevel(StartXp);
        var tit = miscrefs[82].GetComponent<TextMeshProUGUI>();
        double perc = 0;
        var re = miscrefs[84].GetComponent<TextMeshProUGUI>();
        var te = miscrefs[85].GetComponent<TextMeshProUGUI>();
        tit.text = "Level: " + Level;
        perc = (double)RemainingXP / (double)ThreshholdXP;
        miscrefs[83].transform.localScale = new Vector3((float)perc, 1, 1);
        re.text = RemainingXP.ToString() + " xp";
        te.text = ThreshholdXP.ToString() + " xp";

        yield return new WaitForSeconds(1);
        while (StartXp < EndXP)
        {
            StartXp += 2;
            StartXp =  System.Math.Clamp(StartXp, 0, EndXP);
            GetLevel(StartXp);
            tit.text = "Level: " + Level;
            perc = (double)RemainingXP / (double)ThreshholdXP;
            miscrefs[83].transform.localScale = new Vector3((float)perc, 1, 1);
            re.text = RemainingXP.ToString() + " xp";
            te.text = ThreshholdXP.ToString() + " xp";
            yield return new WaitForFixedUpdate();
        }

    }

    public int MapX;
    public int MapY;
    public float camseize;
    public int lastnumcheck = -1;
    bool canupdatemysex = false;
    private void FixedUpdate()
    {
        float gg = camseize / 10f;
        MousePosMainUnScaled = Input.mousePosition;
        //Debug.Log(MousePosMainUnScaled.x + ", " + MousePosMainUnScaled.y);
        MousePosMain = cam.ScreenToWorldPoint(Input.mousePosition);
        MousePos = (MousePosMain * gg) + miscrefs[16].transform.position;

        if(hoveringonmyballsinyourjaw != "")
        {
            miscrefs[97].SetActive(true);
            miscrefs[98].GetComponent<TextMeshProUGUI>().text = hoveringonmyballsinyourjaw;
            hoveringonmyballsinyourjaw = "";
        }
        else
        {
            miscrefs[97].SetActive(false);
        }

        if (canupdatemysex)
        {
            if (GameState != "Game")
            {
                var iiii = RandomFunctions.Instance.GetUnixTime();
                SaveSystem.Instance.ProfileData["Game Time Played"] = (long.Parse(SaveSystem.Instance.ProfileData["Game Time Played"]) + (iiii - GameTimeTime)).ToString();
                canupdatemysex = false;
            }
        }
        else
        {
            if (GameState == "Game")
            {
                GameTimeTime = RandomFunctions.Instance.GetUnixTime();
                canupdatemysex = true;
            }
        }

        TimeBetweenCards -= Time.deltaTime;
        if(TimeBetweenCards <= 0)
        {
            CardStreak = 0;
        }
        if(CardStreak >= 3)
        {
            GetAchievement(6);
        }

        if (checks[5])
        {
            UpdateMyBalls();
        }


        if (GameState == "Game")
        {
            if (!GameOver)
            {
                bool h = false;
                if (!h && (Health <= 0 || Enemyhealth <= 0))
                {
                    PlayerWon = Health > 0;
                    h = true;
                }
                if (h)
                {
                    ToEnd();
                    GameOver = true;
                    GameState = "End";
                }
            }
            if (!checks[7])
            {
                Vector3 pos = MousePos + new Vector3(0.5f, 0.5f, 0);
                pos = new Vector3(Mathf.Floor(pos.x), Mathf.Floor(pos.y), 0);

                var PLayerGrisPos = pos + new Vector3(6, 4);
                var AIGrisPos = pos + new Vector3(-1, 4);
                var e = MousePosMain;
                e.z = miscrefs[4].transform.position.z;

                miscrefs[87].transform.position = e;

                Card cd = null;
                try
                {
                    cd = turf[(int)PLayerGrisPos.x, (int)PLayerGrisPos.y];
                }
                catch
                {

                }
                if (cd == null)
                {
                    try
                    {
                        cd = enemyturf[(int)AIGrisPos.x, (int)AIGrisPos.y];
                    }
                    catch
                    {

                    }
                }

                if (FreezeState && cd != null && cd.CanAttack && !cd.IsPlayerControlled)
                {
                    if (InputManager.IsKey(InputManager.gamekeys["shoot"]))
                    {
                        FreezeState = false;
                        cd.FreezeAmount = 2;
                        CardPlayed.GridPos = cd.GridPos;
                        cd.UpdateDisplay();
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                        
                    }
                    else
                    {
                        miscrefs[91].SetActive(true);
                        miscrefs[91].GetComponent<SpriteRenderer>().size = new Vector2(ConstructSizes[cd.CardType][0], ConstructSizes[cd.CardType][1]);
                        miscrefs[91].transform.position = miscrefs[3].transform.position + new Vector3(cd.GridPos.x, cd.GridPos.y, 0);
                    }
                }
                else
                {
                    miscrefs[91].SetActive(false);
                }



                if (cd != null)
                {
                    miscrefs[87].SetActive(true);

                    var container = miscrefs[87].GetComponent<i_healthpopshjit>();
                    container.Name.text = CardNames[cd.CardType];
                    if (!cd.IsFakeCard)
                    {
                        container.HealthText.text = $"{cd.Health} / {cd.MaxHealth}";
                        container.Bar.localScale = new Vector3(cd.Health / cd.MaxHealth, 1, 1);
                        container.HealthText.gameObject.SetActive(true);
                        container.Bar.gameObject.SetActive(true);
                        container.BarPoop.SetActive(true);
                    }
                    else
                    {
                        container.HealthText.gameObject.SetActive(false);
                        container.Bar.gameObject.SetActive(false);
                        container.BarPoop.SetActive(false);
                    }
                }
                else
                {
                    miscrefs[87].SetActive(false);
                }
            }

            miscrefs[14].transform.localScale = new Vector3(1, Mathf.Clamp(Health / MaxHealth, 0, 1), 1);
            miscrefs[15].transform.localScale = new Vector3(1, Mathf.Clamp(Enemyhealth / MaxEnemyhealth, 0, 1), 1);

            bool zsz = false;
            if (PlacerState)
            {
                pplooker.size = new Vector2(PlacerWidth, PlacerHeight);
                var x = MousePos;
                float pw = PlacerWidth - 1;
                float ph = PlacerHeight - 1;
                float mx = MapX - 1;
                float my = MapY - 1;
                zsz = CheckSidePlace(PlacerCard.CardType);
                var ppp = (zsz ? miscrefs[1] : miscrefs[3]).transform.position;
                x -= ppp;
                x = new Vector3(Mathf.Clamp(x.x, 0 + (pw / 2), mx - (pw / 2)), Mathf.Clamp(x.y, 0 + (ph / 2), my - (ph / 2)), 0);
                x += ppp;
                //var y = new Vector3((int)(x.x - (PlacerWidth/2)), (int)(x.y - (PlacerHeight / 2)), 0);

                //I think you cna add/subtract a vector before this, then put it back after with y variable to shift the grid plane
                var y = new Vector3(Mathf.FloorToInt(x.x + (1 - (PlacerWidth / 2))), Mathf.FloorToInt(x.y + (1 - (PlacerHeight / 2))), 0);
                Placer.transform.position = y + new Vector3(((PlacerWidth - 1) / 2), ((PlacerHeight - 1) / 2), 0);
                GridChecker();
                pplooker.color = CanPlace ? colors[0] : colors[1];
            }
            Placer.SetActive(PlacerState);
            miscrefs[19].SetActive(PlacerState);
            miscrefs[26].SetActive(!(PlacerState || DiscardState || FreezeState));
            miscrefs[43].SetActive((PlacerState || DiscardState || FreezeState));
            var tr = miscrefs[19].transform;
            tr.localScale = new Vector3(MapX, MapY, 1);
            tr.localPosition = new Vector3((zsz ? miscrefs[1] : miscrefs[3]).transform.position.x + ((float)MapX / 2) - 0.5f, 0, 0);
            miscrefs[26].GetComponent<Button>().interactable = IsPlayerTurn;

            if (!checks[7])
            {
                float i = dick.Hand.Count - 1;
                float space = 4;
                float hover_space = 0.3f;
                int xs = 0;
                int hoverindex = -1;
                foreach (var card in dick.Hand)
                {
                    card.HandIndex = xs;
                    if (card.IsHover) hoverindex = xs;
                    card.target_pos = new Vector3(-(i * space / 2) + (space * xs), -6.7f + (Mathf.Sin((Time.time + (0.5f * xs)) * 3) * 0.05f), 0);
                    xs++;
                }
                if (hoverindex >= 0)
                {
                    foreach (var card in dick.Hand)
                    {
                        if (card.HandIndex < hoverindex) card.target_pos += new Vector3(-hover_space, 0);
                        else
                        if (card.HandIndex > hoverindex) card.target_pos += new Vector3(hover_space, 0);
                        else card.target_pos += new Vector3(0, 0.4f, 0);
                    }
                }
                foreach (var card in dick.Hand)
                {
                    card.Display.transform.position = Vector3.Lerp(card.Display.transform.position, card.target_pos, 0.15f);
                }
            }
        }
        if (GameState == "Main Menu")
        {
            if (checks[6])
            {
                buttsex.interactable = CanDeleteDeck();
            }
        }
        if (!canescape && !checks[0])
        {
            int cc = miscrefs[46].transform.childCount;
            canescape = cc > 0;
            lastnumcheck = -1;
        }

        if (canescape && (checks[3] || checks[5] || GameState == "Game") && Multiplayer)
        {
            int cc = miscrefs[46].transform.childCount;
            if (lastnumcheck != cc && cc <= 2)
            {
                if(lastnumcheck > 2)
                {
                    UpdatePlayerNames();
                }
                else
                {
                    if (GameState == "Game" && (cc == 0 || cc == 1))
                    {
                        LostConnection();
                    }
                    else
                    {
                        if (cc == 0)
                        {
                            LostConnection();
                        }
                        else
                        {
                            if (checks[5])
                            {
                                BackToPlay2();
                                UpdatePlayerNames();
                            }
                            else
                            {
                                UpdatePlayerNames();
                            }
                        }
                    }
                }
            }
            lastnumcheck = cc;
        }

        if (TBDAchiev.Count > 0 && AchieveAvailable)
        {
            var i = TBDAchiev[0];
            TBDAchiev.RemoveAt(0);
            StartCoroutine(ShexMyAchievement(i));
        }

    }
    public bool canescape = false;
    public void LostConnection()
    {
        MainMenu();
    }
    public bool CanDeleteDeck()
    {
        return LoadedDecks.Count > 2;
    }

    public void UpdateShaders()
    {
        if (volume.profile.TryGet(out UnityEngine.Rendering.Universal.SplitToning spplit))
        {
            spplit.highlights.Override(new Color(Highlights, Highlights, Highlights));
            spplit.shadows.Override(new Color(Lowlights, Lowlights, Lowlights));
        }
    }

    public void SetSizes()
    {
        ConstructSizes.Clear();
        ConstructSizes.Add(1, new List<int>() { 1, 1 });
        ConstructSizes.Add(2, new List<int>() { 2, 1 });
        ConstructSizes.Add(3, new List<int>() { 2, 1 });
        ConstructSizes.Add(4, new List<int>() { 1, 3 });
        ConstructSizes.Add(6, new List<int>() { 1, 1 });
        ConstructSizes.Add(13, new List<int>() { 2, 1 });
        ConstructSizes.Add(21, new List<int>() { 1, 1 });
        ConstructSizes.Add(24, new List<int>() { 1, 1 });
        ConstructSizes.Add(26, new List<int>() { 3, 1 });
    }

    public void AssembleGameData()
    {
        SetSizes();
    }

    public void Attack(Card construct)
    {
        if (construct.FreezeAmount > 0) return;
        var t = construct.Construct.transform;
        bool gf = construct.IsPlayerControlled;
        var ss = ConstructSizes[construct.CardType];
        var g = RandomFunctions.Instance.SpawnObject(2, miscrefs[13], new Vector3(t.position.x + (gf ? ((float)ss[0] / 2) : -((float)ss[0] / 2)), t.position.y, t.position.z), t.rotation, false, "");
        var sp = g.GetComponent<SpawnData>();
        sp.card = construct;
        var pj = g.GetComponent<Projectile>();
        pj.spawnData = sp;
        pj.GetTarget();
        SoundSystem.Instance.PlaySound(construct.AttackSound);
    }

    public Card CardPlayed;
    public void StartPlacer(Card card)
    {
        PlacerCard = card;
        PlacerState = true;
        PlacerCooldown = 0.1f;

        var a = new Vector2(1, 1);
        if (ConstructSizes.ContainsKey(card.CardType))
        {
            a.x = ConstructSizes[card.CardType][0];
            a.y = ConstructSizes[card.CardType][1];
        }
        PlacerWidth = a.x;
        PlacerHeight = a.y;

    }

    public Vector2 GridPos = new Vector2();
    public bool CanPlace = false;
    public void GridChecker()
    {
        //PlacerState = false;
        CanPlace = false;

        var xy = MousePos;
        float pw = PlacerWidth - 1;
        float ph = PlacerHeight - 1;
        float mx = MapX - 1;
        float my = MapY - 1;

        var ppp = (CheckSidePlace(PlacerCard.CardType) ? miscrefs[1] : miscrefs[3]).transform.position;
        xy -= ppp;
        xy = new Vector3(Mathf.Clamp(xy.x, 0 + (pw / 2), mx - (pw / 2)), Mathf.Clamp(xy.y, 0 + (ph / 2), my - (ph / 2)), 0);
        int x = Mathf.FloorToInt(xy.x + (1 - (PlacerWidth / 2)));
        int y = Mathf.FloorToInt(xy.y + (1 - (PlacerHeight / 2)));
        //x -= (int)((PlacerWidth - 1) / 2);
        //y -= (int)((PlacerHeight - 1) / 2);
        GridPos = new Vector2(x, y);

        bool sexedyounuts = false;
        for (int i = 0; i < PlacerWidth && !sexedyounuts; i++)
        {
            for (int j = 0; j < PlacerHeight; j++)
            {
                if (!ValidLocation((CheckSidePlace(PlacerCard.CardType) ? turf : enemyturf)[x + i, y + j]))
                {
                    sexedyounuts = true;
                    break;
                }
            }
        }
        CanPlace = !sexedyounuts;

    }
    public int supposed_ai_dick = 7;
    public float card_timer = 1.3f;
    public IEnumerator AITurn()
    {
        //AIPlay
        List<List<float>> plays = new List<List<float>>();
        List<List<float>> plays_best = new List<List<float>>();
        List<List<string>> plays_sex = new List<List<string>>();
        yield return new WaitUntil(() =>
        {
            return miscrefs[13].transform.GetComponentsInChildren<Projectile>().Length == 0;
        });
        while ((!IsPlayerTurn) && !IsPlayerTurnWait)
        {
            if (!Multiplayer)
            {
                yield return new WaitForSeconds(card_timer);
            }
            else
            {
                MutliMoveForward = false;
            }
            string e = "";
            //dev test, causes infinte loop whoops
            /*
            int type = 3;
            int x = 0;
            int y = 5;
            Card ind = new Card();
            foreach (var c in AIDeck.Hand)
            {
                if (c.CardType == type) { ind = c; break; }
            }
            if (ConstructSizes.ContainsKey(type))
            {
                AIPlaceCard(ind, new Vector2(x, y));
            }
            */
            //enddevttest
            if (!Multiplayer)
            {
                plays.Clear();
                e = "Ai Deck: ";
                if (DevMode)
                {
                    foreach (var p in AIDeck.Hand)
                    {
                        e += p.CardType + ", ";
                    }
                    Debug.Log(e);


                }
                foreach (var c in AIDeck.Hand)
                {
                    c.UpdateData();
                }
                EvaluatePlay(ref plays);
            }
            plays_best.Clear();

            if (Multiplayer)
            {
                yield return new WaitUntil(() => { return MutliMoveForward || GameState == "Main Menu"; });
                plays.Clear();
            }
            if (GameState == "Main Menu") goto fuck;
            if (DevMode && Multiplayer) Debug.Log("Attempt Play: " + MultiMoveData);
            if (plays.Count > 0 || (Multiplayer && MultiMoveData != "skip"))
            {
                if (!Multiplayer)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        plays_best.Add(new List<float>() { -1, -1 });
                    }
                    var x = plays[0];
                    var x2 = plays[0];
                    for (int i = 0; i < plays.Count; i++)
                    {
                        x = plays[i];
                        for (int i2 = 0; i2 < plays_best.Count; i2++)
                        {
                            if (plays_best.Count > i2 && x[1] > plays_best[i2][1])
                            {
                                x2 = plays_best[i2];
                                plays_best.RemoveAt(i2);
                                plays_best.Insert(i2, x);
                                x = x2;
                            }
                        }
                    }
                    for (int i = 0; i < plays_best.Count; i++)
                    {
                        if (plays_best[i][0] == -1) { plays_best.RemoveAt(i); i--; }
                    }
                }
                if (plays_best.Count > 0 || Multiplayer)
                {
                    Card card = null;
                    int index = 0;
                    if (!Multiplayer)
                    {
                        int f = 0;
                        int f2 = 0;
                        switch (AIDifficulty)
                        {
                            case -1: f2 = 2; break;
                            case 0: f2 = 4; break;
                            case 1: f2 = 7; break;
                        }
                        bool rand = false;
                        if (AIDifficulty < 2)
                        {
                            if (Random.Range(0, f2) == 1)
                            {
                                rand = true;
                            }
                        }
                        if (rand)
                        {
                            f = plays.Count;
                        }
                        else
                        {
                            switch (AIDifficulty)
                            {
                                case -1: f = 7; break;
                                case 0: f = 5; break;
                                case 1: f = 5; break;
                                case 2: f = 3; break;
                            }
                            f = Mathf.Clamp(f, 0, plays_best.Count - 1);
                        }
                        index = Random.Range(0, f);
                        //index = Mathf.Clamp(index, 0, plays_best.Count - 1);
                        //Debug.Log($"sz: {AIDeck.Hand.Count}, {(int)plays_best[index][0]}");
                        if (rand) plays_best = plays;
                        card = AIDeck.Hand[(int)plays_best[index][0]];
                    }
                    else
                    {
                        //convert #1 to #2
                        //#1 string:  cardtype, score, data
                        //#2 list<float>: cardhandindex, score, data
                        plays_best.Clear();
                        plays_sex.Clear();
                        if (DevMode) Debug.Log("Still play attempt with " + MultiMoveData);
                        var efuck = RandomFunctions.Instance.StringToList(MultiMoveData);
                        AIDeck.Hand.Clear();
                        AIDeck.Hand.Add(new Card(0));
                        AIDeck.Hand.Add(new Card(0));
                        AIDeck.Hand.Add(new Card(0));
                        AIDeck.Hand.Add(new Card(0));
                        AIDeck.Hand.Add(new Card(0));
                        AIDeck.Hand[0] = new Card(int.Parse(efuck[0]));
                        efuck[0] = "0";
                        List<float> efuyck2 = new List<float>();
                        List<string> efuyck3 = new List<string>();
                        int i = 0;
                        foreach (var k in efuck)
                        {
                            try
                            {
                                efuyck2.Add(float.Parse(efuck[i]));
                                efuyck3.Add(efuck[i]);
                                i++;
                            }
                            catch
                            {

                            }
                        }
                        plays_best.Add(efuyck2);
                        plays_sex.Add(efuyck3);
                        card = AIDeck.Hand[0];
                    }
                    var m = new Card(card);
                    //Debug.Log($"Card Played {card.CardType}, {m.CardType}");
                    // default data: CardHand Index, Score
                    // Extra data: Position x, position y, cardtype, randseed

                    if (card.SubNumber == 1)
                    {
                        m = new Card(AILastCard);
                    }
                    if (ConstructSizes.ContainsKey(m.CardType))
                    {
                        m.GridPos = new Vector2(plays_best[index][2], plays_best[index][3]);
                        plays_best[index].RemoveAt(3);
                        plays_best[index].RemoveAt(2);
                        if (Multiplayer)
                        {
                            plays_sex[index].RemoveAt(3);
                            plays_sex[index].RemoveAt(2);
                        }
                    }
                    if (m.CardType == 8 || m.CardType == 20)
                    {
                        int jj = -1;
                        foreach (var i in AIDeck.Hand)
                        {
                            if (i.CardType == (int)plays_best[index][2])
                            {
                                jj = i.HandIndex;
                                break;
                            }
                        }
                        plays_best[index].RemoveAt(2);
                        if (Multiplayer)
                        {
                            plays_sex[index].RemoveAt(2);
                        }
                        m.dummyval = jj;
                    }
                    if (m.UsesRandom)
                    {
                        if (Multiplayer)
                        {
                            m.RandomSeed = int.Parse(plays_sex[index][2]);
                            if (DevMode)
                            {
                                ConsoleLol.Instance.ConsoleLog("Incoming Data" + MultiMoveData);
                                ConsoleLol.Instance.ConsoleLog("Random Seed: " + m.RandomSeed);
                            }
                            plays_best[index].RemoveAt(2);
                            if (Multiplayer)
                            {
                                plays_sex[index].RemoveAt(2);
                            }
                        }
                    }
                    if (m.CardType == 25)
                    {
                        m.GridPos = new Vector2(plays_best[index][2], (plays_best[index][3]));
                        plays_best[index].RemoveAt(3);
                        plays_best[index].RemoveAt(2);
                        if (Multiplayer)
                        {
                            plays_sex[index].RemoveAt(3);
                            plays_sex[index].RemoveAt(2);
                        }
                    }
                    int type = CardActionType(m.CardType);
                    CABringOver = m;
                    CardAction(true, type, card, false);
                    if (Multiplayer)
                    {
                        AIDeck.LoadDeck(LoadedDecks[0][1], true);
                    }
                    CardPlayed = card;
                    if (!Multiplayer)
                    {
                        if (CardPlayed != null) AIDeck.Hand.Remove(CardPlayed);
                        AIDrawCard();
                        Dictionary<int, int> d = new Dictionary<int, int>();
                        for (int i = 0; i < AIDeck.Hand.Count; i++)
                        {
                            var dd = AIDeck.Hand[i];
                            if (d.ContainsKey(dd.CardType))
                            {
                                d[dd.CardType]++;
                                if (d[dd.CardType] > ((dd.AIOneMax || HandSizelol < 6) ? 1 : 2))
                                {
                                    AIDeck.Hand.RemoveAt(i);
                                    i--;
                                }
                            }
                            else
                            {
                                d.Add(dd.CardType, 1);
                            }
                        }
                        for (int i = 0; i < (supposed_ai_dick - AIDeck.Hand.Count); i++)
                        {
                            AIDrawCard();
                        }
                    }
                }
            }
            else
            {
                if (!Multiplayer)
                {
                    AIDeck.Hand.RemoveAt(Random.Range(0, AIDeck.Hand.Count));
                    for (int i = 0; i < (supposed_ai_dick - AIDeck.Hand.Count); i++)
                    {
                        AIDeck.Hand.Add(AIDeck.DrawCard());
                    }
                }
                PlacerCard = null;
                CardPlayed = null;
            }
            if (!IsPlayerTurn)
            {
                if (miscrefs[13].transform.GetComponentsInChildren<Projectile>().Length != 0)
                {
                    yield return new WaitUntil(() =>
                    {
                        return miscrefs[13].transform.GetComponentsInChildren<Projectile>().Length == 0;
                    });
                }
                EndTurnCheck();
            }

            /*
            if (Turncount == 1)
            {
                //IsPlayerTurn = false;
                int type = 2;
                int x = 0;
                int y = 5;
                int ind = -1;
                foreach (var c in AIDeck.Hand)
                {
                    if (c.CardType == type) { ind = c.HandIndex; break; }
                }
                if (ConstructSizes.ContainsKey(type))
                {
                    AIPlaceCard(AIDeck.Hand[ind], new Vector2(x, y));
                }
            }
            else
            {
                //Attack(AICons[0]);
                EndTurnCheck();
            }
            */
        }

    fuck:
        yield return null;
    }
    public void AIDrawCard()
    {
        Card draw = null;
        if (determined_draw_cards.Count > 0)
        {
            AIDeck.Hand.Add(new Card(determined_draw_cards[0]));
            determined_draw_cards.RemoveAt(0);
        }
        else
        {
            AIDeck.Hand.Add(AIDeck.DrawCard());
        }
    }
    public IEnumerator WaitForPlayerTurn()
    {
        yield return null;
        yield return new WaitUntil(() =>
        {
            return miscrefs[13].transform.GetComponentsInChildren<Projectile>().Length == 0;
        });
        yield return new WaitForSeconds(card_timer);
        IsPlayerTurn = true;
    }
    public IEnumerator WaitForPoopyGone(int cds)
    {
        yield return null;

        if(CardPlayed != null && CardPlayed.CanCauseAttacks)
        {
            AnnounceAction(false);
        }


        yield return new WaitUntil(() =>
        {
            return miscrefs[13].transform.GetComponentsInChildren<Projectile>().Length == 0;
        });
        yield return null;
        DrawCards(cds, DefaultDelay, 0.1f);
        EndTurnCheck();
    }

    public void AIPlaceCard(Card cd, Vector2 gridpos)
    {
        PlacerCard = cd;

        if (PlacerCard.CardType == 24)
        {
            PlacerCard = new Card(21);
        }

        var s = ConstructSizes[PlacerCard.CardType];
        cd.GridPos = gridpos;
        bool ind = PlacerCard.IndividualParts;
        bool kl = (CheckSidePlace(PlacerCard.CardType));
        var pl = kl ? enemyturf : turf;
        var ppl = kl ? miscrefs[3] : miscrefs[1];
        SoundSystem.Instance.PlaySound(PlacerCard.PlaceeSound);
        for (int i = 0; i < s[0]; i++)
        {
            for (int j = 0; j < s[1]; j++)
            {
                int x = (int)gridpos.x;
                int y = (int)gridpos.y;
                //(CheckSidePlace(PlacerCard.CardType) ? enemyturf : turf)[ + i,  + j] = PlacerCard;

                var f = pl[x + i, y + j];
                if (f != null && f != PlacerCard)
                {
                    f.Health = -1;
                    f.CheckDie(true);
                    if (AICons.Contains(f)) AICons.Remove(f);
                    if (f.Construct != null) Destroy(f.Construct);
                }
                if (ind)
                {
                    var cd2 = new Card(PlacerCard);
                    pl[x + i, y + j] = cd2;
                    CreateConstruct(cd2, ppl.transform.position, new Vector2(x + i, y + j));

                    AICons.Add(cd2);
                }
                else
                {
                    pl[x + i, y + j] = PlacerCard;
                }

            }
        }
        if (!ind)
        {
            var pos = new Vector2(gridpos.x + ((float)(s[0] - 1) / 2), gridpos.y + ((float)(s[1] - 1) / 2));
            CreateConstruct(PlacerCard, (CheckSidePlace(PlacerCard.CardType) ? miscrefs[3] : miscrefs[1]).transform.position, pos);
            AICons.Add(PlacerCard);
        }

    }

    public void EvaluatePlay(ref List<List<float>> plays, List<Card> cardstocheck = null)
    {
        plays.Clear();
        int x = 0;
        List<List<float>> plays2 = new List<List<float>>();
        List<Card> shit_plays = new List<Card>();
        List<Card> cds = new List<Card>();
        List<int> cdsi = new List<int>();
        Dictionary<float, float> cummers = new Dictionary<float, float>();
        if(cardstocheck == null)
        {
            cardstocheck = AIDeck.Hand;
        }
        foreach (var c in cardstocheck)
        {
            c.HandIndex = x;
            if (!cdsi.Contains(c.CardType))
            {
                cds.Add(c);
                cdsi.Add(c.CardType);
            }
            x++;
        }
        // (handindex, score), (handindex, score, posx, posy), (handindex, score),
        float score;
        float tscore;
        float h;
        float amnt;
        bool didshit = false;
        List<float> li = new List<float>();
        List<float> li2 = new List<float>();
        Card cd6969;
        foreach (var card in cds)
        {
            cd6969 = card;
            didshit = false;
            if (card.EnergyCost > AIEnergy)
            {
                goto shid;
            }
        fuckmyballsveryveryhardohyesharderdaddy:
            switch (cd6969.CardType)
            {
                case 1:
                case 24:
                    tscore = AIDifficulty > 0 ? 4000 : 1000;
                    if(cd6969.CardType == 24)
                    {
                        tscore = 750f;
                    }
                    amnt = 2;
                    foreach (var con in PlayerCons)
                    {
                        score = 0;
                        if (con != null && con.CanAttack)
                        {
                            for (int i = 0; i < MapX; i++)
                            {
                                if (ValidLocation(enemyturf[i, (int)con.GridPos.y]))
                                {
                                    plays.Add(new List<float>() { card.HandIndex, tscore - (75 * i), i, (int)con.GridPos.y });
                                    didshit = true;
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    li.Clear();
                    li2.Clear();
                    //goes through everything and finds out how many attackers and defenders are in each row.
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = turf[j, i];
                            if (a != null && a.CanAttack)
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                        }
                        li.Add(k);
                    }
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = enemyturf[j, i];
                            if ((ValidLocation(a)) && (a != null && a.CanAttack))
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                        }
                        li2.Add(k);
                    }
                    for (int i = 0; i < (MapX - 1); i++)
                    {
                        for (int j = 0; j < (MapY); j++)
                        {
                            var x1 = enemyturf[i, j];
                            var x2 = enemyturf[i + 1, j];
                            int kk = MapX - 1;
                            float sc = ((500f - (i * 50f)) + (550f * (li[j])) + (375f * (li2[j])));
                            if (ValidLocation(x1) && ValidLocation(x2))
                            {
                                plays.Add(new List<float>() { card.HandIndex, sc, i, j });
                                didshit = true;
                            }
                        }
                    }
                    break;
                case 3:
                    amnt = 3f;
                    li.Clear();
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = turf[j, i];
                            if ((!ValidLocation(a)) && !(a != null && a.CanAttack))
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                            if (k >= 0.5f && (a != null && a.CanAttack))
                            {
                                k -= 0.5f;
                            }
                        }
                        li.Add(k);
                    }
                    for (int i = 0; i < MapX - 1; i++)
                    {
                        for (int j = 0; j < MapY; j++)
                        {
                            var x1 = enemyturf[i, j];
                            var x2 = enemyturf[i + 1, j];
                            int kk = MapX - 1;
                            kk = kk - i;
                            if (ValidLocation(x1) && ValidLocation(x2))
                            {
                                plays.Add(new List<float>() { card.HandIndex, ((1500f - (kk * 50f)) * amnt) / (amnt + li[j]), i, j });
                                didshit = true;
                            }
                        }
                    }
                    break;

                case 4:
                    li.Clear();
                    li2.Clear();
                    //goes through everything and finds out how many attackers and defenders are in each row.
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = turf[j, i];
                            if (a != null && a.CanAttack)
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                        }
                        li.Add(k);
                    }
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = enemyturf[j, i];
                            if ((ValidLocation(a)) && (a != null && a.CanAttack))
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                        }
                        li2.Add(k);
                    }
                    for (int i = 0; i < MapX; i++)
                    {
                        for (int j = 0; j < (MapY - 2); j++)
                        {
                            var x1 = enemyturf[i, j];
                            var x2 = enemyturf[i, j + 1];
                            var x3 = enemyturf[i, j + 2];
                            int kk = MapX - 1;
                            float sc = ((600f - (i * 50f)) + (350f * (li[j] + li[j + 1] + li[j + 2])) + (250f * (li2[j] + li2[j + 1] + li2[j + 2])));
                            if (ValidLocation(x1) && ValidLocation(x2) && ValidLocation(x3))
                            {
                                plays.Add(new List<float>() { card.HandIndex, sc, i, j });
                                didshit = true;
                            }
                        }
                    }
                    break;
                case 22:
                    amnt = 3;
                    tscore = 0;
                    foreach (var con in AICons)
                    {
                        score = 0;
                        if (con != null && con.CanAttack)
                        {
                            tscore += 500;
                        }
                    }
                    if (tscore > 0)
                    {
                        plays.Add(new List<float>() { card.HandIndex, tscore });
                        didshit = true;
                        for (int z = 1; z < 4; z++)
                        {
                            plays.Add(new List<float>() { card.HandIndex, tscore * amnt / (amnt + z) });
                        }
                    }
                    break;
                case 5:
                    amnt = 3;
                    tscore = 0;
                    foreach (var con in AICons)
                    {
                        score = 0;
                        if (con != null && con.ReallyCanAttack())
                        {
                            tscore += 600;
                        }
                    }
                    if (tscore > 1)
                    {
                        plays.Add(new List<float>() { card.HandIndex, tscore });
                        didshit = true;
                        for (int z = 1; z < 5; z++)
                        {
                            plays.Add(new List<float>() { card.HandIndex, tscore * amnt / (amnt + z) });
                        }
                    }

                    break;
                case 7:
                    amnt = 0;
                    tscore = 0;
                    foreach (var con in AICons)
                    {
                        if (con != null && con.CardType == 6)
                        {
                            amnt++;
                        }
                    }
                    if (amnt > 3) plays.Add(new List<float>() { card.HandIndex, 200 * amnt });
                    if (amnt > 3) plays.Add(new List<float>() { card.HandIndex, 200 * amnt });
                    if (amnt > 3) plays.Add(new List<float>() { card.HandIndex, 200 * amnt });
                    if (amnt > 3) didshit = true;

                    break;
                case 8:
                case 9:
                case 20:
                    didshit = true;
                    break;
                case 10:
                    score = 5000f;
                    if (Enemyhealth < MaxEnemyhealth) { plays.Add(new List<float>() { card.HandIndex, score }); plays.Add(new List<float>() { card.HandIndex, score }); plays.Add(new List<float>() { card.HandIndex, score }); didshit = true; };
                    break;
                case 11:
                    score = 2500;
                    if (Enemyhealth < MaxEnemyhealth)
                    {
                        float pp = (Enemyhealth + MaxEnemyhealth) / (MaxEnemyhealth * 2);
                        pp = 1 - pp;
                        score *= pp;
                        plays.Add(new List<float>() { card.HandIndex, pp });
                        plays.Add(new List<float>() { card.HandIndex, pp });
                        plays.Add(new List<float>() { card.HandIndex, pp });
                        didshit = true;
                    }
                    break;
                case 12:
                    amnt = 0;
                    tscore = 0;
                    foreach (var con in PlayerCons)
                    {
                        if (con != null && con.CardType == 6)
                        {
                            amnt++;
                        }
                    }
                    if (amnt > 3) plays.Add(new List<float>() { card.HandIndex, 150 * amnt });
                    if (amnt > 3) plays.Add(new List<float>() { card.HandIndex, 150 * amnt });
                    if (amnt > 3) didshit = true;

                    break;
                case 13:
                    amnt = 3f;
                    li.Clear();
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = turf[j, i];
                            if (!ValidLocation(a))
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                        }
                        li.Add(k);
                    }
                    for (int i = 0; i < MapX - 1; i++)
                    {
                        for (int j = 0; j < MapY; j++)
                        {
                            var x1 = enemyturf[i, j];
                            var x2 = enemyturf[i + 1, j];
                            int kk = MapX - 1;
                            kk = kk - i;
                            if (ValidLocation(x1) && ValidLocation(x2))
                            {
                                plays.Add(new List<float>() { card.HandIndex, (((1500f / (MapX * 2)) * (li[j] + MapX)) - (kk * 50f)), i, j });
                                didshit = true;
                            }
                        }
                    }
                    break;
                case 14:
                    score = 0;
                    foreach (var a in AICons)
                    {
                        if (a != null && a.ReallyCanAttack())
                        {
                            score += 5001;
                        }
                        if (score >= 10000) break;
                    }
                    if (score > 0)
                    {
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        didshit = true;
                    }
                    break;
                case 15:
                    score = 0;

                    foreach (var a in AICons)
                    {
                        if (!a.IsFakeCard)
                        {
                            score += 100;
                        }
                    }

                    if (score > 500)
                    {
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        didshit = true;
                    }
                    break;
                case 16:
                    if (AILastCard != null)
                    {
                        cd6969 = AILastCard;
                        goto fuckmyballsveryveryhardohyesharderdaddy;
                    }
                    break;
                case 17:
                    h = 0;
                    foreach (var c in AICons)
                    {
                        if (c.CardType == 6)
                        {
                            h++;
                        }
                    }
                    score = h * 170f;
                    if (score >= 1000)
                    {
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        didshit = true;
                    }
                    break;
                case 23:
                    h = 0;
                    foreach (var c in AICons)
                    {
                        if (c.CardType == 21)
                        {
                            h++;
                        }
                    }
                    score = h * 200f;
                    if (score >= 1000)
                    {
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        didshit = true;
                    }
                    break;
                case 18:
                    h = 0;
                    foreach (var c in PlayerCons)
                    {
                        if (!c.IsFakeCard)
                        {
                            h++;
                        }
                    }
                    score = h * 150f;
                    if (score >= 600)
                    {
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        didshit = true;
                    }
                    break;
                case 19:
                    amnt = 3;
                    tscore = 0;
                    foreach (var con in AICons)
                    {
                        score = 0;
                        if (con != null && con.CanAttack)
                        {
                            tscore += 500;
                        }
                    }
                    if (tscore > 0)
                    {
                        plays.Add(new List<float>() { card.HandIndex, tscore });
                        didshit = true;
                        for (int z = 1; z < 4; z++)
                        {
                            plays.Add(new List<float>() { card.HandIndex, tscore * amnt / (amnt + z) });
                            didshit = true;
                        }
                    }
                    break;
                case 26:
                    amnt = 3f;
                    li.Clear();
                    for (int i = 0; i < MapY; i++)
                    {
                        float k = 0;
                        for (int j = 0; j < MapX; j++)
                        {
                            var a = turf[j, i];
                            if ((!ValidLocation(a)) && !(a != null && a.CanAttack))
                            {
                                k++;
                                j += ConstructSizes[a.CardType][0] - 1;
                            }
                            if (k >= 0.5f && (a != null && a.CanAttack))
                            {
                                k -= 0.5f;
                            }
                        }
                        li.Add(k);
                    }
                    for (int i = 0; i < MapX - 2; i++)
                    {
                        for (int j = 0; j < MapY; j++)
                        {
                            var x1 = enemyturf[i, j];
                            var x2 = enemyturf[i + 1, j];
                            var x3 = enemyturf[i + 2, j];
                            int kk = MapX - 1;
                            kk = kk - i;
                            if (ValidLocation(x1) && ValidLocation(x2) && ValidLocation(x3))
                            {
                                plays.Add(new List<float>() { card.HandIndex, ((1500f - (kk * 50f)) * amnt) / (amnt + li[j]), i, j });
                                didshit = true;
                            }
                        }
                    }
                    break;
                case 27:
                    score = 0f;
                    foreach (var con in AICons)
                    {
                        if (con != null)
                        {
                            score += 300 * con.debuffs.Count;
                        }
                    }
                    if (AIOnFire > 0)
                    {
                        score += 750;
                    }
                    if (score >= 500)
                    {
                        for (int z = 1; z < 4; z++)
                        {
                            plays.Add(new List<float>() { card.HandIndex, score });
                            didshit = true;
                        }
                    }
                    break;
                case 25:
                    foreach (var con in PlayerCons)
                    {
                        if (con != null && con.ReallyCanAttack())
                        {
                            plays.Add(new List<float>() { card.HandIndex, 1000f, con.GridPos.x, con.GridPos.y});
                            didshit = true;
                        }
                    }
                    break;
                case 28:
                    if (LastCardTrue != null)
                    {
                        cd6969 = LastCardTrue;
                        goto fuckmyballsveryveryhardohyesharderdaddy;
                    }
                    break;
                case 29:
                    score = 995;
                    score += (250 * (AIEnergy - 5));
                    plays.Add(new List<float>() { card.HandIndex, score });
                    plays.Add(new List<float>() { card.HandIndex, score });
                    plays.Add(new List<float>() { card.HandIndex, score });
                    didshit = true;
                    break;
                case 30:
                    score = 0;
                    score += (150 * (AIEnergy));
                    if(score >= 0)
                    {
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        plays.Add(new List<float>() { card.HandIndex, score });
                        didshit = true;
                    }
                    break;
            }
        shid:
            if (!didshit)
            {
                shit_plays.Add(card);
            }
        }

        foreach (var card in cds)
        {
            cd6969 = card;
            switch (card.CardType)
            {
                case 8:
                    foreach (var cd in shit_plays)
                    {
                        if (CheckDiscardLogic(cd.CardType)) plays2.Add(new List<float>() { card.HandIndex, 15000, cd.CardType });
                    }
                    cummers.Clear();
                    foreach (var pl in plays)
                    {
                        var cdf = AIDeck.Hand[(int)pl[0]];
                        if (cummers.ContainsKey(cdf.CardType))
                        {
                            if (pl[1] > cummers[cdf.CardType]) cummers[cdf.CardType] = pl[1];
                        }
                        else
                        {
                            cummers.Add(cdf.CardType, pl[1]);
                        }
                    }
                    foreach (var f in cummers)
                    {
                        if (f.Value > 0)
                        {
                            float pp = 1500f / f.Value;
                            pp *= 1000f;
                            if (CheckDiscardLogic((int)f.Key)) plays2.Add(new List<float>() { card.HandIndex, pp, f.Key });
                        }
                        else
                        {
                            if (CheckDiscardLogic((int)f.Key)) plays2.Add(new List<float>() { card.HandIndex, 15000, f.Key });
                        }
                    }
                    break;
                case 9:
                    float sc = 0;
                    foreach (var cd in shit_plays)
                    {
                        sc += 5;
                    }
                    cummers.Clear();
                    foreach (var pl in plays)
                    {
                        var cdf = AIDeck.Hand[(int)pl[0]];
                        if (cummers.ContainsKey(cdf.CardType))
                        {
                            if (pl[1] > cummers[cdf.CardType]) cummers[cdf.CardType] = pl[1];
                        }
                        else
                        {
                            cummers.Add(cdf.CardType, pl[1]);
                        }
                    }
                    foreach (var f in cummers)
                    {
                        float pp = 1500f / f.Value;
                        sc += pp;
                    }
                    sc *= 60;
                    plays2.Add(new List<float>() { card.HandIndex, sc, });
                    plays2.Add(new List<float>() { card.HandIndex, sc, });
                    plays2.Add(new List<float>() { card.HandIndex, sc, });
                    break;
                case 20:

                    if (Enemyhealth <= 65)
                    {
                        float scdd = 1500f;
                        if(Enemyhealth <= 35)
                        {
                            scdd = 3500;
                        }
                        foreach (var cd in shit_plays)
                        {
                            if (CheckDiscardLogic(cd.CardType, true)) plays2.Add(new List<float>() { card.HandIndex, scdd, cd.CardType });
                        }
                        cummers.Clear();
                        //finds the highest score of each card type
                        foreach (var pl in plays)
                        {
                            var cdf = AIDeck.Hand[(int)pl[0]];
                            if (cummers.ContainsKey(cdf.CardType))
                            {
                                if (pl[1] > cummers[cdf.CardType]) cummers[cdf.CardType] = pl[1];
                            }
                            else
                            {
                                cummers.Add(cdf.CardType, pl[1]);
                            }
                        }
                        foreach (var f in cummers)
                        {
                            if (f.Value > 0)
                            {
                                float pp = scdd / f.Value;
                                pp *= 100f;
                                if (CheckDiscardLogic((int)f.Key, true)) plays2.Add(new List<float>() { card.HandIndex, pp, f.Key });
                            }
                            else
                            {
                                if (CheckDiscardLogic((int)f.Key, true)) plays2.Add(new List<float>() { card.HandIndex, scdd, f.Key });
                            }
                        }
                    }
                    break;
            }
        }

        foreach (var p in plays2)
        {
            plays.Add(p);
        }

    }
    public void PlacePlacer()
    {
        //PlacerState = false;
        GridChecker();
        if (CanPlace)
        {
            var CardToPlace = PlacerCard;
            if(CardToPlace.CardType == 24)
            {
                CardToPlace = new Card(PlacerCard);
                CardToPlace.CardType = 21;
                CardToPlace.SetVals();
            }


            PlacerState = false;
            int x = (int)GridPos.x;
            int y = (int)GridPos.y;

            bool ind = CardToPlace.IndividualParts;
            bool kl = (CheckSidePlace(CardToPlace.CardType));
            var pl = kl ? turf : enemyturf;
            var ppl = kl ? miscrefs[1] : miscrefs[3];
            for (int i = 0; i < PlacerWidth; i++)
            {
                for (int j = 0; j < PlacerHeight; j++)
                {
                    var f = pl[x + i, y + j];
                    if (f != null)
                    {
                        f.Health = -1;
                        f.CheckDie(true);
                        if (PlayerCons.Contains(f)) PlayerCons.Remove(f);
                        if (f.Construct != null) Destroy(f.Construct);
                    }
                    if (ind)
                    {
                        var cd = new Card(CardToPlace);
                        pl[x + i, y + j] = cd;
                        CreateConstruct(cd, ppl.transform.position, new Vector2(x + i, y + j));
                        PlayerCons.Add(cd);
                    }
                    else
                    {
                        pl[x + i, y + j] = CardToPlace;
                    }
                }
            }

            var x2 = MousePos;

            float pw = PlacerWidth - 1;
            float ph = PlacerHeight - 1;
            float mx = MapX - 1;
            float my = MapY - 1;
            var ppp = (CheckSidePlace(CardToPlace.CardType) ? miscrefs[1] : miscrefs[3]).transform.position;
            x2 -= ppp;
            x2 = new Vector3(Mathf.Clamp(x2.x, 0 + (pw / 2), mx - (pw / 2)), Mathf.Clamp(x2.y, 0 + (ph / 2), my - (ph / 2)), 0);
            x2 += ppp;
            var y2 = new Vector3(Mathf.FloorToInt(x2.x + (1 - (PlacerWidth / 2))), Mathf.FloorToInt(x2.y + (1 - (PlacerHeight / 2))), 0);
            var z2 = y2 + new Vector3(((PlacerWidth - 1) / 2), ((PlacerHeight - 1) / 2), 0);

            SoundSystem.Instance.PlaySound(CardToPlace.PlaceeSound);
            ShitLeCard(PlacerCard);

            if (!ind)
            {
                CreateConstruct(CardToPlace, ppp + new Vector3((PlacerWidth - 1) / 2, ((float)PlacerHeight - 1) / 2, 0), GridPos);

                PlayerCons.Add(CardToPlace);
            }
            //dick.Hand.RemoveAt(PlacerCard.HandIndex);
            if (CardToPlace.Display != null) Destroy(CardToPlace.Display);
            CardToPlace.GridPos = GridPos;
            //Debug.Log(PlacerCard.HandIndex);
            DrawCards(1, DefaultDelay, 0.1f);
            EndTurnCheck();
        }
    }

    public void CreateConstruct(Card cd, Vector3 TurfPos, Vector2 GridPos)
    {
        var con = RandomFunctions.Instance.SpawnObject(0, miscrefs[39], TurfPos + new Vector3(GridPos.x, GridPos.y), Placer.transform.rotation, false, "");
        var sp2 = GetConSprite(cd);
        if (sp2 == 0)
        {
            con.transform.localScale = new Vector3(PlacerWidth, PlacerHeight, 1);
        }
        else
        {
            con.GetComponent<SpriteRenderer>().sprite = Construct_Images[sp2];
        }
        var sp = con.GetComponent<SpawnData>();
        cd.Construct = con;
        cd.GridPos = GridPos;
        sp.card = cd;
    }


    public void AnnounceAction(bool noendmessage = false)
    {
        if (Multiplayer && IsPlayerTurn)
        {
            if (!noendmessage)
            {
                if (CardPlayed != null)
                {
                    string e = "";
                    e += CardPlayed.CardType + ", ";
                    e += "69, ";
                    if (CardPlayed.CardType == 25)
                    {
                        e += $"{(MapX - 1) - CardPlayed.GridPos.x}, {CardPlayed.GridPos.y}";
                    }
                    if (ConstructSizes.ContainsKey(CardPlayed.CardType))
                    {
                        var g = CardPlayed.GridPos.x;
                        g = (MapX - 1) - g;
                        g -= ConstructSizes[CardPlayed.CardType][0] - 1;
                        e += $"{g}, {CardPlayed.GridPos.y}, ";
                    }
                    if (CardPlayed.CardType == 8 || CardPlayed.CardType == 20)
                    {
                        e += "2, ";
                    }
                    if (CardPlayed.UsesRandom)
                    {
                        e += $"{CardPlayed.RandomSeed}, ";
                    }
                    if (DevMode) Debug.Log("Sent game Data: " + e);
                    ServerGamer.Instance.MessageServerRpc(RandomFunctions.Instance.ClientID, e, 2);
                }
                else
                {
                    if (DevMode) Debug.Log("Sent game Data: " + "skip");
                    ServerGamer.Instance.MessageServerRpc(RandomFunctions.Instance.ClientID, "skip", 2);
                }
            }
        }
    }

    public void EndTurnCheck(bool fucky = false, bool noendmessage = false, bool came = false)
    {
        if(CardPlayed == null || (!CardPlayed.CanCauseAttacks))AnnounceAction(noendmessage);

        foreach (var cd in dick.Hand)
        {
            cd.UpdateData();
        }

        FeebleOverload = false;
        BanishDiscardOverload = false;

        if (!IsPlayerTurn && PlayerOnFire > 0)
        {
            PlayerOnFire--;
            for (int i = PlayerCons.Count - 1; i >= 0; i--)
            {
                if (PlayerCons[i].IsFakeCard) continue;
                DamageFunc(PlayerCons[i], 2);
            }
        }
        else if (IsPlayerTurn && AIOnFire > 0)
        {
            AIOnFire--;
            for (int i = AICons.Count - 1; i >= 0; i--)
            {
                if (AICons[i].IsFakeCard) continue;
                DamageFunc(AICons[i], 2);
            }
        }
        HoleOverload = false;

        if (DickCounter >= 10)
        {
            GetAchievement(5);
        }
        DickCounter = 0;

        foreach (var c in AICons)
        {
            c.UpdateDisplay();
        }
        foreach (var c in PlayerCons)
        {
            c.UpdateDisplay();
        }




        miscrefs[65].SetActive(PlayerOnFire > 0);
        miscrefs[66].SetActive(AIOnFire > 0);



        bool canagain = false;
        bool heavy = false;
        var cardp = CardPlayed;
        if (!fucky && CABringOver != null)
        {
            cardp = CABringOver;
        }

        if (fucky)
        {
            CardPlayed = PlacerCard;
        }
        bool endmylife = false;
        if (CardPlayed != null)
        {
            if (CardPlayed.SubNumber != 1)
            {
                if (CardPlayed.IsPlayerControlled)
                {
                    PlayerLastCard = CardPlayed;
                }
                else
                {
                    AILastCard = CardPlayed;
                }
            }
            canagain = cardp.CanPlayAgain;
            heavy = cardp.IsHeavy;
            var iii = CardPlayed.EnergyCost;

            //code to auto end turn has been removed because it was too shit

            if (CardPlayed.IsPlayerControlled)
            {
                PLayerEnergy -= iii;
            }
            else
            {
                AIEnergy -= iii;
            }

        }
        if (CardPlayed != null && IsPlayerTurn)
        {
            if (dick.Hand.Contains(CardPlayed)) dick.Hand.Remove(CardPlayed);
            if (CardPlayed.Display != null) Destroy(CardPlayed.Display);
        }
        DiscardState = false;
        PlacerState = false;
        PlacerCard = null;
        CardPlayed = null;
        CABringOver = null;
        if (heavy)
        {
            if (IsPlayerTurn)
            {
                HeavyPlayer = 1;
            }
            else
            {
                HeavyAi = 1;
            }
        }
        if (canagain && IsPlayerTurn)
        {
            foreach (var c in dick.Hand)
            {
                c.UpdateData();
            }
        }


        if ((!canagain) || came)
        {

            foreach(var con in IsPlayerTurn ? PlayerCons : AICons)
            {
                if(con != null && con.FreezeAmount > 0)
                {
                    con.FreezeAmount--;
                    con.UpdateDisplay();
                }
            }


            if (!IsPlayerTurn && HeavyPlayer > 0)
            {
                AIEnergy += EnergyPerRound;
                HeavyPlayer--;
                Turncount++;
            }
            else if (IsPlayerTurn && HeavyAi > 0)
            {
                PLayerEnergy += EnergyPerRound;
                HeavyAi--;
                Turncount++;
            }
            else
            {



                if (!IsPlayerTurn)
                {
                    Turncount++;
                }
                determined_draw_cards.Clear();
                miscrefs[94].GetComponent<ITurnCountShitBitch>().UpdateColors(!IsPlayerTurn);

                if (Junkyard && IsPlayerTurn)
                {
                    Debug.Log("gigassssss");
                    dick.CardsInDeck.Add(new Card(30));
                }

                miscrefs[99].GetComponent<TextMeshProUGUI>().text = !IsPlayerTurn ? "Your Turn" : "Enemy Turn";

                if (IsPlayerTurn)
                {
                    AIEnergy += EnergyPerRound;
                    IsPlayerTurn = false;
                    IsPlayerTurnWait = false;
                    foreach (var c in AIDeck.Hand)
                    {
                        c.UpdateData();
                    }
                }
                else
                {
                    PLayerEnergy += EnergyPerRound;
                    IsPlayerTurnWait = true;
                    foreach (var c in dick.Hand)
                    {
                        c.UpdateData();
                    }
                    StartCoroutine(WaitForPlayerTurn());
                }

                if (!IsPlayerTurn)
                {
                    StartCoroutine(AITurn());
                }
            }
        }
        else
        {
            StartCoroutine(WaitForEndDrawCardsShit());
        }
    }

    public Card CABringOver = null;
    public void PlayCard(CardDisplay pr)
    {

        var e = pr.spawnData.card;
        if (e.SubNumber == 1)
        {
            if (PlayerLastCard == null) return;
            CABringOver = new Card(PlayerLastCard);
        }
        if (e != null)
        {
            //bool found = false;
            bool canclick = CanPlayCard(e);
            int type = CardActionType(CABringOver != null ? CABringOver.CardType : e.CardType);
            CardAction(canclick, type, e);
        }
    }
    public bool CanPlayCard(Card cd)
    {
        Card f = CABringOver;
        Card f2 = cd;
        if (f != null)
        {
            cd = f;
        }
        int index = cd.CardType;
        bool canclick = true;
        bool found = false;
        var iii = f2.EnergyCost;
        if ((cd.IsPlayerControlled ? PLayerEnergy : AIEnergy) < iii)
        {
            return false;
        }
        switch (index)
        {
            case 11:
            case 10:
                int x = 5;
                if (index == 11) x = 50;
                if (canclick)
                {
                    if ((cd.IsPlayerControlled ? Health : Enemyhealth) >= (cd.IsPlayerControlled ? MaxHealth : MaxEnemyhealth)) canclick = false;
                }
                break;
            case 14:
            case 5:
                if (canclick)
                {
                    foreach (var a in PlayerCons)
                    {
                        if (a.ReallyCanAttack())
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found) canclick = false;
                }
                break;
            case 7:
                if (canclick)
                {
                    int zz = 0;
                    foreach (var a in PlayerCons)
                    {
                        if (a.CardType == 6)
                        {
                            zz++;
                        }
                    }
                    if (zz < 2) canclick = false;
                }
                break;
            case 12:
                if (canclick)
                {
                    int zz = 0;
                    foreach (var a in AICons)
                    {
                        if (a.CardType == 6)
                        {
                            zz++;
                        }
                    }
                    if (zz < 2) canclick = false;
                }
                break;
            case 15:
                if (canclick)
                {
                    int zz = 0;
                    foreach (var a in AICons)
                    {
                        if (!a.IsFakeCard)
                        {
                            zz++;
                        }
                    }
                    foreach (var a in PlayerCons)
                    {
                        if (!a.IsFakeCard)
                        {
                            zz++;
                        }
                    }
                    if (zz < 2) canclick = false;
                }
                break;
            case 17:
                if (canclick)
                {
                    int zz = 0;
                    foreach (var a in PlayerCons)
                    {
                        if (a.CardType == 6)
                        {
                            zz++;
                        }
                    }
                    if (zz < 1) canclick = false;
                }
                break;
            case 25:
                if (canclick)
                {
                    int zz = 0;
                    foreach (var a in AICons)
                    {
                        if (a != null && a.CanAttack)
                        {
                            zz++;
                            break;
                        }
                    }
                    if (zz < 1) canclick = false;
                }
                break;
            default:
                if (ConstructSizes.ContainsKey(index))
                {
                    if (canclick) canclick = CanPlaceConstruct(index);
                }
                break;
        }

        if (f2.CardType == 16 && PlayerLastCard == null)
        {
            canclick = false;
        }
        if (f2.CardType == 28 && AILastCardTrue == null)
        {
            canclick = false;
        }

        return canclick;
    }
    public int CardActionType(int index)
    {
        switch (index)
        {
            case 12:
                return 6;
            case 14:
                return 7;
            case 15:
                return 8;
            case 25:
                return 11;
            case 29:
                return 14;
            case 30:
                return 15;
            case 23:
            case 17:
                return 9;
            case 18:
                return 10;
            case 11:
            case 10:
                return 5;
            case 9:
                return 4;
            case 27:
                return 12;
            case 28:
                return 13;
            case 20:
            case 8:
                return 3;
            case 7:
                return 2;
            case 19:
            case 22:
            case 5:
                return 1;
            default:
                return 0;
        }
    }

    public int GetConSprite(Card cd)
    {
        switch (cd.CardType)
        {
            case 6:
                return 4;
            case 21:
                return 7;
            case 13:
                if (cd.IsPlayerControlled)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
            case 26:
                if (cd.IsPlayerControlled)
                {
                    return 8;
                }
                else
                {
                    return 9;
                }
            case 1:
            case 2:
            case 4:
                return 3;
            case 3:
                if (cd.IsPlayerControlled)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
        }

        return 0;
    }

    public bool CanPlaceConstruct(int card)
    {
        bool failedsize = false;
        var tf = (CheckSidePlace(card) ? turf : enemyturf);
        var sz = ConstructSizes[card];
        for (int x = 0; x < MapX - (sz[0] - 1); x++)
        {
            for (int y = 0; y < MapY - (sz[1] - 1); y++)
            {
                failedsize = false;
                for (int x2 = 0; x2 < sz[0]; x2++)
                {
                    for (int y2 = 0; y2 < sz[1]; y2++)
                    {
                        //Debug.Log($"checked: {x + x2}, {y + y2},  vals: {x}, {x2}, {y}, {y2}  :  {sz[0]}, {sz[1]}");
                        if (!ValidLocation(tf[x + x2, y + y2]))
                        {
                            failedsize = true;
                            break;
                        }
                    }
                    if (failedsize) break;
                }
                if (!failedsize)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public IEnumerator DamageAnim(GameObject con)
    {
        var ren = con.GetComponent<SpriteRenderer>();
        if (ren != null)
        {
            ren.material = mats[1];
            yield return new WaitForSeconds(0.06f);
            if (ren != null)
            {
                ren.material = mats[0];
            }
        }

        yield return null;
    }

    public void ISwearToFuckingChristifThisFixesThisShittyBugIAAmGoingToKillKona(GameObject cd)
    {
        StartCoroutine(DamageAnim(cd));
    }

    public bool ValidLocation(Card CARD)
    {
        bool canexist = false;
        if (CARD == null) canexist = true;
        else
        {
            if (CARD.CardType == 6) canexist = true;
        }
        return canexist;
    }
    public void ShitLeCard(Card card)
    {
        var lcc = LastCardTrue;
        LastCardTrue = card;
        TimeBetweenCards = 1f;
        CardStreak++;
        if (lcc != null)
        {
            if (lcc.CardType == card.CardType)
            {
                SameCardStreak++;
                if (SameCardStreak >= 5)
                {
                    GetAchievement(7);
                }
                if (SameCardStreak >= 25)
                {
                    GetAchievement(9);
                }
            }
            else
            {
                SameCardStreak = 1;
            }
        }
        else
        {
            SameCardStreak = 1;
        }

    }

    public void CardAction(bool canclick, int type, Card card, bool player = true)
    {

        if (player && card.IsInstant)
        {
            ShitLeCard(card);
        }else if (!player)
        {
            AILastCardTrue = card;
        }
        foreach (var cd in dick.Hand)
        {
            cd.UpdateData();
        }
        if (!player)
        {
            var disp = RandomFunctions.Instance.SpawnObject(1, miscrefs[42], miscrefs[42].transform.position, miscrefs[42].transform.rotation, false, "");
            disp.GetComponent<SpawnData>().card = card;
            disp.GetComponent<CardDisplay>().state = 3;
            disp.GetComponent<CardDisplay>().parente = miscrefs[42];
        }
        else
        {
            SaveSystem.Instance.ProfileData["Cards Played"] = (int.Parse(SaveSystem.Instance.ProfileData["Cards Played"]) + 1).ToString();
        }
        if (canclick)
        {
            CardPlayed = card;
            PlacerCard = card;
            if (CABringOver != null)
            {
                card = CABringOver;
            }
        }
        switch (type)
        {
            case 15:
                if (canclick)
                {

                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 1:
                if (canclick)
                {
                    if (card.CardType == 19)
                    {
                        FeebleOverload = true;
                    }
                    if (card.CardType == 22)
                    {
                        HoleOverload = true;
                    }

                    foreach (var a in player ? PlayerCons : AICons)
                    {
                        if (a.CanAttack)
                        {
                            Attack(a);
                        }
                    }
                    if (player)
                    {
                        StartCoroutine(WaitForPoopyGone(1));
                    }
                }
                break;
            case 2:
                //reconstruictio
                if (canclick)
                {
                    List<Card> list = new List<Card>();

                    ConsoleLol.Instance.ConsoleLog("Random Seed Used: " + card.RandomSeed);
                    System.Random rand = new System.Random(card.RandomSeed);
                    List<Card> c1 = new List<Card>();

                    int j = 0;
                    for (int x = 0; x < MapX; x++)
                    {
                        for (int y = 0; y < MapY; y++)
                        {
                            j = player ? x : ((MapX - 1) - x);
                            var cd = (player ? turf : enemyturf)[j, y];
                            if (cd != null && !c1.Contains(cd) && cd.CardType == 6)
                            {
                                var ii = rand.Next(0, c1.Count);
                                c1.Insert(ii, cd);
                            }
                        }
                    }
                    list = c1;
                    for (int i = 0; i < list.Count / 2; i++)
                    {
                        var segsy = list[i];
                        var sg2 = new Card(1);
                        sg2.IsPlayerControlled = player;
                        (player ? turf : enemyturf)[(int)segsy.GridPos.x, (int)segsy.GridPos.y] = sg2;
                        Destroy(segsy.Construct);
                        Gamer.Instance.CreateConstruct(sg2, (player ? Gamer.Instance.miscrefs[1] : Gamer.Instance.miscrefs[3]).transform.position, new Vector2((int)segsy.GridPos.x, (int)segsy.GridPos.y));
                        (player ? Gamer.Instance.PlayerCons : Gamer.Instance.AICons).Add(sg2);
                        (player ? Gamer.Instance.PlayerCons : Gamer.Instance.AICons).Remove(segsy);
                    }
                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 3:
                if (canclick)
                {
                    if(!player && card.CardType == 20)
                    {
                        Enemyhealth = MaxEnemyhealth;
                    }
                    if (card.CardType == 20)
                    {
                        BanishDiscardOverload = true;
                    }
                    //discard and banish
                    if (player)
                    {
                        DiscardState = true;
                    }
                    else
                    {
                        //Debug.Log("Discard: " + card.dummyval);
                        if (!Multiplayer) Discard(AIDeck.Hand[card.dummyval]);
                    }
                }
                break;
            case 4:
                if (canclick)
                {
                    //discard hand
                    if (Multiplayer && !IsPlayerTurn) break;
                    var d = (player ? dick : AIDeck);
                    if (player)
                    {
                        foreach (var card2 in dick.Hand)
                        {
                            Destroy(card2.Display);
                        }
                        d.ClearHand();
                        DrawCards(HandSizelol, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                    else
                    {
                        d.ClearHand();
                        for (int i = 0; i < supposed_ai_dick; i++) AIDeck.Hand.Add(AIDeck.DrawCard());
                        PlacerCard = null;
                        CardPlayed = null;
                    }
                }
                break;
            case 5:
                //repair cards
                if (canclick)
                {
                    switch (card.CardType)
                    {
                        case 10:
                            if (player)
                            {
                                Health += 5;
                                Health = Mathf.Clamp(Health, 0, MaxHealth);
                            }
                            else
                            {
                                Enemyhealth += 5;
                                Enemyhealth = Mathf.Clamp(Enemyhealth, 0, MaxEnemyhealth);
                            }
                            break;
                        case 11:
                            if (player)
                            {
                                Health += 50;
                                Health = Mathf.Clamp(Health, 0, MaxHealth);
                            }
                            else
                            {
                                Enemyhealth += 50;
                                Enemyhealth = Mathf.Clamp(Enemyhealth, 0, MaxEnemyhealth);
                            }
                            break;

                    }
                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 6:
                //sweep sweep sweep
                if (canclick)
                {

                    ConsoleLol.Instance.ConsoleLog("Random Seed Used: " + card.RandomSeed);

                    List<Card> list = new List<Card>();

                    System.Random rand = new System.Random(card.RandomSeed);
                    List<Card> c1 = new List<Card>();

                    int j = 0;
                    for (int x = 0; x < MapX; x++)
                    {
                        for (int y = 0; y < MapY; y++)
                        {
                            j = player ? x : ((MapX - 1) - x);
                            var cd = (player ? enemyturf : turf)[j, y];
                            if (cd != null && !c1.Contains(cd) && cd.CardType == 6)
                            {
                                var ii = rand.Next(0, c1.Count);
                                c1.Insert(ii, cd);
                            }
                        }
                    }
                    list = c1;



                    for (int i = 0; i < list.Count / 2; i++)
                    {
                        (player ? AICons : PlayerCons).Remove(list[i]);
                        (player ? enemyturf : turf)[(int)list[i].GridPos.x, (int)list[i].GridPos.y] = null;
                        Destroy(list[i].Construct);
                    }
                    if (player)
                    {
                        SweepCount++;
                        if(SweepCount >= 7)
                        {
                            GetAchievement(10);
                        }
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 7:
                //doubleshoit
                if (canclick)
                {
                    ConsoleLol.Instance.ConsoleLog("Random Seed Used: " + card.RandomSeed);
                    System.Random rand = new System.Random(card.RandomSeed);
                    var dickhand = player ? PlayerCons : AICons;
                    List<Card> ppapsex = new List<Card>();

                    foreach (var cd in dickhand)
                    {
                        ppapsex.Insert(rand.Next(0, ppapsex.Count), cd);
                    }
                    List<Card> ppap = new List<Card>();
                    foreach (var cd in ppapsex)
                    {
                        if (ppap.Count > 1) break;
                        if (cd.CanAttack && !cd.IsFakeCard && !ppap.Contains(cd) && cd.ReallyCanAttack()) ppap.Add(cd);
                    }
                    foreach (var cd in ppap)
                    {
                        Attack(cd);
                    }

                    if (player)
                    {
                        StartCoroutine(WaitForPoopyGone(1));
                    }
                }
                break;
            case 8:
                //earthqake
                if (canclick)
                {
                    ConsoleLol.Instance.ConsoleLog("Random Seed Used: " + card.RandomSeed);
                    System.Random rand = new System.Random(player ? card.RandomSeed : (card.RandomSeed + 69));
                    List<Card> c1 = new List<Card>();
                    System.Random rand2 = new System.Random(player ? (card.RandomSeed + 69) : card.RandomSeed);
                    List<Card> c2 = new List<Card>();

                    int j = 0;
                    for (int x = 0; x < MapX; x++)
                    {
                        for (int y = 0; y < MapY; y++)
                        {
                            j = x;
                            var cd = turf[j, y];
                            if (cd != null && !c1.Contains(cd))
                            {
                                var ii = rand.Next(0, c1.Count);
                                if (!cd.IsFakeCard) c1.Insert(ii, cd);
                            }
                        }
                    }
                    for (int x = 0; x < MapX; x++)
                    {
                        for (int y = 0; y < MapY; y++)
                        {
                            j = (MapX - 1) - x;
                            var cd = enemyturf[j, y];
                            if (cd != null && !c2.Contains(cd))
                            {
                                var ii = rand2.Next(0, c2.Count);
                                if (!cd.IsFakeCard) c2.Insert(ii, cd);
                            }
                        }
                    }

                    var e = "";
                    foreach (var cd in c1)
                    {
                        e += cd.CardType + ", ";
                    }
                    ConsoleLol.Instance.ConsoleLog("Rand List 1: " + e);
                    e = "";
                    foreach (var cd in c2)
                    {
                        e += cd.CardType + ", ";
                    }
                    ConsoleLol.Instance.ConsoleLog("Rand List 2: " + e);


                    for (int i = 0; i < (c1.Count * 0.5f); i++)
                    {
                        DamageFunc(c1[i], 30);
                    }
                    for (int i = 0; i < (c2.Count * 0.5f); i++)
                    {
                        DamageFunc(c2[i], 30);
                    }
                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 9:
                if (canclick)
                {
                    var cons = player ? PlayerCons : AICons;
                    List<int> indexes = new List<int>();
                    int ifarty = 0;
                    int ctype = card.CardType == 23 ? 21 : 6;
                    foreach (var e in cons)
                    {
                        if (e.CardType == ctype)
                        {
                            indexes.Add(ifarty);
                        }
                        ifarty++;
                    }

                    foreach (var ii in indexes)
                    {
                        Attack(cons[ii]);
                        Destroy(cons[ii].Construct);
                        if (cons[ii].IsPlayerControlled)
                        {
                            turf[(int)cons[ii].GridPos.x, (int)cons[ii].GridPos.y] = null;
                        }
                        else
                        {
                            enemyturf[(int)cons[ii].GridPos.x, (int)cons[ii].GridPos.y] = null;
                        }
                    }
                    for (int i = 0; i < (player ? PlayerCons : AICons).Count; i++)
                    {
                        if ((player ? PlayerCons : AICons)[i].CardType == ctype)
                        {
                            if (player)
                            {
                                PlayerCons.RemoveAt(i);
                                i--;
                            }
                            else
                            {
                                AICons.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                    break;
                }
                break;
            case 10:
                if (canclick)
                {
                    if (player)
                    {
                        AIOnFire = 5;
                    }
                    else
                    {
                        PlayerOnFire = 5;
                    }

                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 11:
                if (canclick)
                {
                    if (player)
                    {
                        FreezeState = true;
                    }
                    else
                    {
                        var cd = turf[(int)card.GridPos.x, (int)card.GridPos.y];
                        cd.FreezeAmount = 2;
                        cd.UpdateData();
                    }
                }
                break;
            case 12:
                if (canclick)
                {
                    if (player)
                    {
                        RandomFunctions.Instance.SpawnObject(9, gameObject, new Vector3(-7.95f, 1.5f, 0), transform.rotation, false, "");
                        PlayerOnFire = 0;
                        foreach (var con in PlayerCons)
                        {
                            if (con != null)
                            {
                                con.Cleanse();
                            }
                        }
                    }
                    else
                    {
                        RandomFunctions.Instance.SpawnObject(9, gameObject, new Vector3(8, 4.5f, 0), transform.rotation, false, "");
                        AIOnFire = 0;
                        foreach (var con in AICons)
                        {
                            if (con != null)
                            {
                                con.Cleanse();
                            }
                        }
                    }

                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 13:
                if (canclick)
                {
                    var lc = player ? AILastCardTrue : LastCardTrue;
                    determined_draw_cards.Add(lc.CardType);
                    if (player)
                    {
                        DrawCards(1, DefaultDelay, 0.1f);
                        EndTurnCheck();
                    }
                }
                break;
            case 14:
                if (canclick)
                {
                    if (player)
                    {
                        TogglePickerMenu();
                    }
                    else
                    {
                        var li = new List<List<float>>();
                        var lic = new List<Card>();
                        lic.Add(AIDeck.DrawCard());
                        lic.Add(AIDeck.DrawCard());
                        lic.Add(AIDeck.DrawCard());
                        EvaluatePlay(ref li, lic);
                        var chosen = lic[0];
                        float score = -69696969;
                        foreach(var c in li)
                        {
                            if (c[1] > score)
                            {
                                score = c[1];
                                chosen = lic[(int)c[0]];
                            }
                        }
                        determined_draw_cards.Add(chosen.CardType);
                        //ai draw I guess
                    }
                }
                break;
            default:
                if (canclick)
                {
                    if (player)
                    {
                        StartPlacer(card);
                    }
                    else
                    {
                        AIPlaceCard(card, card.GridPos);
                    }
                }
                break;
        }
    }
    public bool youtookdamage = false;
    public void CoreDamageFunc(bool player, float damage)
    {
        if (player)
        {
            Health -= damage;
            youtookdamage = true;
        }
        else
        {
            Enemyhealth -= damage;
        }
    }



    public void DamageFunc(Card cd, float Damage)
    {
        ISwearToFuckingChristifThisFixesThisShittyBugIAAmGoingToKillKona(cd.Construct);
        if (FeebleOverload)
        {
            cd.Feebled = true;
        }
        else
        {
            var ph = cd.Health;
            if (cd.Feebled)
            {
                cd.Health = 0;
            }
            else
            {
                cd.Health -= Damage;
            }
            ph = ph - cd.Health;
            if (ph > 0)
            {
                SoundSystem.Instance.PlaySound(cd.TakeDamageSound);
                float x = ConstructSizes[cd.CardType][0] / 2f - 0.25f;
                float y = ConstructSizes[cd.CardType][1] / 2f - 0.25f;
                if (cd.IndividualParts)
                {
                    x = 0.25f;
                    y = 0.25f;
                }
                var p = WorldPosToCanvas(cd.Construct.transform.position + new Vector3(Random.Range(-x, x), 0.5f + Random.Range(-y, y), 0));
                p.z = miscrefs[2].transform.position.z;
                var dam = RandomFunctions.Instance.SpawnObject(8, miscrefs[88], p, miscrefs[2].transform.rotation, false, "");
                var dm = dam.GetComponent<HitINdie>();
                dm.texty.text = ph.ToString();
            }
            cd.CheckDie();
        }
        cd.UpdateDisplay();
    }

    public Vector3 WorldPosToCanvas(Vector3 pos)
    {
        var p = -miscrefs[16].transform.localPosition;
        p += pos * (cam.orthographicSize / cam2.orthographicSize);
        return p;
    }


    public void Discard(Card card)
    {
        bool player = PlacerCard.IsPlayerControlled;

        if (player)
        {
            ShitLeCard(PlacerCard);
        }

        var p = (player ? dick : AIDeck);
        if (BanishDiscardOverload)
        {
            if (player)
            {
                Health = MaxHealth;
            }
            else
            {
                Enemyhealth = MaxEnemyhealth;
            }
            int i = -1;
            int z = 0;
            foreach (var cd in p.CardsInDeck)
            {
                if (cd.CardType == card.CardType)
                {
                    i = z;
                    break;
                }
                z++;
            }
            p.CardsInDeck.RemoveAt(i);

            i = -1;
            for (int z2 = p.RemainingCards.Count - 1; z2 >= 0; z2--)
            {
                if (p.RemainingCards[z2].CardType == card.CardType)
                {
                    i = z2;
                    break;
                }
            }
            if (i > -1)
            {
                p.RemainingCards.RemoveAt(i);
                if (p.RemainingCards.Count == 0)
                {
                    p.ResetRemainigCards();
                }
            }
            if (player)
            {
                DrawCards(1, DefaultDelay, 0.1f);
                EndTurnCheck();
            }
        }
        else
        {
            if (player)
            {
                if(card.CardType == 8)
                {
                    GetAchievement(8);
                }
                Destroy(card.Display);
                Destroy(PlacerCard.Display);
            }
            p.Hand.Remove(card);
            for (int i = 0; i < p.Hand.Count; i++)
            {
                p.Hand[i].HandIndex = i;
            }
            if (player)
            {
                DrawCards(2, DefaultDelay, 0.1f);
                EndTurnCheck();
            }
            else
            {
                //AIDeck.Hand.Remove(PlacerCard);
                AIDeck.Hand.Add(AIDeck.DrawCard());
            }
        }

    }

    public void GetUnlockedCards()
    {
        GetLevel();
        for (int i = 0; i < CardNames.Length; i++)
        {
            bool unlocked = false;
            if (CardUnlockedWithlevels[i] && Level >= CardLevelUnlock[i])
            {
                unlocked = true;
            }
            if (!CardUnlockedWithlevels[i] && Achievements[CardLevelUnlock[i]])
            {
                unlocked = true;
            }
            CardUnlocked[i] = unlocked;
        }
    }

    public void GetLevel(long xp = -1)
    {
        //input: XP
        //output: Level, RemainingXP, ThreshholdXP


        long r_exp = xp > -1 ? xp : XP;
        long base_l = 500; // base cost
        long mult_p = 11; // mult by, then div by 10
        long thresh = base_l;
        Level = 1;
        while (r_exp >= thresh)
        {
            Level++;
            r_exp -= thresh;
            thresh *= mult_p;
            thresh /= 500;
            thresh *= 50;
        }

        if (LastCheckedLevel < Level)
        {
            for (int i = 0; i < (Level - LastCheckedLevel); i++)
            {
                TBDAchiev.Add(new Notification(1, LastCheckedLevel + i + 1));
            }
        }

        LastCheckedLevel = Level;


        //Console.WriteLine($"Level: {lvl}\nExp: {r_exp}/{thresh}");
        ThreshholdXP = thresh;
        RemainingXP = r_exp;
    }

    public bool CheckDiscardLogic(int type, bool isbanish = false)
    {
        //this code is horrible but I dont want to fix it
        int jj = -1;
        foreach (var i in AIDeck.Hand)
        {
            if (i.CardType == type)
            {
                jj = i.HandIndex;
                break;
            }
        }
        //Debug.Log($"{type}, {jj}");
        var cd = AIDeck.Hand[jj];
        bool shart = true;
        if (cd.CanAttack) return false;
        if (cd.CanCauseAttacks) return false;
        if (cd.CanPlayAgain) return false;
        if (isbanish && shart)
        {
            bool kinkle = false;
            foreach(var e in AIDeck.CardsInDeck)
            {
                if (e.CardType == type) kinkle = true;
            }
            shart = kinkle;
        }
        return shart;
    }

    public bool CheckSidePlace(int index)
    {
        //true = player side, false = enemy side
        switch (index)
        {
            default:
                return true;
        }
    }
    public float Dist(Vector3 p1, Vector3 p2)
    {
        float distance = Mathf.Sqrt(
                Mathf.Pow(p2.x - p1.x, 2f) +
                Mathf.Pow(p2.y - p1.y, 2f) +
                Mathf.Pow(p2.z - p1.z, 2f));
        return distance;
    }

    public void ToPlay()
    {
        checks[0] = false;
        checks[2] = true;
        UpdateMenus();
    }
    public void ToggleProfileMenu()
    {
        checks[9] = !checks[9];
        if (checks[9])
        {
            GetLevel();
            miscrefs[49].GetComponent<TMP_InputField>().text = FileSystem.Instance.UniversalData["Username"];
            miscrefs[76].GetComponent<TextMeshProUGUI>().text = "Level: " + Level;
            double perc = (double)RemainingXP / (double)ThreshholdXP;

            miscrefs[77].transform.localScale = new Vector3((float)perc, 1, 1);

            miscrefs[78].GetComponent<TextMeshProUGUI>().text = RemainingXP.ToString() + " xp";
            miscrefs[79].GetComponent<TextMeshProUGUI>().text = ThreshholdXP.ToString() + " xp";
        }

        UpdateMenus();
    }
    public void UpdateUsername()
    {
        var inf = miscrefs[49].GetComponent<TMP_InputField>();
        var n = inf.text;
        FileSystem.Instance.UniversalData["Username"] = n;
        inf.text = n;
    }

    public void SubmitCode()
    {
        var inf = miscrefs[57].GetComponent<TMP_InputField>();
        var n = inf.text;
        inf.text = "";
        if (SaveSystem.Instance.valid_codes.Contains(n.ToLower()))
        {
            var s = SaveSystem.Instance.StringToList(SaveSystem.Instance.LoadString("Codes"));
            s.Add(n);
            SaveSystem.Instance.SaveString("Codes", SaveSystem.Instance.ListToString(s));
            SaveSystem.Instance.ValidateCodes();
        }
    }

    public void UpdatePlayerNames()
    {
        foreach (var g in miscrefs[50].GetComponentsInChildren<no>())
        {
            Destroy(g.gameObject);
        }

        int i = 0;
        foreach (var a in miscrefs[46].GetComponentsInChildren<PLayerHandywandy>())
        {
            var h = RandomFunctions.Instance.SpawnObject(5, miscrefs[50], miscrefs[50].transform.position, miscrefs[50].transform.rotation, false, "");
            var hh = h.GetComponent<no>();
            hh.textfrom.walter = i;
            hh.pwayer = a;
            a.nono = hh;
            i++;
        }

        AllHandJobs = new List<PLayerHandywandy>(miscrefs[46].GetComponentsInChildren<PLayerHandywandy>());


    }

    public int SavedSelDeck = -1;
    public void ToDeckBuiilder()
    {
        GetUnlockedCards();
        SortType = 0;
        checks[0] = false;
        checks[6] = true;
        SavedSelDeck = SelectedDeck;
        SelectedDeck = 1;
        rectum = miscrefs[31].GetComponent<RectTransform>();
        homie = miscrefs[31].GetComponent<TextMeshProUGUI>();
        buttsex = miscrefs[34].GetComponent<Button>();
        miscrefs[90].GetComponent<TMP_InputField>().text = "";
        ReloadDeckThing();
        UpdateMenus();
    }
    public void ToShop()
    {
        //ToAchievemen

        GetUnlockedCards();

        checks[0] = false;
        checks[10] = true;
        RefreshAchievemtns();
        UpdateMenus();
    }

    public void RefreshAchievemtns()
    {
        foreach (var shex in miscrefs[53].GetComponentsInChildren<Image>())
        {
            Destroy(shex.gameObject);
        }

        var ind = SortAchievements();


        for (int i = 0; i < ind.Count; i++)
        {
            var disp = RandomFunctions.Instance.SpawnObject(6, miscrefs[53], miscrefs[53].transform.position, miscrefs[53].transform.rotation, false, "");
            var f = disp.GetComponent<I_Achievem>();
            f.Title.text = AchievementNames[ind[i]];
            f.BallSex = ind[i];
            f.BG.color = f.cowor[Achievements[ind[i]] ? 0 : 1];
            f.Title.color = f.cowor[Achievements[ind[i]] ? 0 : 1];
        }

    }

    public List<int> SortAchievements()
    {
        var list = new List<int>();
        var endlist = new List<int>();
        var order = " !1234567890abcdefghijklmnopqrstuvwxyz-+?";
        for (int i = 0; i < AchievementNames.Length; i++)
        {
            list.Add(i);
        }

        foreach (var cd in list)
        {
            int i = -1;
            bool esc = false;
            int z = 0;
            foreach (var cd2 in endlist)
            {
                string cdname = AchievementNames[cd].ToLower();
                string cd2name = AchievementNames[cd2].ToLower();
                esc = false;
                int m = Mathf.Min(cdname.Length, cd2name.Length);
                bool getout = true;
                if (cdname == cd2name)
                {
                    esc = false;
                }
                else
                {
                    for (int x = 0; x < m && getout; x++)
                    {
                        if (order.IndexOf(cdname[x]) < order.IndexOf(cd2name[x]))
                        {
                            getout = false;
                        }
                        else if (order.IndexOf(cdname[x]) > order.IndexOf(cd2name[x]))
                        {
                            getout = true;
                            esc = true;
                        }
                    }
                }
                if (!esc)
                {
                    i = z;
                    break;
                }
                z++;
            }
            //Debug.Log("endlisted:" + cd.CardType + ", " + i);
            if (i > -1)
            {
                endlist.Insert(i, cd);
            }
            else
            {
                endlist.Add(cd);
            }
        }
        list.Clear();



        return endlist;
    }



    public int AchievemtCrossover = 0;
    public Color AchievemtColorCrossover;
    public void ToggleAchiOverlay()
    {
        checks[11] = !checks[11];
        if (checks[11])
        {
            miscrefs[67].GetComponent<TextMeshProUGUI>().text = AchievementNames[AchievemtCrossover];
            miscrefs[68].GetComponent<TextMeshProUGUI>().text = AchievementDesc[AchievemtCrossover];
            miscrefs[67].GetComponent<TextMeshProUGUI>().color = AchievemtColorCrossover;
            miscrefs[68].GetComponent<TextMeshProUGUI>().color = AchievemtColorCrossover;
            var e = RandomFunctions.Instance.StringToList(AchievementReward[AchievemtCrossover]);
            var s = "<b>Rewards:</b><br>";
            int i = 0;
            foreach (var ee in e)
            {
                if (i != 0)
                {
                    s += "<br>";
                }
                s += $"- {ee}";
                i++;
            }
            miscrefs[69].GetComponent<TextMeshProUGUI>().text = s;
            miscrefs[69].GetComponent<TextMeshProUGUI>().color = AchievemtColorCrossover;
            miscrefs[55].GetComponent<Image>().color = AchievemtColorCrossover;
        }
        UpdateMenus();
    }
    public List<Notification> TBDAchiev = new List<Notification>();
    public void GetAchievement(int index)
    {
        if (!Achievements[index])
        {
            TBDAchiev.Add(new Notification(0, index));
            Achievements[index] = true;
        }
    }

    public void ToPlay2(bool multiplayer)
    {
        lastnumcheck = -1;
        checks[2] = false;
        checks[3] = true;
        Multiplayer = multiplayer;
        Play2();
    }
    public void BackToPlay2()
    {
        if (Multiplayer && NetworkManager.Singleton.IsHost)
        {
            ServerGamer.Instance.MessageServerRpc(RandomFunctions.Instance.ClientID, "shex", 3);
        }
        checks[5] = false;
        checks[3] = true;
        Play2();
    }
    public void Play2()
    {
        UpdateMenus();
        FixColorOnTextShitThihng();
    }
    public string DataHandover = "";
    public void ToPlay3()
    {
        //SelectedDeck = 0;   
        if (Multiplayer)
        {
            LocalHandJob.IsReady.Value = false;
            if (NetworkManager.Singleton.IsHost)
            {
                string e = "";
                LocalHandJob.IsReady.Value = true;
                Dictionary<string, string> data = new Dictionary<string, string>()
                {
                    {"IllegalDecks", AllowIllegalImmigration?"1":"0"},
                    {"HandSize", HandSizeSel.ToString()},
                };
                e = SaveSystem.Instance.DictionaryToString(data);
                Multiplayer = true;

                ServerGamer.Instance.MessageServerRpc(RandomFunctions.Instance.ClientID, e, 0);
            }
            else
            {
                Dictionary<string, string> data = SaveSystem.Instance.StringToDictionary(DataHandover);
                AllowIllegalImmigration = data["IllegalDecks"] == "1";
                HandSizeSel = int.Parse(data["HandSize"]);

                Multiplayer = true;
            }
        }
        UpdateMyBalls();
        checks[3] = false;
        checks[5] = true;
        UpdateMenus();
    }

    public void ToGameButton()
    {
        if (Multiplayer)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                ToGame();
            }
            else
            {
                LocalHandJob.IsReady.Value = !LocalHandJob.IsReady.Value;
            }
        }
        else
        {
            ToGame();
        }
    }


    public void ToGame()
    {
        //SelectedDeck = 0;   
        if (Multiplayer)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                string e = "";
                Dictionary<string, string> data = new Dictionary<string, string>()
                {
                    {"test","69"}
                };
                e = SaveSystem.Instance.DictionaryToString(data);

                ServerGamer.Instance.MessageServerRpc(RandomFunctions.Instance.ClientID, e, 1);
            }
            else
            {
                Dictionary<string, string> data = SaveSystem.Instance.StringToDictionary(DataHandover);
            }
        }
        checks[5] = false;
        StartGame();
        UpdateMenus();
    }
    public void ToEnd()
    {
        checks[4] = true;


        if (PlayerWon && Health < 16)
        {
            GetAchievement(0);
        }
        if (PlayerWon && !youtookdamage)
        {
            GetAchievement(2);
        }
        if (Turncount <= 15)
        {
            if (PlayerWon)
            {
                GetAchievement(3);
            }
            else
            {
                GetAchievement(4);
            }
        }

        if (PlayerWon)
        {
            //game end win stuff
            long basexp = 100;
            if (!Multiplayer)
            {
                switch (AIDifficulty)
                {
                    case -1:
                        basexp -= 20;
                        break;
                    case 1:
                        basexp += 20;
                        break;
                    case 2:
                        basexp += 50;
                        break;
                }
            }
            if (Junkyard)
            {
                basexp += 50;
            }
            StartCoroutine(WinLevelUpAnim(XP, XP + basexp));
            XP += basexp;
        }
        else if (!PlayerWon)
        {
            StartCoroutine(WinLevelUpAnim(XP, XP));
        }

        if (Multiplayer)
        {
            SaveSystem.Instance.ProfileData["PVP Games Played"] = (int.Parse(SaveSystem.Instance.ProfileData["PVP Games Played"]) + 1).ToString();
            if (PlayerWon) SaveSystem.Instance.ProfileData["PVP Games Won"] = (int.Parse(SaveSystem.Instance.ProfileData["PVP Games Won"]) + 1).ToString();
            if (!PlayerWon) SaveSystem.Instance.ProfileData["PVP Games Lost"] = (int.Parse(SaveSystem.Instance.ProfileData["PVP Games Lost"]) + 1).ToString();
        }
        else
        {
            SaveSystem.Instance.ProfileData["Games Played"] = (int.Parse(SaveSystem.Instance.ProfileData["Games Played"]) + 1).ToString();
            if (PlayerWon) SaveSystem.Instance.ProfileData["Games Won"] = (int.Parse(SaveSystem.Instance.ProfileData["Games Won"]) + 1).ToString();
            if (!PlayerWon) SaveSystem.Instance.ProfileData["Games Lost"] = (int.Parse(SaveSystem.Instance.ProfileData["Games Lost"]) + 1).ToString();
        }


        dolphin = miscrefs[35].GetComponent<TextMeshProUGUI>();
        dolphin.text = PlayerWon ? "Victory" : "Defeat";
        UpdateMenus();
    }
    public int DickCounter = 0;
    public void TogglePauseMenu()
    {
        checks[7] = !checks[7];
        UpdateMenus();
    }
    public void ToggleCreditsMenu()
    {
        checks[8] = !checks[8];
        UpdateMenus();
    }
    public void ToggleSettingsMenu()
    {
        miscrefs[86].SetActive(checks[1]);
        checks[1] = !checks[1];
        if (checks[1])
        {
            miscrefs[5].GetComponent<SettingHandler>().SetVals();
        }
        else
        {
            if (GameState == "Main Menu")
            {
                SaveSystem.Instance.SaveGame();
            }
        }
        miscrefs[4].GetComponent<MainMenuShit>().Scramble();

        UpdateMenus();
    }

    public void MainMenu()
    {
        GetUnlockedCards();
        if (checks[6]) SelectedDeck = SavedSelDeck;
        miscrefs[47].SetActive(false);
        ResetChecks();
        SetSizes();
        canescape = false;
        checks[0] = true;
        determined_draw_cards.Clear();
        Multiplayer = false;
        GameState = "Main Menu";
        PlayerLastCard = null;
        AILastCard = null;
        LastCardTrue = null;
        AILastCardTrue = null;
        LocalHandJob = null;
        AllHandJobs.Clear();
        PlacerCard = null;
        CardPlayed = null;
        PlacerState = false;

        if (Time.time > 1f) SaveSystem.Instance.SaveGame();

        NetworkManager.Singleton.Shutdown();

        UpdateMenus();
    }
    public IEnumerator WaitForConn()
    {
        yield return new WaitUntil(() =>
        {
            return miscrefs[46].transform.childCount > 0;
        });
        if(miscrefs[46].transform.childCount > 2)
        {
            MainMenu();
        }
        else
        {
            miscrefs[47].SetActive(false);
        }

    }
    public void ResetChecks()
    {
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i] = false;
        }
    }

    public void ZTest(TMP_InputField simp)
    {
        LoadedDecks[SelectedDeck][0] = simp.text;
    }
    List<GameObject> nerds = new List<GameObject>();
    List<GameObject> nerds2 = new List<GameObject>();
    public void ReloadDeckThing()
    {
        var li = new List<int>();
        li = SortCardIndexes(li);

        var sex = miscrefs[28].GetComponentsInChildren<CardDisplay>();


        for (int i = 0; i < li.Count; i++)
        {
            if (!CardUnlocked[li[i]] || (new Card(li[i])).IsFakeCard)
            {
                li.RemoveAt(i);
                i--;
            }
        }

        var diff = li.Count - sex.Length;
        for (int i2 = 0; i2 < diff; i2++)
        {
            var disp = RandomFunctions.Instance.SpawnObject(1, miscrefs[28], miscrefs[28].transform.position, miscrefs[28].transform.rotation, false, "");
            nerds.Add(disp);
        }
        for (int i2 = 0; i2 < -diff; i2++)
        {
            try
            {
                Destroy(nerds[0]);
                nerds.RemoveAt(0);
            }
            catch
            {

            }
        }


        int i3 = 0;
        foreach(var disp in nerds)
        {
            var cd = new Card(li[i3]);
            disp.GetComponent<SpawnData>().card = cd;
            disp.GetComponent<CardDisplay>().state = 1;
            //disp.GetComponent<CardDisplay>().Start();
            i3++;
        }
        li.Clear();
        li = SortCardIndexes(li, true);

        sex = miscrefs[29].GetComponentsInChildren<CardDisplay>();


        for (int i = 0; i < li.Count; i++)
        {
            if (!CardUnlocked[li[i]] || (new Card(li[i])).IsFakeCard)
            {
                li.RemoveAt(i);
                i--;
            }
        }
        diff = li.Count - sex.Length;
        for (int i2 = 0; i2 < diff; i2++)
        {
            var disp = RandomFunctions.Instance.SpawnObject(1, miscrefs[29], miscrefs[29].transform.position, miscrefs[29].transform.rotation, false, "");
            nerds2.Add(disp);
        }
        for (int i2 = 0; i2 < -diff; i2++)
        {
            try
            {
                Destroy(nerds2[0]);
                nerds2.RemoveAt(0);
            }
            catch
            {

            }
        }
        if(nerds2.Count < li.Count)
        {
            var disp = RandomFunctions.Instance.SpawnObject(1, miscrefs[29], miscrefs[29].transform.position, miscrefs[29].transform.rotation, false, "");
            nerds2.Add(disp);
        }
        i3 = 0;
        foreach (var disp in nerds2)
        {
            if(disp != null)
            {
                var cd = new Card(li[i3]);
                disp.GetComponent<SpawnData>().card = cd;
                disp.GetComponent<CardDisplay>().state = 2;
                disp.GetComponent<CardDisplay>().Start();
                i3++;
            }
        }

        miscrefs[38].GetComponent<TMP_InputField>().text = LoadedDecks[SelectedDeck][0];

        var deck = new Deck();
        deck.LoadDeck(LoadedDecks[SelectedDeck][1], true);
        var xx = deck.CheckValid();

        if (xx)
        {
            homie.text = "Legal Deck";
            homie.color = homie_colors[0];
            miscrefs[31].GetComponent<HoveringBallsInYourJaw>().HoverClarifyText = "Deck is valid";
        }
        else
        {
            homie.text = "Illegal Deck";
            homie.color = homie_colors[1];
            string hh = "";
            foreach (var e in deck.Errors)
            {
                hh += e + ", ";
            }
            miscrefs[31].GetComponent<HoveringBallsInYourJaw>().HoverClarifyText = hh;
        }

    }
    /*
    public IEnumerator SpawnShads(List<string>gg, int sel, int sel2)
    {
        int re = 0;
        var e = miscrefs[58].GetComponent<Scrollbar>();
        for (int i = 1; i < gg.Count; i++)
        {
            if(re > 30)
            {
                yield return null; 
                e.value = perc;
                re = 0;
            }
            if (sel != SelectedDeck || sel2 != vall) goto endme;
            re++;
        }
        for(int i = 0; i < 5; i++)
        {
            yield return null;
            e.value = perc;
        }
    endme:;
    }
    */
    public void CreateNewDeck()
    {
        LoadedDecks.Add(new List<string>() { "Unnamed", $"{gameVer}" });
        SelectedDeck = LoadedDecks.Count - 1;
        ReloadDeckThing();
    }
    public void DeleteDeck()
    {
        if (CanDeleteDeck())
        {
            LoadedDecks.RemoveAt(SelectedDeck);
            ShitFard(false);
        }
    }

    public void ShitFard(bool increment)
    {
        if (Multiplayer)
        {
            LocalHandJob.IsReady.Value = false;
        }
        if (increment)
        {
            SelectedDeck++;
        }
        else
        {
            SelectedDeck--;
        }
        SelectedDeck = RandomFunctions.Instance.Mod(SelectedDeck, LoadedDecks.Count);
        if (SelectedDeck == 0 && checks[6] && !DevMode)
        {
            ShitFard(increment);
        }
        var d = new Deck();
        d.LoadDeck(LoadedDecks[SelectedDeck][1], false);
        if (!checks[6] && d.CardsInDeck.Count < 1)
        {
            ShitFard(increment);
        }
        else
        {
            if (checks[6]) ReloadDeckThing();


            if (checks[5])
            {
                UpdateMyBalls();
            }
        }

    }
    public int SelectedTitle = 0;
    public void TitleShid(bool inc = true)
    {
        if (inc)
        {
            SelectedTitle++;
        }
        else
        {
            SelectedTitle--;
        }
        SelectedTitle = RandomFunctions.Instance.Mod(SelectedTitle, Titles.Count);
        if (SaveSystem.Instance.ProfileData.ContainsKey("Title"))
        {
            SaveSystem.Instance.ProfileData["Title"] = SelectedTitle.ToString();
        }
        else
        {
            SaveSystem.Instance.ProfileData.Add("Title", SelectedTitle.ToString());
        }
        bool re = false;
        switch (SelectedTitle)
        {
            case 2:
                re = !secretplayerstuff[0];
                break;
            case 5:
                re = !secretplayerstuff[6];
                break;
            case 9:
                re = !secretplayerstuff[4];
                break;
            case 12:
                re = !secretplayerstuff[5];
                break;
            case 13:
                re = !secretplayerstuff[1];
                break;
            case 14:
                re = !secretplayerstuff[2];
                break;
            case 15:
                re = !secretplayerstuff[3];
                break;
            case 19:
                re = !Achievements[9];
                break;
            case 18:
                re = !Achievements[10];
                break;
            case 20:
                re = !Achievements[6];
                break;
            case 0:
                break;
            default:
                re = true;
                break;
        }
        if (re) TitleShid(inc);
    }



    public int SelectedDeckDos = 0;
    public void ShitFardDos(bool increment)
    {
        if (Multiplayer)
        {
            LocalHandJob.IsReady.Value = false;
        }
        if (increment)
        {
            SelectedDeckDos++;
        }
        else
        {
            SelectedDeckDos--;
        }
        SelectedDeckDos = RandomFunctions.Instance.Mod(SelectedDeckDos, LoadedDecks.Count);
        var d = new Deck();
        d.LoadDeck(LoadedDecks[SelectedDeckDos][1], false);
        if (d.CardsInDeck.Count < 1)
        {
            ShitFardDos(increment);
        }
        else
        {
            if (checks[5])
            {
                UpdateMyBalls(1);
            }
        }

    }
    public int AiDiffSelect = 0;
    public void FardShit(bool incremeintnt)
    {
        if (incremeintnt)
        {
            AiDiffSelect++;
        }
        else
        {
            AiDiffSelect--;
        }
        AiDiffSelect = RandomFunctions.Instance.Mod(AiDiffSelect, 4);
        FixColorOnTextShitThihng();
    }
    public void FixColorOnTextShitThihng()
    {
        miscrefs[44].GetComponent<TextMeshProUGUI>().color = diff_colors[AiDiffSelect];
    }

    public void UpdateMyBalls(int type = 0)
    {
        var d = new Deck();
        var d2 = new Deck();
        d.LoadDeck(LoadedDecks[SelectedDeck][1], true);
        d2.LoadDeck(LoadedDecks[SelectedDeckDos][1], true);
        d.CheckValid();
        d2.CheckValid();
        bool e = d.ValidDeck;
        bool e2 = d2.ValidDeck;
        miscrefs[36].GetComponent<TextMeshProUGUI>().text = e ? "" : "Illegal Deck";
        miscrefs[45].GetComponent<TextMeshProUGUI>().text = e2 ? "" : "Illegal Deck";
        if (Multiplayer)
        {
            miscrefs[92].GetComponent<TextMeshProUGUI>().text = NetworkManager.Singleton.IsHost ? "Play Game" : "Ready";
        }

        var eee = (e && e2) || AllowIllegalImmigration;
        if(eee && Multiplayer && NetworkManager.Singleton.IsHost)
        {
            bool sexmogger = true;
            foreach(var hj in AllHandJobs)
            {
                if (!hj.IsReady.Value && hj != LocalHandJob)
                {
                    sexmogger = false;
                    break;
                }
            }
            eee = sexmogger;
        }

        miscrefs[37].GetComponent<Button>().interactable = eee;
    }

    public void TogglePickerMenu()
    {
        checks[14] = !checks[14];
        if (checks[14])
        {
            foreach(var cd in miscrefs[96].GetComponentsInChildren<CardDisplay>())
            {
                Destroy(cd.gameObject);
            }
            for(int i = 0; i < 3; i++)
            {
                var cd = RandomFunctions.Instance.SpawnObject(1, miscrefs[96], miscrefs[96].transform.position, miscrefs[96].transform.rotation, false, "").GetComponent<CardDisplay>();
                cd.state = 7;
                cd.GetComponent<SpawnData>().card = dick.DrawCard();
                cd.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
        }
        else
        {
            DrawCards(1, DefaultDelay, 0.1f);
            EndTurnCheck();
        }
        UpdateMenus();
    }

    public void UpdateMenus()
    {
        miscrefs[2].SetActive(true);
        miscrefs[10].SetActive(true);
        miscrefs[4].SetActive(checks[0]); // main menu
        miscrefs[5].SetActive(checks[1]); // settings
        miscrefs[6].SetActive(checks[2]); // play
        miscrefs[7].SetActive(checks[3]); // play2
        miscrefs[17].SetActive(checks[4]); // end screen
        miscrefs[22].SetActive(checks[5]); // play 3
        miscrefs[27].SetActive(checks[6]); // deckbuilder
        miscrefs[40].SetActive(checks[7]); // pause
        miscrefs[48].SetActive(checks[9]); // profile
        miscrefs[51].SetActive(checks[8]); // credits
        miscrefs[52].SetActive(checks[10]); // achievementos
        miscrefs[54].SetActive(checks[11]); // achievementos overgay
        miscrefs[59].SetActive(checks[12]); // logbook
        miscrefs[61].SetActive(checks[13]); // card desc overlay
        miscrefs[95].SetActive(checks[14]); // picker menu
        miscrefs[8].SetActive(!Multiplayer);
        miscrefs[9].SetActive(Multiplayer);
        miscrefs[93].SetActive(Multiplayer);
    }

    public void SetMenu(int menu, bool state)
    {
        switch (menu)
        {
            default:
                checks[menu] = !state;
                break;
        }
        ToggleMenu(menu);
    }

    public void ToggleMenu(int menu)
    {
        switch (menu)
        {
            default:
                checks[menu] = !checks[menu];
                break;
        }
        UpdateMenus();
    }
    public void ToggleLogbook()
    {
        miscrefs[86].SetActive(checks[12]);
        checks[12] = !checks[12];
        if (checks[12])
        {
            SortType = 0;
            miscrefs[89].GetComponent<TMP_InputField>().text = "";
            UpdateLogbookCards();
        }
        UpdateMenus();
    }
    private List<GameObject> shites = new List<GameObject>();
    public void UpdateLogbookCards()
    {
        GetUnlockedCards();
        List<int> indexes = new List<int>();
        indexes = SortCardIndexes(indexes);
        var e = miscrefs[60].GetComponentsInChildren<CardDisplay>();

        int overunder = indexes.Count - e.Length;
        for (int i = 0; i < overunder; i++)
        {
            var disp = RandomFunctions.Instance.SpawnObject(1, miscrefs[60], miscrefs[60].transform.position, miscrefs[60].transform.rotation, false, "");
            shites.Add(disp);
        }
        for (int i = 0; i < -overunder; i++)
        {
            Destroy(shites[0]);
            shites.RemoveAt(0);
        }
        for (int i = 0; i < indexes.Count; i++)
        {
            //Debug.Log("spawned: " + indexes[i]);
            var cd = new Card(indexes[i]);
            if (cd.IsFakeCard) continue;
            var disp = shites[i];
            disp.GetComponent<SpawnData>().card = cd;
            disp.GetComponent<CardDisplay>().state = 5;
            disp.GetComponent<CardDisplay>().index = i;
            disp.GetComponent<CardDisplay>().Start();
        }
    }
    public int SortType = 0;
    public List<int> SortCardIndexes(List<int> inp, bool shart = false)
    {
        var list = new List<Card>();
        var lockedbois = new List<Card>();
        var endlist = new List<Card>();
        var order= " !1234567890abcdefghijklmnopqrstuvwxyz-+";

        bool isdeckb = checks[6];

        string search = miscrefs[isdeckb?90:89].GetComponent<TMP_InputField>().text.ToLower();
        if (shart)
        {
            int x = 0;
            foreach(var z in RandomFunctions.Instance.StringToList(LoadedDecks[SelectedDeck][1]))
            {
                x++;
                if (x == 1) continue;
                var i = int.Parse(z);
                var e = new Card(i);
                if (e.IsFakeCard) continue;
                if (CardUnlocked[i])
                {
                    if (search == "" || CardNames[i].ToLower().Contains(search) || CardDescriptions[i].ToLower().Contains(search)) list.Add(e);
                }
                else
                {
                    if (search == "") lockedbois.Add(e);
                }
            }
        }
        else
        {
            for (int i = 1; i < CardNames.Length; i++)
            {
                var e = new Card(i);
                if (e.IsFakeCard) continue;
                if (CardUnlocked[i])
                {
                    if (search == "" || CardNames[i].ToLower().Contains(search) || CardDescriptions[i].ToLower().Contains(search)) list.Add(e);
                }
                else
                {
                    if (search == "") lockedbois.Add(e);
                }
            }
        }

        foreach (var cd in list)
        {
            int i = -1;
            bool esc = false;
            int z = 0;
            foreach (var cd2 in endlist)
            {
                string cdname = CardNames[cd.CardType].ToLower();
                string cd2name = CardNames[cd2.CardType].ToLower();
                esc = false;
                int m = Mathf.Min(cdname.Length, cd2name.Length);
                bool getout = true;
                if (cdname == cd2name)
                {
                    esc = false;
                }
                else
                {
                    for (int x = 0; x < m && getout; x++)
                    {
                        if (order.IndexOf(cdname[x]) < order.IndexOf(cd2name[x]))
                        {
                            getout = false;
                        }
                        else if (order.IndexOf(cdname[x]) > order.IndexOf(cd2name[x]))
                        {
                            getout = true;
                            esc = true;
                        }
                    }
                }
                if (!esc)
                {
                    i = z;
                    break;
                }
                z++;
            }
            //Debug.Log("endlisted:" + cd.CardType + ", " + i);
            if (i > -1)
            {
                endlist.Insert(i, cd);
            }
            else
            {
                endlist.Add(cd);
            }
        }
        list.Clear();




        switch (SortType)
        {
            case 1:
                //attribue sort
                endlist.Reverse();
                foreach (var cd in endlist)
                {
                    int i = -1;
                    bool esc = false;
                    int z = 0;
                    foreach (var cd2 in list)
                    {
                        esc = false;
                        bool getout = true;
                        for (int x = 0; x < cd.AttributesHeld.Count && getout; x++)
                        {
                            if (cd.AttributesHeld[x] && !cd2.AttributesHeld[x])
                            {
                                getout = false;
                            }
                            else if (!cd.AttributesHeld[x] && cd2.AttributesHeld[x])
                            {
                                getout = false;
                                esc = true;
                            }
                        }
                        if (!esc)
                        {
                            i = z;
                            break;
                        }
                        z++;
                    }
                    //Debug.Log("endlisted:" + cd.CardType + ", " + i);
                    if (i > -1)
                    {
                        list.Insert(i, cd);
                    }
                    else
                    {
                        list.Add(cd);
                    }
                }
                break;
            case 2:
                //energy cost sort
                endlist.Reverse();
                foreach (var cd in endlist)
                {
                    int i = -1;
                    bool esc = false;
                    int z = 0;
                    foreach (var cd2 in list)
                    {
                        esc = false;

                        if(cd.EnergyCost > cd2.EnergyCost)
                        {
                            esc = true;
                        }


                        if (!esc)
                        {
                            i = z;
                            break;
                        }
                        z++;
                    }
                    //Debug.Log("endlisted:" + cd.CardType + ", " + i);
                    if (i > -1)
                    {
                        list.Insert(i, cd);
                    }
                    else
                    {
                        list.Add(cd);
                    }
                }
                break;
            default:
                list = endlist;
                break;
        }


        inp.Clear();


        foreach (var cd in list)
        {
            inp.Add(cd.CardType);
        }
        foreach (var cd in lockedbois)
        {
            inp.Add(cd.CardType);
        }


        return inp;
    }




    public void ChangeSortType(int type)
    {
        if(type != SortType)
        {
            SortType = type;
            if (checks[6])
            {
                ReloadDeckThing();
            }
            else
            {
                UpdateLogbookCards();
            }
        }
    }


    public Card CardToInspect;
    public CardDisplay CardDickSplayed;
    public void ToggleLogbookOvergay()
    {
        checks[13] = !checks[13];
        if (checks[13])
        {
            var ppshex = miscrefs[62].GetComponentInChildren<CardDisplay>();
            if (ppshex != null)
            {
                Destroy(ppshex.gameObject);
            }
            var cd = CardToInspect;
            var disp = RandomFunctions.Instance.SpawnObject(1, miscrefs[62], miscrefs[62].transform.position, miscrefs[62].transform.rotation, false, "");
            disp.GetComponent<SpawnData>().card = cd;
            disp.GetComponent<CardDisplay>().state = 6;
            int index = CardToInspect.CardType;
            bool unlocked = CardUnlocked[index];
            miscrefs[63].GetComponent<TextMeshProUGUI>().text = unlocked ? CardNames[index] : "???";
            miscrefs[64].GetComponent<TextMeshProUGUI>().text = unlocked ? CardLongDescriptions[index] : (CardUnlockedWithlevels[index] ? $"Unlock at level {CardLevelUnlock[index]}" : AchievementDesc[CardLevelUnlock[index]]);
            List<Color> fuckyoucsharp = new List<Color>();
            foreach (var c in AttributeColor)
            {
                fuckyoucsharp.Add(c);
            }
            foreach (var pp in miscrefs[75].GetComponentsInChildren<I_Att>())
            {
                Destroy(pp.gameObject);
            }
            foreach (var o in CardDickSplayed.things)
            {
                var i = RandomFunctions.Instance.SpawnObject(7, miscrefs[75], miscrefs[75].transform.position, miscrefs[75].transform.rotation, false, "");
                var es = i.GetComponent<I_Att>();
                var col = o.GetComponent<Image>().color;
                var coli = fuckyoucsharp.IndexOf(col);
                es.BG.color = col;
                es.Title.text = AttributeName[coli];
                es.Desc.text = AttributeDesc[coli];
            }

        }
        UpdateMenus();
    }

    public void FuckOffUnity(GameObject pp)
    {
        Destroy(pp);
    }

    public void DeckToClipboard()
    {
        var e = LoadedDecks[SelectedDeck][1];
        CopyToCLip(e);
    }
    public void CodeToClipboard()
    {
        var e = RelayMoment.Instance.Join_Code;
        CopyToCLip(e);
    }
    public string CleanDeck(string d)
    {
        var e = RandomFunctions.Instance.StringToList(d, ", ");
        for (int i = 1; i < e.Count; i++)
        {
            int f = int.Parse(e[i]);
            //Debug.Log($"{f}, {!CardUnlocked[f]}");
            if (!CardUnlocked[f])
            {
                e.RemoveAt(i);
                i--;
            }
        }
        return RandomFunctions.Instance.ListToString(e, ", ");
    }

    public void ClipBoardToDeck()
    {
        try
        {
            var e = Clipb.GetClipboard();
            e = CleanDeck(e);
            var p = float.Parse(RandomFunctions.Instance.StringToList(e)[0]) <= gameVer;
            if (p)
            {
                LoadedDecks[SelectedDeck][1] = e;
                ReloadDeckThing();
            }
        }
        catch
        {
            Debug.Log("Failed load");
        }
    }

    public void CopyToCLip(string e)
    {
        Clipb.CopyToClipboard(e);
    }
}



public class Deck
{
    public List<Card> RemainingCards = new List<Card>();
    public List<Card> CardsInDeck = new List<Card>();
    public bool ValidDeck = false;
    public List<string> Errors = new List<string>();
    public List<Card> Hand = new List<Card>();

    public Card DrawCard()
    {
        int x = 0;
        Card card = null;
        try
        {
            card = RemainingCards[x];
        }
        catch
        {
            return null;
        }
        RemainingCards.RemoveAt(x);

        if (RemainingCards.Count == 0)
        {
            ResetRemainigCards();
        }

        return card;
    }
    public void Shuffle(ref List<Card> rem)
    {
        List<Card> cards = new List<Card>();
        foreach (var c in rem)
        {
            cards.Insert(Random.Range(0, cards.Count), c);
        }
        rem = cards;
    }
    public void ClearHand()
    {
        Hand.Clear();
    }
    public void ResetRemainigCards()
    {
        RemainingCards.Clear();
        foreach (var c in CardsInDeck)
        {
            var e = new Card(c);
            e.UpdateShat();
            RemainingCards.Add(e);
        }
        Shuffle(ref RemainingCards);
    }

    public void LoadDeck(string e, bool resetcards)
    {
        // first number is the version number, not a card
        //  4, 69, 123455, 2, 4 
        var es = RandomFunctions.Instance.StringToList(e);

        int x = -1;
        int v = 0;
        int z = 0;
        CardsInDeck.Clear();
        foreach (var i in es)
        {
            if (z == 0)
            {
                v = int.Parse(i);
            }
            else
            {
                try
                {
                    x = int.Parse(i);
                    CardsInDeck.Add(new Card(x));
                }
                catch
                {

                }
            }
            z++;
        }

        if (resetcards) ResetRemainigCards();
        CheckValid();
    }

    public bool CheckValid()
    {
        int x = CardsInDeck.Count;
        Errors.Clear();
        ValidDeck = x <= 75 && x >= 15;
        if (x > 75)
        {
            Errors.Add("Deck size is greater than 75");
        }
        else if (x < 15)
        {
            Errors.Add("Deck size is less than 15");
        }
        List<int> sex = new List<int>();
        List<int> sex2 = new List<int>();
        List<int> sex3 = new List<int>();
        List<int> sex4 = new List<int>();
        List<int> sex5 = new List<int>();
        foreach (var c in CardsInDeck)
        {
            if (!sex.Contains(c.CardType))
            {
                sex.Add(c.CardType);
            }
            if (c.CanAttack)
            {
                sex2.Add(c.CardType);
            }
            if (c.CanCauseAttacks)
            {
                sex3.Add(c.CardType);
            }
            if (c.CanPlayAgain)
            {
                sex4.Add(c.CardType);
            }
            if (c.IsWallType)
            {
                sex5.Add(c.CardType);
            }
        }
        int amnt = sex.Count;
        if (amnt < 5)
        {
            ValidDeck = false;
            Errors.Add("Less than 5 unique cards in the deck");
        }
        int amnt2 = sex2.Count;
        if (amnt2 < 1)
        {
            ValidDeck = false;
            Errors.Add("No attack type cards");
        }
        int amnt3 = sex3.Count;
        if (amnt3 < 1)
        {
            ValidDeck = false;
            Errors.Add("No attack activation cards");
        }
        int amnt4 = sex4.Count;
        if (amnt4 > (CardsInDeck.Count / 2))
        {
            ValidDeck = false;
            Errors.Add("Contains over 50% Play Again cards");
        }
        if ((amnt2 + amnt3) > (CardsInDeck.Count / 3))
        {
            ValidDeck = false;
            Errors.Add("Contains over 33% Offensive cards");
        }
        if ((sex5.Count) <= (CardsInDeck.Count / 5))
        {
            ValidDeck = false;
            Errors.Add("Contains fewer than 20% Wall cards");
        }

        return ValidDeck;
    }

}
public class Card
{
    //stuff
    public int CardType = -1;
    public int SubNumber = -1;
    //stuff assigned when card is played or in play
    public Vector2 GridPos = Vector2.zero;
    public GameObject Construct;
    public bool IsHover = false;
    //stuff assigned when card is drawn
    public bool IsPlayerControlled = false;
    public bool IsWallType = false;
    public bool CanAttack = false;
    public bool IndividualParts = false;
    public bool CanCauseAttacks = false;
    public bool CanPlayAgain = false;
    public bool IsFakeCard = false;
    public bool AISecondEval = false;
    public bool AIOneMax = false;
    public bool IsHeavy = false;
    public GameObject Display;
    public Vector3 target_pos = new Vector3(0, 0, 0);
    public int HandIndex = -1;
    public float Health = 15f;
    public float MaxHealth = 15f;
    public int EnergyCost = 5;
    public int BaseEnergyCost = 5;
    public bool IsDead = false;
    public bool IsInstant = false;
    public int dummyval = -1;
    public int RandomSeed = 69420;
    public bool UsesRandom = false;
    public bool Feebled = false;
    public int AttackSound = 0;
    public int TakeDamageSound = 1;
    public int PlaceeSound = 2;
    public List<string> debuffs = new List<string>();

    public int FreezeAmount = 0;

    public List<bool> AttributesHeld = new List<bool>();
    public void UpdateShat()
    {
        RandomSeed = Random.Range(0, 1000000000);
    }


    public void SetVals(int index = -1)
    {
        UpdateShat();
        bool attacker = false;
        bool indi = false;
        bool wall = false;
        bool causeatt = false;
        bool again = false;
        bool fake = false;
        bool secondeval = false;
        bool one = false;
        bool heavy = false;
        int energy = 5;
        bool rand = false;
        Health = 15f;
        AttackSound = 0;
        TakeDamageSound = 1;
        PlaceeSound = 2;
        if (index == -1) index = CardType;
        IsInstant = !Gamer.Instance.ConstructSizes.ContainsKey(index);
        switch (index)
        {
            case 1:
                wall = true;
                indi = true;
                again = true;
                Health = 15f;
                energy = 1;
                break;
            case 2:
                wall = true;
                indi = true;
                Health = 15f;
                energy = 3;
                break;
            case 3:
                attacker = true;
                Health = 30f;
                energy = 5;
                break;
            case 4:
                wall = true;
                indi = true;
                Health = 15f;
                energy = 4;
                break;
            case 5:
                causeatt = true;
                energy = 5;
                break;
            case 6:
                fake = true;
                energy = 69;
                break;
            case 7:
                one = true;
                energy = 5;
                rand = true;
                break;
            case 8:
                again = true;
                secondeval = true;
                energy = 0;
                IsInstant = false;
                break;
            case 9:
                secondeval = true;
                one = true;
                energy = 0;
                break;
            case 10:
                again = true;
                one = true;
                energy = 1;
                break;
            case 11:
                one = true;
                energy = 7;
                break;
            case 12:
                again = true;
                energy = 4;
                rand = true;
                break;
            case 13:
                attacker = true;
                Health = 30f;
                energy = 7;
                break;
            case 14:
                again = true;
                causeatt = true;
                energy = 3;
                rand = true;
                break;
            case 15:
                heavy = true;
                one = true;
                energy = 15;
                rand = true;
                break;
            case 16:
                SubNumber = 1;
                one = true;
                break;
            case 17:
                one = true;
                energy = 10;
                break;
            case 18:
                one = true;
                energy = 4;
                break;
            case 19:
                one = true;
                causeatt = true;
                break;
            case 20:
                IsInstant = false;
                break;
            case 21:
                fake = true;
                energy = 69;
                break;
            case 22:
                causeatt = true;
                energy = 7;
                break;
            case 23:
                one = true;
                energy = 10;
                break;
            case 24:
                energy = 1;
                again = true;
                break;
            case 25:
                energy = 4;
                again = true;
                break;
            case 26:
                Health = 25;
                energy = 7;
                attacker = true;
                heavy = true;
                one = true;
                break;
            case 27:
                energy = 3;
                break;
            case 28:
                energy = 2;
                SubNumber = 2;
                again = true;
                break;
            case 29:
                energy = 2;
                again = true;
                break;
            case 30:
                energy = 1;
                again = true;
                break;
        }
        CanAttack = attacker;
        IsWallType = wall;
        IndividualParts = indi;
        CanCauseAttacks = causeatt;
        CanPlayAgain = again;
        IsFakeCard = fake;
        AISecondEval = secondeval;
        AIOneMax = one;
        IsHeavy = heavy;
        EnergyCost = energy;
        BaseEnergyCost = energy;
        UsesRandom = rand;
        MaxHealth = Health;
        AttributesHeld = new List<bool>
        {
            CanPlayAgain,
            CanAttack,
            CanCauseAttacks,
            IsWallType,
            IsHeavy
        };
    }

    public bool ReallyCanAttack()
    {
        bool e = CanAttack;
        if (e)
        {
            e = FreezeAmount <= 0;
        }
        return e;
    }

    public void CheckDie(bool overr2ide = false)
    {
        if (IsDead) return;
        if (Health <= 0 || overr2ide)
        {

            //IsDead = true;
            var g = Gamer.Instance;
            if (Construct != null) g.FuckOffUnity(Construct);
            if (g.ConstructSizes.ContainsKey(CardType))
            {
                var x = IndividualParts ? new List<int>() { 1, 1 } : g.ConstructSizes[CardType];
                var pp = g.CheckSidePlace(CardType);
                if (!IsPlayerControlled) pp = !pp;
                if (IsPlayerControlled)
                {
                    g.PlayerCons.Remove(this);
                }
                else
                {
                    g.AICons.Remove(this);
                }
                //Debug.Log((pp?"PlayerSideClear":"EnemySideClear") + $"cd: {CardType}, {(int)Time.time}, {(int)GridPos.x}, {(int)GridPos.y}");
                var p = pp ? g.turf : g.enemyturf;

                Card segsy;
                if (Gamer.Instance.HoleOverload)
                {
                    segsy = new Card(21);
                }
                else
                {
                    segsy = IsWallType ? new Card(6) : null;
                }
                Gamer.Instance.DickCounter++;
                for (int v1 = 0; v1 < x[0]; v1++)
                {
                    for (int v2 = 0; v2 < x[1]; v2++)
                    {
                        p[(int)GridPos.x + v1, (int)GridPos.y + v2] = segsy;
                        if (segsy != null)
                        {
                            Gamer.Instance.CreateConstruct(segsy, (Gamer.Instance.CheckSidePlace(CardType) ? (IsPlayerControlled ? Gamer.Instance.miscrefs[1] : Gamer.Instance.miscrefs[3]) : (IsPlayerControlled ? Gamer.Instance.miscrefs[3] : Gamer.Instance.miscrefs[1])).transform.position, new Vector2((int)GridPos.x + v1, (int)GridPos.y + v2));
                            segsy.IsPlayerControlled = IsPlayerControlled;
                            (IsPlayerControlled ? Gamer.Instance.PlayerCons : Gamer.Instance.AICons).Add(segsy);
                        }
                    }
                }

            }

        }
    }

    public void UpdateData()
    {
        if (SubNumber == 1)
        {
            var e = (IsPlayerControlled ? Gamer.Instance.PlayerLastCard : Gamer.Instance.AILastCard);
            if (e != null)
            {
                EnergyCost = e.BaseEnergyCost * 2;
            }
        }
        if (Display != null)
        {
            Display.GetComponent<CardDisplay>().Start();
        }
    }

    public void UpdateDisplay()
    {
        if (Construct != null)
        {
            var con = Construct.GetComponent<I_Constructmyballs>();
            con.melol = this;
            con.SetParticle(0, Feebled);
            if (Feebled && !debuffs.Contains("Feebled"))
            {
                debuffs.Add("Feebled");
            }
            if (!Feebled && debuffs.Contains("Feebled"))
            {
                debuffs.Remove("Feebled");
            }
            con.SetParticle(1, FreezeAmount > 0);
            if (FreezeAmount > 0 && !debuffs.Contains("Freeze"))
            {
                debuffs.Add("Freeze");
            }
            if (!(FreezeAmount > 0) && debuffs.Contains("Feebled"))
            {
                debuffs.Remove("Feebled");
            }
        }
    }
    public void Cleanse()
    {
        Feebled = false;
        FreezeAmount = 0;
        UpdateDisplay();
        debuffs.Clear();
    }

    public Card(int cardType)
    {
        CardType = cardType;
        SetVals();
    }
    public Card(Card c)
    {
        CardType = c.CardType;
        GridPos = c.GridPos;
        IsPlayerControlled = c.IsPlayerControlled;
        SubNumber = c.SubNumber;
        if (c.Display != null) Display = c.Display;
        if (c.Construct != null) Construct = c.Construct;
        if (c.target_pos != null) target_pos = c.target_pos;
        SetVals();
        EnergyCost = c.EnergyCost;
        RandomSeed = c.RandomSeed;
    }
    public Card()
    {

    }
}

public class Notification
{
    public int Type = 0;
    public int Data = 0;

    public Notification(int a, int b)
    {
        Type = a;
        Data = b;
    }
}
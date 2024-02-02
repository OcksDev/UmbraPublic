using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public int state = 0;
    public int index = 0;
    public string CardState = "Game";
    public SpawnData spawnData;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Dick;
    public TextMeshProUGUI DickNBalls;
    public TextMeshProUGUI DickNBallsWithdrawls;
    public Image Ass;
    private RectTransform rt;
    public RectTransform thing;
    public Transform container;
    public Color32[] colors;
    private Button butt;
    public GameObject shitter;
    public GameObject parente;
    public GameObject LockState;
    public Image ClickMyVBS;
    public bool IsUnlocked = false;
    private float alaive = 0;
    public Color[] colorsss;
    public List<GameObject> things = new List<GameObject>();
    // Start is called before the first frame update


    private void Awake()
    {
        butt = GetComponent<Button>();
    }

    public void Start()
    {
        rt = GetComponent<RectTransform>();
        spawnData = GetComponent<SpawnData>();
        heheheha();
    }
    public void heheheha()
    {
        var fc = spawnData.card;
        var tfc = fc;
        var f = fc.CardType;
        Name.text = Gamer.Instance.CardNames[f];
        Dick.text = Gamer.Instance.CardDescriptions[f];
        string j = "Instant";
        if (Gamer.Instance.ConstructSizes.ContainsKey(f))
        {
            var hh = Gamer.Instance.ConstructSizes[f];
            j = hh[1] + "x" + hh[0];
        }
        DickNBalls.text = j;
        Vector3 sizeofmyballs = new Vector3(1, 1, 1);
        int imagestff = f;
        if (fc.SubNumber == 1)
        {
            var tfc2 = (fc.IsPlayerControlled ? Gamer.Instance.PlayerLastCard : Gamer.Instance.AILastCard);
            if (tfc2 != null)
            {
                tfc = tfc2;
                imagestff = tfc.CardType;
            }
        }
        if (tfc.SubNumber == 2)
        {
            var tfc2 = (fc.IsPlayerControlled ? Gamer.Instance.AILastCardTrue : Gamer.Instance.LastCardTrue);
            if (tfc2 != null)
            {
                tfc = tfc2;
                imagestff = tfc.CardType;
                Ass.color = colorsss[1];
            }
            else
            {
                Ass.color = colorsss[0];
            }
        }
        else
        {
            Ass.color = colorsss[0];
        }
        Ass.sprite = Gamer.Instance.CardImages[imagestff];
        switch (imagestff)
        {
            case 2:
            case 3:
                sizeofmyballs.x = 2;
                break;
            case 4:
                sizeofmyballs.x = 0.5f;
                sizeofmyballs.y = 1.5f;
                break;
            case 22:
            case 19:
            case 5:
                sizeofmyballs.x = 1.4f;
                break;
            case 7:
            case 8:
                sizeofmyballs.x = 1.2f;
                sizeofmyballs.y = 1.2f;
                break;
            case 9:
                sizeofmyballs.x = 1.92f;
                sizeofmyballs.y = 1.2f;
                break;
            case 10:
            case 20:
            case 25:
            case 27:
            case 29:
                sizeofmyballs.x = 1.4f;
                sizeofmyballs.y = 1.4f;
                break;
            case 28:
                sizeofmyballs.x = 1.7f;
                sizeofmyballs.y = 1.7f;
                break;
            case 11:
                sizeofmyballs.x = 2.1f;
                sizeofmyballs.y = 1.5f;
                break;
            case 12:
                sizeofmyballs.x = 1f;
                sizeofmyballs.y = 1.4f;
                break;
            case 13:
                sizeofmyballs.x = 2f;
                sizeofmyballs.y = 1f;
                break;
            case 15:
                sizeofmyballs.x = 2.1f;
                sizeofmyballs.y = 1.5f;
                break;
            case 23:
            case 17:
                sizeofmyballs.x = 1.8f;
                sizeofmyballs.y = 1.5f;
                break;
            case 18:
                sizeofmyballs.x = 1.5f;
                sizeofmyballs.y = 1.5f;
                break;
            case 26:
                sizeofmyballs.x = 2.4f;
                sizeofmyballs.y = 0.8f;
                break;
        }
        Ass.transform.localScale = sizeofmyballs;

        /*
        foreach(var thing in things)
        {
            Destroy(thing);
        }
        things.Clear();
        */
        List<int> ballers = new List<int>();
        int amnt = 0;
        for (int i = 0; i < 5; i++)
        {
            int e = -1;
            switch (i)
            {
                case 0:
                    e = tfc.CanPlayAgain ? 0 : -1;
                    break;
                case 1:
                    e = tfc.CanAttack ? 1 : -1;
                    break;
                case 2:
                    e = tfc.CanCauseAttacks ? 2 : -1;
                    break;
                case 3:
                    e = tfc.IsWallType ? 3 : -1;
                    break;
                case 4:
                    e = tfc.IsHeavy ? 4 : -1;
                    break;
            }
            if (e != -1)
            {
                amnt++;
                ballers.Add(e);
            }
        }
        if (!Gamer.Instance.CardUnlocked[spawnData.card.CardType])
        {
            ballers.Clear();
            amnt = 0;
        }
        //Debug.Log($"Pre: {things.Count}, {amnt}, {colors.Length}, {ballers.Count}");
        if (things.Count < amnt)
        {
            int ppshex = things.Count;
            for (int i = 0; i < (amnt - ppshex); i++)
            {
                var o = RandomFunctions.Instance.SpawnObject(3, container.gameObject, container.position, container.rotation, false, "");
                things.Add(o);
            }
        }
        else if (things.Count > amnt)
        {
            int ppshex = things.Count;
            for (int i = 0; i < (ppshex - amnt); i++)
            {
                Destroy(things[things.Count - 1]);
                things.RemoveAt(things.Count - 1);
            }
        }

        //Debug.Log($"Post: {things.Count}, {amnt}, {colors.Length}, {ballers.Count}");
        for (int i = 0; i < 5 && i < amnt; i++)
        {
            things[i].GetComponent<Image>().color = Gamer.Instance.AttributeColor[ballers[i]];
        }



        DickNBallsWithdrawls.text = spawnData.card.EnergyCost.ToString();
        switch (state)
        {
            case 3:
                butt.enabled = false;
                shitter.SetActive(false);
                StartCoroutine(Animate());
                LockState.SetActive(false);
                break;
            default:
                UpdateUnlock();
                break;
        }
        UnlockedByShitty = Gamer.Instance.CardUnlocked[spawnData.card.CardType];
        if (state != 4) { UnlockedByShitty = false; }
        else
        {
            if (UnlockedByShitty) butt.interactable = false;
        }
        BoightBoiBoils();
        ClickMyVBS.enabled = true;
        if (state == 6)
        {
            butt.enabled = false;
            ClickMyVBS.enabled = false;
        }
    }

    public void UpdateUnlock()
    {
        var g = Gamer.Instance;
        IsUnlocked = g.CardUnlocked[spawnData.card.CardType];
        if (state != 4)
        {
            LockState.SetActive(!IsUnlocked);
        }
        else
        {
            LockState.SetActive(false);
        }
    }
    public IEnumerator Animate()
    {
        yield return null;
        bool e = true;
        while (e)
        {
            transform.position = parente.transform.position + new Vector3(-Mathf.Sin(alaive * 1.5f) * 4, -alaive * 5f, 0);
            yield return null;
            if (alaive >= 3.2f)
            {
                Destroy(gameObject);
                e = false;
            }

        }
    }


    private void FixedUpdate()
    {
        var r = RandomFunctions.Instance;
        var g = Gamer.instance;
        if (g.GameState == "Game")
        {
            if (state != 3 && state != 5 && state != 7)
            {
                var g2 = Gamer.Instance.miscrefs[13].GetComponentsInChildren<Projectile>();
                if (g2.Length > 0)
                {
                    butt.interactable = false;
                }
                else
                {
                    if (g.PlacerState || g.DiscardState || g.FreezeState)
                    {
                        butt.interactable = BoightBoiBoils();
                    }
                    else
                    {
                        spawnData.card.IsHover = r.CheckInsideRect(g.GameState == "Game" ? thing : rt, g.MousePosMainUnScaled, g.cam, 1.1f);

                        bool good = g.IsPlayerTurn;
                        if (good)
                        {
                            good = Gamer.Instance.CanPlayCard(spawnData.card);
                        }

                        butt.interactable = good;
                    }
                }
            }
            if(state == 7)
            {
                butt.interactable = true;
            }
        }
        else if (g.GameState == "Main Menu")
        {
            if (state == 4)
            {
            }
            else if (state == 1 || state == 2)
            {
                butt.interactable = true;
            }
        }

        if (state == 3)
        {
            alaive += Time.deltaTime;
        }
    }

    public bool BoightBoiBoils()
    {
        var g = Gamer.instance;
        bool k = g.PlacerCard == spawnData.card;
        if (!g.IsPlayerTurn) k = false;
        spawnData.card.IsHover = k;
        int i = -1;
        if (g.BanishDiscardOverload)
        {
            var remcards = g.dick.CardsInDeck;
            int z = 0;
            foreach (var cd in remcards)
            {
                if (cd.CardType == spawnData.card.CardType)
                {
                    i = z;
                    break;
                }
                z++;
            }
        }
        bool canclock = (k && !g.DiscardState) || (!k && g.DiscardState);
        if (canclock && g.BanishDiscardOverload) canclock = i > -1;
        if (canclock && g.FreezeState) canclock = false;
        return canclock;
    }


    public bool UnlockedByShitty = false;
    public void Clickity()
    {
        var g = Gamer.Instance;
        switch (state)
        {
            case 0:
                bool canclick = g.IsPlayerTurn;
                if (g.PlacerState) canclick = false;
                if (g.PlacerCard != null) canclick = false;
                bool discard = g.DiscardState;
                if (discard)
                {
                    if (spawnData.card == g.PlacerCard) break;
                    g.Discard(spawnData.card);
                }
                else
                {
                    if (canclick) g.PlayCard(this);
                }
                break;
            case 1:
                if (IsUnlocked)
                {
                    g.LoadedDecks[g.SelectedDeck][1] += ", " + spawnData.card.CardType;
                    g.ReloadDeckThing();
                }
                break;
            case 2:
                var pness = RandomFunctions.Instance.StringToList(g.LoadedDecks[g.SelectedDeck][1]);
                var e = pness[0];
                pness.RemoveAt(0);
                pness.Remove(spawnData.card.CardType.ToString());
                pness.Insert(0, e);
                g.LoadedDecks[Gamer.Instance.SelectedDeck][1] = RandomFunctions.Instance.ListToString(pness);
                g.ReloadDeckThing();
                break;
            case 3:
                break;
            case 4:
                //oldshopcode
                break;
            case 5:
                g.CardToInspect = spawnData.card;
                g.CardDickSplayed = this;
                g.ToggleLogbookOvergay();
                break;
            case 6:
                break;
            case 7:
                g.determined_draw_cards.Add(spawnData.card.CardType);
                g.TogglePickerMenu();
                break;
        }
    }

}

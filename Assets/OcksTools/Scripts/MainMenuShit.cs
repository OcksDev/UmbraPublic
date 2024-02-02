using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuShit : MonoBehaviour
{
    public RectTransform[] buttons;
    public Image[] paths;
    public Image[] alwayson;
    public Image[] whitetocolor;
    public Image[] miscy;


    //Default setup to make this a singleton
    public static MainMenuShit instance;
    public static MainMenuShit Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (Instance == null) instance = this;
    }

    private void OnApplicationQuit()
    {
        //save game or something idk man
    }
    public bool IsScramblingTime = false;
    public bool IsBallingTime = false;
    public bool Clickergame = false;
    public bool gaydaring = false;
    public BigInteger money = 2;
    public TextMeshProUGUI titt;
    public TMP_FontAsset[] fonty;
    public Sprite[] ss;
    public Image Core;
    public GameObject baller;
    public Button clicker;
    public TextMeshProUGUI clicker_text;
    public GameObject ballerholder;
    // Update is called once per frame

    public void FardStart()
    {
        gameObject.SetActive(true);
        if (Gamer.Instance.CanBeBoozaled)
        {
            int inc = 0;
            if (Random.Range(0, 200) == 59)
            {
                gaydaring = true;
                titt.text = "Gadir 2";
                titt.fontSize = 80;
                inc++;
            }
            else
            {
                titt.text = "Umbra";
                titt.fontSize = 90;
            }
            if (Random.Range(0, 200) == 59)
            {
                titt.font = fonty[1];
                inc++;
            }
            else
            {
                titt.font = fonty[0];
            }
            Core.sprite = ss[0];
            if (Random.Range(0, 200) == 59)
            {
                Gamer.Instance.main_colors[1] = Gamer.Instance.main_colors[2];
                Gamer.Instance.main_colors[3] = Gamer.Instance.main_colors[2];
                Core.sprite = ss[1];
                inc++;
            }
            if (Random.Range(0, 500) == 59)
            {
                IsScramblingTime = true;
                Scramble();
                inc++;
            }
            if (Random.Range(0, 500) == 59)
            {
                foreach (var b in buttons)
                {
                    b.GetComponent<TextMeshProUGUI>().text = "Kitchen Gun";
                    b.GetComponent<TextMeshProUGUI>().fontSize = 32.73f;
                }
                inc++;
            }
            if (Random.Range(0, 500) == 59)
            {
                baller.SetActive(true);
                inc++;
            }
            if (Random.Range(0, 1000) == 59)
            {
                Core.sprite = ss[2];
                inc++;
            }
            if (Random.Range(0, 500) == 59)
            {
                IsBallingTime = true;
                StartCoroutine(BallerFallers());
                inc++;
            }
            if (Random.Range(0, 500) == 59)
            {
                money = 2;
                Clickergame = true;
                clicker.enabled = true;
                clicker_text.gameObject.SetActive(true);
                clicker_text.text = "$2";
                inc++;
            }

            if (inc > 0)
            {
                SaveSystem.Instance.ProfileData["Secrets"] = (int.Parse(SaveSystem.Instance.ProfileData["Secrets"]) + inc).ToString();
            }
        }
    }
    /*
    private void Update()
    {
        if (InputManager.IsKeyDown(KeyCode.L))
        {
            if (true)
            {
                gaydaring = true;
                titt.text = "Gadir 2";
                titt.fontSize = 80;
            }
            else
            {
                titt.text = "Umbra";
                titt.fontSize = 90;
            }
            if (true)
            {
                titt.font = fonty[1];
            }
            else
            {
                titt.font = fonty[0];
            }
            Core.sprite = ss[0];
            if (true)
            {
                Gamer.Instance.main_colors[1] = Gamer.Instance.main_colors[2];
                Gamer.Instance.main_colors[3] = Gamer.Instance.main_colors[2];
                Core.sprite = ss[1];
            }
            if (true)
            {
                IsScramblingTime = true;
                Scramble();
            }
            if (true)
            {
                foreach (var b in buttons)
                {
                    b.GetComponent<TextMeshProUGUI>().text = "Kitchen Gun";
                    b.GetComponent<TextMeshProUGUI>().fontSize = 32.73f;
                }
            }
            if (true)
            {
                baller.SetActive(true);
            }
            if (true)
            {
                Core.sprite = ss[2];
            }
            if (true)
            {
                IsBallingTime = true;
                StartCoroutine(BallerFallers());
            }

            if (true)
            {
                money = 2;
                Clickergame = true;
                clicker.enabled = true;
                clicker_text.gameObject.SetActive(true);
                clicker_text.text = "$2";
            }
        }
    }

    */
    public void Click()
    {
        money *= 15;
        money /= 10;
        clicker_text.text = "$" + RandomFunctions.Instance.NumToRead(money.ToString());
    }
    public IEnumerator BallerFallers()
    {
        yield return null;

        while (true)
        {
            RandomFunctions.Instance.SpawnObject(4, ballerholder, ballerholder.transform.position + new UnityEngine.Vector3(Random.Range(-16f, 16f), 0, 0), ballerholder.transform.rotation, false, "");
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }

    }
    public void Scramble()
    {
        if (IsScramblingTime)
        {
            List<GameObject> things = new List<GameObject>();
            foreach (var i in alwayson) things.Add(i.gameObject);
            foreach (var i in whitetocolor) things.Add(i.gameObject);
            foreach (var i in paths) things.Add(i.gameObject);
            //foreach(var i in buttons) things.Add(i.gameObject);
            foreach (var i in miscy) things.Add(i.gameObject);
            foreach (var g in things)
            {
                g.transform.position = gameObject.transform.position + new UnityEngine.Vector3(Random.Range(-16f, 16f), Random.Range(-9f, 9f), 0);
            }
        }

    }

    private void OnEnable()
    {
        if (Gamer.Instance != null)
        {
            j();
            if (IsScramblingTime) Scramble();
            if (IsBallingTime) StartCoroutine(BallerFallers());
        }
    }
    void FixedUpdate()
    {
        j();
    }
    public void j()
    {
        int hover = -1;
        int x = 0;
        foreach (var rectum in buttons)
        {
            if (RandomFunctions.Instance.CheckInsideRect(rectum, Gamer.Instance.MousePosMainUnScaled, Gamer.Instance.cam, 1.2f))
            {
                hover = x;
                break;
            }
            x++;
        }
        x = 0;
        SetPaths(hover);
    }
    public void SetPaths(int hover)
    {
        foreach (var im in alwayson)
        {
            im.color = Gamer.Instance.main_colors[1];
        }
        foreach (var im in whitetocolor)
        {
            im.color = Gamer.Instance.main_colors[3];
        }
        titt.color = Gamer.Instance.main_colors[1];
        var g = Gamer.Instance;
        foreach (var path in paths)
        {
            path.color = g.main_colors[0];
        }
        foreach (var butt in buttons)
        {
            var e = butt.GetComponent<TextMeshProUGUI>();
            e.color = new Color32(75, 75, 75, 255);
        }

        if (hover > -1)
        {
            paths[20 + hover].color = g.main_colors[1];
            buttons[hover].GetComponent<TextMeshProUGUI>().color = g.main_colors[1];
            if (hover < 5)
            {
                paths[0].color = g.main_colors[1];
                paths[hover + 5].color = g.main_colors[1];
                if (hover < 2)
                {
                    paths[2].color = g.main_colors[1];
                }
                if (hover > 2)
                {
                    paths[3].color = g.main_colors[1];
                }
                if (hover == 0) paths[1].color = g.main_colors[1];
                if (hover == 4) paths[4].color = g.main_colors[1];
            }
            else
            {
                paths[10].color = g.main_colors[1];
                paths[hover + 10].color = g.main_colors[1];
                if (hover < 7)
                {
                    paths[12].color = g.main_colors[1];
                }
                if (hover > 7)
                {
                    paths[13].color = g.main_colors[1];
                }
                if (hover == 5) paths[11].color = g.main_colors[1];
                if (hover == 9) paths[14].color = g.main_colors[1];
            }
        }
    }

}

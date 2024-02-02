using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using UnityEngine;

public class DialogLol : MonoBehaviour
{
    public GameObject DialogBoxObject;
    private DialogBoxL pp;
    public TextAsset[] files = new TextAsset[1];
    public TextAsset[] chooses = new TextAsset[1];
    public bool dialogmode = false;
    public int filenum = -1;
    public int linenum = 0;
    public int charnum = -1;
    public float cps = -1;
    public float cps2 = -1;
    public float cps3 = -1;
    private int charl = 0;
    private float cp2 = -1;
    private float cp = -1;
    private float cp3 = -1;
    public string speaker = "";
    public string fulltext = "";
    public string color = "";
    public string bg_color = "";
    public string tit_color = "";
    public string datatype = "Dialog";
    private List<string> str = new List<string>();
    private string ActiveFileName = "";


    public static DialogLol instance;

    // Start is called before the first frame update
    public static DialogLol Instance
    {
        get { return instance; }

        //bug: you can use rich text like <br> and <i> in the console 
    }

    private void Awake()
    {
        if (Instance == null) instance = this;
        DialogBoxObject.SetActive(true);
    }


    void Start()
    {
        ResetDialog();
        pp = DialogBoxObject.GetComponent<DialogBoxL>();

        //DialogBoxObject.SetActive(dialogmode);
    }

    public void Choose(int index)
    {
        string g2 = str[2];
        List<string> list2 = new List<string>(g2.Split(Environment.NewLine));
        list2.RemoveAt(0);
        UseEnding(list2[index]);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.IsKeyDown(InputManager.gamekeys["dialog_skip"]) || InputManager.IsKeyDown(InputManager.gamekeys["dialog_skip_mouse"]))
        {
            cp = 0;
            NextLine();
        }
        if(cp3 <= 0)
        {
            cp -= Time.deltaTime;
            if (cp <= 0 && charl != fulltext.Length)
            {
                cp += cp2;
                if (cp < 0) charl += Math.Abs((int)(cp / cp2));
                charl += 1;
                cp = cp2;
                upt();
                string e = GetText();
                e = e.Substring(Math.Clamp(e.Length - 1, 0, e.Length));
                if (e == " " || e.Contains("\n"))
                {
                    cp3 = e == " " ? cps2 : cps3;
                }
            }
        }
        else
        {
            cp3 -= Time.deltaTime;
        }
    }
    public void FixedUpdate()
    {
        DialogBoxObject.SetActive(dialogmode);
    }

    public void upt()
    {
        pp.text = GetText();
        pp.title = speaker;
        pp.color = color;
        pp.tit_color = tit_color;
        pp.UpdateColor();
        pp.UpdateText();
    }
    public string GetText()
    {
        string e = fulltext;
        e = e.Substring(0, Math.Clamp(charl, 0, fulltext.Length));
        if(e == fulltext)
        {
            charl = fulltext.Length;
        }
        return e;
    }
    public void NextLine()
    {
        if(filenum != -1)
        {
            switch (datatype)
            {
                case "Dialog":
                    if (charl == fulltext.Length)
                    {
                        linenum += 3;
                        charl = -1;
                        string r = str[Math.Clamp(linenum, 0, str.Count) - 1];
                        if (r.Contains("<End>"))
                        {
                            UseEnding(r);
                        }
                        else
                        {
                            string g = str[linenum - 1];
                            List<string> list = new List<string>(g.Split(", "));
                            List<string> list23 = new List<string>(g.Split("<"));
                            fulltext = str[linenum];
                            SetDefaultParams();
                            foreach(var attribute in list23)
                            {
                                if (attribute.Contains(">"))
                                {
                                    string he = attribute.Substring(0, attribute.IndexOf(">"));
                                    List<string> he2 = new List<string>(he.Split("="));
                                    ApplyAttribute(he2[0], he2[1]);
                                }
                            }
                            cp2 = 1 / cps;
                            pp.text = "";
                            pp.title = speaker;
                            pp.color = color;
                            pp.tit_color = tit_color;
                            pp.bg_color = bg_color;
                            pp.UpdateColor();
                        }
                    }
                    else
                    {
                        charl = fulltext.Length;
                        upt();
                    }
                    break;
                case "Choose":
                    string g2 = str[1];
                    List<string> list2 = new List<string>(g2.Split(Environment.NewLine));
                    list2.RemoveAt(0);
                    int i = 0;
                    foreach (var s in list2)
                    {
                        pp.qs[i] = s;
                        i++;
                    }
                    speaker = str[0];
                    fulltext = " ";
                    upt();
                    break;
            }
        }
    }

    public void ApplyAttribute(string key, string data)
    {
        List<string> list = new List<string>();
        string aaa = "";
        switch (key)
        {
            case "Name":
                speaker = data;
                break;
            case "Speed":
                list = new List<string>(data.Split(", "));
                // Characters per second
                if (list.Count > 1 && list[0] != "-") cps = float.Parse(list[0]);
                // Delay in seconds between each word
                if (list.Count > 2 && list[1] != "-") cps2 = float.Parse(list[1]);
                // Delay in seconds between each line
                if (list.Count > 3 && list[2] != "-") cps3 = float.Parse(list[2]);
                break;
            case "TitleColor":
                list = new List<string>(data.Split(","));
                aaa = list[0] + "|" + list[1] + "|" + list[2] + "|" + list[3];
                tit_color= aaa;
                break;
            case "TextColor":
                list = new List<string>(data.Split(","));
                aaa = list[0] + "|" + list[1] + "|" + list[2] + "|" + list[3];
                color = aaa;
                break;
            case "BgColor":
                list = new List<string>(data.Split(","));
                aaa = list[0] + "|" + list[1] + "|" + list[2] + "|" + list[3];
                bg_color = aaa;
                break;
            default:
                Debug.LogWarning("Unknown Dialog Attribute: \"" + key + "\"  (Dialog File: " + ActiveFileName + ")");
                break;
        }
    }

    public void UseEnding(string r)
    {
        r = r.Split(Environment.NewLine)[0];
        string h = "";
        if (r.Contains("<Scene="))
        {
            h = "<Scene=";
            string ee = r.Substring(r.IndexOf(h) + h.Length);
            ee = ee.Substring(0, ee.Length - 1);
            //Debug.Log(ee);
            //ConsoleLol.Instance.ConsoleLog(ee);
            StartDialog(int.Parse(ee));
        }
        else if (r.Contains("<Choose="))
        {
            h = "<Choose=";
            string ee = r.Substring(r.IndexOf(h) + h.Length);
            ee = ee.Substring(0, ee.Length - 1);
            //ConsoleLol.Instance.ConsoleLog(ee);
            StartDialog(int.Parse(ee), "Choose");
        }
        else
        {
            //just closes the dialog menu
            ResetDialog();
            pp.text = "";
            pp.title = "";
            pp.UpdateColor();
        }
    }

    public void SetDefaultParams()
    {
        cps = 20;
        cps2 = 0;
        cps3 = 0;
        speaker = "?";
        color = "255|255|255|255";
        tit_color = "255|255|255|255";
        bg_color = "59|50|84|255";
        if (pp != null)
        {
            int i = 0;
            while(i < pp.qs.Count)
            {
                pp.qs[i] = "";
                i++;
            }
        }
    }


    public void ResetDialog()
    {
        filenum = -1;
        charnum = 0;
        fulltext = "?";
        charl = 1;
        linenum= -2;
        cp = 0;
        dialogmode = false;
        datatype = "Dialog";
        SetDefaultParams();
    }
    public void StartDialog(int dialog, string datat = "Dialog")
    {
        ResetDialog();
        dialogmode = true;
        DialogBoxObject.SetActive(true);
        filenum = dialog;
        datatype= datat;

        //just closes the OcksTools Console when opening any dialog.
        ConsoleLol.Instance.CloseConsole();

        switch (datat)
        {
            case "Dialog":
                str = new List<string>(files[filenum].text.Split("</> "));
                string d1 = str[0];
                str.RemoveAt(0);
                ActiveFileName= d1.Split(Environment.NewLine)[0];
                ConsoleLol.Instance.ConsoleLog(datatype + ": " + ActiveFileName, "#bdbdbdff");
                NextLine();
                break;
            case "Choose":
                str = new List<string>(chooses[filenum].text.Split("</>"));
                string d2 = str[0];
                ActiveFileName = d2.Split(Environment.NewLine)[0];
                ConsoleLol.Instance.ConsoleLog(datatype + ": " + ActiveFileName, "#bdbdbdff");
                NextLine();
                break;
        }
    }
    public string GetLineFrom(int index, int line, string boner = "Dialog")
    {
        var str = new List<string>();
        switch (boner)
        {
            case "Dialog":
                str = new List<string>(files[index].text.Split("</> "));
                break;
            case "Choose":
                str = new List<string>(chooses[index].text.Split("</>"));
                break;
        }
        return str[line];
    }
}

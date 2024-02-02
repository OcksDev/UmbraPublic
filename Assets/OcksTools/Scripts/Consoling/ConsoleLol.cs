using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ConsoleLol : MonoBehaviour
{
    public GameObject ConsoleObject;
    public static ConsoleLol instance;

    public bool enable = false;
    private List<string> prev_commands = new List<string>();
    private string s = "";
    private List<string> command = new List<string>();
    public string BackLog = "";
    private int balls = 0;
    private int comm = 0;
    // Start is called before the first frame update
    public static ConsoleLol Instance
    {
        get { return instance; }

        //bug: you can use rich text like <br> and <i> in the console 
    }

    private void Awake()
    {
        prev_commands.Clear();
        BackLog = "";
        ConsoleChange(false);
        if (Instance == null) instance = this;
    }
    private void Update()
    {
        if (InputManager.IsKeyDown(InputManager.gamekeys["console"]))
        {
            ConsoleChange(!enable);
        }else if(InputManager.IsKeyDown(InputManager.gamekeys["close_menu"]))
        {
            ConsoleChange(false);
        }


        if (enable && InputManager.IsKeyDown(InputManager.gamekeys["console_up"]))
        {
            CommandChange(-1);
        }
        if (enable && InputManager.IsKeyDown(InputManager.gamekeys["console_down"]))
        {
            CommandChange(1);
        }
    }

    public void CommandChange(int i)
    {
        var sp = ConsoleObject.GetComponentInChildren<TMP_InputField>();
        if(prev_commands.Count > 0)
        {
            comm += i;
            comm = Math.Clamp(comm, 0, prev_commands.Count - 1);
            sp.text = prev_commands[comm];
        }
    }

    public void Submit()
    {
        if (InputManager.IsKeyDown(InputManager.gamekeys["console"]) || InputManager.IsKeyDown(InputManager.gamekeys["close_menu"]) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) return;
        balls = 1;
        var pp = ConsoleObject.GetComponentInChildren<Scrollbar>();
        if (pp != null) pp.value = 1;
        //this must be run when the text is finished editing
        try
        {
            s = ConsoleObject.GetComponentInChildren<TMP_InputField>().text;
            if (s != "" && (prev_commands.Count == 0 || prev_commands[prev_commands.Count - 1] != s)) prev_commands.Add(s);
            s = s.ToLower();
            command = s.Split(' ').ToList();

            for(int i = 0; i<10; i++)
            {
                command.Add("");
            }
            ConsoleLog("> " + s, "#7a7a7aff");
            switch (command[0])
            {
                case "help":
                    switch (command[1])
                    {
                        case "joe":
                            ConsoleLog((

                                "Joe" +
                                "<br> - joe <mother>"

                            ), "#bdbdbdff");
                            break;
                        case "settimescale":
                            ConsoleLog((

                                "Sets the scale of  time" +
                                "<br> - settimescale <time scale>"

                            ), "#bdbdbdff");
                            break;
                        case "dialog":
                            ConsoleLog((

                                "General dialog manager" +
                                "<br> - dialog <#>" + 
                                "<br> - dialog stop"

                            ), "#bdbdbdff");
                            break;
                        case "test":
                            ConsoleLog((

                                "Runs some tests and stuff" +
                                "<br> - test chat" +
                                "<br> - test tag" +
                                "<br> - test circle" +
                                "<br> - test destroy" +
                                "<br> - test garbage" +
                                "<br> - test listall"

                            ), "#bdbdbdff");
                            break;
                        default:
                            ConsoleLog((

                                "Available Commands:" +
                                "<br> - joe" +
                                "<br> - settimescale" +
                                "<br> - test" + 
                                "<br> - dialog"

                            ), "#bdbdbdff");
                            break;

                    }
                    break;

                case "test":
                    switch (command[1])
                    {
                        case "tag":
                            Tags.dict.Add("penis", gameObject);

                            ConsoleLog((

                                "test result: " + Tags.dict["penis"].name

                            ), "#bdbdbdff");
                            Tags.ClearAllOf("penis");
                            break;
                        case "circle":
                            RandomFunctions.Instance.SpawnObject(0, gameObject, Vector3.zero, Quaternion.Euler(0, 0, 0));
                            break;
                        case "chat":
                            for(int i = 0; i < 10; i++)
                            {
                                ChatLol.Instance.WriteChat("Chat Test Lol", "#" + UnityEngine.Random.ColorHSV().ToHexString());
                            }
                            break;
                        case "listall":
                            foreach (var d in Tags.dict)
                                ConsoleLog((

                                    "test result: " + d

                                ), "#bdbdbdff");
                            break;
                        case "max":
                            ConsoleLog((

                                "Double Max: " + double.MaxValue.ToString()

                            ), "#bdbdbdff");
                            break;
                        case "destroy":
                            foreach (var d in Tags.dict)
                                Destroy(d.Value);
                            break;
                        case "garbage":
                            Tags.GarbageCleanup();
                            break;
                        default:
                            ConsoleLog((

                                "Invalid Test"

                            ), "#ff0000ff");
                            break;

                    }
                    break;
                case "joe":
                    switch (command[1])
                    {
                        case "mother":
                            ConsoleLog((

                                "AYYYYYEEEEEE"

                            ), "#bdbdbdff");
                            break;
                        default:
                            ConsoleLog((

                                "Who is joe?"

                            ), "#bdbdbdff");
                            break;
                    }
                    break;
                case "dialog":
                    switch (command[1])
                    {
                        case "stop":
                            DialogLol.Instance.ResetDialog();
                            ConsoleLog((

                                "All dialog has been stopped"

                            ), "#bdbdbdff");
                            break;
                        default:
                            DialogLol.Instance.StartDialog(int.Parse(command[1]));
                            CloseConsole();
                            break;
                    }
                    break;
                case "settimescale":
                    try
                    {
                        float f = float.Parse(command[1]);
                        Time.timeScale = f;
                        ConsoleLog((

                            "Time scale changed to " + f

                        ), "#bdbdbdff");
                    }
                    catch
                    {
                        ConsoleLog((

                            "Invalid time scale input"

                        ), "#bdbdbdff");
                    }
                    break;
                default:
                    ConsoleLog("Unknown Command: " + command[0], "#ff0000ff");

                    break;
            }
        }
        catch
        {
            ConsoleLog("Invalid command", "#ff0000ff");
        }

        comm = prev_commands.Count;
    }

    public void ConsoleLog(string text = "Logged", string hex = "\"white\"")
    {
        BackLog = BackLog + "<br><color=" + hex + ">" + text;
        balls = 1;
    }
    public void CloseConsole()
    {
        ConsoleChange(false);
    }
    void ConsoleChange(bool e = false)
    {
        enable = e;
        ConsoleObject.SetActive(e);
        if (e)
        {
            var imp = ConsoleObject.GetComponentInChildren<TMP_InputField>();
            imp.text = "";
            imp.Select();
        }
    }

    private void FixedUpdate()
    {
        balls--;
        if (balls == 0)
        {
            var pp = ConsoleObject.GetComponentInChildren<Scrollbar>();
            if (pp != null) pp.value = 1;
        }
    }


}

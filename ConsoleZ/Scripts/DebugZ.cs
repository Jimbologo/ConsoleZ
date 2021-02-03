using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum LogType
{
    Empty,
    Default,
    Warning,
    Error
}

/// <summary>
/// Holds instance to be called from within scene
/// </summary>
public class DebugZ : MonoBehaviour
{
    public static DebugZ instance;

    //Holds Settings such as color & prefix data
    public ConsoleZSettings consoleZSettings;

    [SerializeField] private GameObject newLinePrefab;
    [SerializeField] private GameObject lineSeperatorPrefab;
    [SerializeField] private Transform logContentParent;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject parentObject;

    private bool isActive = false;

    /// <summary>
    /// Store all commands with descriptions
    /// </summary>
    private string[] commands = new string[]
    {
        "/Help - Shows all commands",
        "/Log - Logs a default debug",
        "/LogWarning - Logs a warning",
        "/LogError - Logs an error",
        "/Close - Closes the ConsoleZ window"
    };

    /// <summary>
    /// Setup instance & Log Package info before hiding
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);

        //Hide console by default
        parentObject.SetActive(false);

        LogInfo();
    }

    /// <summary>
    /// Log package info
    /// </summary>
    private void LogInfo()
    {
        LogEmpty(@"-------------------------------------------------------------------------------");
        LogEmpty(@" ");
        LogEmpty(@"   Your using ConsoleZ, Try /help for all known Commands!");
        LogEmpty(@" ");
        LogEmpty(@"-------------------------------------------------------------------------------");
        LogEmpty(@" ");
        LogEmpty(@"");
    }

    /// <summary>
    /// Used to check for access key pressed
    /// </summary>
    private void Update()
    {
        //Check if we want to close or open the console
        if(Input.GetKeyDown(consoleZSettings.accessKey))
        {
            isActive = !isActive;
            parentObject.SetActive(isActive);
        }
    }

    /// <summary>
    /// Logs a message to the console as default prefix
    /// </summary>
    /// <param name="a_msg"></param>
    public void Log(string a_msg)
    {
        //Create new line Obj
        GameObject newLine = Instantiate(newLinePrefab, logContentParent);
        ConsoleZNewLine newLineText = newLine.GetComponent<ConsoleZNewLine>();

        //Pass message info to setup Ui
        newLineText.SetupUI(a_msg, LogType.Default);

        //Reset Verical position so new logs are at the bottom
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    /// <summary>
    /// Logs a message with the log prefix
    /// </summary>
    /// <param name="a_msg"></param>
    public void LogEmpty(string a_msg)
    {
        //Create new line Obj
        GameObject newLine = Instantiate(newLinePrefab, logContentParent);
        ConsoleZNewLine newLineText = newLine.GetComponent<ConsoleZNewLine>();

        //Pass message info to setup Ui
        newLineText.SetupUI(a_msg, LogType.Empty);

        //Reset Verical position so new logs are at the bottom
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    /// <summary>
    /// Logs a message to the console with the warning prefix
    /// </summary>
    /// <param name="a_msg"></param>
    public void LogWarning(string a_msg)
    {
        //Create new line Obj
        GameObject newLine = Instantiate(newLinePrefab, logContentParent);
        ConsoleZNewLine newLineText = newLine.GetComponent<ConsoleZNewLine>();

        //Pass message info to setup Ui
        newLineText.SetupUI(a_msg, LogType.Warning);

        //Reset Verical position so new logs are at the bottom
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    /// <summary>
    /// logs a message to the console with the error prefix
    /// </summary>
    /// <param name="a_msg"></param>
    public void LogError(string a_msg)
    {
        //Create new line Obj
        GameObject newLine = Instantiate(newLinePrefab, logContentParent);
        ConsoleZNewLine newLineText = newLine.GetComponent<ConsoleZNewLine>();

        //Pass message info to setup Ui
        newLineText.SetupUI(a_msg, LogType.Error);

        //Reset Verical position so new logs are at the bottom
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    /// <summary>
    /// Called once the input field has exited
    /// </summary>
    public void OnInputField()
    {
        //lets store the inputted text locally
        string newInput = inputField.text;

        //Check we are using a command
        if(newInput[0] != '/')
        {
            LogWarning("Unknown Command use /help to show commands");
            return;
        }

        //Make input lowercase
        newInput = newInput.ToLower();

        //Split all arguments up
        string[] args = newInput.Split(' ');

        //Used to combine args
        string msg = "";

        switch (args[0])
        {
            case "/help":
                //Show a list of all commands
                LogEmpty(" ");
                LogEmpty("Current List of all known Commands:");
                for (int i = 0; i < commands.Length; ++i)
                {
                    LogEmpty(commands[i]);
                }
                LogEmpty(" ");
                break;
            case "/log":
                //log a default msg
                for (int i = 1; i < args.Length; ++i)
                {
                    msg += " " + args[i];
                }
                Log(msg);
                break;
            case "/logwarning":
                //log a warning
                for (int i = 1; i < args.Length; ++i)
                {
                    msg += " " + args[i];
                }
                LogWarning(msg);
                break;
            case "/logerror":
                //log an error
                for (int i = 1; i < args.Length; ++i)
                {
                    msg += " " + args[i];
                }
                LogError(msg);
                break;
            case "/close":
                //closes the console window
                isActive = false;
                parentObject.SetActive(isActive);
                break;
            default:
                break;
        }

        //Reset the input field
        inputField.text = "";

        //Force Update the canvas 
        Canvas.ForceUpdateCanvases();

        //Try reselect the inputfield
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }
}

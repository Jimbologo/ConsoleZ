using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ConsoleZNewLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logTypeTextUI;
    [SerializeField] private TextMeshProUGUI msgTextUI;

    /// <summary>
    /// Sets up the new line as a set log type
    /// </summary>
    /// <param name="a_msg"></param>
    /// <param name="a_logType"></param>
    public void SetupUI(string a_msg, LogType a_logType)
    {
        //Apply color and correct prefix relevent to the log type
        switch (a_logType)
        {
            case LogType.Default:
                logTypeTextUI.color = DebugZ.instance.consoleZSettings.defaultColor;
                logTypeTextUI.text = DebugZ.instance.consoleZSettings.defaultTypeText;
                break;
            case LogType.Warning:
                logTypeTextUI.color = DebugZ.instance.consoleZSettings.warningColor;
                logTypeTextUI.text = DebugZ.instance.consoleZSettings.warningTypeText;
                break;
            case LogType.Error:
                logTypeTextUI.color = DebugZ.instance.consoleZSettings.errorColor;
                logTypeTextUI.text = DebugZ.instance.consoleZSettings.errorTypeText;
                break;
            default:
                logTypeTextUI.color = DebugZ.instance.consoleZSettings.defaultColor;
                logTypeTextUI.text = "";
                break;
        }

        //Set the message text & color
        msgTextUI.color = DebugZ.instance.consoleZSettings.defaultColor;
        msgTextUI.text = a_msg;

        //Try for an update to the new line
        GetComponent<HorizontalLayoutGroup>().spacing = 2;
        logTypeTextUI.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

}

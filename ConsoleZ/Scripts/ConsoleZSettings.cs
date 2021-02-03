using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ConsoleZSettings : ScriptableObject
{
    [Header("Default Open/Close Key")]
    public KeyCode accessKey;

    [Header("Prefix Colors")]
    public Color defaultColor;
    public Color warningColor;
    public Color errorColor;

    [Header("Prefix's")]
    public string defaultTypeText = "[Default]:";
    public string warningTypeText = "[Warning]:";
    public string errorTypeText = "[Error]:";
}

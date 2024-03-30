using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevToolUIPC : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ConsoleText;


    private void LogInfo(string msg)
    {
        ConsoleText.text += $"\n[{DateTime.Now}][DevTool] : {msg}";
    }
}

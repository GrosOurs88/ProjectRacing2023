using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugDevNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI devNameText;

    public static DebugDevNameDisplay instance;

    private void Awake()
    {
        instance = this;
    }

    public void DisplayName(string _name)
    {
        devNameText.text = "Zone : " + _name;
    }    
}

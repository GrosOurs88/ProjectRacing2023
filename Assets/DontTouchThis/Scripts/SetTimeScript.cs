using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTimeScript : MonoBehaviour
{
    public TimeInfos finalTime;
    public TextMeshProUGUI textTime;

    void Start()
    {
        textTime.text = getTimeString(finalTime.time);
    }

    string getTimeString(float time)
    {
        int timeInt = (int)(time);
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;
        float fraction = (time * 100) % 100;
        if (fraction > 99) fraction = 99;
        return string.Format("{0}:{1:00}:{2:00}", minutes, seconds, fraction);
    }
}

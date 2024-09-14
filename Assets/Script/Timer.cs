using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    float sumTime;

    TMP_Text timerText;

    int tenmin;
    int min;
    int tensec;
    int sec;
    int milsec;

    // Start is called before the first frame update
    void Start()
    {
        sumTime = 0f;
        timerText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        sumTime += Time.deltaTime;
        if ( sumTime > 0.1f)
        {
            sumTime -= 0.1f;
            milsec ++;
        }
        if (milsec >= 10)
        {
            milsec -= 10;
            sec++;
        }
        if (sec >= 10)
        {
            sec -= 10;
            tensec++;
        }
        if (tensec >= 6)
        {
            tensec -= 6;
            min++;
        }
        if (min >= 10)
        {
            min -= 10;
            tenmin++;
        }
        
        timerText.text = tenmin.ToString() + min.ToString() + ":"+ tensec.ToString() + sec.ToString() + "." +milsec.ToString() ;

    }
}

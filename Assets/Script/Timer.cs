using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Timer : MonoBehaviour
{

    float sumTime;
    float sumCalTime;

    Text highText;

    public bool stop;
    //TMP_Text timerText;
    Text timerText;
    int _tenmin;
    int _min;
    int _tensec;
    int _sec;
    int _milsec;

    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 5000);
        }
        highText = gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        highText = CalTime(highText, PlayerPrefs.GetFloat("highscore"));
        stop = false;
        sumTime = 0f;
        sumCalTime = 0f;
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if (sumTime < PlayerPrefs.GetFloat("highscore"))
            {
                PlayerPrefs.SetFloat("highscore", sumTime);
                highText = CalTime(highText, PlayerPrefs.GetFloat("highscore"));
            }
            return;
        }

        sumCalTime += Time.deltaTime;
        sumTime += Time.deltaTime;

        if ( sumCalTime > 0.1f)
        {
            sumCalTime -= 0.1f;
            _milsec ++;
        }
        if (_milsec >= 10)
        {
            _milsec -= 10;
            _sec++;
        }
        if (_sec >= 10)
        {
            _sec -= 10;
            _tensec++;
        }
        if (_tensec >= 6)
        {
            _tensec -= 6;
            _min++;
        }
        if (_min >= 10)
        {
            _min -= 10;
            _tenmin++;
        }
        
        timerText.text = _tenmin.ToString() + _min.ToString() + ":"+ _tensec.ToString() + _sec.ToString() + "." +_milsec.ToString() ;

    }

    Text CalTime(Text ttext, float inputTime)
    {
        int tenmin = 0;
        int min = 0;
        int tensec = 0;
        int sec = 0;
        int milsec = 0;

        tenmin = (int)inputTime / 600;
        inputTime = inputTime - tenmin * 600;
        min = (int)inputTime / 60;
        inputTime = inputTime - min * 60;
        tensec = (int)inputTime / 10;
        inputTime = inputTime - tensec * 10;
        sec = (int)inputTime;
        milsec = (int)((inputTime - (float)sec) * 10f);

        ttext.text = tenmin.ToString() + min.ToString() + ":" + tensec.ToString() + sec.ToString() + "." + milsec.ToString();
        return ttext;
    }


}

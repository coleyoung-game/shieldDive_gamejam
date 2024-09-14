using Chan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    float endY;
    GameObject player;
    float percent;
    float canvasHeight;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        canvasHeight = gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (endY > -1)
        {
            endY = GameObject.Find("StageManager").GetComponent<StageManager>().LastYValue;
        }

        percent = player.transform.position.y / (endY+10);
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2 (15f, -(canvasHeight) * percent);

    }

}

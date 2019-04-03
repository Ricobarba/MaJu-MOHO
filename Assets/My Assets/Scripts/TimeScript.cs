using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public static bool flow;
    public static float second;
    public static float minute;
    public int show;

    public Text timeText;

    public Transform timePosition;

    // Start is called before the first frame update
    void Start()
    {
        flow = true;
        timeText.transform.position = timePosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (flow)
        {

            if (second<10f)
                timeText.text = minute.ToString() + ":0" + Mathf.RoundToInt(second-0.5f).ToString();
            else
                timeText.text = minute.ToString() + ":" + Mathf.RoundToInt(second-0.5f).ToString();
            second += Time.deltaTime;
            if (second>60f)
            {
                minute += 1;
                second += -60f;
            }
        }

        else
            timeText.text = "GOOOOOOOOAAAAL";

    }
}

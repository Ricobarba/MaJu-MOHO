using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScript : MonoBehaviour
{
    public static bool flow;
    public static float second;
    public static int minute;
    public float maxSecond = 30f;
    public int maxMinute = 1;

    public GameObject[] possibleCharacters;
    public int selected;
    public GameObject player1;

    public Text timeText;

    public Transform timePosition;

    // Start is called before the first frame update
    void Start()
    {
        flow = true;
        timeText.transform.position = timePosition.position;
        player1 = Instantiate(possibleCharacters[selected], Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if ((minute > maxMinute) || ((minute == maxMinute) && (second >= maxSecond)))
        {
            ScoreScript.leftScore = 0;
            ScoreScript.rightScore = 0;
            TimeScript.minute = 0;
            TimeScript.second = 0;
            SceneManager.LoadScene("FirstMenu");
            TimeScript.flow = false;
        }
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

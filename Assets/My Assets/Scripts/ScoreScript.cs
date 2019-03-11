using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public static int leftScore;
    public static int rightScore;

    public Text leftScoreText;
    public Text rightScoreText;

    public Transform leftScorePosition;
    public Transform rightScorePosition;


    // Start is called before the first frame update
    public void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();

        //leftScoreText.transform.position = new Vector3(28, 0, 0);
        //rightScoreText.transform.position = new Vector3(-23, 0, 0);
        leftScoreText.transform.position = leftScorePosition.position;
        rightScoreText.transform.position = rightScorePosition.position;
    }


    // Update is called once per frame
    void Update()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    public void addPoint(string side)
    {
        if (side == "Left")
            leftScore = leftScore + 1;
        else if (side == "Right")
            rightScore = rightScore + 1;
    }
}

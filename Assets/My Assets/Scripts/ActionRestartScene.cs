using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ActionRestartScene : ActionScript
{
    public int maxScore = 2;
    public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
    {
        if (_events != null)
        {
            if (ScoreScript.leftScore == maxScore)
            {
                ScoreScript.leftScore = 0;
                ScoreScript.rightScore = 0;
                TimeScript.minute = 0;
                TimeScript.second = 0;
                TimeScript.flow = false;
                SceneManager.LoadScene("FirstMenu");
            }
            else if (ScoreScript.rightScore == maxScore)
            {
                ScoreScript.leftScore = 0;
                ScoreScript.rightScore = 0;
                TimeScript.minute = 0;
                TimeScript.second = 0;
                TimeScript.flow = false;
                SceneManager.LoadScene("FirstMenu");
            }
            else
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
        yield return null;
    
    }


}
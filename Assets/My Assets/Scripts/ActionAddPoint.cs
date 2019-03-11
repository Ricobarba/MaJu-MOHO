using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ActionAddPoint : ActionScript
{
    public string side;

    public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
    {
        if (_events != null)
        {
            if (side == "Left")
                ++ScoreScript.leftScore;
            else if (side == "Right")
                ++ScoreScript.rightScore;

        }
        yield return null;

    }

}
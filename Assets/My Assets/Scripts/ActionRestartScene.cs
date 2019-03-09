using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ActionRestartScene : ActionScript
{
    public string side;
   


   // public void RestartScene()
   // {
     //   if (_events != null)
     //   {
     //       foreach (var ev in _events)
     //       {
     //          if (ev != null)
     //                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
     //       }
     //   }

    //}

    public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
    {
        if (_events != null)
        {
            if (side == "Left")
                ++ScoreScript.leftScore;
            else if (side == "Right")
                ++ScoreScript.rightScore;

            foreach (var ev in _events)
            {
                if (ev != null)
                {
                    
                    SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
                }
            }
        }
        yield return null;
    
    }


}
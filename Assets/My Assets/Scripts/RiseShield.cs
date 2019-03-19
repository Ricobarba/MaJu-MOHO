using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseShield : MonoBehaviour
{
    public Character1Controller charac;
    public GameObject shield;


    // Update is called once per frame
    void Update()
    {
        //_gameObjectToActivate.SetActive(_setActive);
        if(Input.GetButtonDown(charac.guardStr))
        {
            shield.SetActive(true);
        }

        if(Input.GetButtonUp(charac.guardStr))
        {
            shield.SetActive(false);
        }

    }
}

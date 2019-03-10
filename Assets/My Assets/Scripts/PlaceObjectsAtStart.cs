using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsAtStart : MonoBehaviour
{
    public Transform[] _teleportTransforms;
    public GameObject[] _teleportTargets;

    public int nbPosition;
    public int nbTargets;

    public List<int> numberTable;
    int N;

    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        nbPosition = this._teleportTransforms.Length;
        nbTargets = this._teleportTargets.Length;
        
        for (int i=0; i<nbPosition; i++)
        {
            numberTable.Add(i);
        }


        foreach  (GameObject target in _teleportTargets)
        {
            N = this.numberTable.Count;

            int k;
            k = rand.Next(N);

            // _targetGameObject.transform.position = _teleportTransform.position;
            target.transform.position = this._teleportTransforms[k].position;

            numberTable.Remove(k);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicBars : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public Transform target5;

    public Transform[] targets;

    // Start is called before the first frame update
    void Awake()
    {
        targets = new Transform[10];
        targets[0] = target;
        targets[1] = target2;
        targets[2] = target3;
        targets[3] = target4;
        targets[4] = target5;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

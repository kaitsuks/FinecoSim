using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairDressers : MonoBehaviour
{

    public Transform target;
    public Transform target2;
    public Transform target3;
    public Transform target4;
    public Transform target5;
    public Transform target6;
    public Transform target7;
    public Transform target8;
    public Transform target9;

    public Transform[] targets;

    // Start is called before the first frame update
    void Awake()
    {
        targets = new Transform[9];
        targets[0] = target;
        targets[1] = target2;
        targets[2] = target3;
        targets[3] = target4;
        targets[4] = target5;
        targets[5] = target6;
        targets[6] = target7;
        targets[7] = target8;
        targets[8] = target9;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

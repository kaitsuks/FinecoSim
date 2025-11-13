using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    public GameObject toBeHandled;
    public bool isActive = false;
    int timex = 0;

    // Start is called before the first frame update
    void Start()
    {
        HandleObject();
    }

    public void CloseObject()
    {
        toBeHandled.SetActive(false);
    }

    public void OpenObject()
    {
        toBeHandled.SetActive(true);
    }

    public void HandleObject()
    {
        toBeHandled.SetActive(isActive);
    }

    public void ToggleObject()
    {
        //toBeHandled.SetActive(isActive);
        timex = 1 - timex;
        if (timex == 0)
        {
            toBeHandled.SetActive(false);
        }
        else
        {
            toBeHandled.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

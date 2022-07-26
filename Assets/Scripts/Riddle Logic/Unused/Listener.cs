using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Speaker>().hellothere += Kenobi;
    }

    private void Kenobi()
    {
        Debug.Log("KENOOOOOBIIII!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        FindObjectOfType<Speaker>().hellothere -= Kenobi;
    }
}

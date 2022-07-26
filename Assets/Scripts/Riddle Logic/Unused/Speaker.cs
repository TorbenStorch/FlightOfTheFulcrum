using System;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public event Action hellothere;

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.name == "Cube (1)")
       {
           if (hellothere != null)
           {
                hellothere();
           }
       }
    }
}

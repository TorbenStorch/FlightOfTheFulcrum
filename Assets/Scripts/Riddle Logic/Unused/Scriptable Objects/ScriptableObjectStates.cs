using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

[CreateAssetMenu(menuName = "StateInformation", fileName = "StateInformation")]
public class ScriptableObjectStates : ScriptableObject
{
    //public GameObject[] togglableObjects;

    public void ToggleGameObjects(GameObject[] togglableObjects, bool boolean)
    {
        togglableObjects.ToggleGameObjectArray(boolean);
    }
}

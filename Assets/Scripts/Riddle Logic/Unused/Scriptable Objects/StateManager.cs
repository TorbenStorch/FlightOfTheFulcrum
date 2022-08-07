using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

public class StateManager : MonoBehaviour
{
    public ScriptableObjectStates scriptableObjectStates;
    public GameObject[] togglableObjects;

    private void Start()
    {
        togglableObjects.ToggleGameObjectArray(false);
    }

    public void ToggleGameObjects(bool boolean)
    {
        scriptableObjectStates.ToggleGameObjects(togglableObjects, boolean);
    }
}

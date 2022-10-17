/*-------------------------------------------------------
Creator: Philipp Petry
Project: Fulcrum
Last change: 01-07-2022
Topic: Define winning condition for the attached Tear Object
---------------------------------------------------------*/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TearInteractable))]
public class TearTimeMeasurement : MonoBehaviour
{
    [Header("Set the Mesh Renderer this object")]
    [SerializeField] private MeshRenderer rend;
    [SerializeField] private SkinnedMeshRenderer rendSkinned;

    [Header("Set Win Condition Range")]
    [SerializeField] private bool pastWins;

    public Material _material;
    private float _pastValue, _futureValue;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float minPastValue, maxPastValue, minFutureValue, maxFutureValue;

    //[HideInInspector]
    public bool tearCorrect = false;

    [Header("Enable this, if you want to disable time manipulation for this object once it has been in the correct TimeMeasurement")]
    [SerializeField] public bool WinLock;
    //[HideInInspector]
    public bool disableEditing = false;
    [SerializeField] private bool hasWon;

    //reset the material so that it doesn't need to be done in the editor every time after playing
    [Header("Reset the Past and Future Value of the Material on Awake")]
    [SerializeField] private bool resetMaterialInRuntime;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float basePastValue, baseFutureValue;

    public UnityEvent TearCorrect;
    public UnityEvent TearFalse;

    public bool enableDebug;

    void Awake()
    {
        if (resetMaterialInRuntime)
            resetMaterial();
    }

    private void resetMaterial()
    {
        if (rend != null)
        {
            _material = rend.sharedMaterial;
            _material.SetFloat("_BlendToPast", basePastValue);
            _material.SetFloat("_BlendToFuture", baseFutureValue);
        }
        else if (rendSkinned != null)
        {
            _material = rendSkinned.sharedMaterial;
            _material.SetFloat("_BlendToPast", basePastValue);
            _material.SetFloat("_BlendToFuture", baseFutureValue);
        }

    }

    void Update()
    {
        if (rend != null)
        {
            _pastValue = rend.sharedMaterial.GetFloat("_BlendToPast");
            _futureValue = rend.sharedMaterial.GetFloat("_BlendToFuture");
        }
        else if (rendSkinned != null)
        {
            _pastValue = rendSkinned.sharedMaterial.GetFloat("_BlendToPast");
            _futureValue = rendSkinned.sharedMaterial.GetFloat("_BlendToFuture");
        }
        else
        {
            Debug.Log("Assign one kind Mesh Renderer to " + gameObject.name);
        }

        if (WinLock && tearCorrect && !hasWon)
        {
            disableEditing = true;
            hasWon = true;
            TearCorrect.Invoke();
        }

        if (pastWins)
        {
            if (_pastValue > minPastValue && _pastValue < maxPastValue)
                tearCorrect = true;
            
            else 
                tearCorrect = false;
        } else if (!pastWins)
        {
            if (_futureValue > minFutureValue && _futureValue < maxFutureValue)
                tearCorrect = true;  

            else
                tearCorrect = false;
        }

        if (enableDebug)
        {
            Debug.Log("Past Value: " + _pastValue);
            Debug.Log("Future Value: " + _futureValue);
            //Debug.Log(gameObject.name + " in win range");
        }

        if (tearCorrect)
        {
            TearCorrect.Invoke();
        }
        else
        {
            TearFalse.Invoke();
        }
    }
}

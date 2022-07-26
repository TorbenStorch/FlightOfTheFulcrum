/*-------------------------------------------------------
Creator: Philipp Petry
Project: Fulcrum
Last change: 01-07-2022
Topic: Define winning condition for the attached Tear Object
---------------------------------------------------------*/
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TearInteractable))]
public class TearTimeMeasurement : MonoBehaviour
{
    [SerializeField] private bool pastWins;

    private Material _material;
    private TearInteractable _tearIneractableScript;
    private float _pastValue, _futureValue;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float minPastValue, maxPastValue, minFutureValue, maxFutureValue;

    //[HideInInspector]
    public bool tearCorrect = false;


    void Awake()
    {
        _tearIneractableScript = GetComponent<TearInteractable>();
        GetMaterial();
    }

    void Update()
    {
        GetMaterial();

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
        

        if (tearCorrect)
        {
            Debug.Log(gameObject.name + " in win range");
        }
        else
        {
            Debug.Log(gameObject.name + " not in win range anymore");
        }
    }



    private void GetMaterial()
    {
        if (TryGetComponent(out SkinnedMeshRenderer skinnedRenderer))
            _material = gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        else if (TryGetComponent(out MeshRenderer meshRenderer))
            _material = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        

        if (_material != null)
        {
            _pastValue = _material.GetFloat("_BlendToPast");
            _futureValue = _material.GetFloat("_BlendToFuture");
        }
        else
            Debug.Log("No Material has been found for Game Object: " + gameObject.name.ToString());
    }
}

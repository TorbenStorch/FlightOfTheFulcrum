/*-------------------------------------------------------
Creator: Philipp Petry
Project: Fulcrum
Last change: 01-07-2022
Topic: Adjusts Blendshape Value according to the controller rotation
---------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScrolling : MonoBehaviour
{
    public SkinnedMeshRenderer _tearRenderer;
    public MeshRenderer _tearMeshRenderer;
    public float _blendValue;
    public float _controllerRotZ;
    public Raycast _rayCastScript;
    public bool enableTimeScrolling;
    [SerializeField] GameObject heatFVX;
    public AudioSource manipulationSound;

    private GameObject controllerRight;
    private bool blendShapeObjScrolling, regularShapeObjScrolling;
    private TearTimeMeasurement timeMeasurementScript;

    private bool tearTimeWin_byFelix;

    void Start()
    {
        blendShapeObjScrolling = false; 
        regularShapeObjScrolling = false; 
        controllerRight = this.gameObject;
        heatFVX.SetActive(false);
    }

    void Update()
    {
        _controllerRotZ = controllerRight.transform.rotation.z * 100;
        _blendValue = _controllerRotZ;

        //checks if the Tear Object can be edited in time
        if (_rayCastScript._rayCastHit)
        {
            timeMeasurementScript = _rayCastScript.targetObject.GetComponent<TearTimeMeasurement>();
            switch (timeMeasurementScript.disableEditing)
            {
                case false: tearTimeWin_byFelix = true; break;
                case true: tearTimeWin_byFelix = false; break;
            }
        }

        // time manipulation for objects for object with Skinned Mesh Renderer
        if (_rayCastScript._rayCastHit == true && _rayCastScript.targetRenderer != null && enableTimeScrolling && tearTimeWin_byFelix)
        {
            blendShapeObjScrolling = true;
            _tearRenderer = _rayCastScript.targetRenderer;


            if (_blendValue >= 0)
            {
                _tearRenderer.SetBlendShapeWeight(1, _blendValue);
                _rayCastScript.targetMaterial.SetFloat("_BlendToPast", _blendValue/100);

                //rest other shape key
                _tearRenderer.SetBlendShapeWeight(0, 0);
                _rayCastScript.targetMaterial.SetFloat("_BlendToFuture", 0);

            }
            else if (_blendValue < 0)
            {
                _tearRenderer.SetBlendShapeWeight(0, _blendValue - 2 * _blendValue);
                _rayCastScript.targetMaterial.SetFloat("_BlendToFuture", (_blendValue - 2 * _blendValue) / 100);

                //reset other shape key
                _tearRenderer.SetBlendShapeWeight(1, 0);
                _rayCastScript.targetMaterial.SetFloat("_BlendToPast", 0);
            }
        }
        else
        {
            blendShapeObjScrolling = false;
        }

        // time manipulation for objects for object with Mesh Renderer
        if (_rayCastScript._rayCastHit == true && _rayCastScript.targetMeshRenderer != null && enableTimeScrolling && tearTimeWin_byFelix)
        {
            regularShapeObjScrolling = true;
            if (_blendValue >= 0)
            {
                _rayCastScript.targetMaterial.SetFloat("_BlendToPast", _blendValue / 100);
                _rayCastScript.targetMaterial.SetFloat("_BlendToFuture", 0);
            }

            else if (_blendValue < 0) 
            { 
                _rayCastScript.targetMaterial.SetFloat("_BlendToFuture", (_blendValue - 2 * _blendValue) / 100);
                _rayCastScript.targetMaterial.SetFloat("_BlendToPast", 0);
            }        
        }
        else
        {
            regularShapeObjScrolling = false;
        }


        if (blendShapeObjScrolling || regularShapeObjScrolling)
        {
            heatFVX.SetActive(true);
            manipulationSound.Play();
        }
        else if (!blendShapeObjScrolling && !regularShapeObjScrolling)
        {
            heatFVX.SetActive(false);
            manipulationSound.Stop();
        }
        // Debug.Log("Blend to Past Value: " + _rayCastScript.targetMaterial.GetFloat("_BlendToPast").ToString());
        // Debug.Log("Blend to Past Future: " + _rayCastScript.targetMaterial.GetFloat("_BlendToFuture").ToString());
        // Debug.Log("controller rotation z = " + _blendValue / 100);
    }

    public void enableTimeScroll() => enableTimeScrolling = true;
    public void disableTimeScroll() => enableTimeScrolling = false;

}

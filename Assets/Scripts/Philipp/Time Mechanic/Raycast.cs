/*-------------------------------------------------------
Creator: Philipp Petry
Project: Fulcrum
Last change: 22-06-2022
Topic: Shoots Raycast from Hand that gets all Scrolling relevant values from the object
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Raycast : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 direction;
    [HideInInspector]
    public  GameObject targetObject;

    //public bool isGrabbed;
    public SkinnedMeshRenderer targetRenderer;
    public MeshRenderer targetMeshRenderer;
    public Material targetMaterial;

    [SerializeField] LayerMask layerMask;

    [HideInInspector]
    public bool _rayCastHit;

    void Start()
    {
        //direction = new Vector3(0, -90, 0);
        direction = Vector3.forward;
    }

    void Update()
    {
        origin = transform.position;
        direction = Vector3.forward;

        RaycastHit hit;
        if (Physics.Raycast(origin, transform.TransformDirection(direction), out hit, 100f, layerMask))
        {
            targetObject = hit.transform.gameObject;
            // targetObject.TryGetComponent(out TearInteractable targetInteractable);
            // isGrabbed = targetInteractable.isGrabbed;
            Debug.Log("Object Found: " + targetObject.name);

            if (targetObject.TryGetComponent(out SkinnedMeshRenderer skinnedRenderer))
            {
                targetMeshRenderer = null;
                targetRenderer = targetObject.GetComponent<SkinnedMeshRenderer>();
                targetMaterial = targetRenderer.sharedMaterial;
                Debug.Log("Skinned Mesh Render found in: " + targetObject.name);
            }
            else if (targetObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                targetRenderer = null;
                targetMeshRenderer = targetObject.GetComponent<MeshRenderer>();
                targetMaterial = targetMeshRenderer.material;
                Debug.Log("Mesh Render found in: " + targetObject.name);
            }
            else
            {
                Debug.LogWarning("Mesh Renderer or Target Mesh Renderer Missing from Object: " + targetObject.name);
            }


            Debug.Log("Raycast Hit");
            //Debug.Log(hit.point);
            _rayCastHit = true;
            Debug.DrawRay(origin, transform.TransformDirection(direction) * hit.distance, Color.red);
        }
        else
        {
            _rayCastHit = false;
            Debug.DrawRay(origin, transform.TransformDirection(direction) * 100f, Color.blue);
        }

    }
}

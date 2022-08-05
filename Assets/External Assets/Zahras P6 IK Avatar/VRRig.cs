using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{ 
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{

    [SerializeField] Transform headBone;
    float turnSmoothness;
    float startSmoothness;
    float headRotationX;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        turnSmoothness = 3;
        startSmoothness = turnSmoothness;
        
    }

    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized; //Felix' code

        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        #region Fix for Arm Rotation Issue when head is turning
        //Preventing the body from rotating when the head is pointing up by settig the turn value to 0
        headRotationX = headBone.transform.rotation.x;
        if (headRotationX <= 0)
            turnSmoothness = 0;

        else
            turnSmoothness = startSmoothness;
        #endregion

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}

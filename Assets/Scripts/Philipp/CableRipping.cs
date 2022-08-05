using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CableRipping : MonoBehaviour
{
    [SerializeField] bool Use_Debug_Statements;
    [SerializeField] InputActionReference leftVelocity;
    [SerializeField] InputActionReference rightVelocity;
    [Range (0.2f, 0.5f)]
    [SerializeField] float cableResistanceMin;
    [Range(0.6f, 0.9f)]
    [SerializeField] float cableResistanceMax;
    private float l_veloX, l_veloY, l_veloZ, r_veloX, r_veloY, r_veloZ;

    [SerializeField] float spring = 200;
    [SerializeField] float damper = 200;
    [SerializeField] float minDistance= 0.0f;
    [SerializeField] float maxDistance = 0.01f;

    List<GameObject> cableStartObjects = new List<GameObject>();
    private GameObject[] cableStartPositionAR;

    List<CableEnds> cableEndObjects = new List<CableEnds>();
    private GameObject[] cableEndPositionAR;

    private class CableEnds
    {
        public GameObject gmObj;
        public GameObject startingPoint;
        public float breakForce;
        public bool isConnected = true;
    }

    private void Awake()
    {
        #region array into lists
        cableStartPositionAR = GameObject.FindGameObjectsWithTag("CableStart");
        cableEndPositionAR = GameObject.FindGameObjectsWithTag("CableEnd");
        
        for (int i = 0; i < cableEndPositionAR.Length; i++)
        {
            cableEndObjects.Add(new CableEnds { gmObj = cableEndPositionAR[i], breakForce = Random.Range(cableResistanceMin, cableResistanceMax) });
        }
        
        for (int i = 0; i < cableStartPositionAR.Length; i++)
        {
            cableStartPositionAR[i].GetComponent<CableComponent>().endPoint = cableEndObjects[i].gmObj.transform;
            cableStartObjects.Add(cableStartPositionAR[i].gameObject);
        }

        for (int i = 0; i < cableEndObjects.Count; i++)
        {
            cableEndObjects[i].startingPoint = cableStartObjects[i];
        }
        #endregion
        
        if (Use_Debug_Statements)
        {
            PrintList();
        }
    }

    void PrintList()
    {
        for (int l = 0; l < cableStartObjects.Count; l++)
        {
            Debug.Log(" || GameObject Name: " + cableStartObjects[l].name);
        }

        for (int i = 0; i < cableEndObjects.Count; i++)
        {
            Debug.Log(" || GameObject Name: " + cableEndObjects[i].gmObj.name + " || Cable Break Force: " + cableEndObjects[i].breakForce);
        }
    }

    void Update()
    {
        var leftVelocity = this.leftVelocity.action.ReadValue<Vector3>();
        l_veloX = leftVelocity.x;
        l_veloY = leftVelocity.y;
        l_veloZ = leftVelocity.z;

        var rightVelocity = this.rightVelocity.action.ReadValue<Vector3>();
        r_veloX = rightVelocity.x;
        r_veloY = rightVelocity.y;
        r_veloZ = rightVelocity.z;

        if (Use_Debug_Statements)
        {
            Debug.Log("Left Velocity = " + leftVelocity);
            Debug.Log("Right Velocity = " + r_veloX);
        }

        for (int i = 0; i < cableEndObjects.Count; i++)
        {
            if (cableEndObjects[i].isConnected || r_veloX > cableEndObjects[i].breakForce)
            {
                DetachCable(cableEndObjects[i]);
            }
        } 
    }

    private void DetachCable(CableEnds item)
    {
        item.gmObj.transform.SetParent(item.startingPoint.transform);
        item.startingPoint.AddComponent<SpringJoint>();

        var springJoint = item.startingPoint.GetComponent<SpringJoint>();
        springJoint.connectedBody = item.gmObj.GetComponent<Rigidbody>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.spring = spring;
        springJoint.damper = damper;
        springJoint.maxDistance = maxDistance;
        springJoint.minDistance = minDistance;
        item.gmObj.GetComponent<BoxCollider>().isTrigger = false;
        item.gmObj.GetComponent<Rigidbody>().useGravity = true;
        item.isConnected = false;

        if (Use_Debug_Statements)
        {
            Debug.Log("Cable Detached: " + item.gmObj.name);
        }
    }
}

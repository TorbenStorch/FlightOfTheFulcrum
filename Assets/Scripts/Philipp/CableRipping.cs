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
    [Range(0.6f, 2.0f)]
    [SerializeField] float cableResistanceMax;
    private float l_veloX, l_veloY, l_veloZ, r_veloX, r_veloY, r_veloZ;

    [SerializeField] float spring = 200;
    [SerializeField] float damper = 200;
    [SerializeField] float minDistance= 0.0f;
    [SerializeField] float maxDistance = 0.01f;

    private class r_CableEnds
    {
        public GameObject gmObj;
        public GameObject startingPoint;
        public float breakForce;
        public bool isConnected = true;
    }

    private class l_CableEnds
    {
        public GameObject gmObj;
        public GameObject startingPoint;
        public float breakForce;
        public bool isConnected = true;
    }

    List<GameObject> r_cableStartObjects = new List<GameObject>();
    private GameObject[] r_cableStartPositionAR;

    List<r_CableEnds> r_cableEndObjects = new List<r_CableEnds>();
    private GameObject[] r_cableEndPositionAR;

    List<GameObject> l_cableStartObjects = new List<GameObject>();
    private GameObject[] l_cableStartPositionAR;

    List<l_CableEnds> l_cableEndObjects = new List<l_CableEnds>();
    private GameObject[] l_cableEndPositionAR;


    private void Awake()
    {
        #region array into lists
        r_cableStartPositionAR = GameObject.FindGameObjectsWithTag("r_CableStart");
        r_cableEndPositionAR = GameObject.FindGameObjectsWithTag("r_CableEnd");

        l_cableStartPositionAR = GameObject.FindGameObjectsWithTag("l_CableStart");
        l_cableEndPositionAR = GameObject.FindGameObjectsWithTag("l_CableEnd");

        for (int i = 0; i < r_cableEndPositionAR.Length; i++)
        {
            r_cableEndObjects.Add(new r_CableEnds { gmObj = r_cableEndPositionAR[i], breakForce = Random.Range(cableResistanceMin, cableResistanceMax) });
        }

        for (int i = 0; i < l_cableEndPositionAR.Length; i++)
        {
            l_cableEndObjects.Add(new l_CableEnds { gmObj = l_cableEndPositionAR[i], breakForce = Random.Range(cableResistanceMin, cableResistanceMax) });
        }

        for (int i = 0; i < r_cableStartPositionAR.Length; i++)
        {
            r_cableStartPositionAR[i].GetComponent<CableComponent>().endPoint = r_cableEndObjects[i].gmObj.transform;
            r_cableStartObjects.Add(r_cableStartPositionAR[i].gameObject);
        }

        for (int i = 0; i < l_cableStartPositionAR.Length; i++)
        {
            l_cableStartPositionAR[i].GetComponent<CableComponent>().endPoint = l_cableEndObjects[i].gmObj.transform;
            l_cableStartObjects.Add(l_cableStartPositionAR[i].gameObject);
        }

        for (int i = 0; i < r_cableEndObjects.Count; i++)
        {
            r_cableEndObjects[i].startingPoint = r_cableStartObjects[i];
        }

        for (int i = 0; i < l_cableEndObjects.Count; i++)
        {
            l_cableEndObjects[i].startingPoint = l_cableStartObjects[i];
        }
        #endregion

        if (Use_Debug_Statements)
        {
            PrintList();
        }
    }

    void PrintList()
    {
        for (int l = 0; l < r_cableStartObjects.Count; l++)
        {
            Debug.Log(" || GameObject Name: " + r_cableStartObjects[l].name);
        }

        for (int i = 0; i < r_cableEndObjects.Count; i++)
        {
            Debug.Log(" || GameObject Name: " + r_cableEndObjects[i].gmObj.name + " || Cable Break Force: " + r_cableEndObjects[i].breakForce);
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
            Debug.Log("Left Velocity = " + l_veloX);
            Debug.Log("Right Velocity = " + r_veloX);
        }

        for ( int i = 0; i < l_cableEndObjects.Count; i++)
        {
            if (l_cableEndObjects[i].isConnected ||
                l_veloX > l_cableEndObjects[i].breakForce)
            {
                l_DetachCable(l_cableEndObjects[i]);
            }
        }
 
        for (int i = 0; i < r_cableEndObjects.Count; i++)
        {
            if ( r_cableEndObjects[i].isConnected || 
                r_veloX > r_cableEndObjects[i].breakForce)
            {
                r_DetachCable(r_cableEndObjects[i]);
            }
        }

    }

    private void r_DetachCable(r_CableEnds item)
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

    private void l_DetachCable(l_CableEnds item)
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

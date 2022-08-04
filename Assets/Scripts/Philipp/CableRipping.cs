using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CableRipping : MonoBehaviour
{
    [SerializeField] GameObject cableObj;
    [SerializeField] InputActionReference leftVelocity;
    [SerializeField] InputActionReference rightVelocity;
    [SerializeField] float cableResistance;
    private float l_veloX, l_veloY, l_veloZ, r_veloX, r_veloY, r_veloZ;

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


        Debug.Log("Left Velocity = " + leftVelocity);
        Debug.Log("Right Velocity = " + r_veloX);

        if (r_veloX > cableResistance)
        {
            Debug.Log("CableFree");
            cableObj.transform.SetParent(null);
            //cableObj.GetComponent<Rigidbody>().useGravity = true;
        }    
    }
}

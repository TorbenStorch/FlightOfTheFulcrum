//Credits: Justin P Barnett, on Youtube: https://www.youtube.com/watch?v=pgX2tLIXNZ8
using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(CharacterController))]
public class RoomScaleFix : MonoBehaviour
{
    private CharacterController _character;
    private XROrigin _xrOrigin;


    void Start()
    {
        _character = GetComponent<CharacterController>();
        _xrOrigin = GetComponent<XROrigin>();
    }

    void FixedUpdate()
    {
        var centerPoint = transform.InverseTransformPoint(_xrOrigin.Camera.transform.position);
        _character.center = new Vector3(centerPoint.x, _character.height / 2 + _character.skinWidth, centerPoint.z);

        //Super tiny jittering of the player controller in order to force unity to calculate collisions
        _character.Move(new Vector3(0.001f, 0.0f, 0.001f));
        _character.Move(new Vector3(-0.001f, 0.0f, -0.001f));
    }
}

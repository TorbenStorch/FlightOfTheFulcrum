// Credits: Justin P Barnett, on Youtube: https://www.youtube.com/watch?v=EmjBonbATS0 //
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] private Transform resetTransform;
    [SerializeField] private GameObject xrOrigin;
    [SerializeField] private Camera _camera;
    [SerializeField] private float resetDelay;

    void Start()
    {
        if (resetTransform && xrOrigin && _camera)
        {
            Invoke("ResetPosition", resetDelay);
        }
    }

    [ContextMenu("Reset Player")]
    public void ResetPosition()
    {
        var rotationAngleY = _camera.transform.rotation.eulerAngles.y - resetTransform.rotation.eulerAngles.y;
        xrOrigin.transform.Rotate(0, -rotationAngleY, 0);

        var distanceDiff = resetTransform.position - _camera.transform.position;
        xrOrigin.transform.position += distanceDiff;

        Debug.Log("Player Position has been reset!");
    }
}

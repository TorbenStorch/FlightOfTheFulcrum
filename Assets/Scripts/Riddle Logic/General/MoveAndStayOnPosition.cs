using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndStayOnPosition : MonoBehaviour
{
	[SerializeField] private GameObject objectToMove;
	[SerializeField] private Transform target;
	[SerializeField] private float moveSteps;

	bool objOnTarget;
	private void Update()
	{
		if (objOnTarget)
		{
			objectToMove.transform.position = target.position;
			objectToMove.transform.rotation = target.rotation;
		}
	}


	public void RemoveFromTarget()
	{
		objOnTarget = false;
		objectToMove.gameObject.GetComponent<Rigidbody>().useGravity = true;
		objectToMove.gameObject.GetComponent<Rigidbody>().isKinematic = false;
	}
	public void MoveObjToTarget()
	{
		if (!objOnTarget)
		{
			StartCoroutine(MoveToTarget(objectToMove.transform, target));
			objectToMove.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			objectToMove.gameObject.GetComponent<Rigidbody>().useGravity = false;
		}
	}
	IEnumerator MoveToTarget(Transform moveObject, Transform target)
	{
		float t = 0f;
		yield return new WaitForSeconds(1);
		while (Vector3.Distance(moveObject.position, target.position) >= 0.1f)
		{
			t += moveSteps;
			moveObject.position = Vector3.MoveTowards(moveObject.position, target.position, t);
			yield return null;
		}
		objOnTarget = true;
	}


}

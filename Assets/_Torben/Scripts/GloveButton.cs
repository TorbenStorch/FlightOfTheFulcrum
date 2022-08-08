/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 30-06-2022
Topic: Play Audio when the target eneters the trigger
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GloveButton : MonoBehaviour
{
	[SerializeField] private GameObject handCollider;
	[SerializeField] private GameObject frame;

	//values from Philipp
	[SerializeField] private Material GloveEmissiveMat;
	[SerializeField] Color StetoColor;
	[SerializeField] Color TimeColor;

	[Range(0.1f,1f)]
	[SerializeField] private float timeToGoDown = 0.1f;
	[Range(0.1f, 1f)]
	[SerializeField] private float timeToGoUp = 0.1f;

	public UnityEvent onPress;
	public UnityEvent onDown;

	bool buttonDown;
	Vector3 startPos;

    private void Start() => startPos = gameObject.transform.localPosition;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == handCollider && !buttonDown)
		{
			onPress.Invoke();
			StartCoroutine(ButtonGoesDown());
		}
		if (other.gameObject == frame)
		{
			buttonDown = true;
			onDown.Invoke();
		}
	}

	public void ButtonGoesUpCall() => StartCoroutine(ButtonGoesUp());

	IEnumerator ButtonGoesDown()
	{
		while (!buttonDown)
		{
			gameObject.transform.localPosition -= new Vector3(0, timeToGoDown * Time.deltaTime, 0);
			yield return null;
		}
		yield return null;
	}

	IEnumerator ButtonGoesUp()
	{
		while (gameObject.transform.localPosition.y < startPos.y)
		{
			gameObject.transform.localPosition += new Vector3(0, timeToGoUp * Time.deltaTime, 0);
			yield return null;
		}
		buttonDown = false;
		yield return null;
	}

	//extension from philipp to make the glove material change
	public void changeColorToTime() =>  GloveEmissiveMat.SetColor("_EmissionColor", TimeColor);
    

	public void changeColorToSteto() => GloveEmissiveMat.SetColor("_EmissionColor", StetoColor);
	
}

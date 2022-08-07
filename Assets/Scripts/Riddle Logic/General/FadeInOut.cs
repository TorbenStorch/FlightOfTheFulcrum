using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;
using UnityEngine.VFX;

public class FadeInOut : MonoBehaviour
{
	[SerializeField] Renderer myRenderer;
	

	[SerializeField] float alphaSteps;
	[SerializeField] float goalAplhaAmount;
	[SerializeField] float delayBeforeFadeBegin;
	Color aplhaColor;

	public void FadeAlphaIn()
	{
		aplhaColor = myRenderer.material.color;
		aplhaColor.a = 0;
		myRenderer.material.color = aplhaColor;

		StartCoroutine(FadeIn());
	}
	public void FadeAlphaOut()
	{
		aplhaColor = myRenderer.material.color;
		//aplhaColor.a = 1;
		//myRenderer.material.color = aplhaColor;

		StartCoroutine(FadeOut());
	}


	IEnumerator FadeIn()
	{
		yield return new WaitForSeconds(delayBeforeFadeBegin);
		aplhaColor = myRenderer.material.color;
		while (aplhaColor.a < goalAplhaAmount)
		{
			aplhaColor.a += alphaSteps;
			myRenderer.material.color = aplhaColor;
			yield return null;
		}
		yield return null;
	}
	IEnumerator FadeOut()
	{
		while (aplhaColor.a > goalAplhaAmount)
		{
			aplhaColor.a -= alphaSteps;
			myRenderer.material.color = aplhaColor;
			yield return null;
		}
		yield return null;
	}
}

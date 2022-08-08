/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-08-2022
Topic: Fade Alpha, Shader Transparency, Shader EdgeTint
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;
using UnityEngine.VFX;

public class FadeInOut : MonoBehaviour
{

	[Header("Normal Transparent Material")]
	[SerializeField] Renderer materialAlphaRenderer;
	Color aplhaColor;

	[Header("Shader Transparent Material")]
	[SerializeField] Renderer shaderRenderer;
	[HideInInspector] public static int transparencyID = Shader.PropertyToID("_Transparency");
	[HideInInspector] public static int edgeTintID = Shader.PropertyToID("_EdgeTintTest");

	[Header("Color Stuff")]
	Color startColor;
	[SerializeField] Color endColor;
	[SerializeField] float colorSteps;



	[Header("Fade IN Adjustments")]
	[SerializeField] float fadeInSteps;
	[SerializeField] float goalFadeInTransparencyAmount;
	[SerializeField] float delayBeforeFadeInBegin;
	[Header("Fade OUT Adjustments")]
	[SerializeField] float fadeOutSteps;
	[SerializeField] float goalFadeOutTransparencyAmount;
	[SerializeField] float delayBeforeFadeOutBegin;


	#region Alpha Fade
	public void FadeAlphaIn()
	{
		if (materialAlphaRenderer == null) return;

		aplhaColor = materialAlphaRenderer.material.color;
		aplhaColor.a = 0;
		materialAlphaRenderer.material.color = aplhaColor;

		StartCoroutine(FadeAlphaMatIn());
	}
	public void FadeAlphaOut()
	{
		if (materialAlphaRenderer == null) return;

		aplhaColor = materialAlphaRenderer.material.color;
		//aplhaColor.a = 1;
		//myRenderer.material.color = aplhaColor;

		StartCoroutine(FadeAlphaMatOut());
	}

	IEnumerator FadeAlphaMatIn()
	{
		yield return new WaitForSeconds(delayBeforeFadeInBegin);
		aplhaColor = materialAlphaRenderer.material.color;
		while (aplhaColor.a < goalFadeInTransparencyAmount)
		{
			aplhaColor.a += fadeInSteps;
			materialAlphaRenderer.material.color = aplhaColor;
			yield return null;
		}
		yield return null;
	}
	IEnumerator FadeAlphaMatOut()
	{
		yield return new WaitForSeconds(delayBeforeFadeOutBegin);
		while (aplhaColor.a > goalFadeOutTransparencyAmount)
		{
			aplhaColor.a -= fadeOutSteps;
			materialAlphaRenderer.material.color = aplhaColor;
			yield return null;
		}
		yield return null;
	}
	#endregion

	#region Shader Transparency Fade
	public void FadeShaderTransparencyIn()
	{
		shaderRenderer.material.SetFloat(transparencyID, 0f);
		StartCoroutine(FadeTransparencyIn());
	}

	public void FadeShaderTransparencyOut()
	{
		//shaderTransparencyRenderer.material.SetFloat(transparencyID, 1f);
		StartCoroutine(FadeTransparencyOut());
	}

	IEnumerator FadeTransparencyIn()
	{
		float counter = shaderRenderer.material.GetFloat(transparencyID);
		yield return new WaitForSeconds(delayBeforeFadeInBegin);
		while (counter < goalFadeInTransparencyAmount)
		{
			counter += fadeInSteps;
			shaderRenderer.material.SetFloat(transparencyID, counter);
			yield return null;
		}
		shaderRenderer.material.SetFloat(transparencyID, goalFadeInTransparencyAmount);
		yield return null;
	}

	IEnumerator FadeTransparencyOut()
	{
		float counter = shaderRenderer.material.GetFloat(transparencyID);
		yield return new WaitForSeconds(delayBeforeFadeOutBegin);
		while (counter > goalFadeOutTransparencyAmount)
		{
			counter -= fadeOutSteps;
			shaderRenderer.material.SetFloat(transparencyID, counter);
			yield return null;
		}
		shaderRenderer.material.SetFloat(transparencyID, goalFadeOutTransparencyAmount);
		yield return null;
	}
	#endregion

	#region Shader EdgeTint Color
	public void FadeShaderEdgeTint()
	{
		//shaderRenderer.material.SetColor(edgeTintID, endColor);
		startColor = shaderRenderer.material.GetColor(edgeTintID);
		StartCoroutine(FadeEdgeTint());
	}
	IEnumerator FadeEdgeTint()
	{
		float t = 0f;
		while (shaderRenderer.material.GetColor(edgeTintID) != endColor)
		{
			t += colorSteps;
			var color = Color.Lerp(startColor, endColor, t);
			shaderRenderer.material.SetColor(edgeTintID, color);
			yield return null;
		}
		shaderRenderer.material.SetColor(edgeTintID, endColor);
		yield return null;
	}
	#endregion
}

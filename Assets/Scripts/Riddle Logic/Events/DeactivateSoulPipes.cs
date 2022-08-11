using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSoulPipes : MonoBehaviour
{
	[SerializeField] Material soulSuckMat;

	[ColorUsage(true, true)]
	[SerializeField] Color soulSuckOnColor;

	[ColorUsage(true, true)]
	[SerializeField] Color soulSuckOffColor;



	[SerializeField] float colorFadeSpeed;

	private void Start() => soulSuckMat.SetColor("_EmissionColor", soulSuckOnColor);



	public void DisableSoulSuckMaterial()
	{
		StartCoroutine(FadeOutEmissiveColor());
	}

	IEnumerator FadeOutEmissiveColor()
	{
		float counter = 0f;
		while (counter < 1)
		{
			counter += colorFadeSpeed;
			soulSuckMat.SetColor("_EmissionColor", Color.Lerp(soulSuckMat.GetColor("_EmissionColor"), soulSuckOffColor, counter));
			yield return null;
		}
		soulSuckMat.SetColor("_EmissionColor", soulSuckOffColor);
		yield return null;
	}
}

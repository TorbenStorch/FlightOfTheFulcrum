using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DeactivateVFX : MonoBehaviour
{
	[SerializeField] private VisualEffect soulVfx;
	[SerializeField] float fadeSpeed;
	[SerializeField] float goalFadeOutThicknessAmount = 0f;

	public void FadeVFXOut()
	{
		StartCoroutine(FadeVFThickness());
	}

	IEnumerator FadeVFThickness()
	{
		float counter = soulVfx.GetFloat("Thickness"); ;
		while (counter > goalFadeOutThicknessAmount)
		{
			counter -= fadeSpeed;
			soulVfx.SetFloat("Thickness", counter);
			yield return null;
		}
		soulVfx.SetFloat("Thickness", goalFadeOutThicknessAmount);
		yield return null;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class DeactivateVFX : MonoBehaviour
{
	[SerializeField] private VisualEffect soulVfx;
	[SerializeField] float fadeOutSpeed, fadeInSpeed;
	[SerializeField] float goalFadeOutThicknessAmount = 0f, goalFadeInThicknessAmount = 0f;

	public UnityEvent onVfxDisabled, onVfXEnable;

	public void FadeVFXOut()
	{
		StartCoroutine(FadeOutVFThickness());
	}

	public void FadeVFXIn()
	{
		StartCoroutine(FadeInVFThickness());
	}

	IEnumerator FadeOutVFThickness()
	{
		float counter = soulVfx.GetFloat("Thickness"); ;
		while (counter > goalFadeOutThicknessAmount)
		{
			counter -= fadeOutSpeed;
			soulVfx.SetFloat("Thickness", counter);
			yield return null;
		}
		soulVfx.SetFloat("Thickness", goalFadeOutThicknessAmount);
		soulVfx.enabled = false;
		onVfxDisabled.Invoke();
		yield return null;
	}


	IEnumerator FadeInVFThickness()
	{
		soulVfx.SetFloat("Thickness", 0f);
		float counter = soulVfx.GetFloat("Thickness"); 
		soulVfx.enabled = true;
		while (counter < goalFadeInThicknessAmount)
		{
			counter += fadeInSpeed;
			soulVfx.SetFloat("Thickness", counter);
			yield return null;
		}
		soulVfx.SetFloat("Thickness", goalFadeInThicknessAmount);
		//onVfxDisabled.Invoke();
		yield return null;
	}
}

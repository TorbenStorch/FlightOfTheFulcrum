using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class DeactivateVFX : MonoBehaviour
{
	[SerializeField] private VisualEffect soulVfx;
	[SerializeField] float fadeSpeed;
	[SerializeField] float goalFadeOutThicknessAmount = 0f;

	public UnityEvent soulSuckDisable;
	[SerializeField] Material soulSuckMat;
	[SerializeField] Color soulSuckNewColor;
	[SerializeField] float colorFadeSpeed;

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
		soulVfx.enabled = false;
		soulSuckDisable.Invoke();
		yield return null;
	}

    public void disableSoulSuckMaterial()
    {
		//soulSuckMat.SetColor("_EmissionColor", soulSuckNewColor);
		StartCoroutine(fadeLerp());
	}

	IEnumerator fadeLerp()
    {
		float f;
		f = 0f;
		while (f < 1)
        {
			f += colorFadeSpeed;
			soulSuckMat.SetColor("_EmissionColor", Color.Lerp(soulSuckMat.GetColor("_EmissionColor"), soulSuckNewColor, f));
			yield return null;
		}
		soulSuckMat.SetColor("_EmissionColor", Color.Lerp(soulSuckMat.GetColor("_EmissionColor"), soulSuckNewColor, 1));
		yield return null;
    }

}

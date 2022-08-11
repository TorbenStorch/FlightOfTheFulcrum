using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{   
	[SerializeField] Renderer materialRenderer;
	[SerializeField] int materialArrayIndex = 0;

	public void ChangeTheMaterial(Material changeMaterial)
	{
		Material[] mat = materialRenderer.GetComponent<Renderer>().materials;
		mat[1] = changeMaterial;
		materialRenderer.GetComponent<Renderer>().materials = mat;
	}
}

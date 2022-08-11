using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	[SerializeField] float delay = 0f;
	[SerializeField] int sceneIndex = 0;
	public void LoadScene()
	{
		StartCoroutine(LoadSceneDelayed());
	}
	IEnumerator LoadSceneDelayed()
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadSceneAsync(sceneIndex);
	}
}

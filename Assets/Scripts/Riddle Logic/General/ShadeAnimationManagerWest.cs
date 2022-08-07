/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-06-2022
Topic: Animator connecton to each West shade & calling of animation.
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeAnimationManagerWest : MonoBehaviour
{
	[SerializeField] Animator anim01, anim02, anim03;

	private void Start()
	{
		DieAnim();
	}
	public void LetterAnim() => anim01.SetBool("doLetter", true);
	public void MedicineAnim() => anim02.SetBool("doCough", true);
	public void DieAnim() => anim03.SetBool("doDie", true);
}

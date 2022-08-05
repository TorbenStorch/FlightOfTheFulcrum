using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_West : MonoBehaviour
{

    [SerializeField] Animator anim01, anim02, anim03;


    void Start()
    {
        anim01.SetBool("doLetter", true);
        anim02.SetBool("doCough", true);
        anim03.SetBool("doDie", true);
    }

    void Update()
    {

    }
}

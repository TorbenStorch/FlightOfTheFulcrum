/*-------------------------------------------------------
Creator: Philipp Petry
Project: Fulcrum
Last change: 17-06-2022
Topic: Adds rotating magic sigils to the game object. 
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Rotation : MonoBehaviour
{
    [SerializeField] GameObject magicOut;
    [SerializeField] GameObject magicMid;
    [SerializeField] GameObject magicInner;
    [SerializeField] Vector3 rot;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fadeSpeed;

    [SerializeField] Material matOut, matMid, matInner;

    [SerializeField] TimeScrolling scrollingScript;
    private float rotationDefault;
    private bool startFadingIn, startFadingOut;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rotationDefault = rotationSpeed;
        materialSetup();
    }

    void Update()
    {
        if (scrollingScript._rayCastScript._rayCastHit == true)
        {
            startFadingIn = true;
            startFadingOut = false;
            rotationSpeed = scrollingScript._controllerRotZ;
            audioSource.Play();
        }
        else
        {
            startFadingOut = true;
            startFadingIn = false;
            rotationSpeed = rotationDefault;
            audioSource.Stop();
        }

        //audioSource.pitch = rotationSpeed;
        //magicOut.transform.Rotate(rot * rotationSpeed * Time.deltaTime);
        magicMid.transform.Rotate(rot * rotationSpeed * 2 * Time.deltaTime);
        magicInner.transform.Rotate(rot * rotationSpeed / 10 * Time.deltaTime);

        if (audioSource.isPlaying)
        {
            Debug.Log("Currently Playing: " + audioSource.name);
        }

        #region alpha fade
        Color color = matOut.color;

        if (startFadingIn && !startFadingOut)
        {
            if (color.a < 1)
            {
                color.a += fadeSpeed;
                matOut.color = color;
                matMid.color = color;
                matInner.color = color;
                //Debug.Log(color.a);
            }
            else
            {
                startFadingIn = false;
            }
        }

        if (startFadingOut && !startFadingIn)
        {
            if (color.a > 0.0f)
            {
                color.a -= fadeSpeed;
                matOut.color = color;
                matMid.color = color;
                matInner.color = color;
                //Debug.Log(color.a);
            }
            else
            {
                color.a = 0;
                matOut.color = color;
                matMid.color = color;
                matInner.color = color;
                startFadingOut = false;
            }
        }
        #endregion

    }


    private void materialSetup()
    {
        //matOut = transform.GetChild(0).GetComponent<Material>();
        //matMid = transform.GetChild(1).GetComponent<Material>();
        //matInner = transform.GetChild(2).GetComponent<Material>();

        Color colorOut = matOut.color;
        colorOut.a = 0f;
        matOut.color = colorOut;

        Color colorMid = matMid.color;
        colorMid.a = 0f;
        matMid.color = colorMid;

        Color colorInner = matInner.color;
        colorInner.a = 0f;
        matInner.color = colorInner;
    }
}

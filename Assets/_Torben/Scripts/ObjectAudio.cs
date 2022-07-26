/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 30-06-2022
Topic: Play Audio when the target eneters the trigger

Explanaition:
When the Target enters the trigger-collider, the audio will start.
When the Target enters the furthestDistanceToHead radium around soundOrigin,
the volume increases until it is at maximum at the closestDistanceToHead radius around 
the soundOrigin.
When the Target leaves the trigger-collider, the audio will stop - reentering will restart the audio.

-> If u want the audio to start directly when entering the trigger-collider, adjust the furthestDistanceToHead
so that it is at the same position/scale as the trigger-collider.
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Needs a Collider with isTrigger true & atleast one Rigidbody (either on this or on target)!
[RequireComponent(typeof(AudioSource))]
public class ObjectAudio : MonoBehaviour
{
	[Header("Target to enter TriggerCollider")]
	[SerializeField] GameObject target; //the object that should start the audio
	[Header("Origin of the sound")]
	[SerializeField] GameObject soundOrigin; //the 3D position of the audio
	[Header("Start/Stop/Maximum radius for audio")]
	[Range(0f, 2f)]
	[SerializeField] float closestDistanceToHead = 0f; //the radius where the audio will be at max
	[Range(0f, 10f)]
	[SerializeField] float furthestDistanceToHead = 1f; //the radius where the audio will start (lowest)
	[Header("Maximum volume of AudioSourceClip")]
	[Tooltip("The Volume of the AudioSource should stay at 1, here it can be adjusted to be lower!")]
	[SerializeField] float volumeMax = 1f; //change this in unity to adjust the maximum volume

	public static bool canHearAudio { set; get; }


	AudioSource audioSource;
	bool canPlayAudio;

	[Space(10)]
	[Header("___")]
	[Header("Unity Editor - Gizmo")]
	[SerializeField] bool _visibleOnSelect;
	[SerializeField] bool _wiredSpheres;


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (!target) Debug.LogError("Target is missing!");
	}
	private void Update()
	{
		if (!canHearAudio)
		{
			canPlayAudio = false;
		}

		if (canPlayAudio && !audioSource.isPlaying)
		{
			audioSource.Play();
			Debug.Log(audioSource.clip.name + " started playing");
		}
		if (!canPlayAudio && audioSource.isPlaying)
		{
			audioSource.Stop();
			Debug.Log(audioSource.clip.name + " stopped playing");
		}

		if (audioSource.isPlaying)
		{
			//Map the distance between the target and soundOrigin to closestDistanceToHead & furthestDistanceToHead 
			//-> the volume is 0*volumeMax if the target is outside of furthestDistanceToHead & the volume is 1*volumeMax if target is inside/on closestDistanceToHead
			float distance = Vector3.Distance(soundOrigin.transform.position, target.transform.position) - furthestDistanceToHead;
			audioSource.volume = volumeMax * (distance / (closestDistanceToHead - furthestDistanceToHead));
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == target && canHearAudio)
			canPlayAudio = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == target)
			canPlayAudio = false;
	}

	#region Gizmo
	void OnDrawGizmos() //visulazation for furthestDistanceToHead & closestDistanceToHead radius
	{
		if (!_visibleOnSelect)
		{
			if (_wiredSpheres)
			{
				Gizmos.color = new Color(0, 1, 0, 0.3f);
				Gizmos.DrawWireSphere(soundOrigin.transform.position, closestDistanceToHead);

				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(soundOrigin.transform.position, furthestDistanceToHead);
			}
			else
			{
				Gizmos.color = new Color(0, 1, 0, 0.3f);
				Gizmos.DrawSphere(soundOrigin.transform.position, closestDistanceToHead);

				Gizmos.color = new Color(1, 1, 0, 0.3f);
				Gizmos.DrawSphere(soundOrigin.transform.position, furthestDistanceToHead);
			}
		}
	}
	void OnDrawGizmosSelected() //visulazation for furthestDistanceToHead & closestDistanceToHead radius
	{
		if (_visibleOnSelect)
		{
			if (_wiredSpheres)
			{
				Gizmos.color = new Color(0, 1, 0, 0.3f);
				Gizmos.DrawWireSphere(soundOrigin.transform.position, closestDistanceToHead);

				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(soundOrigin.transform.position, furthestDistanceToHead);
			}
			else
			{
				Gizmos.color = new Color(0, 1, 0, 0.3f);
				Gizmos.DrawSphere(soundOrigin.transform.position, closestDistanceToHead);

				Gizmos.color = new Color(1, 1, 0, 0.3f);
				Gizmos.DrawSphere(soundOrigin.transform.position, furthestDistanceToHead);
			}
		}
	}
	#endregion
}

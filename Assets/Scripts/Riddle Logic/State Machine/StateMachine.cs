using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    State currentState;
    [SerializeField] GameObject[] togglableObjects_1;
    [SerializeField] GameObject[] togglableObjects_2;
    [SerializeField] GameObject[] togglableObjects_3;
    public static bool state_1_finished;

    void Start()
    {
        currentState = new Dying(togglableObjects_1, togglableObjects_2, togglableObjects_3);
    }

    void Update()
    {
        currentState = currentState.Process();
    }

    public void PlayerCloseToShade()
    {
        state_1_finished = true;
    }
}

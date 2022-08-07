using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

public class State
{
    public enum STATE //enum is sort of a list of int-numbers but you can use the name of each thing instead of the number
    {
        //State_1_Dying, State_2_Letter, State_3_Medicine, State_4_Victory
        STATE_1_DYING, STATE_2_LETTER, STATE_3_MEDICINE, STATE_4_VICTORY
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;

    protected EVENT stage;
    protected State nextState;
    protected GameObject[] toggleObjects_1;
    protected GameObject[] toggleObjects_2;
    protected GameObject[] toggleObjects_3;

    //Constructor
    public State(GameObject[] _toggleObjects_1, GameObject[]  _toggleObjects_2, GameObject[]  _toggleObjects_3)
    {
        stage = EVENT.ENTER;
    }

    public virtual void Enter(){stage = EVENT.UPDATE;} //pushes from enter state into the update state
    public virtual void Update(){stage = EVENT.UPDATE;}
    public virtual void Exit(){stage = EVENT.EXIT;}

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}


public class Dying : State
{
    GameObject[] togglableObjects;

    public Dying(GameObject[] _toggleObjects_1, GameObject[] _toggleObjects_2, GameObject[] _toggleObjects_3)
        : base(_toggleObjects_1, _toggleObjects_2, _toggleObjects_3)
    {
        togglableObjects = _toggleObjects_1;
        name = STATE.STATE_1_DYING;
    }

    public override void Enter()
    {
        togglableObjects.ToggleGameObjectArray(false);
        base.Enter(); //go to base class enter -> so it pushes us to update
    }
    public override void Update()
    {
        if (StateMachine.state_1_finished == true)
        {
            nextState = new Letter(toggleObjects_1, toggleObjects_2, toggleObjects_3);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}


public class Letter : State
{
    GameObject[] togglableObjects;

    public Letter(GameObject[] _toggleObjects_1, GameObject[] _toggleObjects_2, GameObject[] _toggleObjects_3)
        : base(_toggleObjects_1, _toggleObjects_2, _toggleObjects_3)
    {
        togglableObjects = _toggleObjects_2;
        name = STATE.STATE_2_LETTER;
    }

    public override void Enter()
    {
        togglableObjects.ToggleGameObjectArray(false);
        base.Enter();
    }

    public override void Update()
    {
        //if ()
        //{
        //    nextState = new Medicine(toggleObjects_1, toggleObjects_2, toggleObjects_3);
        //    stage = EVENT.EXIT;
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class Medicine : State
{
    GameObject[] togglableObjects;

    public Medicine(GameObject[] _toggleObjects_1, GameObject[] _toggleObjects_2, GameObject[] _toggleObjects_3)
        : base(_toggleObjects_1, _toggleObjects_2, _toggleObjects_3)
    {
        togglableObjects = _toggleObjects_3;
        name = STATE.STATE_3_MEDICINE;
    }

    public override void Enter()
    {
        togglableObjects.ToggleGameObjectArray(false);
        base.Enter();
    }

    public override void Update()
    {
        //if ()
        //{
        //    nextState = new Victory(toggleObjects_1, toggleObjects_2, toggleObjects_3);
        //    stage = EVENT.EXIT;
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}



public class Victory : State
{
    public Victory(GameObject[] _toggleObjects_1, GameObject[] _toggleObjects_2, GameObject[] _toggleObjects_3)
        : base(_toggleObjects_1, _toggleObjects_2, _toggleObjects_3)
    {
        name = STATE.STATE_4_VICTORY;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        //if ()
        //{
        //    nextState = new Dying(toggleObjects_1, toggleObjects_2, toggleObjects_3);
        //    stage = EVENT.EXIT;
        //}
    }
    public override void Exit()
    {
        base.Exit();
    }
}
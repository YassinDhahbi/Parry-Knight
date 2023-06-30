using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void EnterState(StateMachine StateMachine);

    public abstract void ExitState(StateMachine StateMachine);

    public abstract void UpdateState(StateMachine StateMachine);
}
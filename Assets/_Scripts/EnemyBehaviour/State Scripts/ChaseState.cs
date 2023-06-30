using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "Chase State", menuName = " StateMachine/States/ChaseState")]
public class ChaseState : State
{
    public override void EnterState(StateMachine StateMachine)
    {
    }

    public override void ExitState(StateMachine StateMachine)
    {
    }

    public override void UpdateState(StateMachine StateMachine)
    {
        ((EnemyStateMachine)StateMachine).Chase();
    }
}
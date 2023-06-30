using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack State", menuName = " StateMachine/States/AttackState")]
public class AttackState : State
{
    public override void EnterState(StateMachine StateMachine)
    {
        ((Enemy)StateMachine).SetAttackState(true);
    }

    public override void ExitState(StateMachine StateMachine)
    {
        ((Enemy)StateMachine).SetAttackState(false);
    }

    public override void UpdateState(StateMachine StateMachine)
    {
        ((EnemyStateMachine)StateMachine).Attack();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateMachine : StateMachine
{
    public abstract void Attack();

    public abstract void Chase();
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private float maxHP;

    private float currentHP;

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private UnityEvent OnDeath;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        currentHP = maxHP;
    }

    public void Damage(float damage)
    {
        if (currentHP >= 1)
        {
            currentHP -= damage;
            TweenEffect(1f);
        }
        if (currentHP < 1)
        {
            OnDeath.Invoke();
            this.enabled = false;
        }
    }

    private void TweenEffect(float tweenTimer)
    {
        Action<float> updateValue = (float value) =>
        {
            hpBar.fillAmount = value;
        };
        LTDescr tween = LeanTween.value(hpBar.fillAmount, currentHP / maxHP, tweenTimer);
        tween.setOnUpdate(updateValue);
    }
}
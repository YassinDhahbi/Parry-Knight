using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private ExpMeter expMeter;

    [SerializeField]
    private ParticleSystem levelUpParticleSystem;

    [SerializeField]
    private float delayBeforePanelAppearance;

    [SerializeField]
    private GameObject levelUpCardsHolder;

    private void Awake()
    {
        expMeter.Init();
    }

    public void UpDateLevel(int value)
    {
        expMeter.UpdateMeter(value);
    }

    public void LevelUP()
    {
        StartCoroutine(LevelUpBehaviour());
    }

    private IEnumerator LevelUpBehaviour()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            levelUpParticleSystem.Play();
            yield return new WaitForSeconds(delayBeforePanelAppearance);
            GameManager.Instance.gameIsPaused = true;
            levelUpParticleSystem.Stop();
            levelUpCardsHolder.SetActive(true);
        }
    }
}

[System.Serializable]
public class ExpMeter
{
    [SerializeField]
    private float nextMaxExp;

    [SerializeField]
    private float currentExp;

    [SerializeField]
    private int currentExpMilestoneIndex;

    [SerializeField]
    private List<int> listOfExpMilestones;

    [SerializeField]
    private TextMeshProUGUI levelIndicatorTmp;

    [SerializeField]
    private Image expBar;

    [SerializeField]
    private float tweenEffectTimer = 0.5f;

    public void Init()
    {
        nextMaxExp = listOfExpMilestones[0];
        currentExp = 0;
        expBar.fillAmount = currentExp / nextMaxExp;
    }

    public void UpdateMeter(int additionalExp)
    {
        currentExp += additionalExp;
        TweenEffect(tweenEffectTimer);
        if (currentExp >= nextMaxExp)
        {
            if (currentExpMilestoneIndex < listOfExpMilestones.Count)
            {
                currentExpMilestoneIndex++;
                nextMaxExp = listOfExpMilestones[currentExpMilestoneIndex];
                levelIndicatorTmp.text = "Level " + (currentExpMilestoneIndex + 2);
            }
            EventManager.Instance.OnPlayerLevelUP.Raise();
            currentExp = 0;
            expBar.fillAmount = 0;
        }
    }

    public void TweenEffect(float tweenTimer)
    {
        Action<float> updateValue = (float value) =>
        {
            expBar.fillAmount = value;
        };
        LTDescr tween = LeanTween.value(expBar.fillAmount, currentExp / nextMaxExp, tweenTimer);
        tween.setOnUpdate(updateValue);
    }
}
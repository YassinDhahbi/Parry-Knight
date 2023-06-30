using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    private float currentHp;

    [SerializeField]
    private float maxHp;

    [SerializeField]
    private TextMeshProUGUI hpTMP;

    [SerializeField]
    private float healthAdditionPerPickUp;

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private float hpTweenEffectTimer = 0.5f;

    private void Awake()
    {
        Initialize();
    }

    public void ReduceHp(float dmgValue)
    {
        currentHp -= dmgValue;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    private void Initialize()
    {
        currentHp = maxHp;
        hpTMP.text = currentHp + " / " + maxHp;
        hpBar = hpTMP.transform.parent.GetComponent<Image>();
    }

    public void SetTMP(TextMeshProUGUI tmp)
    {
        hpTMP = tmp;
    }

    public void UpdateHP()
    {
        TweenEffect();
    }

    public void VignetteModifier(Volume globalVolume)
    {
        Vignette vignette;
        globalVolume.profile.TryGet(out vignette);
        if (vignette.intensity.value > 0.25f)
        {
            return;
        }
        var currentValue = vignette.intensity.value + 0.05f;
        vignette.intensity.Override(currentValue);
    }

    public void RestoreVignette(Volume globalVolume)
    {
        Vignette vignette;
        globalVolume.profile.TryGet(out vignette);
        var currentValue = 0f;
        vignette.intensity.Override(currentValue);
    }

    private void TweenEffect()
    {
        Action<float> updateValue = (float value) =>
        {
            hpBar.fillAmount = value;
            hpTMP.text = currentHp + " / " + maxHp;
            if (currentHp <= 1)
            {
                EventManager.Instance.OnPlayerDeath.Raise();
                EventManager.Instance.OnGameLost.Raise();
            }
        };
        LTDescr tween = LeanTween.value(hpBar.fillAmount, currentHp / maxHp, hpTweenEffectTimer);
        tween.setOnUpdate(updateValue);
    }

    public void UpdateMaxHP(float t)
    {
        maxHp += t;
        UpdateHP();
    }

    public void Heal()
    {
        currentHp += healthAdditionPerPickUp;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        UpdateHP();
    }
}
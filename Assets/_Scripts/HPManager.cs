using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
[CreateAssetMenu(menuName = "Managers/ HP Manager")]
public class HPManager : ScriptableObjectSingleton<HPManager>
{
    [SerializeField]
    FloatVariable currentHp;
    [SerializeField]
    FloatVariable maxHp;
    public void ReduceHp(float dmgValue)
    {
        currentHp.Value -= dmgValue;
    }

    public void ImpactUI(UnityEngine.UI.Image hpBar)
    {
        hpBar.fillAmount = currentHp.Value / maxHp.Value;
    }
    public void Initialize()
    {
        currentHp.Value = maxHp.Value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager instance;

    [SerializeField]
    private ParticleSystem hitEffect;

    [SerializeField]
    private ParticleSystem bloodEffect;

    [SerializeField]
    private List<TMPro.TextMeshProUGUI> listOfDmgIndicator;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnHit(Vector2 position)
    {
        hitEffect.transform.position = position;
        hitEffect.Play();
    }

    public void DistributeBlood(Vector2 newPos, Transform newParent)
    {
        bloodEffect.transform.position = newPos;
        bloodEffect.transform.parent = newParent;
        bloodEffect.Play();
    }

    public void SpawnDamageNumber(Vector2 pos, int dmgValue)
    {
        var randomIndex = Random.Range(0, listOfDmgIndicator.Count);
        var currentSelectedIndicator = listOfDmgIndicator[randomIndex];
        currentSelectedIndicator.transform.position = pos;
        currentSelectedIndicator.text = dmgValue.ToString();
        LeanTween.scale(currentSelectedIndicator.gameObject, Vector3.one, 0.1f).setEaseInOutBack().setOnComplete(() => { LeanTween.scale(currentSelectedIndicator.gameObject, Vector3.zero, 0.5f); });
    }
}
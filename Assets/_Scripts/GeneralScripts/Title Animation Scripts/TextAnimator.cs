using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimator : MonoBehaviour
{
    [SerializeField]
    private string text;

    [SerializeField]
    private Transform textHolderObject;

    [SerializeField]
    private TextMeshProUGUI textTMP;

    [SerializeField]
    private float targetScaleMultiplier;

    private List<GameObject> listOfTextLetters = new List<GameObject>();

    private void Awake()
    {
        Spawner();
        AnimationBehaviour();
    }

    private void Spawner()
    {
        for (int i = 0; i < text.Length; i++)
        {
            var newTMP = Instantiate(textTMP, textHolderObject);
            newTMP.text = text[i].ToString();
            newTMP.transform.localScale = Vector3.one * (targetScaleMultiplier - 1);
            listOfTextLetters.Add(newTMP.gameObject);
        }
    }

    private void AnimationBehaviour()
    {
        foreach (GameObject item in listOfTextLetters)
        {
            LeanTween.scale(item, Vector3.one * targetScaleMultiplier, 1).setLoopPingPong();
        }
    }
}
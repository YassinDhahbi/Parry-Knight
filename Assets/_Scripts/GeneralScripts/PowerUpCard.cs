using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionTMP;

    [SerializeField]
    private Image gemSprite;

    [SerializeField]
    private PowerUpCardData powerUpCardData;

    private Button powerUpCardButton;
    private Vector3 initialScale;

    private void OnEnable()
    {
        powerUpCardData.Init(gemSprite, descriptionTMP);
        Animate();
        powerUpCardButton = GetComponent<Button>();
        transform.rotation = Quaternion.identity;
        gemSprite.transform.rotation = Quaternion.identity;
        powerUpCardButton.enabled = true;
    }

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void Animate()
    {
        #region Animate Object Scale

        transform.localScale = initialScale * 0.9f;
        LeanTween.scale(gameObject, initialScale, 2f).setLoopPingPong();

        #endregion Animate Object Scale
    }

    public void OnClickBehaviour()
    {
        powerUpCardData.OnCardClicked();
        powerUpCardButton.enabled = false;

        #region Animation

        LeanTween.cancel(gameObject);
        var randomValue = Random.Range(0, 2);
        var randomRotFloat = randomValue == 0 ? 90 : -90;
        LeanTween.rotateZ(gameObject, randomRotFloat, 1f);
        LeanTween.scale(gameObject, Vector3.zero, 1f).setOnComplete(() =>
        {
            transform.parent.gameObject.SetActive(false);
            GameManager.Instance.gameIsPaused = false;
        });

        #endregion Animation
    }
}
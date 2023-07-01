using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private int timeInMinutes;

    [SerializeField]
    private int timeInSeconds;

    [SerializeField]
    private TMPro.TextMeshProUGUI timerTmp;

    private float currentTimer;

    private void Awake()
    {
        currentTimer = timeInSeconds;
        InvokeRepeating("TimerCounter", 0, 1);
    }

    private void TimerCounter()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            if (timeInSeconds <= 0 && timeInMinutes > 0)
            {
                timeInMinutes--;
                timeInSeconds = 59;
            }
            if (timeInSeconds > 0)
            {
                timeInSeconds -= 1;
            }
            if (timeInSeconds == 0 && timeInMinutes == 0)
            {
                EventManager.Instance.OnGameLost.Raise();
                CancelInvoke("TimerCounter");
                this.enabled = false;
            }
            string secondsCorrector = timeInSeconds > 9 ? "" : "0";
            string minutesCorrector = timeInMinutes > 9 ? "" : "0";
            timerTmp.text = minutesCorrector + timeInMinutes + " : " + secondsCorrector + timeInSeconds;
        }
    }
}
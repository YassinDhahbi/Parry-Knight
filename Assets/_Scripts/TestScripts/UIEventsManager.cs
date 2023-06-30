using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventsManager : MonoBehaviour
{
    public static UIEventsManager instance;

    [SerializeField]
    private List<GameObject> listOfBloodSpatterImages;

    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject losePanel;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Image customCursorImage;

    [SerializeField]
    private TMPro.TextMeshProUGUI numberOfEnemiesTMP;

    private Camera cam;

    private void OnEnable()
    {
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.Escape).performed += ctx => PauseMenuHandler();
    }

    private void OnDisable()
    {
        InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.Escape).performed -= ctx => PauseMenuHandler();
    }

    private void PauseMenuHandler()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
            settingsPanel.SetActive(false);
            GameManager.Instance.gameIsPaused = true;
        }
    }

    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
        cam = Camera.main;
    }

    private void Update()
    {
        var newPos = cam.ScreenToWorldPoint(InputSystem.Instance.GetInputSchemeByID(ControlIdentifier.MousePosition).ReadValue<Vector2>());
        newPos.z = 0;
        customCursorImage.transform.position = newPos;
        customCursorImage.transform.localPosition = new Vector3(customCursorImage.transform.localPosition.x, customCursorImage.transform.localPosition.y, 0);
    }

    [ContextMenu("Test")]
    public void UIEaseInSpawn()
    {
        if (GameManager.Instance.gameIsPaused == false)
        {
            int randomIndex = 0;
            for (int i = 0; i < listOfBloodSpatterImages.Count; i++)
            {
                var image = listOfBloodSpatterImages[i];
                if (image.transform.localScale.magnitude == 0)
                {
                    randomIndex = i;
                }
            }

            LeanTween.scale(listOfBloodSpatterImages[randomIndex], Vector2.one * 0.6f, 1f).setEaseInOutBack();
        }
    }

    public void UIEaseInHide()
    {
        for (int i = 0; i < listOfBloodSpatterImages.Count; i++)
        {
            var image = listOfBloodSpatterImages[i];
            LeanTween.scale(image, Vector2.zero, 1f).setEaseInOutBack();
        }
    }

    public void SetWinPanel(bool state)
    {
        winPanel.SetActive(true);
    }

    public void SetLosePanel(bool state)
    {
        losePanel.SetActive(true);
    }

    public void UpdateNumberOfEnemiesContainer(int n)
    {
        numberOfEnemiesTMP.text = n + " left";
    }
}
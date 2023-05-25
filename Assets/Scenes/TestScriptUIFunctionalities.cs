using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TestScriptUIFunctionalities : MonoBehaviour
{
    public ItemBaseDetails baseDetails;
    public void CheckName(GameObject target)
    {
        var targetScript = target.GetComponent<TestScriptUIFunctionalities>();
        SendData(targetScript);

    }


    public TMPro.TextMeshProUGUI nameTMP;
    public TMPro.TextMeshProUGUI descriptionTMP;

    void SendData(TestScriptUIFunctionalities target)
    {
        target.nameTMP.text = baseDetails.itemName;
        target.descriptionTMP.text = baseDetails.description;
    }
    void ResetData(TestScriptUIFunctionalities target)
    {
        target.nameTMP.text = "";
        target.descriptionTMP.text = "";
    }
    public void ResetData(GameObject target)
    {
        var targetScript = target.GetComponent<TestScriptUIFunctionalities>();
        ResetData(targetScript);
    }

    public void UpdatePose(GameObject target)
    {
        target.transform.position = Input.mousePosition;
    }


}

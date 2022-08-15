using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{ 
    [SerializeField]
    private ScriptableValue<int> TP;
    [SerializeField]
    private ScriptableValue<int> TN;
    [SerializeField]
    private ScriptableValue<int> FP;
    [SerializeField]
    private ScriptableValue<int> FN;

    [SerializeField]
    private TextMeshProUGUI accVal;
    [SerializeField]
    private TextMeshProUGUI precVal;
    [SerializeField]
    private TextMeshProUGUI recVal;
    [SerializeField]
    private TextMeshProUGUI FMesVal;
    [SerializeField]
    private TextMeshProUGUI TPRVal;
    [SerializeField]
    private TextMeshProUGUI TNRVal;

    private void OnEnable()
    {
        var prec = ((float)TP.Value / (TP.Value + FP.Value));
        var rec = ((float)TP.Value / (TP.Value + FN.Value));
        accVal.text = ((TP.Value + TN.Value) / (float)(TP.Value + TN.Value + FP.Value + FN.Value)).ToString();
        precVal.text = prec.ToString();
        recVal.text = rec.ToString();
        FMesVal.text = (2f * (prec * rec) / (prec + rec)).ToString();
        TPRVal.text = ((float)TP.Value / (TP.Value + FN.Value)).ToString();
        TNRVal.text = ((float)TN.Value / (TN.Value + FP.Value)).ToString();
    }
}

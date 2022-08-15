using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntermediateResultsPresenter : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothness;
    [SerializeField]
    private float lowerPointTime;

    [SerializeField]
    private ScriptableValue<int> trueNegative;
    [SerializeField]
    private ScriptableValue<int> truePositive;
    [SerializeField]
    private ScriptableValue<int> falseNegative;
    [SerializeField]
    private ScriptableValue<int> falsePositive;

    [SerializeField]
    private TextMeshProUGUI text;

    private int prevTrueNeg;
    private int prevTruePos;
    private int prevFalseNeg;
    private int prevFalsePos;

    private Vector3 initialPosition;

    private Vector3 targetPosition;

    private WaitForSeconds downWait;

    private Coroutine moveCoroutine;

    public void Capture()
    {
        var dtp = truePositive.Value - prevTruePos;
        var dtn = trueNegative.Value - prevTrueNeg;
        var dfp = falsePositive.Value - prevFalsePos;
        var dfn = falseNegative.Value - prevFalseNeg;
        Save();
        if (dtp == 0 && dtn == 0 && dfn == 0 && dfp == 0)
            return;
        var correct = dtp + dtn;
        var all = dtp + dtn + dfp + dfn;
        text.text = $"{correct}/{all}";
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(DownAndBack());
    }

    private void Save()
    {
        prevFalseNeg = falseNegative.Value;
        prevFalsePos = falsePositive.Value;
        prevTrueNeg = trueNegative.Value;
        prevTruePos = truePositive.Value;
    }

    private IEnumerator DownAndBack()
    {
        targetPosition = target.position;
        yield return downWait;
        targetPosition = initialPosition;
        moveCoroutine = null;
    }

    private void Awake()
    {
        Save();
        initialPosition = transform.position;
        targetPosition = initialPosition;
        downWait = new WaitForSeconds(lowerPointTime);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
    }
}

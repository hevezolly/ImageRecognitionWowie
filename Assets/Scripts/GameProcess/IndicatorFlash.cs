using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorFlash : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private float flashTime;
    [SerializeField]
    private Color flashColor;

    private Color standartColor;

    private Coroutine waitForChangeCoroutine;
    private WaitForSeconds wait;

    private void Awake()
    {
        standartColor = renderer.color;
        wait = new WaitForSeconds(flashTime);
    }

    public void Flash()
    {
        if (waitForChangeCoroutine != null)
            StopCoroutine(waitForChangeCoroutine);
        waitForChangeCoroutine = StartCoroutine(WaitForChange());
    }

    private IEnumerator WaitForChange()
    {
        renderer.color = flashColor;
        yield return wait;
        renderer.color = standartColor;
        waitForChangeCoroutine = null;
    }
}

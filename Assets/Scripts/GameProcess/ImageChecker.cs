using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChecker : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<int> currentNumberOfPictures;
    [SerializeField]
    private ScriptableValue<int> correctCounter;
    [SerializeField]
    private ScriptableValue<int> incorrectCounter;

    [SerializeField]
    private Rect checkRect;

    [SerializeField]
    private ScriptableValue<Requirement> currentRequirement;

    [SerializeField]
    private bool disiredCheckResult;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            CheckPictures();    
    }

    private void CheckPictures()
    {
        var collisions = Physics2D.OverlapBoxAll((Vector2)transform.position + checkRect.center, checkRect.size, 0);
        foreach (var c in collisions)
        {
            if (!c.TryGetComponent<ImageDummy>(out var image))
                continue;
            if (checkRect.Contains(c.transform.position - transform.position))
            {
                RecivePicture(image);
            }
        }
    }

    private void OnCorrectPicture()
    {
        correctCounter.Value += 1;
    }

    private void OnIncorrectPicture()
    {
        incorrectCounter.Value += 1;
    }

    private void RecivePicture(ImageDummy image)
    {
        var checkResult = image.Description.IsFitForRequirement(currentRequirement.Value);
        if (disiredCheckResult == checkResult)
        {
            OnCorrectPicture();
        }
        else
        {
            OnIncorrectPicture();
        }
        Destroy(image.gameObject);
        currentNumberOfPictures.Value -= 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + checkRect.center, checkRect.size);
    }

    private void OnApplicationQuit()
    {
        correctCounter.Value = 0;
        incorrectCounter.Value = 0;
    }
}

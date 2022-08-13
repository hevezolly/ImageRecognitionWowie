using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthOffsetter : MonoBehaviour, ISortingOrderOffset
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private ScriptableValue<float> orderOffsetMultiple;
    
    public void OnSelect()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            orderOffsetMultiple.Value * renderer.sortingOrder * -1);
    }

    public void SetLookupRenderer(SpriteRenderer renderer)
    {
        this.renderer = renderer;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthOffsetter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;
    [SerializeField]
    private ScriptableValue<float> orderOffsetMultiple;
    
    public void OnSelected()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            orderOffsetMultiple.Value * renderer.sortingOrder * -1);
    }
}

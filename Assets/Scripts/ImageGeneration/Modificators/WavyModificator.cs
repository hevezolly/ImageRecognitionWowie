using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="image gen/modifications/wavy")]
public class WavyModificator : ImagePartModification
{
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float frequency;
    [SerializeField]
    private bool useX;
    public override void MakeChangeTo(ImagePart part)
    {
        var amplitudeVec = new Vector2(0, amplitude);
        if (useX)
            amplitudeVec = new Vector2(amplitude, 0);
        part.usedMaterial.SetVector("_WaveAmplitude", amplitudeVec);
        part.usedMaterial.SetFloat("_frequency", frequency);
    }
}

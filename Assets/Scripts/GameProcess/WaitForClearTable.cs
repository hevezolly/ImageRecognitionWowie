using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForClearTable : MonoBehaviour, IGameStage
{
    [SerializeField]
    private ScriptableValue<int> numberOfCars;
    [SerializeField]
    private UnityEvent stageFinishEvent;
    public UnityEvent onStageFinished => stageFinishEvent;

    public void StartStage(GameStageArgs args)
    {
        if (numberOfCars.Value <= 0)
        {
            onStageFinished?.Invoke();
            return;
        }
        numberOfCars.ValueChangeEvent.AddListener(Check);
    }

    private void Check(int count)
    {
        if (count == 0)
        {
            numberOfCars.ValueChangeEvent.RemoveListener(Check);
            onStageFinished?.Invoke();
        }
    }
}

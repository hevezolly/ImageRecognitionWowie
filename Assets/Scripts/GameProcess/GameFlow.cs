using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameFlow : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<Requirement> currentRequirement;

    [SerializeField]
    private ImageGenerator imageGen;

    [SerializeField]
    private RequestFromRequirement requestGenerator;

    [SerializeField]
    private RequirementGenerator requirementGenerator;

    [SerializeField]
    private OpenedBook book;

    [SerializeField]
    private List<IRef<IGameStage>> stages;


    private int currentStageIndex;

    private IGameStage currentStage;

    public GameStageArgs args { get; private set; }

    public UnityEvent StageCyckleEvent;

    private void Start()
    {
        CykleStage();
    }

    private void Awake()
    {
        args = new GameStageArgs()
        {
            requestGenerator = requestGenerator,
            imageGen = imageGen,
            requirementGenerator = requirementGenerator,
            book = book,
        };
    }

    private bool CykleStage()
    {
        StageCyckleEvent?.Invoke();
        if (currentStageIndex == stages.Count)
            return false;
        currentStage = stages[currentStageIndex].I;
        currentStage.onStageFinished.AddListener(OnStageFinished);
        currentStage.StartStage(args);

        currentStageIndex++;
        return true;
    }

    private void OnStageFinished()
    {
        currentStage.onStageFinished.RemoveListener(OnStageFinished);
        CykleStage();
    }



    private void OnApplicationQuit()
    {
        if (currentRequirement == null)
            return;
        currentRequirement.Value = null;
    }
}

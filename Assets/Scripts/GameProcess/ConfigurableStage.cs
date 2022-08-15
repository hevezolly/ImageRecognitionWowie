using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfigurableStage : MonoBehaviour, IGameStage
{
    [SerializeField]
    private UnityEvent stageFinishEvent;

    

    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private DialogueDisplay display;

    [SerializeField]
    private List<Substage> substages;

    private WaitForEndOfFrame waitForFrame;

    public UnityEvent onStageFinished => stageFinishEvent;

    private bool CanContinue;

    private void Awake()
    {
        waitForFrame = new WaitForEndOfFrame();
    }

    public void StartStage(GameStageArgs args)
    {
        StartCoroutine(StageProcess());
    }

    private void OnDialogueFinished()
    {
        display.DialogueFinishedEvent.RemoveListener(OnDialogueFinished);
        CanContinue = true;
    }

    private IEnumerator StageProcess()
    {
        foreach (var substage in substages)
        {
            if (substage.useDialogue)
            {
                dialogue.ShowLine(substage.line);
                if (!substage.ignoreDialogue)
                {
                    CanContinue = false;
                    display.DialogueFinishedEvent.AddListener(OnDialogueFinished);
                    while (!CanContinue)
                    {
                        yield return waitForFrame;
                    }
                }
            }
            if (substage.waitAfter)
                yield return new WaitForSeconds(substage.wait);
            substage.StageActionAfterDialogue?.Invoke();
            if (substage.waitAfterSubstage)
                yield return new WaitForSeconds(substage.substageWait);
        }
        stageFinishEvent?.Invoke();
    }


}

[System.Serializable]
public class Substage
{
    public bool useDialogue;
    [TextArea()]
    public string line;

    public bool ignoreDialogue;

    public bool waitAfter;
    public float wait;

    public UnityEvent StageActionAfterDialogue;

    public bool waitAfterSubstage;

    public float substageWait;
}

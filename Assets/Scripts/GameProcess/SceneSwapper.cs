using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneSwapper : ScriptableObject
{
    [SerializeField]
    private List<ScriptableValue<int>> valuesToReset;
    [SerializeField]
    private ScriptableValue<string> prompt;
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        ResetValues();
    }

    public void GoToLeves()
    {
        SceneManager.LoadScene("Main");
        ResetValues();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ResetValues()
    {
        valuesToReset.ForEach(v => v.Value = 0);
        prompt.Value = "";
    }
}

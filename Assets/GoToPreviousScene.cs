using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static string previousSceneName;

    public void SetPreviousScene()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
    }

    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("Previous scene name is not set.");
        }
    }
}

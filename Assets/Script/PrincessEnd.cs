using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincessEnd : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// 애니메이션에서 실행 시킴.
    /// </summary>
    // Update is called once per frame
    public void EndRestart()
    {
        Time.timeScale = 1.0f;
        Timer t_Timer = FindObjectOfType<Timer>();
        string t_Title = t_Timer.IsHighscore ? "Congratulation!" : "Information";
        string t_Dec = t_Timer.IsHighscore ? $"New Record! \n {t_Timer.TimerTxt}" : t_Timer.TimerTxt;
        MainSystem.Instance.Popup.SetData(t_Title, t_Dec, "OK", () =>
        {
            MainSystem.Instance.Popup.SetClose();
            MainSystem.Instance.SceneLoader.LoadScene("MainMenu");
        });
        MainSystem.Instance.Popup.SetOpen();
        //SceneManager.LoadScene("MainMenu");

    }
}

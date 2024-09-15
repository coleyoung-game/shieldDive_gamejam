using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBttControl : MonoBehaviour
{
    [SerializeField] private string m_StartSceneName;
    [SerializeField] private string m_QuitSceneName;
    [SerializeField] private string m_DragonSceneName;
    public void StartBttClick()
    {
        //SceneManager.LoadScene(m_StartSceneName);
        MainSystem.Instance.SceneLoader.LoadScene(m_StartSceneName);
    }

    public void SettingBttClick()
    {
        SceneManager.LoadScene("GameSettings");
    }

    public void GoToHell()
    {
        MainSystem.Instance.SceneLoader.LoadScene(m_QuitSceneName);
    }
    public void GoHuntDragon()
    {
        MainSystem.Instance.SceneLoader.LoadScene(m_DragonSceneName);
    }

}

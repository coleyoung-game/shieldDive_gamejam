using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBttControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartBttClick()
    {
        SceneManager.LoadScene("Prototype_3");

    }

    public void SettingBttClick()
    {
        SceneManager.LoadScene("GameSettings");
    }
}

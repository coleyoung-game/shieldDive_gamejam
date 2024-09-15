using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image m_Img_Fade;

    private IEnumerator IE_LoadSceneHandle = null;

    void Start()
    {
        LoadScene("MainMenu");
    }

    public void LoadScene(string _Name)
    {
        if(IE_LoadSceneHandle != null)
        {
            StopCoroutine(IE_LoadSceneHandle);
            IE_LoadSceneHandle = null;
        }
        StartCoroutine(IE_LoadSceneHandle = IE_LoadScene(_Name));
    }

    private IEnumerator IE_LoadScene(string _Name)
    {
        yield return IE_FadeEffect(true,1);
        var t_Sceneload = SceneManager.LoadSceneAsync(_Name);
        yield return new WaitUntil(() => t_Sceneload.isDone);
        yield return new WaitForSeconds(0.5f);
        yield return IE_FadeEffect(false, 1);
        Debug.Log($"[{_Name}]LoadScene Completed.");
        yield return null;
    }


    private IEnumerator IE_FadeEffect(bool _IsIn, float _Time)
    {
        float t_CurrTime = 0.0f;
        while(t_CurrTime < _Time)
        {
            t_CurrTime += Time.deltaTime;
            m_Img_Fade.color = new Color(1, 1, 1, _IsIn ? t_CurrTime / _Time : 1 - t_CurrTime / _Time);
            yield return null;
        }
        //m_Img_Fade.color = Color.black;
    }


}

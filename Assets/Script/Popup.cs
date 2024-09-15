using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Popup : MonoBehaviour
{
    [SerializeField] private Text m_Title;
    [SerializeField] private Text m_Desc;
    [SerializeField] private Text m_Check;
    [SerializeField] private Button m_Btn_AfterAct;

    private Action m_After_Act = null;
    private IEnumerator IE_PopupEffectHandle = null;

    public void SetData(string _Title, string _Desc, string _Check, Action _AfterAct = null)
    {
        m_Title.text = _Title;
        m_Desc.text = _Desc;
        m_Check.text = _Check;
        if (_AfterAct != null)
            m_Btn_AfterAct.onClick.AddListener(() => _AfterAct());
        else
            m_Btn_AfterAct.onClick.AddListener(() => SetClose());
        m_Btn_AfterAct.interactable = false;
    }
    public void SetOpen()
    {
        gameObject.SetActive(true);
        if (IE_PopupEffectHandle != null)
            return;
        StartCoroutine(IE_PopupEffectHandle = IE_PopupEffect(true, 1, () => transform.localScale = Vector3.one)) ;
    }
    public void SetClose()
    {
        StartCoroutine(IE_PopupEffectHandle = IE_PopupEffect(false, 1, () =>
        {
            gameObject.SetActive(false);
            transform.localScale = Vector3.one*0.01f; 
        }));
    }

    private IEnumerator IE_PopupEffect(bool _IsOpen, float _Time, Action _AfterAct = null)
    {
        float t_CurrTime = 0.0f;
        float t_Calcu;
        while(t_CurrTime < _Time)
        {
            yield return null;
            t_CurrTime += Time.deltaTime;
            t_Calcu = _IsOpen ? easeOutBack(t_CurrTime / _Time) : 1 - easeOutBack(t_CurrTime / _Time);
            if (t_Calcu < 0) t_Calcu = 0;
            transform.localScale = Vector3.one * t_Calcu;
        }
        //transform.localScale = Vector3.one;
        m_Btn_AfterAct.interactable = true;
        if(_AfterAct != null)
            _AfterAct();
        IE_PopupEffectHandle = null;
    }
    float easeInOutBack(float _Number)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
        return _Number < 0.5
          ? (Mathf.Pow(2 * _Number, 2) * ((c2 + 1) * 2 * _Number - c2)) / 2
          : (Mathf.Pow(2 * _Number - 2, 2) * ((c2 + 1) * (_Number * 2 - 2) + c2) + 2) / 2;
    }

    float easeOutBack(float _Number)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return (float)(1f + c3 * Mathf.Pow(_Number - 1f, 3f) + c1 * Math.Pow(_Number - 1f, 2f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private IEnumerator IE_Vibration(GameObject _Obj, float _Time, float _Power)
    {
        float t_CurrTime = 0.0f;
        Vector3 t_OriginPos = _Obj.transform.position;
        while (t_CurrTime < _Time)
        {
            yield return null;
            t_OriginPos = _Obj.transform.position;
        }
    }

}

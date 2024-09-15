using UnityEngine;

namespace Chan
{
    public class ShakeButtonController : MonoBehaviour
    {
        public void OnShakeButtonClick()
        {
            // 현재 상태를 반전시킵니다
            bool isShakeEnabled = PlayerPrefs.GetInt("CameraShakeEnabled", 1) == 1;
            PlayerPrefs.SetInt("CameraShakeEnabled", isShakeEnabled ? 0 : 1);
            PlayerPrefs.Save(); // 변경된 값을 즉시 저장합니다
        }
    }
}

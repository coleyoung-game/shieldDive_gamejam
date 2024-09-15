using UnityEngine;
using UnityEngine.UI;

namespace Chan // 네임스페이스 이름을 프로젝트에 맞게 변경할 수 있습니다.
{
    public class ToggleButtonController : MonoBehaviour
    {
        public Sprite onSprite;   // 첫 번째로 설정할 이미지
        public Sprite offSprite;  // 두 번째로 설정할 이미지
        private bool isOn = false;
        private Image buttonImage;

        private void Start()
        {
            buttonImage = GetComponent<Image>();
            UpdateButtonImage(); // 초기 이미지 설정
        }

        public void OnButtonClick()
        {
            isOn = !isOn; // 상태를 반전시킵니다 (True <-> False)
            UpdateButtonImage();

            // 상태를 PlayerPrefs에 저장하여 진동 상태를 관리
            PlayerPrefs.SetInt("CameraShakeEnabled", isOn ? 1 : 0);
            PlayerPrefs.Save(); // 변경된 값을 즉시 저장합니다
        }
        private void UpdateButtonImage()
        {
            // 버튼을 클릭할 때마다 이미지를 무조건 변경
            if (isOn)
            {
                buttonImage.sprite = onSprite;
            }
            else
            {
                buttonImage.sprite = offSprite;
            }
        }
    }
}

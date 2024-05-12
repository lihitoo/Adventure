using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform[] characterTransforms;
    public GameObject[] characters; // Danh sách các nhân vật
    private int currentCharacterIndex = 0; // Chỉ số của nhân vật hiện tại
    private void Awake()
    {
        SwitchCharacter(currentCharacterIndex);
    }
    void Update()
    {
        // Kiểm tra nếu người dùng bấm nút để chuyển đổi nhân vật (ví dụ: nút Q hoặc Tab)
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Tab))
        {
            int temp = currentCharacterIndex;
            // Tăng chỉ số nhân vật hiện tại
            currentCharacterIndex++;

            // Nếu chỉ số vượt quá số lượng nhân vật, quay lại nhân vật đầu tiên
            if (currentCharacterIndex >= characters.Length)
            {
                currentCharacterIndex = 0;
            }

            // Chuyển đổi hiển thị giữa các nhân vật
            characters[currentCharacterIndex].transform.position = characters[temp].transform.position;

            SwitchCharacter(currentCharacterIndex);
        }
    }

    // Hàm để chuyển đổi giữa các nhân vật
    private void SwitchCharacter(int index)
    {
        // Ẩn tất cả các nhân vật

        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        // Hiển thị nhân vật mới
        characters[currentCharacterIndex].SetActive(true);
        virtualCamera.transform.position = characterTransforms[currentCharacterIndex].position;
        virtualCamera.Follow = characterTransforms[currentCharacterIndex];
    }
}
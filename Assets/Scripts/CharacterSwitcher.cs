using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public GameObject[] characters; // Danh sách các nhân vật
    bool[] isDead = { false };
    public Transform[] characterTransforms;
    private int currentCharacterIndex = 0; // Chỉ số của nhân vật hiện tại
    Damageable damageable;
    public delegate void OnCharacterSwitch(GameObject newCharacter);
    public event OnCharacterSwitch CharacterSwitched;

    private void Awake()
    {
        SwitchCharacter(currentCharacterIndex);
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        damageable = characters[currentCharacterIndex].GetComponent<Damageable>();
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
        if (!damageable.IsAlive)
        {

            currentCharacterIndex++;
            if (currentCharacterIndex >= characters.Length)
            {
                currentCharacterIndex = 0;
            }
            //SwitchCharacter(currentCharacterIndex);
            Invoke("tempSwitchCharacter", 2f);
        }
    }
    void tempSwitchCharacter()
    {
        SwitchCharacter(currentCharacterIndex);

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
        characters[index].SetActive(true);
        virtualCamera.transform.position = characterTransforms[index].position;
        virtualCamera.Follow = characterTransforms[index];

        // Notify listeners about the character switch
        CharacterSwitched?.Invoke(characters[index]);
    }

    public GameObject isChosen()
    {
        return characters[currentCharacterIndex];
    }
}

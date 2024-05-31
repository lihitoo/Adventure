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
    private int currentCharacterIndex = 0;
    private int temp; // Chỉ số của nhân vật hiện tại
    Damageable damageable;

    public delegate void OnCharacterSwitch(GameObject newCharacter);

    public event OnCharacterSwitch CharacterSwitched;
    public int charactersCount;
    public static CharacterSwitcher Instance { get; private set; }
    Transform tempTransform;

    private void Awake()
    {
        tempTransform = new GameObject("TempTransform").transform;
        tempTransform.transform.position = characters[0].transform.position;
        SwitchCharacter(0);
        damageable = GetComponent<Damageable>();
        charactersCount = characters.Length;
        Instance = this;
    }

    void Update()
    {
        if (charactersCount != 0)
        {
            damageable = characters[currentCharacterIndex].GetComponent<Damageable>();
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Tab))
            {
                tempTransform.transform.position = characters[currentCharacterIndex].transform.position;
                currentCharacterIndex++;
                if (currentCharacterIndex >= characters.Length)
                {
                    for (int i = 0; i < characters.Length; i++)
                    {
                        damageable = characters[i].GetComponent<Damageable>();
                        if (damageable.IsAlive)
                        {
                            currentCharacterIndex = i;
                            break;
                        }
                    }
                }

                //characters[currentCharacterIndex].transform.position = tempTransform.transform.position;
                SwitchCharacter(currentCharacterIndex);
            }

            if (!damageable.IsAlive)
            {
                if (charactersCount > 0)
                {
                    tempTransform.transform.position = characters[currentCharacterIndex].transform.position;
                    charactersCount--;
                    currentCharacterIndex++;
                    if (currentCharacterIndex >= characters.Length)
                    {
                        for (int i = 0; i < characters.Length; i++)
                        {
                            damageable = characters[i].GetComponent<Damageable>();
                            if (damageable.IsAlive)
                            {
                                currentCharacterIndex = i;
                            }
                        }
                    }

                    //SwitchCharacter(currentCharacterIndex);
                    Invoke("tempSwitchCharacter", 2f);
                }
            }
        }
    }

    void tempSwitchCharacter()
    {
        SwitchCharacter(currentCharacterIndex);
    }

    private void SwitchCharacter(int index)
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[index].SetActive(true);
        characters[index].transform.position = tempTransform.transform.position;
        virtualCamera.transform.position = characters[index].transform.position;
        virtualCamera.Follow = characterTransforms[index];

        // Notify listeners about the character switch
        CharacterSwitched?.Invoke(characters[index]);
    }

    public GameObject isChosen()
    {
        return characters[currentCharacterIndex];
    }
}
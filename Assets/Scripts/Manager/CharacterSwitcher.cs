﻿using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public GameObject[] characters;
    bool[] isDead = new bool[105];
    public Transform[] characterTransforms;
    private int currentCharacterIndex = 0;
    private int temp;
    Damageable damageable;

    public delegate void OnCharacterSwitch(GameObject newCharacter);

    public event OnCharacterSwitch CharacterSwitched;
    public int charactersCount;
    public static CharacterSwitcher Instance { get; private set; }
    Transform tempTransform;

    private void Awake()
    {
        tempTransform = new GameObject("TempTransform").transform;
        tempTransform.transform.position = characters[currentCharacterIndex].transform.position;
        SwitchCharacter(currentCharacterIndex);
        damageable = characters[currentCharacterIndex].GetComponent<Damageable>();
        charactersCount = characters.Length;
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i <= 100; i++) isDead[i] = false;
    }

    void Update()
    {
        if (charactersCount != 0)
        {
            damageable = characters[currentCharacterIndex].GetComponent<Damageable>();

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Tab))
            {
                tempTransform.position = characters[currentCharacterIndex].transform.position;
                currentCharacterIndex++;
                if (currentCharacterIndex >= characters.Length || isDead[currentCharacterIndex])
                {
                    for (int i = 0; i < characters.Length; i++)
                    {
                        if (!isDead[i])
                        {
                            currentCharacterIndex = i;
                            break;
                        }
                    }
                }

                characters[currentCharacterIndex].transform.position = tempTransform.position;
                SwitchCharacter(currentCharacterIndex);
            }

            if (!damageable.IsAlive)
            {
                isDead[currentCharacterIndex] = true;
                charactersCount--;
                tempTransform.position = characters[currentCharacterIndex].transform.position;
                for (int i = 0; i < characters.Length; i++)
                {
                    if (!isDead[i])
                    {
                        currentCharacterIndex = i;
                        break;
                    }
                }

                characters[currentCharacterIndex].transform.position = tempTransform.position;
                Invoke("tempSwitchCharacter", 2f);
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


        virtualCamera.transform.position = characters[index].transform.position;
        virtualCamera.Follow = characterTransforms[index];

        CharacterSwitched?.Invoke(characters[index]);
    }

    public GameObject isChosen()
    {
        return characters[currentCharacterIndex];
    }
}
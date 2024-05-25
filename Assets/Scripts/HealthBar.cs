using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    CharacterSwitcher characterSwitcher;
    public Slider healthSlider;
    public TMP_Text healthText;
    Damageable playerDamageable;

    private void Awake()
    {
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();
        if (characterSwitcher != null)
        {
            characterSwitcher.CharacterSwitched += OnCharacterSwitched;
            UpdatePlayerReference(characterSwitcher.isChosen());
        }
    }

    private void UpdatePlayerReference(GameObject player)
    {
        if (playerDamageable != null)
        {
            playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
        }

        playerDamageable = player.GetComponent<Damageable>();

        if (playerDamageable != null)
        {
            playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
            UpdateHealthUI(playerDamageable.Health, playerDamageable.MaxHealth);
        }
    }

    private void OnCharacterSwitched(GameObject newCharacter)
    {
        UpdatePlayerReference(newCharacter);
    }

    private void OnEnable()
    {
        if (playerDamageable != null)
        {
            playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
        }
    }

    private void OnDisable()
    {
        if (playerDamageable != null)
        {
            playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
        }
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        UpdateHealthUI(newHealth, maxHealth);
    }

    private void UpdateHealthUI(int health, int maxHealth)
    {
        healthSlider.value = (float)health / maxHealth;
        healthText.text = $"HP: {health} / {maxHealth}";
    }
}

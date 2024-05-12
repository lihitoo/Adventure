using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider;
    public TMP_Text healthText;
    Damageable playerDamageable;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }
    void Start()
    {

        healthSlider.value = (float)playerDamageable.Health / (float)playerDamageable.MaxHealth;
        healthText.text = "HP: " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }
    private void OnEnable()
    {
        if (playerDamageable != null)
            playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        if (playerDamageable != null)
            playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = (float) newHealth / (float) maxHealth;
        healthText.text = "HP: " + newHealth + " / " + maxHealth;
    }

}
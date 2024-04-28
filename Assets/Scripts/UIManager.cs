 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas canvasText;
    private void Awake()
    {
        canvasText = FindObjectOfType<Canvas>();
    }
    void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }
    void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }
    public void CharacterTookDamage(GameObject character,int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmptext = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, canvasText.transform).GetComponent<TMP_Text>();
        tmptext.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthHealed)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmptext = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, canvasText.transform)
            .GetComponent<TMP_Text>();
        tmptext.text = healthHealed.ToString();
    }
}

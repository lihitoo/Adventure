using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance { get; private set; }
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas canvasText;
    public GameObject gameOver;

    private void Awake()
    {
        canvasText = FindObjectOfType<Canvas>();
        Instance = this;
    }

    private void Update()
    {
        if (CharacterSwitcher.Instance.charactersCount == 0)
        {
            Invoke("PauseGame", 1.5f);
        }
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

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmptext = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, canvasText.transform)
            .GetComponent<TMP_Text>();
        tmptext.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthHealed)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmptext = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, canvasText.transform)
            .GetComponent<TMP_Text>();
        tmptext.text = healthHealed.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log("ESC");
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            SceneManager.LoadScene("QuitScene");
#endif
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
}
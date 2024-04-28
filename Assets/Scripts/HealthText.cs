using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    RectTransform textTransform;
    public TextMeshProUGUI textMeshPro;
    public float fadeTime = 1f;
    private float timeElapsed = 0f;
    private Color startColor;
    void Start()
    {
        
    }
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        startColor = textMeshPro.color;
    }
    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        float fadeAlpha = startColor.a * (1 - (timeElapsed / fadeTime));
        if (timeElapsed < fadeTime)
        {
            
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            timeElapsed = 0;
            Destroy(gameObject);
        }
    }
}

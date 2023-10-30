using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("UIManager is not NULL");
        Instance = this;

        _text = transform.Find("Message").GetComponent<TextMeshProUGUI>();
    }

    public void TextOut(string content, float time)
    {
        _text.alpha = 0.2f;
        _text.text = content;

        StartCoroutine(AlphaDown(time));
    }

    private IEnumerator AlphaDown(float time)
    {
        yield return new WaitForSeconds(time);

        _text.DOFade(0, time);
    }
}

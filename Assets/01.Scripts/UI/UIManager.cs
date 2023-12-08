using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : SingleTon<UIManager>
{
    private TextMeshProUGUI _messgaeText;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _gameOverText;

    private void Awake()
    {
        _messgaeText = transform.Find("Message").GetComponent<TextMeshProUGUI>();
        _scoreText = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        _gameOverText = transform.Find("GameOver").GetComponent<TextMeshProUGUI>();

        GameManager.Instance.OnGameOverEvent += GameOverTextOut;
    }

    public void MessageTextOut(string content, float time)
    {
        _messgaeText.alpha = 0.2f;
        _messgaeText.text = content;

        StartCoroutine(AlphaDown(time));
    }

    public void ScoreTextOut(int content)
    {
        _scoreText.alpha = 1f;
        _scoreText.text = $"{content}";
    }

    public void GameOverTextOut()
    {
        StopAllCoroutines();
        _gameOverText.alpha = 0f;
        _gameOverText.DOFade(1f, 1f);
    }

    private IEnumerator AlphaDown(float time)
    {
        yield return new WaitForSeconds(time);

        _messgaeText.DOFade(0, time);
    }

    private IEnumerator AlphaUp(float time)
    {
        yield return new WaitForSeconds(time);

        _messgaeText.DOFade(1, time);
    }
}

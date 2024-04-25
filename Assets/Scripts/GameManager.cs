using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreTextIngame;
    [SerializeField] TMP_Text restartTimeText;
    [SerializeField] int secondsToRestart;

    void Awake() => Instance = this;

    public void End(int score)
    {
        scoreText.text = $"SCORE{Environment.NewLine}{score}m";
        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        int counter = secondsToRestart;
        while (counter > 0)
        {
            restartTimeText.text = $"Time to restart:{Environment.NewLine}{counter}";
            yield return new WaitForSeconds(1);
            counter--;
        }
        SceneManager.LoadScene(0);
    }

    public void setScore(int score)
    {
        scoreTextIngame.text  = $"SCORE{Environment.NewLine}{score}";
    }

    public void CheckpointReached(int score, float seconds)
    {
        StartCoroutine(LowerScore(score, seconds));
    }

    IEnumerator LowerScore(int score, float seconds)
    {
        float timePerScore = seconds / score;
        while (score > 0)
        {
            yield return new WaitForSeconds(timePerScore);
            score--;
            setScore(score);
        }
    }
}

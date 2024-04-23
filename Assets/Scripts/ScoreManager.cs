using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int scorePerScaleUnit;
    [SerializeField] float scoreBurnedPerSecond;
    int score = 1;

    void Start()
    {
        if (scoreBurnedPerSecond <= 0f) scoreBurnedPerSecond = 1f;
    }

    public int GetScore() => score;

    public float GetRunningSeconds() => score / scoreBurnedPerSecond;

    void SetScore(int value)
    {
        score = Mathf.Clamp(value, 1, int.MaxValue);
        ScalePlayer();
    }

    void ScalePlayer() // CHECK SCALING -> CHANGE COLOR INSTEAD?
    {
        //float scale = 1 + (score / scorePerScaleUnit);
        //transform.localScale = Vector3.one * scale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = other.gameObject.GetComponent<Obstacle>();
            int newScore = obstacle.GetResult(score);
            SetScore(newScore);
        }
    }
}

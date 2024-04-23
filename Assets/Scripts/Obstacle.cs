using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Operator operatorType;
    [SerializeField] int value;
    [SerializeField] bool randomValue;
    [SerializeField] int randomMinValue;
    [SerializeField] int randomMaxValue;

    void Start()
    {
        if (randomValue) value = Random.Range(randomMinValue, randomMaxValue);
        GetComponentInChildren<TMP_Text>().text = GetOperationString();
    }

    string GetOperationString()
    {
        return operatorType switch
        {
            Operator.ADD => $"+{value}",
            Operator.SUBTRACT => $"-{value}",
            Operator.MULTIPLY => $"x{value}",
            Operator.DIVIDE => $"÷{value}",
            _ => $"{value}",
        };
    }

    public int GetResult(int startValue)
    {
        if (value == 0) return startValue;
        return operatorType switch
        {
            Operator.ADD => startValue + value,
            Operator.SUBTRACT => startValue - value,
            Operator.MULTIPLY => startValue * value,
            Operator.DIVIDE => startValue / value,
            _ => startValue,
        };
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
}

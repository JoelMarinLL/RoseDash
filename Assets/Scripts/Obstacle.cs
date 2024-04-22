using System.Collections;
using System.Collections.Generic;
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
}

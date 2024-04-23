using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.01f;
    [SerializeField] InputActionAsset _moveAction;
    InputAction _moveMap;
    float movement;
    bool hasEnded = false;
    Rigidbody _rb;

    void Awake()
    {
        _moveMap = _moveAction.FindActionMap("PlayerMovement").FindAction("HorizontalMovement");
        _rb = GetComponent<Rigidbody>();
    }

    void Update() // ¡CAN MOVE PAST WALLS!
    {
        getInput();
        if (!hasEnded) _rb.MovePosition(new Vector3(_rb.position.x + movement * _speed, _rb.position.y, _rb.position.z + _speed));
    }

    void getInput()
    {
        movement = _moveMap.ReadValue<float>();
    }
    public void ArriveCheckPoint()
    {
        DisableInputMovement();
        StartCoroutine(FinalRun());
    }

    IEnumerator FinalRun()
    {

        yield return new WaitForSecondsRealtime(GetComponent<ScoreManager>().GetRunningSeconds());
        hasEnded = true;
        int score = Mathf.RoundToInt(transform.position.z) - MapGenerator.Instance.CheckpointZPosition;
        GameManager.Instance.End(score);
    }

    private void DisableInputMovement()
    {
        _moveMap.Disable();
    }

    private void OnEnable()
    {
        _moveMap.Enable();
    }
    private void OnDisable()
    {
        _moveMap.Disable();
    }
}

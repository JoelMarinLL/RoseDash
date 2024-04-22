using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] InputActionAsset _moveAction;
    InputAction _moveMap;
    float movement;
    Rigidbody _rb;

    void Awake()
    {
        _moveMap = _moveAction.FindActionMap("PlayerMovement").FindAction("HorizontalMovement");
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        _rb.MovePosition(new Vector3(_rb.position.x + movement * _speed, _rb.position.y, _rb.position.z + _speed));
        //transform.Translate(movement * _speed, 0, 0);
        Debug.Log(movement);
    }

    void getInput()
    {
        movement = _moveMap.ReadValue<float>();
    }

    public void DisableInputMovement()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    private int _maxSpeed = 10; 

    private float _accelerationMultiplier = 1;

    public InputAction DriveAction;
    public InputAction AccelerationAction;

    [SerializeField]private Vector2 _driveValues;

    private void Update()
    {
        //Debug.Log(accelerationMultiplier);
        if (AccelerationAction.ReadValue<float>() == 1)
        {
            if (_accelerationMultiplier != 2f)
            {
                _accelerationMultiplier += 0.003f;
            }

            if (_accelerationMultiplier >= 2)
            {
                _accelerationMultiplier = 2f;
            }
        }
        else if (AccelerationAction.ReadValue<float>() == 0)
        {
            if (_accelerationMultiplier != 1)
            {
                _accelerationMultiplier -= 0.05f;
            }

            if (_accelerationMultiplier <= 1)
            {
                _accelerationMultiplier = 1;
            }
        }

        _driveValues = DriveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _maxSpeed * Time.deltaTime * _driveValues.y * _accelerationMultiplier);
        transform.Translate(Vector3.right * _maxSpeed * Time.deltaTime * _driveValues.x * _accelerationMultiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boost")
        {
            //make the car speed faster for a set amount of time (if a new one is hit just reset the boost time not culmative speed)
            _maxSpeed += 3;
            Invoke("ResetSpeed", 3);
            Destroy(other.gameObject);
        }

        if (other.tag == "FinishLine")
        {
            GameEvents.GameFinish.Invoke();
        }
    }

    private void ResetSpeed()
    {
        _maxSpeed = 10;
    }

    private void OnEnable()
    {
        DriveAction.Enable();
        AccelerationAction.Enable();
    }

    private void OnDisable()
    {
        DriveAction.Disable();
        AccelerationAction.Disable();
    }
}

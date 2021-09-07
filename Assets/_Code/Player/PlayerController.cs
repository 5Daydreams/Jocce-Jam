using System;
using System.Collections;
using System.Collections.Generic;
using _Code;
using UnityEngine;

[RequireComponent(typeof(ProjectileLauncher))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private string fireKeyboard;
    [SerializeField] private float speed = 2.0f;
    private Vector3 _movementDirecion;
    private Vector3 _firingDirection;
    private ProjectileLauncher weapon;
    private bool triedShoot;

    private void Awake()
    {
        weapon = this.GetComponent<ProjectileLauncher>();
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    void Update()
    {
        ReadKeyboardInput();
        ReadMouseInput();

        ProcessMovement();
        Fire(triedShoot);

        triedShoot = false;
    }

    private void Fire(bool value)
    {
        weapon.SetTriggerHeld(value);
        weapon.SetShootDirection(_firingDirection);
    }

    private void ProcessMovement()
    {
        this.transform.position += _movementDirecion * speed * Time.deltaTime;

        float angle = Mathf.Atan2(_movementDirecion.y, _movementDirecion.x) * Mathf.Rad2Deg;

        if (triedShoot)
        {
            angle = Mathf.Atan2(_firingDirection.y, _firingDirection.x) * Mathf.Rad2Deg;
        }

        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10 * Time.deltaTime);
    }

    private void ReadKeyboardInput()
    {
        _movementDirecion.x = Input.GetAxisRaw("Horizontal");
        _movementDirecion.y = Input.GetAxisRaw("Vertical");

        if (_movementDirecion.sqrMagnitude > 0.1)
        {
            _firingDirection = _movementDirecion;
        }

        if (Input.GetKey(fireKeyboard))
        {
            triedShoot = true;
        }
    }

    private void ReadMouseInput()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // mouse.z must be 0, otherwise objects move in Z

        Vector3 cachedDirection = (mousePos - this.transform.position).normalized;
        
        _firingDirection = cachedDirection;

        if (Input.GetMouseButton(0))
        {
            triedShoot = true;
        }
    }
}
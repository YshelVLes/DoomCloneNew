using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PLayerController : BaseCharacterController
{
    [SerializeField] float sensitivity = 10;

    protected override void Awake()
    {
        base.Awake();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Rotate(Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime);
        MoveLocal(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PLayerController : MonoBehaviour
{
    [SerializeField] float forwardForse= 1;
    //[SerializeField] float sideForse = 1; 

    [SerializeField] float speed = 1;
    //�� ������� �������� � ������� �������������� 
    [SerializeField] float sensitivity = 10;

    CharacterController characterController;

    [SerializeField] GameObject cam;

    Vector3 surfaceNormal;

    float verticalSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    //����� ������ ��� ������, ������� ����� ����������� � ������ ������� ������ 

    void Update()
    {
        //GetAxisRaw - ������������ ����� ��������, GetAxis - c� ����������
        
        float mouseX = Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        //������������ ����� �������, ������������ � ��������� moveDirection
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal);

        // ���� ����� � ���������� ����������� � ��������� � ���� 
        //��������� ��� ��������� ������ 
      

        Debug.DrawLine(transform.position, transform.position + moveDirection * 2, Color.blue);

        //�������� ������ ���������� ���
        transform.Rotate(new Vector3(0, mouseX * sensitivity * Time.deltaTime, 0)); 

        

        if(characterController.isGrounded)
        {
            verticalSpeed = 0;
        }
        else
        {
            //9.8f - �������������� ���������� 
            verticalSpeed -= 9.8f * Time.deltaTime;
        }

        //Move �� ������� ����������, SimpleMove �������
        characterController.Move((moveDirection * speed + Vector3.up * verticalSpeed) * Time.deltaTime);

        

        //�������� ��� ���� �� ��������� 
        //if(characterController.SimpleMove(moveDirection))
        //{
        //   if( Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2))
        //    {
        //        //������ ������� ��, ��� � ��� ������ + �������� ������ ������� ��������� 
        //        transform.position = hit.point + transform.up * characterController.height / 2;
        //    }
        //}


        //float mouseX = Input.GetAxis("Mouse X");
        //transform.Rotate(new Vector3(0, mouseX * Time.deltaTime, 0));
    }

    //��� �� Update, �� ����������� ����� ������������� ���-�� ������� 
    //������ �������� ��� ��������
    //�������� � ������� ����� 

    //private void FixedUpdate()
    //{

    //    float mouseX = Input.GetAxis("Mouse X");
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    Vector3 forse = Vector3.zero;

    //   forse += transform.forward * vertical * Time.fixedDeltaTime * sideForse;
    //   forse += transform.right * horizontal * Time.fixedDeltaTime * sideForse;

    //    //deltaTime - ������� ���������� �������, fixedDeltaTime - ���������� ��������

    //    float rotation = mouseX * sensitivity * Time.deltaTime;

    //    playerRigidbody.AddForce(forse);

    //    transform.Rotate(new Vector3(0,rotation, 0));
    //}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //hit.normal - ��������������� ������ 

        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);

        surfaceNormal = hit.normal;

        //Debug.DrawLine(transform.position, transform.position +  )

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PLayerController : MonoBehaviour
{
 
    //[SerializeField] float sideForse = 1; 

    [SerializeField] float speed = 10;
    //на сколько градусов в секунду поворачиваемся 
    [SerializeField] float sensitivity = 10;

    CharacterController characterController;

    [SerializeField] GameObject cam;

    Vector3 surfaceNormal;

    //для гравитации 
    float verticalSpeed;

    public float VerticalSpeed
    {
       get => verticalSpeed;
        set { verticalSpeed = value; }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    //Менее точный для физики, которая будет обновляться в другой частоте кадров 

    void Update()
    {

        //float mouseX = Input.GetAxis("Mouse X");
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X")) * sensitivity * Time.deltaTime;

        transform.Rotate(rotation);

        if(characterController.isGrounded)
        {
            verticalSpeed = -0.1f;
        }
        else
        {
          
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);

        Vector3 velocity = transform.TransformDirection(input);
     

        //quaternipn - математика поворота 
        Quaternion slopeRotation =  Quaternion.FromToRotation(Vector3.up, surfaceNormal);
        Vector3 adjustedVelocity= slopeRotation * velocity;

        // if = ?, условия до и после : 
        // -0 - это двигаемся мы вниз или вверх 
        velocity = adjustedVelocity.y < 0 ? adjustedVelocity : velocity;
        velocity.y += verticalSpeed;

        characterController.Move(velocity * speed * Time.deltaTime);





        //GetAxisRaw - моментальная смена значений, GetAxis - cо смягчением
        
      

        //Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        ////ограничиваем длину вектора, приравниваем к структуре moveDirection
        //moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
        //moveDirection = transform.TransformDirection(moveDirection);
        //moveDirection = Vector3.ProjectOnPlane(moveDirection, surfaceNormal);

        // даем знать о глобальных координатах и переводим в свои 
        //погуглить про трансформ вектор 
      

        //Debug.DrawLine(transform.position, transform.position + moveDirection * 2, Color.blue);

        ////кручение вокруг конкретной оси
        //transform.Rotate(new Vector3(0, mouseX * sensitivity * Time.deltaTime, 0)); 

        

        //if(characterController.isGrounded)
        //{
        //    verticalSpeed = 0;
        //}
        //else
        //{
        //    //9.8f - гравитационная постоянная 
        //    verticalSpeed -= 9.8f * Time.deltaTime;
        //}

        //Move не считает гравитацию, SimpleMove считает
        //characterController.Move((moveDirection * speed + Vector3.up * verticalSpeed) * Time.deltaTime);

        

        //проводим луч вниз от персонажа 
        //if(characterController.SimpleMove(moveDirection))
        //{
        //   if( Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2))
        //    {
        //        //делаем центром то, что с чем рейкас + половина высоты капсулы персонажа 
        //        transform.position = hit.point + transform.up * characterController.height / 2;
        //    }
        //}


        //float mouseX = Input.GetAxis("Mouse X");
        //transform.Rotate(new Vector3(0, mouseX * Time.deltaTime, 0));
    }

    //тот же Update, но обновляется через фиксированное кол-во времени 
    //Хорошо подходит для консолей
    //работаем с физикой здесь 

    //private void FixedUpdate()
    //{

    //    float mouseX = Input.GetAxis("Mouse X");
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    Vector3 forse = Vector3.zero;

    //   forse += transform.forward * vertical * Time.fixedDeltaTime * sideForse;
    //   forse += transform.right * horizontal * Time.fixedDeltaTime * sideForse;

    //    //deltaTime - частота обновления объекта, fixedDeltaTime - постоянная величина

    //    float rotation = mouseX * sensitivity * Time.deltaTime;

    //    playerRigidbody.AddForce(forse);

    //    transform.Rotate(new Vector3(0,rotation, 0));
    //}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //hit.normal - нормализованный вектор 

        Debug.DrawLine(hit.point, hit.point + hit.normal * 10, Color.red);

        surfaceNormal = hit.normal;

        //Debug.DrawLine(transform.position, transform.position +  )

    }
}

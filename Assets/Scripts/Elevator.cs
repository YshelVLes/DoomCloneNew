using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private float verticalSpeed;

    [SerializeField] float speed;
   
    [Header("ElevatorPoints")]
    [SerializeField] Transform upPoint;
    [SerializeField] Transform downPoint;
    [SerializeField] Transform target;

    [SerializeField] private bool up = false;

    public float LerpSpeed = 0.5f;


    private void OnCharacterEnter(BaseCharacterController controller)
    {
        if (controller != null)
        {
            controller.gameObject.transform.SetParent(this.gameObject.transform);
            StartCoroutine(nameof(MoveElevator));
        }

    }

    private void OnCharacterStay(BaseCharacterController controller)
    {
       
    }

    private void OnCharacterExit(BaseCharacterController controller)
    {
        if(Vector3.Distance(transform.position, target.position) > 0.01f)
        {
           StopCoroutine(nameof(MoveElevator));
        }
           controller.gameObject.transform.parent = null;
    }

    IEnumerator MoveElevator()
    {

        if(!up)
        {
            target = upPoint;
        }
        else if(up)
        {
            target = downPoint;
        }

        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime * LerpSpeed);
            up = !up;
            yield return up; 
        }

    }
}

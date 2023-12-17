using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToObject : MonoBehaviour
{
   // [SerializeField] GameObject Player;
    private GameObject overlapingActor = null;
    //�������� ��� �����
    public bool ableMove;

    private float verticalSpeed;

    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform[] points;
    [SerializeField] float lerpnumber;


    int i; //
    bool reverse; //����������� ��� ����������

    private void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;
    }

    private void Update()
    {
        //�������� ���� ������ ����������
        if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
        {
            //�������������
            ableMove = false;

            if (i == points.Length - 1) //��������� ���� ��� ��������� �����
            {
                //�������� � �������� �������
                reverse = true;
                i--;
                return;
            }
            else if (i == 0)
            {
                reverse = false;
                i++;
                return;
            }
        }

        if (ableMove)
        {//������� ��������� � �����
            transform.position = Vector3.Lerp(transform.position, points[i].position, speed * Time.deltaTime * lerpnumber);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PLayerController>() != null)
        {
            Debug.Log("player");
            overlapingActor = other.gameObject;
            overlapingActor.transform.parent = transform;

            overlapingActor.GetComponent<PLayerController>().VerticalSpeed = -100f;
            
           
            verticalSpeed = overlapingActor.GetComponent<PLayerController>().VerticalSpeed;
            ableMove = true;

        }

    }

    private void OnTriggerStay(Collider other)
    {
        overlapingActor.GetComponent<PLayerController>().VerticalSpeed = -100f;
        Debug.Log(overlapingActor.GetComponent<PLayerController>().VerticalSpeed);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("vishel");
        if(other.gameObject == overlapingActor && overlapingActor != null)
        {
            ableMove = false;
            overlapingActor.GetComponent<PLayerController>().VerticalSpeed = verticalSpeed;
           
            overlapingActor.transform.parent = null;
            overlapingActor = null;
        }
    }
}

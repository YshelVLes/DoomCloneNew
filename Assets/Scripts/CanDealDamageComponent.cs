using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDealDamageComponent : MonoBehaviour
{
    [SerializeField] int damageAmout = 10;

    //����� �� ����� ��������� � ��� �������? �� ���� �� ����� ���� ������ ����� ����������
    private GameObject overlapingActor = null;

    public int Damage
    {
        get => damageAmout;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        //���� ��� ���������� 
        overlapingActor = other.gameObject;
        DealDamage();
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject == overlapingActor)
        {
            overlapingActor = null;
        }
    }

    void DealDamage()
    {
       if( overlapingActor != null && overlapingActor.GetComponent<DamagableComponent>() != null)
        {
            overlapingActor.GetComponent<DamagableComponent>().Hp -= Damage;
            Debug.Log(overlapingActor.GetComponent<DamagableComponent>().Hp);
        }
       else
        {
            Debug.Log("sth wrong");
        }
    }


}

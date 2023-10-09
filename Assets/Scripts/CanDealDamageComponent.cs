using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDealDamageComponent : MonoBehaviour
{
    [SerializeField] int damageAmout = 20;

    //нужно ли через пропертиз и его сделать? по идее он нужен лишь внутри этого компонента
    private GameObject overlapingActor = null;

    public int Damage
    {
        get => damageAmout;
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.GetComponent<DamagableComponent>() != null)
        {
            overlapingActor = other.gameObject;
        }
        else return;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == overlapingActor)
        {
            StartCoroutine(DealDamageEverySecond());
        }
       
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
        if (overlapingActor != null && overlapingActor.GetComponent<DamagableComponent>() != null)
        {
            overlapingActor.GetComponent<DamagableComponent>().Hp -= Damage;
            Debug.Log(overlapingActor.GetComponent<DamagableComponent>().Hp);
        }
        else
        {
            Debug.Log("sth wrong");
        }
    }

   IEnumerator DealDamageEverySecond()
    {
        DealDamage();
        yield return new WaitForSeconds(1);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDealDamageComponent : MonoBehaviour
{
    [SerializeField] int damageAmout = 10;

    //нужно ли через пропертиз и его сделать? по идее он нужен лишь внутри этого компонента
    private GameObject overlapingActor = null;

    public int Damage
    {
        get => damageAmout;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        //если нет компонента 
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

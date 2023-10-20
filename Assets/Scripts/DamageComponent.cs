using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] int damageAmout = 10;

    //нужно ли через пропертиз и его сделать? по идее он нужен лишь внутри этого компонента
    

    private DamagableComponent damagableComponent; 

    public int Damage
    {
        get => damageAmout;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        //если нет компонента 
        other.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damagable);
        damagableComponent = damagable;

        Debug.Log(damagableComponent);

       
    }

    private void OnTriggerExit(Collider other)
    {
      
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(0);

       if(damagableComponent != null)
        {
            damagableComponent.Hp -= Damage;
           
        }
       else
        {
            Debug.Log("sth wrong");
        }
    }

 

}

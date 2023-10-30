using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
   
    protected void DealDamage(DamagableComponent damagable, int damage)
    {
       
       if(damagable != null)
        {
            damagable.Hp -= damage;
            Debug.Log(damagable.Hp);
        }
       else
        {
            Debug.Log("sth wrong");
        }
    }

    protected void Heal(DamagableComponent damagable, int heal)
    {
        if (damagable != null && damagable.Hp < 100)
        {
            damagable.Hp += heal;
            Debug.Log(damagable.Hp);
        }
        else
        {
            Debug.Log("sth wrong");
        }
    }
}

 



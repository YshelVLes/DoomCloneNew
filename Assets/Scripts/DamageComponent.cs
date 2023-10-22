using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] int damageAmout = 10;

    //нужно ли через пропертиз и его сделать? по идее он нужен лишь внутри этого компонента
    
   
    public int Damage
    {
        get => damageAmout;
    }


    private void OnCharacterEnter(PLayerController controller)
    {
        
    }

    private void OnCharacterStay(PLayerController controller)
    {

    }

    private void OnCharacterExit(PLayerController controller)
    {
        
    }


    public void DealDamage(DamagableComponent damagable)
    {
       
       if(damagable != null)
        {
            damagable.Hp -= Damage;
            Debug.Log(damagable.Hp);   
        }
       else
        {
            //Debug.Log("sth wrong");
        }
    }

 

}

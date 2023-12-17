using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private DamagableComponent damagableComponent;

    [SerializeField] int healAmout = 10;
    public int Heal => healAmout;

    void OnCharacterStay(PLayerController controller)
    {
     
    }

   void OnCharacterEnter(PLayerController controller)
    {

        if (controller.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damageble)
            && controller.GetComponent<DamagableComponent>().Hp < controller.GetComponent<DamagableComponent>().MaxHp)
        {
            damageble.Hp += Heal;
            Debug.Log($"{damageble.Hp} current HP");
            Destroy(gameObject);
        }
        else
            Debug.Log("HPMAX");
     }

   void OnCharacterExit()
    {
      
    }
}

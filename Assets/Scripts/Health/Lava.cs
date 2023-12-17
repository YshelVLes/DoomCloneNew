using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private DamagableComponent damagableComponent;

    [SerializeField] int damage;

    public int Damage => damage;

    private ArrayList damagables = new ArrayList();

    void OnCharacterStay(BaseCharacterController controller)
    {
        //print($"lava: {damagableComponent}");
    }

    void OnCharacterEnter(BaseCharacterController controller)
    {
        if (damagables.Contains(controller) == false)
        {
            damagables.Add(controller);
        }
        
        foreach(BaseCharacterController damagable in damagables)
        {
            if(damagable.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damagableComponent))
            {
                Debug.Log("happend");
                StartCoroutine(nameof(LavaDamage), damagableComponent); 
            }
        }

    }

    void OnCharacterExit(BaseCharacterController controller)
    {
        if (damagables.Contains(controller))
        {
            if (controller.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damagableComponent))
            {
                Debug.Log("exit");
                StopCoroutine(nameof(LavaDamage));
            }

            damagables.Remove(controller);
        }
    }

    IEnumerator LavaDamage(DamagableComponent damagableComponent)
    {
        yield return new WaitForSeconds(1);
        damagableComponent.Hp -= Damage;
        Debug.Log($"{damagableComponent.gameObject.name} current HP = {damagableComponent.Hp}");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : DamageComponent
{
    private DamagableComponent damagableComponent;


    void OnCharacterStay(PLayerController controller)
    {
        //print($"lava: {controller.name}");

    }

    void OnCharacterEnter(PLayerController controller)
    {
        controller.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damagable);
        damagableComponent = damagable;
        InvokeRepeating(nameof(LavaDamage), 0, 1);
    }

    void OnCharacterExit()
    {
        CancelInvoke(nameof(LavaDamage));
        damagableComponent = null;
    }

    void LavaDamage()
    {
        DealDamage(damagableComponent);
    }

    void FindDamagable()
    {

    }
}

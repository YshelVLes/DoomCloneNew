using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : HealthManager
{
    private DamagableComponent damagableComponent;

    [SerializeField]
    int heal;

        void OnCharacterStay(PLayerController controller)
        {
            //print($"lava: {controller.name}");

        }

        void OnCharacterEnter(PLayerController controller)
        {
            controller.gameObject.TryGetComponent<DamagableComponent>(out DamagableComponent damagable);
            damagableComponent = damagable;
            Heal(damagableComponent, heal);
            Destroy(this.gameObject);

        }

        void OnCharacterExit()
        {
            damagableComponent = null;
        }

        

    
}

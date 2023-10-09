using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoreComponent : MonoBehaviour
{
    [SerializeField] int healAmout = 10;
    public int Heal => healAmout;

    private GameObject overlapingActor = null;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<DamagableComponent>() != null && other.gameObject.GetComponent<PLayerController>() != null)
        {
            overlapingActor = other.gameObject;
            overlapingActor.GetComponent<DamagableComponent>().Hp += Heal;
            Destroy(this.gameObject);
        }
        else return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == overlapingActor)
        {
            overlapingActor = null;
        }
    }
}

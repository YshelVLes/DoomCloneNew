using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] int damageAmout = 10;

    //����� �� ����� ��������� � ��� �������? �� ���� �� ����� ���� ������ ����� ����������
    
   
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

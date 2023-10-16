using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] UIAim aim;

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.green);

       

        foreach(DamagableComponent enemy in EnemyManager.Enemies)
        {
            // transform.forward
            Vector3 enemyDirection = enemy.transform.position - transform.position;
            Vector3 enemyDirection2D = enemyDirection;

            enemyDirection2D.y = 0;
            enemyDirection2D = enemyDirection2D.normalized;

           enemyDirection = enemyDirection.normalized;

           // Product - это умножение
           //Скалярное и векторное умножение 
           // Dot Product - разница между двумя векторами, скалярное умножение 
           
           float angle = Mathf.Acos(Vector3.Dot(transform.forward, enemyDirection2D)) * Mathf.Rad2Deg;

        if(angle < 3)
            {
               CapsuleCollider enemyCollider = enemy.GetComponent<CapsuleCollider>();

                Vector3 unitFrac = new Vector3(0, enemyCollider.height / 2);

               // RaycastHit hit; 

                if(AimLineAttack(enemy.transform.position) 
                    || AimLineAttack(enemy.transform.position + unitFrac)
                    || AimLineAttack(enemy.transform.position - unitFrac))
                {
                    aim.CanShoot = true;
                    return;
                }
        
            }

            //if (angle < 3)
            //{
            //    aim.CanShoot = true;
            //    print("CanShoot");
            //}
            //else return;

        }
        aim.CanShoot = false;


        //if(  Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) && hit.collider.TryGetComponent(out DamagableComponent damagable) )
        //  {
        //      Debug.Log("Can Damage");
        //  }
    }

    bool AimLineAttack(Vector3 targetPos)
    {
        if (Physics.Linecast(transform.position, targetPos, out RaycastHit hit) && hit.collider.GetComponent<DamagableComponent>())
        {
            Debug.DrawLine(transform.position, targetPos, Color.green); 
            
            return true;
        }
        return false;
    }
}

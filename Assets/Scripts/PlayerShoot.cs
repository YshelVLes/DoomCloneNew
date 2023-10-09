using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.green);

        foreach(DamagableComponent enemy in EnemyManager.Enemies)
        {
           // transform.forward
           Vector3 enemyDirection = (enemy.transform.position - transform.position).normalized;


           // Product - это умножение
           //Скалярное и векторное умножение 
           // Dot Product - разница между двумя векторами, скалярное умножение 
           
            //print(Vector3.Dot(transform.forward, enemyDirection)); 
        }
        

      //if(  Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) && hit.collider.TryGetComponent(out DamagableComponent damagable) )
      //  {
      //      Debug.Log("Can Damage");
      //  }
    }
}

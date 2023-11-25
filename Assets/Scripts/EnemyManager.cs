using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnemyManager 
{
    //статическое поле - работает всю жизнь программы, его можно достать изо всех точек кода 
    //HashSet - все эл уликальные 
    static HashSet<DamagableComponent> damagableComponents = new HashSet<DamagableComponent>();

   public static DamagableComponent GetFirstVisibleTarget(
       Transform sourceTransform, 
       float coneAngle,
       Affiliation affiliation,
       float maxDistance)
   { 

        foreach (DamagableComponent enemy in EnemyManager.Enemies.Where(damagable => (damagable.Affiliation & affiliation) > 0))
        {
            // transform.forward
            Vector3 enemyDirection = enemy.transform.position - sourceTransform.position;

            //если длина вектора больше чем            - пропускаем этот элемент в foreach 
            if (enemyDirection.sqrMagnitude > maxDistance * maxDistance)
                continue;


            Vector3 enemyDirection2D = enemyDirection;
            enemyDirection2D.y = 0;
            enemyDirection2D = enemyDirection2D.normalized;

            enemyDirection = enemyDirection.normalized;

            // Product - это умножение
            //—кал€рное и векторное умножение 
            // Dot Product - разница между двум€ векторами, скал€рное умножение 

            float angle = Mathf.Acos(Vector3.Dot(sourceTransform.forward, enemyDirection2D)) * Mathf.Rad2Deg;

            if (angle < coneAngle)
            {
                CharacterController enemyCollider = enemy.GetComponent<CharacterController>();

                Vector3 unitFrac = new Vector3(0, enemyCollider.height / 2);

                // RaycastHit hit; 

                if (AimLineAttack(sourceTransform, enemy.transform.position)
                    || AimLineAttack(sourceTransform, enemy.transform.position + unitFrac)
                    || AimLineAttack(sourceTransform, enemy.transform.position - unitFrac))
                {
                   
                    return enemy;
                }

            }

            //if (angle < 3)
            //{
            //    aim.CanShoot = true;
            //    print("CanShoot");
            //}
            //else return;

        }

        return null;


        //if(  Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) && hit.collider.TryGetComponent(out DamagableComponent damagable) )
        //  {
        //      Debug.Log("Can Damage");
        //  }
    }
    

    static bool AimLineAttack(Transform sourceTransform, Vector3 targetPos)
    {
        if (Physics.Linecast(sourceTransform.position, targetPos, out RaycastHit hit) && hit.collider.GetComponent<DamagableComponent>())
        {
            Debug.DrawLine(sourceTransform.position, targetPos, Color.green);

            return true;
        }
        return false;
    }

    //ћожно IEnumerator - это объекты которые можно перечисл€ть 
    public static IReadOnlyCollection<DamagableComponent> Enemies => damagableComponents;

    public static void RegisterEnemy(DamagableComponent damagable)
    {
        damagableComponents.Add(damagable);
    }

    public static void UnregisterEnemy(DamagableComponent damagable)
    {
        damagableComponents.Remove(damagable);
    }


}

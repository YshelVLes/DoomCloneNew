using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableComponent : MonoBehaviour
{
    [SerializeField] int hp = 100;

    [SerializeField] Affiliation affiliation;

    public Affiliation Affiliation => affiliation;

    int currentHp;

    bool isDead;

    private void OnEnable()
    {
        EnemyManager.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        EnemyManager.UnregisterEnemy(this);
    }

    private void Start()
    {
        currentHp = hp;
    }

    public bool IsDead => isDead;

    public int Hp
    {
        get => currentHp;
        set
        {
            if (isDead)
                return;

            currentHp = value;

            if (currentHp <= 0)
            {
                Die();
            }
        }
    }

    public int MaxHp
    {
        get => hp;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} is dead");
        isDead = true;
    }
}

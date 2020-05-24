using UnityEngine;
using System.Collections;
using System;

public class PatrolState : IEnemyState
{
    private float patrolTimer;

    private float patrolDuration;

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        patrolDuration = UnityEngine.Random.Range(1, 10);
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Patroling");
        Patrol();

        enemy.Move();

        if (enemy.Target != null && enemy.InMeeleRange)
        {
            enemy.ChangeState(new MeleeState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Patrol()
    {
        

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}

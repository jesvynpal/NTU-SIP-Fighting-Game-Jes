using UnityEngine;
using System.Collections;

public interface IEnemyState
{
    void Execute(); //update on all states
    void Enter(Enemy enemy); //Switch into states
    void Exit(); 
    void OnTriggerEnter(Collider2D other);
	
}

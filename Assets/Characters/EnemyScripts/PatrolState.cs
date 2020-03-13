using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyStates {

    private Enemy enemy;

    /// переменные для таймера патрулирования
    private float patrolTimer;
    private float patrolDuration=Random.Range(5f,10f);// = 10f;

    /// точка входа в состояние
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    /// Update (обновляет состояния в зависимости 
    /// от дистанции между врагом и игровым персонжем,
    /// а также наличием цели)
    public void Execute()
    {
        Patrol();
        enemy.Move();
        if (enemy.Target != null && enemy.isInThrowRange) 
        {
            enemy.ChangeState(new TargetInRangeState());
        }
    }

    /// точка выхода из состояния
    public void Exit() { }

    
    /// поворот AI на точках поворота (т.е. когда ИИ достгает специального маркера, 
    /// он начинает идти в др сторону, т.е. ходит туда-сюда)
    public void OnTriggerEnter(Collider2D other)
    {/*
        if (other.tag == "AI Turning Point")
        {
            enemy.Flip();
        }*/
    }
    

    /// переход от патрулирования к остановке спустя фиксированное время 
    /// (по желанию можно сделать рандом в небольших пределах)
    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}

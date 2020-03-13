using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyStates {

    private Enemy enemy;
    
    /// переменные таймера для остановки между патрулями
    private float idleTimer;
    private float idleDuration = Random.Range(3f, 7f);//5f;

    /// точка входа в состояние
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    /// Update (обновляет состояния в зависимости от наличия цели)
    public void Execute()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    /// точка выхода из состояния
    public void Exit() { }

    /// проверка на пересечения коллизий
    public void OnTriggerEnter(Collider2D other) { }

    /// переход от остановки к патрулированию спустя фиксированное время 
    /// (по желанию можно сделать рандом в небольших пределах)
    private void Idle()
    {
        enemy.anim.SetFloat("Speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}

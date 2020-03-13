using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInRangeState : IEnemyStates {

    private Enemy enemy;

    /// переменные для дальней атаки врага (лук, ножи для метания и т.д.)
    private float throwRangeAttackTimer;
    private float throwRangeAttackCoolDown=2f;
    private bool  canShot=true;

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
        ThrowRangeAttack();
        if (enemy.isInMeleeRange)
        {
            enemy.ChangeState(new MeleeAttackState());
        }
        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    /// точка выхода из состояния
    public void Exit()   { }

    /// проверка на пересечения коллизий
    public void OnTriggerEnter(Collider2D other) { }

    /// дальняя атака (лук, ножи для метания и т.д.)
    /// для того, чтобы враг не атаковал игрока без остановки,
    /// используется таймер, задерживающий следующую атаку
    private void ThrowRangeAttack()
    {
        throwRangeAttackTimer += Time.deltaTime;
        if (throwRangeAttackTimer >= throwRangeAttackCoolDown)
        {
            canShot = true;
            throwRangeAttackTimer = 0;
        }
        if (canShot)
        {
            canShot = false;
            enemy.anim.SetTrigger("Shot");
        }
    }
}

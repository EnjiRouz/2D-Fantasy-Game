using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStates {

    void Enter(Enemy enemy);    // точка входа в состояние
    void Execute(); // обновляет состояния
    void Exit();    // точка выхода из состояния
    void OnTriggerEnter(Collider2D other);  // проверка на пересечения коллизий
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGeneration : MonoBehaviour
{

    private bool isGenerated = false;

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

        int x = Physics2D.GetContacts(gameObject.GetComponent<Collider2D>(), new Collider2D[] { player.GetComponent<Collider2D>() });
        if (Input.GetKeyDown(KeyCode.E) && x > 0 && !isGenerated)
        {
            isGenerated = true;
            var value = Random.Range(10, 100);
            var inventory = FindObjectOfType<Inventory>();
            inventory.money += value;

            FindObjectOfType<QuestNoticeManager>().ShowNotice(
                new QuestNotice("Нашёл деньги", "+ " + value + " монет"));

            Debug.Log(inventory.money);
        }
        else if (Input.GetKeyDown(KeyCode.E) && x > 0)
        {
            FindObjectOfType<QuestNoticeManager>().ShowNotice(
                new QuestNotice("Пусто", "Тут ничего нет, я всё забрал"));
        }  
    }
}

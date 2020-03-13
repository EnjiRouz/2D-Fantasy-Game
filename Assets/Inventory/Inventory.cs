using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Инвентарь, имущество героя. Может хранить не более одного предмета одного типа.
/// </summary>
public class Inventory : MonoBehaviour
{

    /// <summary>
    /// Содержимое инвентаря, рассортировано по типам.
    /// </summary>
   public Dictionary<ItemType, Item> items;

    /// <summary>
    /// Интерфейсный элемент инвентаря.
    /// </summary>
    private GameObject inventoryPanel;

    /// <summary>
    /// Количество денег в кошельке героя.
    /// </summary>
    public int money;

    /// <summary>
    /// Интерфейсные элементы - слоты инвентаря.
    /// </summary>
    private InventorySlot[] slots;

    //текущее оружие в руках
    public Item currentWeapon;

    //текущий плащ
    public Item currentBack;

    public void Save()
    {
        SaveLoadManager.SaveInventory(this);
    }

    public void Load()
    {
        Dictionary<ItemType, Item> loadedStats = SaveLoadManager.LoadInventory();

        items = loadedStats;

    }

    /// <summary>
    /// Поместить предмет в инвентарь. Если предмет такого типа уже есть, в инвентаре остается более полезный.
    /// </summary>
    /// <param name="item">Предмет, помещаемый в инвентарь.</param>
    /// <returns>Выпавший из инвентаря предмет. Если помещение предмета прошло без конфликта. будет <code>null</code>.
    /// Если же в инвентаре уже был предмет того же типа, что и помещаемый, то выпавший предмет - менее полезный из двух.</returns>
    public Item Put(Item item)
    {
        Item drop = null;
        if (items[item.type] == null)
        {
            items[item.type] = item;
        }
        else
        {
            Item good;
            Item bad;

            if (items[item.type].CompareTo(item) == -1)
            {
                bad = items[item.type];
                good = item;
            }
            else
            {
                bad = item;
                good = items[item.type];
            }

            items[item.type] = good;
            drop = bad;
        }

        return drop;
    }

    /// <summary>
    /// Выбросить предмет из инвентаря.
    /// </summary>
    /// <param name="t">тип выбрасываемого предмета.</param>
    /// <returns>Выбрасываемый предмет</returns>
    public Item Drop(ItemType t)
    {
        Item drop = items[t];
        items[t] = null;
        return drop;
    }

    /// <summary>
    /// Обработчик кадра. Считываем действия пользователя. Если нужно, показываем интерфейс инвентаря.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            SwitchVisible();
        }
    }

    /// <summary>
    /// Показываем (т.е. разрешаем отрисовку) интерфейса инвентаря.
    /// </summary>
    private void SwitchVisible()
    {
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            int i = 0;
            foreach (var val in Enum.GetValues(typeof(ItemType)))
            {
                ItemType key = (ItemType)val;
                var item = items.ContainsKey(key) ? items[key] : null;
                slots[i++].SetItem(item);
            }
        }
    }

    /// <summary>
    /// Узнать предмет в инвентаре по его типу.
    /// </summary>
    /// <param name="t">Тип предемета</param>
    /// <returns>Предмет, находящийся в инвентаре.</returns>
    public Item Peek(ItemType t)
    {
        return items[t];
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Start()
    {
        items = new Dictionary<ItemType, Item>();
        slots = new InventorySlot[Enum.GetNames(typeof(ItemType)).Length];
        inventoryPanel = GameObject.FindGameObjectWithTag("Inventory");

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        foreach (var t in Enum.GetValues(typeof(ItemType)))
        {
            items.Add((ItemType)t, null);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventoryPanel.transform.GetChild(i).gameObject.GetComponent<InventorySlot>();
        }
    }

}

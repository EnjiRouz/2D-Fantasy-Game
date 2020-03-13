using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Магазин
/// </summary>
public class Shop : MonoBehaviour
{
    /// <summary>
    /// Категория товаров и текущий товар в ней. 
    /// Хранит все товары магазина, принадлежащие к одному типу (например, все шлемы).
    /// Возможно "пролистывать" товары в категории, т.е. последовательно менять текущий товар.
    /// </summary>
    class Category
    {
        /// <summary>
        /// Товары в категории
        /// </summary>
        List<Item> items;

        /// <summary>
        /// Индекс текущего товара
        /// </summary>
        int current = -1;

        /// <summary>
        /// Текущий товар. Равен <code>null</code> если в категории нет товаров.
        /// </summary>
        public Item CurrentItem
        {
            get
            {
                return current == -1 ? null : items[current];
            }
        }

        /// <summary>
        /// Перелистнуть товар, т.е. сделать текущим следующий товар категории.
        /// Перелистывание закольцовано, т.е. после последнего товара будет выбран первый.
        /// </summary>
        public void Next()
        {
            current++;
            if (items == null || items.Count == 0)
            {
                current = -1;
            }
            else if (current >= items.Count)
            {
                current = 0;
            }
        }

        /// <summary>
        /// Удалить заданный товар из категории.
        /// Если товар был выбран, выбранным станет следующий товар.
        /// </summary>
        /// <param name="item">Заданный товар.</param>
        public void Remove(Item item)
        {
            var succsess = items.Remove(item);
            if (succsess)
            {
                current--;
                Next();
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="items">товары для помещения в категорию. Ответственность за их принадлежность к одному типу лежит на вызывающем коде.</param>
        public Category(params Item[] items)
        {
            this.items = new List<Item>(items);
            Next();
        }
    }

    /// <summary>
    /// Товары, распределенные по категориям
    /// </summary>
    Dictionary<ItemType, Category> categories;

    /// <summary>
    /// Покупатель - это инвентарь, в который складывать покупки
    /// </summary>
    Inventory customer;

    /// <summary>
    /// Выбранный товар. В случае покупки будет куплен именно он
    /// </summary>
    Item selected;

    /// <summary>
    /// Интерфейсный элемент - панель, на которой отображается выбранный товар
    /// </summary>
    GameObject itemPanel;

    /// <summary>
    /// Интерфейсный элемент - панель, на которой отображается магазин
    /// </summary>
    public GameObject shopPanel;

    /// <summary>
    /// Герой, пришедший в магазин. Именно в его инвентарь будут сложены покупки.
    /// </summary>
    public GameObject hero;

    /// <summary>
    /// Шаблон объекта. Нужен для порождения игровых предметов в магазине.
    /// Unity запрещает порождать игровые объекты при помощи <code>new</code>
    /// </summary>
    public Item itemTemplate;

    /// <summary>
    /// Инициализация магазина
    /// </summary>
    void Start()
    {
        //shopPanel = this.gameObject;
        itemPanel = shopPanel.transform.Find("ItemPanel").gameObject;
        customer = hero.GetComponent<Inventory>();
        UpdateMoney();

        Category helmets = new Category(
            NewItem(type: ItemType.Helmet, name: "Helmet1", price: 10, protection: 1, image: Resources.Load<Sprite>("Helmets/VikingLeatherHelm")),
            NewItem(type: ItemType.Helmet, name: "Helmet2", price: 20, protection: 5, image: Resources.Load<Sprite>("Helmets/MercenaryHelm2")),
            NewItem(type: ItemType.Helmet, name: "Helmet3", price: 30, protection: 7, image: Resources.Load<Sprite>("Helmets/BerserkHelm")),
            NewItem(type: ItemType.Helmet, name: "Helmet4", price: 50, protection: 10, image: Resources.Load<Sprite>("Helmets/WarriorHelm")),
            NewItem(type: ItemType.Helmet, name: "Helmet5", price: 10, protection: 1, image: Resources.Load<Sprite>("Helmets/InquisitorHat")));
        Category armors = new Category(
            NewItem(type: ItemType.Armor, name: "Armor1", price: 10, protection: 1, image: Resources.Load<Sprite>("Armor/Armor")),
            NewItem(type: ItemType.Armor, name: "Armor2", price: 50, protection: 5, image: Resources.Load<Sprite>("Armor/VikingRoughArmor2")),
            NewItem(type: ItemType.Armor, name: "Armor3", price: 100, protection: 10, image: Resources.Load<Sprite>("Armor/SamuraiLight3")));
        Category backs = new Category(
            NewItem(type: ItemType.Back, name: "Back1", price: 5, protection: 1, image: Resources.Load<Sprite>("Back/WhiteCloak")),
            NewItem(type: ItemType.Back, name: "Back2", price: 15, protection: 2, image: Resources.Load<Sprite>("Back/RedCloak")),
            NewItem(type: ItemType.Back, name: "Back3", price: 25, protection: 3, image: Resources.Load<Sprite>("Back/BatmanCloak")));
        Category oneHandedeWeapons = new Category(
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded1", price: 5, attack: 3, image: Resources.Load<Sprite>("OneHanded/AssassinDagger")),
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded2", price: 20, attack: 6, image: Resources.Load<Sprite>("OneHanded/FireWarriorSword")),
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded3", price: 70, attack: 8, image: Resources.Load<Sprite>("OneHanded/VikingAxe3")),
            NewItem(type: ItemType.OneHandedWeapon, name: "OneHanded4", price: 90, attack: 10, image: Resources.Load<Sprite>("OneHanded/VikingSword3")));
        Category twoHandedeWeapons = new Category(
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded1", price: 5, attack: 10, image: Resources.Load<Sprite>("TwoHanded/HeavySword")),
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded2", price: 20, attack: 15, image: Resources.Load<Sprite>("TwoHanded/SamuraiSword3")),
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded3", price: 40, attack: 25, image: Resources.Load<Sprite>("TwoHanded/VikingAxe2")),
            NewItem(type: ItemType.TwoHandedWeapon, name: "TwoHanded4", price: 80, attack: 35, image: Resources.Load<Sprite>("TwoHanded/VikingSword1")));
        Category shields = new Category(
             NewItem(type: ItemType.Shield, name: "Shield1", price: 10, protection: 2, image: Resources.Load<Sprite>("Shield/CrusaderShield")),
             NewItem(type: ItemType.Shield, name: "Shield2", price: 20, protection: 17, image: Resources.Load<Sprite>("Shield/IronShield3")),
             NewItem(type: ItemType.Shield, name: "Shield3", price: 30, protection: 27, image: Resources.Load<Sprite>("Shield/KnightShield")));
        Category bows = new Category(
            NewItem(type: ItemType.Bow, name: "Bow1", price: 30, attack: 10, image: Resources.Load<Sprite>("Bow/HunterBow")),
            NewItem(type: ItemType.Bow, name: "Bow2", price: 40, attack: 30, image: Resources.Load<Sprite>("Bow/HunterBow2")),
            NewItem(type: ItemType.Bow, name: "Bow3", price: 50, attack: 45, image: Resources.Load<Sprite>("Bow/VikingShortBow")));
        Category quests = new Category(
            NewItem(type: ItemType.QuestItem, name: "QuestItem1", price: 100, image: Resources.Load<Sprite>("Quest/condom")),
            NewItem(type: ItemType.QuestItem, name: "QuestItem2", price: 500, image: Resources.Load<Sprite>("Quest/bot")),
            NewItem(type: ItemType.QuestItem, name: "QuestItem3", price: 150, image: Resources.Load<Sprite>("Quest/pick")),
            NewItem(type: ItemType.QuestItem, name: "QuestItem4", price: 2500, image: Resources.Load<Sprite>("Quest/pot")));

        categories = new Dictionary<ItemType, Category>();
        categories.Add(ItemType.Helmet, helmets);
        categories.Add(ItemType.Armor, armors);
        categories.Add(ItemType.Back, backs);
        categories.Add(ItemType.OneHandedWeapon, oneHandedeWeapons);
        categories.Add(ItemType.TwoHandedWeapon, twoHandedeWeapons);
        categories.Add(ItemType.Shield, shields);
        categories.Add(ItemType.Bow, bows);
        categories.Add(ItemType.QuestItem, quests);
    }

    /// <summary>
    /// Вспомогательный метод для более удобного порождения нового предмета
    /// </summary>
    /// <param name="type">тип порождаемого предмета</param>
    /// <param name="name">название предмета</param>
    /// <param name="price">цена предмета</param>
    /// <param name="protection">Защита, используется если предмет по типу - доспех. Значение по умолчанию - ноль.</param>
    /// <param name="attack">Атака, используется если предмет по типу - оружие. Значение по умолчанию - ноль.</param>
    /// <param name="image">рисунок, изображающий предмет. По умолчанию - <code>null</code></param>
    /// <returns>Новый предмет</returns>
    Item NewItem(ItemType type, string name, int price, int protection = 0, int attack = 0, Sprite image = null)
    {
        var good1 = Instantiate(itemTemplate);
        good1.type = type;
        good1.name = name;
        good1.price = price;
        good1.protection = protection;
        good1.attack = attack;
        good1.image = image;
        return good1;
    }

    /// <summary>
    /// Купить текущий предмет в магазине и уложить его в инвентарь покупателя. Обновить визуальное состояние магазина
    /// </summary>
    public void Buy()
    {
        Buy(selected);
        UpdateMoney();
        UpdateSelected();
    }

    /// <summary>
    /// "Промотать" предмет, т.е. выбрать следующий предмет в заданной категории. Обновляет визуальное состояние магазина.
    /// </summary>
    /// <param name="t">категория товаров в магазине. Строка, должна содержать имя типа предмета, например "OneHandedWeapon"</param>
    public void Next(string t)
    {
        var key = (ItemType)Enum.Parse(typeof(ItemType), t);
        categories[key].Next();
        selected = categories[key].CurrentItem;
        UpdateSelected();
    }

    /// <summary>
    /// Удалить предмет из магазина.
    /// </summary>
    /// <param name="item">предмет, который надо удалить.</param>
    void DeleteItem(Item item)
    {
        categories[item.type].Remove(item);
    }

    /// <summary>
    /// Купить предмет в магазине и положить его в инвентарь покупателя.
    /// </summary>
    /// <param name="item">покупаемый предмет.</param>
    void Buy(Item item)
    {
        if (item != null && customer != null && customer.money >= item.price)
        {
            customer.Put(item);
            customer.money -= item.price;
            DeleteItem(item);
            selected = categories[item.type].CurrentItem;
        }
    }

    /// <summary>
    /// Заисать текст в указанный элемент пользовательского интерфейса.
    /// </summary>
    /// <param name="uiElement">имя элемента</param>
    /// <param name="value">текст, который нужно поместить.</param>
    void SetInterfaceName(string uiElement, string value)
    {
        var x = itemPanel.transform.Find(uiElement).GetChild(0);
        var y = x.gameObject.GetComponent<Text>();
        y.text = value;
    }

    /// <summary>
    /// Обновить интерфейсный текст в соответствии с текущим состоянием денег у покупателя.
    /// </summary>
    void UpdateMoney()
    {
        if (customer != null)
        {
            SetInterfaceName("Money", customer.money.ToString());
        }

    }

    /// <summary>
    /// Обновить интерфейсные элементы в соответствии с характеристиками выбранного товара.
    /// </summary>
    void UpdateSelected()
    {
        SetInterfaceName("Info", selected != null ? selected.name : "");
        SetInterfaceName("Protection", selected != null ? selected.protection.ToString() : "0");
        SetInterfaceName("Attack", selected != null ? selected.attack.ToString() : "0");
        SetInterfaceName("Price", selected != null ? selected.price.ToString() : "0");

        var x = itemPanel.transform.Find("ItemImage").GetChild(0);
        var y = x.gameObject.GetComponent<Image>();
        y.sprite = selected != null ? selected.image : null;
        y.gameObject.SetActive(selected != null);
    }

    /// <summary>
    /// Обработчик нажатия мыши. С его помощью переключаем категории, по которым щелкнул пользователь.
    /// При этом выбирается в качестве текущего товара текущий предмет в категории.
    /// </summary>
    /// <param name="name">имя категории, должно совпадать с одним из типов предметов, например, "OneHandedWeapon"</param>
    public void OnPointerClick(string name)
    {
        ItemType t = (ItemType)Enum.Parse(typeof(ItemType), name);
        selected = categories[t].CurrentItem;
        UpdateSelected();
    }

    /// <summary>
    /// Интерфейсное действие - закрыть интерфейс магазина
    /// </summary>
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    /// <summary>
    /// Обработчик кадра. Обрабатываем ввод пользователя, если нужно - включаем интерфейс магазина.
    /// </summary>
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int x = Physics2D.GetContacts(
            gameObject.GetComponent<Collider2D>(),
            new Collider2D[] { player.GetComponent<Collider2D>() });

        if (Input.GetKeyUp(KeyCode.E) && x > 0)
        {
            SwitchVisible();
        }
    }

    /// <summary>
    /// Включаем (т.е. разрешаем отрисовку) интерфейс магазина.
    /// </summary>
    private void SwitchVisible()
    {
        if (!shopPanel.activeSelf)
        {
            shopPanel.SetActive(true);
        }
    }
}

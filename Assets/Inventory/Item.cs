using Assets.FantasyHeroes.Scripts;
using System;
using UnityEngine;

/// <summary>
/// Предмет
/// </summary>
public class Item : MonoBehaviour, IComparable
{
    /// <summary>
    /// Тип предмета
    /// </summary>
    public ItemType type;

    /// <summary>
    /// Изображение предмета
    /// </summary>
    public Sprite image;

    /// <summary>
    /// Название предмета
    /// </summary>
    public string name;

    /// <summary>
    /// Вес предмета
    /// </summary>
    public float weight;

    /// <summary>
    /// Цена предмета
    /// </summary>
    public int price;

    /// <summary>
    /// Защита. Должн быть отлична от нуля только для защищающих предметов - доспехов, шлемов, щитов и плащей.
    /// </summary>
    public int protection;

    /// <summary>
    /// Атака. Должна быть отлична от нуля только для оружия - однроручного, двуручного или луков.
    /// </summary>
    public int attack;

    /// <summary>
    /// Предмет можно сложить в инвентарь.
    /// При помощи этой характеристики реализуются предметы, которые можно добыть лишь при некотором условии (см. Меч-из-камня)
    /// </summary>
    public bool pickable;

    /// <summary>
    /// Метод сравнениея полезности предметов.
    /// Защитные предметы сравниваются по защите, оружие сравнивается по атаке, во всех остальных случаях - по цене.
    /// </summary>
    /// <param name="obj">предмет, который нужно сравнить с этим (this)</param>
    /// <returns>-1 если этот предмет менее полеен, +1 если более полоезен, 0 если предметы одинаково полезны (см. йогурты)</returns>
    /// <exception cref="ArgumentException">Если параметр не имеет тип <see cref="Item"/></exception>
    public int CompareTo(object obj)
    {
        int result = 0;
        if (obj is Item)
        {
            var item2 = obj as Item;
            if (item2 == null)
            {
                result = 1;
            }
            else if (this.type != item2.type)
            {
                result = price.CompareTo(item2.price);
            }
            else if (this.type == ItemType.OneHandedWeapon ||
                this.type == ItemType.TwoHandedWeapon ||
                this.type == ItemType.Bow)
            {
                result = attack.CompareTo(item2.attack);
            }
            else if (this.type == ItemType.Helmet ||
                this.type == ItemType.Armor ||
                this.type == ItemType.Back ||
                this.type == ItemType.Shield)
            {
                result = protection.CompareTo(item2.protection);
            }
            else
            {
                result = price.CompareTo(item2.price);
            }
        }
        else
        {
            throw new ArgumentException("obj");
        }

        return result;
    }

    /// <summary>
    /// Интерфейсное действие - скрыть изображение предмета на экране, поместить предмет в инвентарь.
    /// </summary>
    /// <param name="inventory">инвентраь, в который поместиьт предмет.</param>
    public void PickUp(Inventory inventory)
    {
        Item item = gameObject.GetComponent<Item>();
        if (item != null && inventory != null)
        {
            inventory.Put(item);
        }
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Интерфейсное действие - надеть предмет на персонажа (т.е. нарисовать его поверх некоторой зоны изображения персонажа)
    /// </summary>
    /// <param name="c"></param>
    public void PutOn(Character c)
    {
        switch (type)
        {
            case ItemType.Helmet:
                c.Helmet = image;
                c.HelmetRenderer.sprite = c.Helmet;
                break;
            case ItemType.Armor:
                //TODO
                break;
            case ItemType.Back:
                c.Back = image;
                c.BackRenderer.sprite = c.Back;
                c.GetComponent<Inventory>().currentBack = this;
                break;
            case ItemType.OneHandedWeapon:
                c.WeaponType = WeaponType.Melee1H;
                c.PrimaryMeleeWeapon = image;
                c.PrimaryMeleeWeaponRenderer.sprite = c.PrimaryMeleeWeapon;
                c.GetComponent<Inventory>().currentWeapon = this;
                break;
            case ItemType.TwoHandedWeapon:
                c.WeaponType = WeaponType.Melee2H;
                c.PrimaryMeleeWeapon = image;
                c.PrimaryMeleeWeaponRenderer.sprite = c.PrimaryMeleeWeapon;
                c.BuildWeaponTrails();
                c.GetComponent<Inventory>().currentWeapon = this;
                break;
            case ItemType.Shield:
                c.Shield = image;
                c.ShieldRenderer.sprite = c.Shield;
                break;
            case ItemType.Bow:
                c.WeaponType = WeaponType.Bow;
                c.BowRiser = image;
                c.BowRiserRenderers.ForEach(i => i.sprite = c.BowRiser);
                c.BowArrowRenderers.ForEach(i => i.sprite = c.BowArrow);
                c.BowLimbRenderers.ForEach(i => i.sprite = c.BowLimb);
                c.GetComponent<Inventory>().currentWeapon = this;
                //c.SetBow(
                //  FindSprite(SpriteCollection.BowArrow, entry), 
                //  FindSprite(SpriteCollection.BowLimb, entry), 
                //  FindSprite(SpriteCollection.BowRiser, entry));

                break;
            case ItemType.QuestItem:
                //TODO
                break;
            default:
                break;
        }
        c.Initialize();
    }

    /// <summary>
    /// Обработчик кадра. Проверяем ввод пользователя, если нужно, запускаем процедуру подбора предмета.
    /// </summary>
    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            int x = Physics2D.GetContacts(collider,
                new Collider2D[] { player.GetComponent<Collider2D>() });
            if ((Input.GetKeyDown(KeyCode.E)) && x > 0 && pickable)
            {
                PickUp(player.GetComponent<Inventory>());
            }
        }
    }

}

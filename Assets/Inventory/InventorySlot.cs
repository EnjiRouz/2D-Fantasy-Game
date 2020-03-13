using Assets.FantasyHeroes.Scripts;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Слот инвентаря. Интерфейсный элемент
/// </summary>
public class InventorySlot : MonoBehaviour {

    /// <summary>
    /// Кнопка с изображением предмета в слоте
    /// </summary>
    public GameObject imageButton;

    /// <summary>
    /// Кнопка удаления предмета.
    /// </summary>
    public Button removeButton;

    /// <summary>
    /// Герой, которому принадлежит инвентарь
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Предмет в слоте
    /// </summary>
    Item item;

    /// <summary>
    /// Инвентарь, которому принадлежит слот
    /// </summary>
    Inventory inventory;

    /// <summary>
    /// Установить предмет в качестве содержимого слота.
    /// Интерфейсное действие - орисовывает предмет на кнопке и кнопку удаления.
    /// </summary>
    /// <param name="item">предмет</param>
    public void SetItem(Item item)
    {
        this.item = item;
        try
        {
            if (item != null)
            {
                imageButton.GetComponent<Image>().enabled = true;
                imageButton.GetComponent<Image>().sprite = item.image;
                if (removeButton != null)
                {
                    removeButton.interactable = true;
                }
                imageButton.SetActive(true);
            }
        }
        catch { }
    }

    /// <summary>
    /// Обработчик нажатия на кнопку предмета.
    /// </summary>
    public void OnUse()
    {
        var hero = player.GetComponent<Character>();
        item.PutOn(hero);
    }    

    /// <summary>
    /// Обработчик нажатия на кнопку удаления предмета
    /// </summary>
    public void OnRemove()
    {
        inventory.Drop(item.type);
        imageButton.GetComponent<Image>().sprite = null;
        imageButton.GetComponent<Image>().enabled = false;
        removeButton.interactable = false;
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }
}

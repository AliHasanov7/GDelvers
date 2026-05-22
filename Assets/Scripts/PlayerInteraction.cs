using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private Item itemInRange; // Теперь храним ссылку прямо на компонент Item
    private GameObject currentItemInHand;

    // Вызывается Новой системой ввода (кнопка E)
    public void OnInteract(InputValue value)
    {
        Debug.Log("OnInteract");
        if (value.isPressed)
        {
            // Если рядом есть предмет со скриптом Item и руки пусты
            if (itemInRange != null && currentItemInHand == null)
            {
                Debug.Log($"Взаимодействие! Пытаемся подобрать: {itemInRange.itemName}");
                PickUpItem();
            }
        }
    }

    // ТАК КАК ПРЕДМЕТ ТВЕРДЫЙ (НЕ ТРИГГЕР) — ИСПОЛЬЗУЕМ ONCOLLISION
    private void OnCollisionEnter(Collision collision)
    {
        // Ищем компонент Item у объекта, с которым столкнулись (или у его родителя)
        Item item = collision.gameObject.GetComponentInParent<Item>();

        if (item != null)
        {
            itemInRange = item;
            Debug.Log($"[Физический контакт] Обнаружен предмет: {item.itemName}!");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Item item = collision.gameObject.GetComponentInParent<Item>();

        // Если мы отошли от того самого предмета, который был в зоне видимости
        if (item != null && item == itemInRange)
        {
            Debug.Log($"Мы отошли от предмета: {item.itemName}");
            itemInRange = null;
        }
    }

    private void PickUpItem()
    {
        // Запоминаем игровой объект предмета
        currentItemInHand = itemInRange.gameObject;
        itemInRange = null;

        // Отключаем физику, чтобы предмет не вырывался из рук и не падал
        Rigidbody itemRigidbody = currentItemInHand.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = true; 
        }

        // Выключаем коллайдер на время удержания в руке, чтобы игрок не летал на собственном предмете
        Collider itemCollider = currentItemInHand.GetComponent<Collider>();
        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }

        // Тут логика привязки к руке (transform.SetParent и т.д.)
        currentItemInHand.transform.position = transform.position + transform.forward * 1.5f; // Временно спавним чуть впереди
        Debug.Log("Предмет успешно подобран в руки!");
    }
}
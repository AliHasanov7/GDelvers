using UnityEngine;

// Автоматически добавляет Rigidbody и Collider, если их забыли повесить
[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [Header("Настройки предмета")]
    public string itemName = "Безымянный предмет";
    
    // Сюда можно добавить ID предмета, тип и т.д.
}
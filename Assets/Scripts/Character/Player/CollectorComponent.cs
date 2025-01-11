using DG.Tweening;
using Inventory;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CollectorComponent : MonoBehaviour, ICollectItem
{
    [Header("Collect")]
    [SerializeField]private float collectRadius;
    public float CollectRadius { get => collectRadius; private set => collectRadius = value; }
    public LayerMask ItemLayer;

    public void TryCollectItems()
    {
        Collider2D[] itemsInRadius = Physics2D.OverlapCircleAll(transform.position, CollectRadius, ItemLayer);

        if (itemsInRadius.Length > 0)
        {
            foreach (var item in itemsInRadius)
            {
                // Обработка поднятия предмета
                StartCoroutine(CollectItem(item.gameObject));
            }
        }
        else
        {
            // Вывод сообщения, если предметов нет
            Debug.Log("Я ничего не вижу");
        }
    }
    public IEnumerator CollectItem(GameObject item)
    {

        Debug.Log($"Поднят предмет: {item.name}");
        //Анимация подбора предмета
        Sequence anim = DOTween.Sequence();
        var inventory = FindObjectOfType<CharacterStorage>().inventory;

        if (!inventory.AddItem(item.GetComponent<ItemInfo>().ItemData, 1)) yield return null;

        yield return anim
            .Append(item.transform.DOMoveX(transform.position.x, 1f).SetEase(Ease.InQuad))
            .Join(item.transform.DOMoveY(transform.position.y, 1f).SetEase(Ease.InBounce))
            .OnComplete(() => Destroy(item))
            .WaitForCompletion();
    }
    private void OnDrawGizmosSelected()
    {
        // Установка цвета для Gizmos
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, CollectRadius);
    }
}
public class CollectorEvent: UnityEvent
{

}
using DG.Tweening;
using Inventory;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropItemController: MonoBehaviour, IDropItem
{
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private List<Item>? itemDrop;
    public List<Item> ItemDrop => itemDrop;

    [Header("Animation drop settings")]
    [Min(0)]
    [SerializeField] private float itemDropTimeDuration;
    [Min(0)]
    [SerializeField] private float scatterItemDropRadius;

    public IEnumerator DropItems()
    {
        foreach (var obj in ItemDrop)
        {
            // Создаём объект для выпадения предмета
            GameObject item = CreateItemObject(obj);

            // Определяем позицию земли с учетом высоты коллайдера
            Vector2 dropTargetPosition = CalculateDropPosition(item.transform.position);

            // Анимация выпадения
            yield return AnimateItemDrop(item, dropTargetPosition);
        }
        Destroy(gameObject);
    }
    // Метод для создания объекта
    private GameObject CreateItemObject(Item obj)
    {
        GameObject item = new GameObject(obj.name);
        var spriteRenderer = item.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = obj.Image;
        spriteRenderer.sortingOrder = 5;

        var collider = item.AddComponent<CircleCollider2D>();
        collider.radius = 0.2f;
        collider.isTrigger = true;

        item.layer = Mathf.RoundToInt(Mathf.Log(itemLayer.value, 2));
        item.transform.position = transform.position;

        var itemInfo = item.AddComponent<ItemInfo>();
        itemInfo.Initialize(obj);


        Debug.Log($"{item.name} создан.");
        return item;
    }

    // Метод для расчёта конечной позиции на земле
    private Vector2 CalculateDropPosition(Vector2 startPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, Mathf.Infinity, floorLayer);
        return hit.collider != null ? new Vector2(startPosition.x, hit.point.y + 0.1f) : startPosition;
    }

    // Метод для анимации выпадения
    private IEnumerator AnimateItemDrop(GameObject item, Vector2 targetPosition)
    {
        Sequence itemDrop = DOTween.Sequence();
        yield return itemDrop
            .Append(item.transform.DOMoveX(item.transform.position.x + Random.Range(-scatterItemDropRadius, scatterItemDropRadius), itemDropTimeDuration).SetEase(Ease.OutCubic))
            .Join(item.transform.DOMoveY(targetPosition.y, itemDropTimeDuration).SetEase(Ease.OutBounce))
            .WaitForCompletion();
    }
}

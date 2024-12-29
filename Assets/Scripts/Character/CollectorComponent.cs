
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CollectorComponent : MonoBehaviour, ICollectItem
{
    [Header("Collect")]
    public float CollectRadius { get; private set; }
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
        yield return anim
            .Append(item.transform.DOMoveX(transform.position.x, 1f).SetEase(Ease.InQuad))
            .Join(item.transform.DOMoveY(transform.position.y, 1f).SetEase(Ease.InBounce))
            .OnComplete(() => Destroy(item))
            .WaitForCompletion();
    }
}
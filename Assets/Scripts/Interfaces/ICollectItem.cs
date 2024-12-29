using System.Collections;
using UnityEngine;

public interface ICollectItem
{
    float CollectRadius { get; }
    void TryCollectItems();
    IEnumerator CollectItem(GameObject item);
}
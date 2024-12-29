using Inventory;

using System.Collections;
using System.Collections.Generic;

public interface IDropItem 
{
    List<Item> ItemDrop { get; }
    IEnumerator DropItems();
}

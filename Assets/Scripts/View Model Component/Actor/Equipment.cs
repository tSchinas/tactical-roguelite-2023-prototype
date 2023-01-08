using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public const string EquippedNotification = "Equipment.EquippedNotification";
    public const string UnequippedNotification = "Equipment.UnequippedNotification";

    public IList<Equippable> Items { get { return _items.AsReadOnly(); } }
    List<Equippable> _items = new();

    public void Equip(Equippable item, EquipSlots slots)
    {
        Unequip(slots);

        _items.Add(item);
        item.transform.SetParent(transform);
        item.slots = slots;
        item.OnEquip();

        this.PostNotification(EquippedNotification, item);
    }
    public void Unequip(Equippable item)
    {
        item.OnUnequip();
        item.slots = EquipSlots.None;
        item.transform.SetParent(transform);
        _items.Remove(item);

        this.PostNotification(UnequippedNotification, item);
    }

    public void Unequip(EquipSlots slots)
    {
        for (int i=_items.Count -1; i>=0;--i)
        {
            Equippable item = _items[i];
            if ((item.slots & slots) != EquipSlots.None)
                Unequip(item);
        }
    }
}

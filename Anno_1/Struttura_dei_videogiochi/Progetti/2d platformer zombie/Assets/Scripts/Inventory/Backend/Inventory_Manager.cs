using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class Inventory_Manager
{
    [SerializeField] List<Inventory_slot> slots;

    // GET // SET
    public List<Inventory_slot> Slots => slots;
    public int Slots_count => slots.Count;

    // Constructor
    public Inventory_Manager(int size)
    {
        slots = new List<Inventory_slot>(size);
        for (int i = 0; i < size; i++)
        {
            slots.Add(new Inventory_slot());
        }
    }

    // Events

    public UnityAction<Inventory_slot> OnInventorySlotChanged;

    // Methods

    public bool Add_toInventory(Item_data item_data, int amount)
    {
        // finding all inventory slots with the same item and where there is room left
        List<Inventory_slot> fillable_slots = slots.FindAll(i => i.Item_Data != null);
        List<Inventory_slot> inventory_slots = fillable_slots.FindAll(i => i.Item_Data.id == item_data.id && i.Room_left(amount));

        if (inventory_slots.Count > 0)
        {
            //Debug.Log("ENTER");
            for (int i = 0; i < inventory_slots.Count; i++)
            {
                int qt_to_insert = inventory_slots[i].Item_Data.max_stack_size - inventory_slots[i].Amount;
                //Debug.Log(qt_to_insert);
                if (amount <= qt_to_insert)
                {
                    // adding to slot and quitting
                    inventory_slots[i].Add_toStack(amount);
                    OnInventorySlotChanged?.Invoke(inventory_slots[i]);
                    return true;
                }
                else
                {
                    amount -= qt_to_insert;
                }
            }
        }

        if (amount > 0)
        {
            //Debug.Log("ENTER b");
            for (int i = 0; i < slots.Count; i++)
            {
                // finding first empty slot
                if (slots[i].Item_Data == null)
                {
                    int max_qt = item_data.max_stack_size;
                    // adding 
                    //Debug.Log($"max qt:{max_qt}, amount:{amount}");
                    if (amount <= max_qt)
                    {
                        slots[i] = new Inventory_slot(item_data, amount);
                        OnInventorySlotChanged?.Invoke(slots[i]);
                        return true;
                    }
                    else
                    {
                        slots[i] = new Inventory_slot(item_data, max_qt);
                        OnInventorySlotChanged?.Invoke(slots[i]);
                        amount -= max_qt;
                    }

                }
            }
        }
        // No room
        return false;
    }

    public bool Add_toInventory(Item_data item_data, int amount, int index)
    {
        //Debug.Log($"index: {index}, count: {slots.Count}");
        // TO REMOVE|DEBUG
        if (slots[index].Item_Data != null)
        {
            Debug.LogWarning($"slot: {index} should be empty!");
        }

        if (slots[index].Item_Data == null)
        {
            slots[index] = new Inventory_slot(item_data, amount);
            OnInventorySlotChanged?.Invoke(slots[index]);

            return true;
        }
        
        

        // No room
        return false;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Inventory_slot
{
    [SerializeField] private Item_data item_Data;
    [SerializeField] private int stack_amount;

    // GET // SET
    public Item_data Item_Data => item_Data;
    public int Amount => stack_amount;

    public Inventory_slot(Item_data item_Data, int amount)
    {
        this.item_Data = item_Data;
        this.stack_amount = amount;
    }

    public Inventory_slot()
    {
        Clear_slot();
    }

    // METHODS

    public void Clear_slot()
    {
        item_Data = null;
        stack_amount = -1;
    }

    public void Add_toStack(int amount)
    {
        this.stack_amount += amount;
    }

    public void Remove_fromStack(int amount)
    {
        this.stack_amount -= amount;
    }

    public bool Room_left(int amount_toAdd)
    {
        return (stack_amount + amount_toAdd <= item_Data.max_stack_size);
    }

    // using amount_remaining
    public bool Room_left(int amount_toAdd,out int amount_remaining) {
        amount_remaining = item_Data.max_stack_size - stack_amount;
        return Room_left(amount_toAdd);
    }


}
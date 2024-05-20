using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class Slot_UI : MonoBehaviour
{
    public Image item_sprite;
    public TextMeshProUGUI quantity;

    private Button button;

    private Inventory_Holder inventory_holder;
    private Inventory_Manager inventory_manager;
    private Inventory_slot assigned_inventory_slot;
    private int index;

    // GET // SET
    public Inventory_Holder Inventory_holder{ get { return inventory_holder; } set { inventory_holder = value; } }
    public Inventory_slot Assigned_inventory_slot => assigned_inventory_slot;
    public int Index { get { return index; } set { index = value; } }

    // Events

    public UnityAction<int> OnSlotChanged;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(Button_clicked);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clear_slot()
    {
        Debug.LogWarning("SO WHAT");
        assigned_inventory_slot?.Clear_slot();
        item_sprite.sprite = null;
        item_sprite.color = Color.clear;
        quantity.text = "";
        OnSlotChanged?.Invoke(index);
        inventory_holder.Save_data();
    }

    // makes possible to assign to the slot a value and updates the UI
    public void Initialize(Inventory_slot slot)
    {
        inventory_manager = inventory_holder.Inventory_Manager;
        assigned_inventory_slot = slot;
        Update_UI_slot();
    }

    public void Update_UI_slot()
    {
        if (assigned_inventory_slot.Item_Data != null && assigned_inventory_slot.Amount > 0)
        {
            item_sprite.color = Color.white;
            item_sprite.sprite = assigned_inventory_slot.Item_Data.sprite;
            quantity.text = (assigned_inventory_slot.Amount == 1) ? "" : assigned_inventory_slot.Amount + "";
            OnSlotChanged?.Invoke(index);
            inventory_holder.Save_data();
        }
        else
        {
            Clear_slot();
        }

    }

    public void Button_clicked()
    {
        Mouse_Slot mouse_slot = Mouse_Slot._mouse_slot;
        Inventory_slot slot = mouse_slot.slot;
        Inventory_Manager inventory_Manager = mouse_slot.holder.Inventory_Manager;

        // mouse_slot empty, we take from slot_ui if smth inside
        if (slot.Item_Data == null && assigned_inventory_slot.Item_Data != null)
        {
            inventory_Manager.Add_toInventory(assigned_inventory_slot.Item_Data, assigned_inventory_slot.Amount);
            mouse_slot.Update_UI_Mouse_slot();
            Clear_slot();
        }
        else
        // mouse_slot has smth, we give to slot_ui if empty
        if (slot.Item_Data != null && assigned_inventory_slot.Item_Data == null)
        {
            // left shift is being pressed, just transfer one item to Slot_UI
            if (Input.GetKey(KeyCode.LeftShift))
            {
                int qt_to_pass = 1;
                // updating backend
                inventory_manager.Add_toInventory(slot.Item_Data, qt_to_pass, index);
                slot.Remove_fromStack(qt_to_pass);

                // updating frontend
                Update_UI_slot();
                mouse_slot.Update_UI_Mouse_slot();
            }
            else
            // transfer everything to Slot_UI
            {
                inventory_manager.Add_toInventory(slot.Item_Data, slot.Amount, index);
                mouse_slot.Clear_slot();
            }

        }
        else
        // both mouse_slot and slot_ui have smth
        if (slot.Item_Data != null && assigned_inventory_slot.Item_Data != null)
        {
            // same item
            if (slot.Item_Data.id == assigned_inventory_slot.Item_Data.id)
            {
                // left shift is being pressed, just transfer one item to Slot_UI
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (assigned_inventory_slot.Amount < assigned_inventory_slot.Item_Data.max_stack_size)
                    {
                        int qt_to_pass = 1;
                        // updating backend
                        assigned_inventory_slot.Add_toStack(qt_to_pass);
                        slot.Remove_fromStack(qt_to_pass);

                        // updating frontend
                        Update_UI_slot();
                        mouse_slot.Update_UI_Mouse_slot();
                    }
                }
                else
                // we transfer all possible items to Slot_UI
                {

                    int qt_to_pass = slot.Item_Data.max_stack_size - assigned_inventory_slot.Amount;
                    qt_to_pass = (slot.Amount <= qt_to_pass) ? slot.Amount : qt_to_pass;

                    // updating backend
                    assigned_inventory_slot.Add_toStack(qt_to_pass);
                    slot.Remove_fromStack(qt_to_pass);

                    // updating frontend
                    Update_UI_slot();
                    mouse_slot.Update_UI_Mouse_slot();

                }
            }
            else
            // different item
            {
                // exchange items
                // backend
                Inventory_slot temp = new Inventory_slot(assigned_inventory_slot.Item_Data, assigned_inventory_slot.Amount);
                assigned_inventory_slot.Clear_slot();
                inventory_manager.Add_toInventory(slot.Item_Data, slot.Amount, index);
                mouse_slot.Clear_slot();
                inventory_Manager.Add_toInventory(temp.Item_Data, temp.Amount);

                // update frontend
                Update_UI_slot();
                mouse_slot.Update_UI_Mouse_slot();
            }
        }
        
    }


}

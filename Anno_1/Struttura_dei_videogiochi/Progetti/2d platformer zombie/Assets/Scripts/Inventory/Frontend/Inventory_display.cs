using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory_display : MonoBehaviour
{
    public Inventory_Holder inventory_holder;

    // the parent gameobject containing the slots UI
    public GameObject inventory_container;

    private Slot_UI[] slots_UI;
    private Generate_slots generate_slots;
    private Inventory_Manager inventory_manager;

    private bool inventory_manager_ready = false;

    private void Awake()
    {



    }

    void Start()
    {

    }

    private void OnDisable()
    {
        inventory_manager.OnInventorySlotChanged -= Update_Slot_UI;
        Destroy();
    }


    // waiting for inventory_manager to be created;

    public void Inventory_manager_ready()
    {
        inventory_manager_ready = true;
        // we enter in case the slots are not generated but already there
        if (GetComponent<Generate_slots>() == null)
        {
           Actually_Ready();
        }
    }


    // Both Slots and Inventory_manager are ready we can Initialize the slots

    private IEnumerator Ready()
    {
        yield return null;
        Actually_Ready();


    }

    private void Actually_Ready() {
        generate_slots = GetComponent<Generate_slots>();
        if (generate_slots != null)
        {
            if (inventory_holder == null) return;
            Debug.LogWarning("READY");

            inventory_manager = inventory_holder.Inventory_Manager;


            //if (slots_UI != null) {
            //    for (int i = 0; i < slots_UI.Count; i++)
            //    {
            //        slots_UI.RemoveAt(i);
            //    }
            //generate_slots.Destroy_Slots();
            //}

            generate_slots.Generate_Slots(inventory_manager.Slots_count);

            slots_UI = inventory_container.GetComponentsInChildren<Slot_UI>();
            inventory_manager.OnInventorySlotChanged += Update_Slot_UI;
            Initizialize();
        }
        else
        {
            inventory_manager = inventory_holder.Inventory_Manager;
            slots_UI = inventory_container.GetComponentsInChildren<Slot_UI>();
            inventory_manager.OnInventorySlotChanged += Update_Slot_UI;
            Initizialize();
        }


    }

    public void Destroy()
    {
        if (slots_UI != null)
        {
            foreach (var slot in slots_UI)
            {
                if (slot != null)
                {
                    Destroy(slot.gameObject);
                }
            }
            slots_UI = null;
        }

        foreach (Transform slot_UI in inventory_container.transform)
        {
            Destroy(slot_UI.gameObject);
        }

        Resources.UnloadUnusedAssets(); // Force cleanup of unused assets
    }

    public void Generate(int size) {
        generate_slots = GetComponent<Generate_slots>();
        if (generate_slots != null)
            generate_slots.Generate_Slots(size);
    }

    // binding the backend slots with the frontend slots in the UI
    // also giving it an index, so I know which slot to update in the inventory manager
    private void Initizialize()
    {
        if (slots_UI != null)
        {
            for (int i = 0; i < inventory_manager.Slots_count; i++)
            {
                slots_UI[i].Inventory_holder = inventory_holder;
                slots_UI[i].Initialize(inventory_manager.Slots[i]);
                slots_UI[i].Index = i;
            }
            
        }
    }


    
    // swaps the inventory holder with the one we want to display
    public void Update_inventory_holder(Inventory_Holder inventory_holder)
    {
        //if (this.inventory_holder == inventory_holder)
        //{
        //    Debug.Log("Same inventory_holder instance, skipping update.");
        //    return;
        //}

        this.inventory_holder = inventory_holder;
        // setting up again the slots and the inventory manager
        Actually_Ready();
    }

    public void Update_Slot_UI(Inventory_slot slot)
    {
        for (int i = 0; i < slots_UI.Length; i++)
        {
            if (inventory_manager.Slots[i].Equals(slot))
            {
                slots_UI[i].Initialize(slot);
                break;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

public class Inventory_Holder : MonoBehaviour, I_Savable<Inventory_Manager>
{
    [SerializeField] private Container_data container_data;
    [SerializeField] private Inventory_display inventory_display;

    public int num_instance;

    private Inventory_Manager inventory_manager;

    public Inventory_Manager Inventory_Manager => inventory_manager;
    public Inventory_display Inventory_Display => inventory_display;

    void Awake()
    {
        // Loading data if any
        Inventory_Manager inv_man = Load_data();

        if (inv_man == null)
        {
            inventory_manager = new Inventory_Manager(container_data.slots_count);
        }
        else
        {
            Debug.Log("Loaded");
            inventory_manager = inv_man;
        }

        // We do not need an inventory display for Mouse_slot,
        // while Containers all use the predefined inventory_display
        // Makes sure we do not try to display the items before inv_man is initialized
        if (GetComponent<Mouse_Slot>() == null && GetComponent<Container>() == null) inventory_display.Inventory_manager_ready();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save_data()
    {
        Save_manager<Inventory_Manager>.Save_Data(inventory_manager, GetType().Name + num_instance);
    }

    public Inventory_Manager Load_data()
    {
        return Save_manager<Inventory_Manager>.Load_Data(GetType().Name + num_instance);
    }



}

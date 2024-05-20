using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mouse_Slot : MonoBehaviour
{
    public Inventory_Holder holder;
    public Image item_sprite;
    public TextMeshProUGUI quantity;

    private Inventory_Manager inventory_manager;
    private Inventory_slot inventory_slot;
    private bool active;
    private CanvasGroup canvas_group;

    public Inventory_slot slot => inventory_slot;
    public bool Active { get { return active; } set { active = value; } }

    // Singleton to avoid multiple mouse slots
    public static Mouse_Slot _mouse_slot;

    public static Mouse_Slot Mouse_slot { 
        get { return _mouse_slot; }
    }

    void Awake()
    {
        _mouse_slot = this;
    }

    private void OnDestroy()
    {
        if (_mouse_slot != null)
        {
            Destroy(_mouse_slot);
        }
            inventory_manager.OnInventorySlotChanged -= Initialize;
    }

    // Start is called before the first frame update
    void Start()
    {

        inventory_manager = holder.Inventory_Manager;
        inventory_slot = inventory_manager.Slots[0];
        inventory_manager.OnInventorySlotChanged += Initialize;
        
        active = false;

        foreach (Transform child in transform)
        {
            child.transform.gameObject.SetActive(false);
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.position = Input.mousePosition;
        }
    }

    // Methods

    public void Clear_slot()
    {
        slot?.Clear_slot();
        active = false;
        item_sprite.sprite = null;
        item_sprite.color = Color.clear;
        quantity.text = "";
        foreach (Transform child in transform)
        {
            child.transform.gameObject.SetActive(false);
        }
    }

    // makes possible to assign to the slot a value and updates the UI
    public void Initialize(Inventory_slot slot)
    {
        inventory_slot = slot;
        Debug.Log("Initialize"); 
        foreach (Transform child in transform)
        {
            child.transform.gameObject.SetActive(true);
        }
        Update_UI_Mouse_slot();
    }

    public void Update_UI_Mouse_slot()
    {
        if (slot.Item_Data != null && slot.Amount > 0)
        {
            active = true;
            item_sprite.color = Color.white;
            item_sprite.sprite = slot.Item_Data.sprite;
            Debug.Log("image assigned");
            quantity.text = (slot.Amount == 1) ? "" : slot.Amount + "";
        }
        else
        {
            Clear_slot();
        }
    }
}

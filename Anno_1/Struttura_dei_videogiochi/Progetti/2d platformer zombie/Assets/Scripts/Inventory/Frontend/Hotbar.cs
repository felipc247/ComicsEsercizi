using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public GameObject item_holding_point;

    private int current_index;
    private bool equipping;

    private Slot_UI[] slots_UI;
    private Image[] backgrounds;
    private Color start_color;

    // Singleton to avoid creating multiple hotbars
    public static Hotbar _hotbar;

    public static Hotbar Hotbar_get
    {
        get { return _hotbar; }
    }

    void Awake()
    {
        _hotbar = this;
    }
    // GET // SET

    // can only change cur index from this script
    public int Current_index => current_index;

    public bool Equipping => equipping;

    // Start is called before the first frame update
    void Start()
    {
        slots_UI = GetComponentsInChildren<Slot_UI>();
        backgrounds = new Image[slots_UI.Length];
        current_index = 0;
        last_slot = current_index;
        equipping = false;

        for (int i = 0; i < slots_UI.Length; i++)
        {
            // checks if something changed
            slots_UI[i].OnSlotChanged += Update_Slot;
        }

        int j = 0;

        // getting the backgrounds
        foreach (Transform child in transform)
        {
            foreach (Transform child2 in child)
            {
                if (child2.gameObject.GetComponent<Image>() != null)
                {
                    backgrounds[j] = child2.gameObject.GetComponent<Image>();
                    break;
                }
            }
            j++;
        }
        start_color = backgrounds[0].color;
        backgrounds[current_index].color = Color.red;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            equipping = !equipping;
            if (equipping) Equip_item();
            else Unequip_item();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Drop_item();
        }
        Check_for_slot_change_keyboard();
        Check_for_slot_change_mouse_wheel();
    }

    // Methods

    // used to update slot in case something happens in the current slot
    public void Update_Slot(int index)
    {
        // quit if not current index
        if (current_index != index) return;

        if (slots_UI[index].Assigned_inventory_slot.Item_Data != null && equipping)
        {
            // destroy previous item
            Unequip_item();
            // show new one
            Equip_item();
        }
        else
        {
            Unequip_item();
        }

    }

    private int last_slot = -1;
    private bool previous_item;

    public void Check_for_slot_change_keyboard()
    {
        int starting_code = 49; // alpha1

        // checking if an alpha key is being pressed
        for (int i = 0; i < slots_UI.Length; i++)
        {
            // if one pressed, change highlighted slot 
            if (Input.GetKeyDown((KeyCode)starting_code++))
            {
                if (last_slot != -1)
                {
                    backgrounds[last_slot].color = start_color;
                }
                last_slot = i;
                current_index = i;
                backgrounds[i].color = Color.red;

                // removing previous item if present
                if (previous_item)
                {
                    foreach (Transform child in item_holding_point.transform)
                    {
                        if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                }

                // if slot is not empty
                if (slots_UI[i].Assigned_inventory_slot.Item_Data != null)
                {
                    if (equipping) Equip_item();
                    // we know there's a previous item
                    previous_item = true;
                    break;
                }
                else
                {
                    previous_item = false;
                }

                // stop checking, bc key pressed found
                break;
            }
        }
    }

    public void Check_for_slot_change_mouse_wheel()
    {
        // getting y movement in the mouse wheel
        float scroll_wheel = Input.mouseScrollDelta.y;

        // quit method if no movement
        if (scroll_wheel == 0)
            return;

        // resetting color
        if (last_slot != -1)
        {
            backgrounds[last_slot].color = start_color;
        }

        // !! Slots loop !!
        // going down, go to previous slot
        if (scroll_wheel < 0f)
        {
            current_index = (last_slot > 0) ? last_slot - 1 : slots_UI.Length - 1;
        }
        // going up, go to next slot
        else if (scroll_wheel > 0f)
        {
            current_index = (last_slot < slots_UI.Length - 1) ? last_slot + 1 : 0;
        }

        // save last slot for next round
        last_slot = current_index;
        // swap color background for currently selected slot
        backgrounds[current_index].color = Color.red;

        // removing previous item if present
        if (previous_item)
        {
            foreach (Transform child in item_holding_point.transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    Destroy(child.gameObject);
                }
            }

        }

        // if slot is not empty
        if (slots_UI[current_index].Assigned_inventory_slot.Item_Data != null)
        {
            if (equipping) Equip_item();
            // we know there's a previous item
            previous_item = true;
        }
        else
        {
            previous_item = false;
        }

    }

    private GameObject equipped_item;

    public void Equip_item()
    {
        // showing item in player's hands if anything inside
        if (slots_UI[current_index].Assigned_inventory_slot.Item_Data == null)
            return;

        // instantiating the object and setting it in the right place
        equipped_item = Instantiate(slots_UI[current_index].Assigned_inventory_slot.Item_Data.prefab);
        
        equipped_item.transform.position = item_holding_point.transform.position;
        equipped_item.transform.parent = item_holding_point.transform;
        equipped_item.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void Unequip_item()
    {
        if (equipped_item != null) Destroy(equipped_item);
    }

    public void Drop_item()
    {
        if (equipping)
        {
            // quit if nothing to drop
            if (slots_UI[current_index].Assigned_inventory_slot.Item_Data == null)
                return;

            int qt_to_remove = 1;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                GameObject dropped_item = Instantiate(slots_UI[current_index].Assigned_inventory_slot.Item_Data.prefab);
                dropped_item.transform.position = item_holding_point.transform.position;
                slots_UI[current_index].Assigned_inventory_slot.Remove_fromStack(qt_to_remove);
            }
            else
            {
                qt_to_remove = slots_UI[current_index].Assigned_inventory_slot.Amount;
                slots_UI[current_index].Assigned_inventory_slot.Remove_fromStack(qt_to_remove);
                for (int i = 0; i < qt_to_remove; i++)
                {
                    GameObject dropped_item = Instantiate(slots_UI[current_index].Assigned_inventory_slot.Item_Data.prefab);
                    dropped_item.transform.position = item_holding_point.transform.position;
                }
            }

            slots_UI[current_index].Update_UI_slot();
        }
    }

}

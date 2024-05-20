using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Container_data container_data;
    public Material outline_mat;
    public GameObject container_display;

    private SpriteRenderer spriteRenderer;
    private Material default_mat;

    public Inventory_Holder inv_hold;
    // makes sure we can not open a container if not inside range
    private bool openable;
    // keeps track of the display state(shown or not)
    private bool open;
    // the inventory display we are changing
    private Inventory_display inventory_display;

    // Start is called before the first frame update

    void Start()
    {
        inventory_display = container_display.GetComponent<Inventory_display>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inv_hold = GetComponent<Inventory_Holder>();
        // saving default mat so I can go back to it when player is not triggering
        default_mat = spriteRenderer.material;
        openable = false;
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        // displaying container
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory_display != null && openable && !open)
            {
                open = true;

                CanvasRenderer canvasRenderer = GetComponent<CanvasRenderer>();
                container_display.SetActive(true);
                inventory_display.Update_inventory_holder(inv_hold);


                Debug.Log("UTTTT");

            }
            else if (inventory_display != null && openable && open)
            {
                DisableDisplay();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger");
            openable = true;

            spriteRenderer.material = outline_mat;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            openable = false;
            spriteRenderer.material = default_mat;
            DisableDisplay();
        }
    }

    // Methods

    private void DisableDisplay()
    {
        open = false;
        //inv_hold.Save_data();
        //inventory_display.Destroy();
        if (container_display != null) container_display.SetActive(false);
    }

}

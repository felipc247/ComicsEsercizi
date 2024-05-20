using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Collectible : MonoBehaviour
{
    public Item_data item_data;
    public Material outline_mat;

    private SpriteRenderer spriteRenderer;
    private Material default_mat;

    private Inventory_Holder inv_hold;
    private bool pickable;

    // Start is called before the first frame update

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // saving default mat so I can go back to it when player is not triggering
        default_mat = spriteRenderer.material;
        pickable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E)) {
            if (inv_hold != null && pickable)
            {
                if (inv_hold.Inventory_Manager.Add_toInventory(item_data, 1))
                {
                    //Debug.Log("Item added");
                    Destroy(gameObject);
                }
                else
                {
                    //Debug.Log("NO ROOM");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            inv_hold = collision.gameObject.GetComponent<Player_Handle>().hotbar.GetComponent<Inventory_Holder>();
            pickable = true;

            spriteRenderer.material = outline_mat;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inv_hold = null;
            pickable = false;
            spriteRenderer.material = default_mat;

        }
    }

}

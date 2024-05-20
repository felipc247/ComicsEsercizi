using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    public GameObject gun;
    public Transform gun_holding_point;
    // Start is called before the first frame update

    private GameObject gun_object;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Equip_Unequip();
        }
    }

    private bool equipped = false;

    //methods

    private void Equip_Unequip()
    {
        if (gun != null)
        {
            if (equipped)
            {
                Destroy(gun_object);
            }
            else
            {
                // preparing gun
                gun_object = Instantiate(gun, gun_holding_point);
                gun_object.transform.localScale = Vector3.one;

                // making gun child of gun_holding_point, so it moves with player
                gun_object.transform.parent = gun_holding_point;
            }
            equipped = !equipped;
        }
    }
}

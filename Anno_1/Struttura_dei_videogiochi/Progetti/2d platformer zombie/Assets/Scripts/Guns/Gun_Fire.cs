using System.Collections;
using UnityEngine;

public class Gun_Fire : MonoBehaviour
{
    public GameObject bullet_prefab;
    public Transform fire_point;

    public Gun_Data gun_data;

    private bool can_shoot;
    private bool can_reload;

    // Start is called before the first frame update
    void Start()
    {
        can_shoot = true;
        can_reload = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && can_shoot && can_reload)
        {
            can_shoot = false; // can't shoot
            StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.O) && can_reload)
        {
            can_reload = false; // can't reload
            StartCoroutine(Reload());
        }

    }

    // using coroutine to handle fire_rate, so player can't shoot
    private IEnumerator Shoot()
    {

        if (gun_data.Left_ammos > 0)
        {
            gun_data.Left_ammos--;
            Debug.Log("Ammos left:" + gun_data.Left_ammos);
            // shooting actions
            GameObject bullet = Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);
            // setting bullet_speed before spawning the bullet
            Bullet_Info bullet_Info = bullet.GetComponent<Bullet_Info>();
            bullet_Info.bullet_speed = gun_data.bullet_speed;
            bullet_Info.dmg = gun_data.dmg;
        }
        else
        {
            // message to player
            Debug.Log("No Ammo left");
        }

        // waiting for cooldown
        yield return new WaitForSeconds(gun_data.fire_rate);

        can_shoot = true; // can shoot again
    }

    // methods

    private IEnumerator Reload()
    {

        Debug.Log("Reloading");

        yield return new WaitForSeconds(gun_data.reload_speed);

        Debug.Log("Done Reloading");
        gun_data.Left_ammos = gun_data.magazine_size;

        can_reload = true;
    }

}

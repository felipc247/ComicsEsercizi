using UnityEngine;

[CreateAssetMenu(fileName = "Gun_data", menuName = "ScriptableObjects/Gun_Data_ScriptableObject", order = 1)]

public class Gun_Data : ScriptableObject
{
    public enum GunType { Pistol, Rifle, Shotgun };
    public GunType gunType;

    public int magazine_size;
    public float fire_rate;
    public float reload_speed;
    public float bullet_speed;
    public int dmg;

    private int left_ammos = 6;

    public int Left_ammos { get { return left_ammos; } set { left_ammos = value; } }


}

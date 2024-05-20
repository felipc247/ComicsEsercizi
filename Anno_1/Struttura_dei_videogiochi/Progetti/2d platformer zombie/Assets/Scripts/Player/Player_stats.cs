using UnityEngine;

[CreateAssetMenu(fileName = "Player_stats", menuName = "ScriptableObjects/Player_stats", order = 1)]

public class Player_stats : ScriptableObject
{
    public int health;
    public int cur_health;
    public int melee_damage;
    public float melee_delay;
}

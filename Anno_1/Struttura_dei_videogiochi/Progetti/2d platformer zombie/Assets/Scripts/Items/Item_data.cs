using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_data", menuName = "ScriptableObjects/Item_data_ScriptableObject", order = 1)]
public class Item_data : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefab;
    public int id;
    public string displayed_name;
    [TextArea(4,4)] public string description;
    public int max_stack_size;
}
 
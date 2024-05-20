using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Container_data", menuName = "ScriptableObjects/Container_data_ScriptableObject", order = 1)]

public class Container_data : ScriptableObject
{
    public int slots_count;
    [TextArea(4, 4)] public string description;
}

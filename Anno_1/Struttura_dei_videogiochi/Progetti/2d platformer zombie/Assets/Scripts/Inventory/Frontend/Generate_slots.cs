using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generate_slots : MonoBehaviour
{
    public GameObject slot_prefab;

    // GET // SET

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Methods

    public void Generate_Slots(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject slot_UI = Instantiate(slot_prefab, transform);
        }
    }

    public void Destroy_Slots()
    {
        int i = 0;
        foreach (Transform slot_UI in transform)
        {
            Destroy(slot_UI.gameObject);
            i++;
        }

        Debug.LogWarning("Count " + i);

        i = 0;
        foreach (Transform slot_UI in transform)
        {
            Destroy(slot_UI.gameObject);
            i++;
        }

        Debug.LogWarning(" Second Count " + i);

    }

}

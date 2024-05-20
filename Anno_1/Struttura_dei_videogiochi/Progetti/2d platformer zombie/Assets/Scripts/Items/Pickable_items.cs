using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Pickable_items : MonoBehaviour, I_Savable<Pickable_items>
{
    public List<GameObject> items;

    private string save_path;

    void Start()
    {
        // building save_path, equal to this class' name and the name of the current scene
        save_path = GetType().Name + "_" + SceneManager.GetActiveScene().name;

        if (Load_data() != null)
        {
            // Data are there, we load the game
            items = Load_data().items;
            for (int i = 0; i < items.Count; i++)
            {
                Transform tr = items[i].transform;
                GameObject item = Instantiate(items[i]);
                item.transform.position = tr.position;
            }
        }
        else
        {
            // if no data, means first time entering the scene, so we save the items
            foreach (Transform child in transform)
            {
                items.Add(child.gameObject);
            }
            Save_data();
        }

    }
    // Methods
    public Pickable_items Load_data()
    {
        return Save_manager<Pickable_items>.Load_Data(save_path);
    }

    public void Save_data()
    {
        Save_manager<Pickable_items>.Load_Data(save_path);
    }

}

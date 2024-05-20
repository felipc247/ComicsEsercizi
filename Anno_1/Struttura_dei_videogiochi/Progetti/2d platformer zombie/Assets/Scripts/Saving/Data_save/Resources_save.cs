using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
// Handles resources
public class Resources_save : I_Savable<Resources_save>
{
    [SerializeField] private int coins;

    private string dir = "Resources_save\\";
    private string filename = "";

    // GET // SET
    public int Coins => coins;

    // Start is called before the first frame update
    void Start()
    {

    }

    // METHODS

    public void SetUp()
    {
        filename = GetType().Name;
        Debug.LogError(filename);
        // Tryna get the resources if they exist
        Resources_save resources_Save = Save_manager<Resources_save>.Load_Data(dir + filename);
        if (resources_Save != null)
        {
            Set_resources(resources_Save);
        }
        else
        {
            coins = 0;
        }
    }

    public Resources_save Load_data()
    {
        return Save_manager<Resources_save>.Load_Data(dir + filename);
    }

    public void Save_data()
    {
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, dir)))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, dir));
        }
        Save_manager<Resources_save>.Save_Data(this, dir + filename);
    }

    // Puts back the values of the resources
    private void Set_resources(Resources_save resources)
    {
        coins = resources.Coins;
    }

    public void Add_coins(int amount)
    {
        coins += amount;
    }

    // Checks if we have enough coins before removing them
    public bool Remove_coins(int amount)
    {
        if (coins > amount && coins > 0)
        {
            coins -= amount;
            return true;
        }
        else
        {
            // UI MESSAGE
            Debug.LogWarning("NOT ENOUGH COINS");
        }

        return false;
    }
}

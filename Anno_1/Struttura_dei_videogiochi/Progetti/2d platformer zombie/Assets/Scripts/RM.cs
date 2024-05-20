using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RM : MonoBehaviour
{
    public TextMeshProUGUI coins_text;

    // Handles showing the resources and using them
    [SerializeField] private Resources_save resources;


    // Start is called before the first frame update
    void Start()
    {
        resources = new Resources_save();
        resources.SetUp();
        SetUp_Refresh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Methods

    public void SetUp_Refresh()
    {
        coins_text.text = "Coins: " + resources.Coins;
    }

    public void Refresh() { 
        coins_text.text = "Coins: " + resources.Coins;
        resources.Save_data();
    }

    public void Add_coins(int amount)
    {
        resources.Add_coins(amount);
        Refresh();
    }

    public void Remove_coins(int amount)
    {
        bool success = resources.Remove_coins(amount);
        if (success)
        {
            Refresh();
        }
    }

}

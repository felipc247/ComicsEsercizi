using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            Load_scene();
        }
    }

    // Methods

    private void Load_scene() { 
        // ANIMATION
        SceneManager.LoadSceneAsync(scene);        
    }
}

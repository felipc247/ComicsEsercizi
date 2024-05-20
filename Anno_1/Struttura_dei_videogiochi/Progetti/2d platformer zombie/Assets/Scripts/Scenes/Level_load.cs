using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_load : MonoBehaviour
{
    public string level;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Load_scene);
        TextMeshProUGUI t = button.GetComponentInChildren<TextMeshProUGUI>();
        t.text = level;
    }

    private void Load_scene() {
        SceneManager.LoadScene(level);
    }

}

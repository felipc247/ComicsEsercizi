using UnityEngine;

public class GM : MonoBehaviour
{
    private static GM _instance;

    public static GM Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    private float player_horizontal = 1;

    public float Player_horizontal { get { return player_horizontal; } set { player_horizontal = value; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

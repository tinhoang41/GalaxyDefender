using UnityEngine;
using System.Collections;

public class GameData
{
    float totalScore;
}
public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


}

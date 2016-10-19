using UnityEngine;
using System.Collections;

[System.Serializable]
public struct EnemyDataParameter
{
    public int minLevel;
    public int maxLevel;
    public int currentLevel;

    public EnemyDataParameter(int min, int max, int current)
    {
        minLevel = min;
        maxLevel = max;
        currentLevel = current;
    }
}

public class EnemyGenerator : MonoBehaviour {

    protected static EnemyGenerator instance;

    public GameObject polygonObject;
    public GameObject circleObject;
    public GameObject triangleObject;

    // Use this for initialization
    void Start () {
        instance = this;
    }
    
    public static void GeneratePolygon(EnemyDataParameter data, Vector3 position, Vector3 velocity, Quaternion rotation)
    {
        var enemy = Instantiate(instance.polygonObject, position, rotation) as GameObject;
        if (enemy == null)
            return;
        enemy.GetComponent<EnemyData>().Initialize(data);
         //enemy.Initialize(name, health, weapon, forceUser);
    }

    public static void GenerateCircle(EnemyDataParameter data, Vector3 position, Vector3 velocity, Quaternion rotation)
    {
        var enemy = Instantiate(instance.circleObject, position, rotation) as GameObject;

        if (enemy == null)
            return;

        enemy.GetComponent<EnemyData>().Initialize(data);
        enemy.GetComponent<CircleMover>().Initialize(velocity);
        //Debug.Log("Generated circle");
        //enemy.Initialize(name, health, weapon, forceUser);
    }
}

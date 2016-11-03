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
    public GameObject diamonObject;
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
    }

    public static void GenerateTriangle(EnemyDataParameter data, Vector3 position, Vector3 velocity, Quaternion rotation)
    {
        var enemy = Instantiate(instance.triangleObject, position, rotation) as GameObject;
        if (enemy == null)
            return;
        enemy.GetComponent<EnemyData>().Initialize(data);
    }

    public static void GenerateDiamond(EnemyDataParameter data, Vector3 position, Vector3 velocity, Quaternion rotation)
    {
        var enemy = Instantiate(instance.diamonObject, position, rotation) as GameObject;
        if (enemy == null)
            return;
        enemy.GetComponent<EnemyData>().Initialize(data);
    }

    public static void GenerateCircle(EnemyDataParameter data, Vector3 position, Vector3 velocity, Quaternion rotation)
    {
        var enemy = Instantiate(instance.circleObject, position, rotation) as GameObject;

        if (enemy == null)
            return;

        enemy.GetComponent<EnemyData>().Initialize(data);
        enemy.GetComponent<CircleMover>().Initialize(velocity);
    }
}

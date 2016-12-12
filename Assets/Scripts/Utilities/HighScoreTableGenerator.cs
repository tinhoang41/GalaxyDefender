using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreTableGenerator : MonoBehaviour {

    public GameObject scoreLinePrefab;
    public Vector3 startingPosition;
    public Vector3 positionChanger;
    
    List<GameObject> scoreLines;

    
	// Use this for initialization
	void Start ()
    {
        scoreLines = new List<GameObject>();
        var record = GlobalControl.Instance.pUserManager.pRecordedData.highScoresData;
        for(int i = 0; i < record.Length; i++)
        {
            if (i >= record.Length / 2)
                continue;
            record[i].pIsValid = true;
            //if (record[i] == null || !record[i].pIsValid)
            //    continue;
            var lineposition = startingPosition + (positionChanger * i);
            var scoreLineObject = (GameObject)Instantiate(scoreLinePrefab, lineposition, Quaternion.identity);
            scoreLineObject.transform.SetParent(this.transform, false);
            scoreLines.Add(scoreLineObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Score : MonoBehaviour
{
    public int score;
    public GameObject objscore;
     
    // Use this for initialization
    void Start()
    {

    }
     
    // Update is called once per frame
    void OnGUI () 
    {
        GUI.color = Color.white;
        GUILayout.Label(" SCORE: " + score.ToString());
    }

    private void Update()
    {
        score = (int)(objscore.transform.position.x + 1.9)/5;
    }
}

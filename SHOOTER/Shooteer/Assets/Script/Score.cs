using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update


    //public static UI_Score Instance;

    [SerializeField] TextMeshProUGUI ScoreDisplay;
    public static int ScoreVal;

    void Start()
    {
        
    }
    public int AddScore(int value)
    {
        ScoreVal += value;
        UpdateScore(ScoreVal);
        return ScoreVal;
    }
    // Update is called once per frame
    public void UpdateScore(int value)
    {
        ScoreDisplay.text = "Score: " + value.ToString();
    }
    void Update()
    {
        
    }
}

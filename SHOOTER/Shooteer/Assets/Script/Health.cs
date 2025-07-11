using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI HealthDisplay;
    public static double HP = 3;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(HP);
    }
    public void UpdateHealth(double value)
    {
        HealthDisplay.text = "Health: " + value.ToString();
    }
    public double takeDamage(double value)
    {
        HP -= value;
        UpdateHealth(HP);
        if(HP <= 0)
        {

            

        }
        return HP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

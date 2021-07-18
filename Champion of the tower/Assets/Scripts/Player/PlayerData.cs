using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static int initiative = 110;
    private static int m_health = 60;
    public static int health {
        get { return m_health;  }
        set { if(value < 0)
                {
                m_health = 0;
            }
            else
            {
                m_health = value;
            }
        }
    }
    public static int playerActionPoint = 6;
    public static int playerMovementPoint = 3;
    public static string playerName = "Boburus";

    public TextMeshProUGUI playerTextName;
    public TextMeshProUGUI playerTextHealth;
    public TextMeshProUGUI playerTextActionPoint;
    public TextMeshProUGUI playerTextMovementPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerTextName.text = playerName;
        playerTextHealth.text = "Health : " + health.ToString();
        playerTextActionPoint.text = "PA : " + playerActionPoint.ToString();
        playerTextMovementPoint.text = "PM : " + playerMovementPoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        playerTextName.text = playerName;
        playerTextHealth.text = "Health : " + health.ToString();
        playerTextActionPoint.text = "PA : " + playerActionPoint.ToString();
        playerTextMovementPoint.text = "PM : " + playerMovementPoint.ToString();

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    
}

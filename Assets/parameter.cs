using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class parameter : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text speedText;
    public TMP_Text pointText;

    private void Start()
    {
        GetStats();
    }

    private void Update()
    {
        GetStats();
    }
    public void AddHealth()
    {
        if(PlayerAttributes.points>0)
        {
            PlayerAttributes.health += 10;
            healthText.text = healthText.text.ToString();
            PlayerAttributes.points -= 1;
        }
    }

    public void AddSpeed()
    {
        if (PlayerAttributes.points > 0)
        {
            PlayerAttributes.speed += 1.0f;
            speedText.text = speedText.text.ToString();
            PlayerAttributes.points -= 1;
        }
    }

    public void GetStats()
    {
        int receivedHealthPoints = PlayerAttributes.health;
        float receivedSpeedValue = PlayerAttributes.speed;
        int receivedPoint = PlayerAttributes.points;

        healthText.text = receivedHealthPoints.ToString();
        speedText.text = receivedSpeedValue.ToString();
        pointText.text = receivedPoint.ToString();
    }

}


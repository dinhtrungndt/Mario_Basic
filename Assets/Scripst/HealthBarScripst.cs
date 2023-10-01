using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScripst : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health, bool isOnRight) 
    {
        slider.value = health;
        slider.direction = isOnRight ? Slider.Direction.LeftToRight : Slider.Direction.RightToLeft;
    }

    internal void SetHealth(int nowHealth)
    {
        throw new NotImplementedException();
    }
}

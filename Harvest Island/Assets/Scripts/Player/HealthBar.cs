using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float health = 100;

    public Slider Slider { get => slider; /*set => slider = value;*/ }
    public float Health { get => health; /*set => health = value;*/ }

    public void Start()
    {
        float distract = -10;
        ChangeHealth(distract);
    }
    public void ChangeHealth(float value)
    {
        health += value; 
        slider.value = health;
    }

}

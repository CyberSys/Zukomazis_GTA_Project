using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public float Health;
    public float MaxHealth;

    public float Hunger;
    public float Thirst;

    public float HungerRate;
    public float ThirstRate;

    public Slider HungerBar;
    public Slider ThirstBar;
    public Slider HealtBar;

    public Text HealthText;
    public Text HungerText;
    public Text ThirstText;


    void Start()
    {
        
    }


    void Update()
    {
        TextBarLink();
        Needs();
    }

    private void TextBarLink()
    {
        HealtBar.value = Health;
        HealthText.text = Health.ToString("f");
        HealtBar.maxValue = MaxHealth;

        ThirstBar.value = Thirst;
        HungerBar.value = Hunger;

        ThirstText.text = Thirst.ToString("f");
        HungerText.text = Hunger.ToString("f");


    }


    private void Needs()
    {
        
        Hunger -= HungerRate * Time.deltaTime;
        Thirst -= ThirstRate * Time.deltaTime;

        

        
    }
}

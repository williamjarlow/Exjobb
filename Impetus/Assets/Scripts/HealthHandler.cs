using UnityEngine;
using System.Collections;

public class HealthHandler : MonoBehaviour {
    public int maximumHealth;
    [HideInInspector]
    public float currentHealth;

    [SerializeField]
    Bar healthBar;

	// Use this for initialization
	void Start () {
        currentHealth = maximumHealth;
        healthBar.BarValue = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ChangeHealthValue(float damageValue, bool setValue = false)
    {
        if (setValue == false)
            currentHealth -= damageValue;
        else
            currentHealth = damageValue;
        healthBar.BarValue = currentHealth;
    }
}

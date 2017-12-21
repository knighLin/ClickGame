using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour {

    public Slider healthSlider;
    private int currentHealth;//如果沒有設值，一開始是0
    public float smooth = 5f;
    public bool Death
    {
        get
        {
            return currentHealth <= healthSlider.minValue;//如果小於最小值Death回傳true
        }
    }

    /*[ContextMenu("Test Value")]//測試
    private void TestHurt()
    {
        Init(100);
        Hurt(20);
    }*/
    public void Init(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;
        currentHealth = (int)healthSlider.maxValue;
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        currentHealth = (int)Mathf.Max(healthSlider.minValue, currentHealth);//當當前血量小於最小值，只會顯示最小值，不會變負數。(Math.Max->誰大回傳誰)
        
    }
	
	
	// Update is called once per frame
	void Update () {
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * smooth);

	}
}

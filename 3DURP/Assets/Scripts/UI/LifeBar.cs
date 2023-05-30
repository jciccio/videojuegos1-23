using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    [SerializeField] Slider LifeSlider;
    [SerializeField] HealthScriptableObject HealthSO;

    // Start is called before the first frame update
    void Start()
    {
        LifeSlider = GetComponent<Slider>();
        LifeSlider.value = HealthSO.Health/HealthSO.MaxHealth;
    }

    void OnEnable(){
        HealthSO.HealthEventChange.AddListener(SetLifeBar);
    }

    void OnDisable(){
        HealthSO.HealthEventChange.RemoveListener(SetLifeBar);
    }

    void SetLifeBar(float pts){
        LifeSlider.value = pts;
    }



 
}

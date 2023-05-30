using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HealthManagerScriptableObject", menuName ="Scriptable Objects/Health Manager")]
public class HealthScriptableObject : ScriptableObject
{
   
    public float Health;
    public float MaxHealth = 100f;

    [System.NonSerialized] public UnityEvent<float> HealthEventChange;

    private void OnEnable(){
        if(HealthEventChange == null){
            HealthEventChange = new UnityEvent<float>();
        }
    }

    private void OnDisable(){

    }

    public void SetHealth(float qty){
        Health = Mathf.Clamp(Health+qty, 0, MaxHealth);
        HealthEventChange?.Invoke(Health/MaxHealth);
    }


}

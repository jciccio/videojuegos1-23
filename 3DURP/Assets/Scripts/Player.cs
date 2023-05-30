using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] HealthScriptableObject HealthSO;

    public void AddLife(float pts){
        HealthSO.SetHealth(pts);
    }
}

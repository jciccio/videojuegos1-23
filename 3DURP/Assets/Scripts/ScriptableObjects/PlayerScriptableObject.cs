using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable Objects/Player Scriptable Object")]
public class PlayerScriptableObject : ScriptableObject
{
   
    public int Health;
    public float Speed;
    public int MaxHealth;

    private void Awake () => Debug.Log("Awake");
    private void OnEnable () => Debug.Log("On Enable");
    private void OnDisable () => Debug.Log("On Disable");
    private void OnDestroy () => Debug.Log("On Destroy");
    private void OnValidate () => Debug.Log("On Validate");

}

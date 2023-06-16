using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image currentHealth;
    public CanvasGroup fade;

    [SerializeField] private float defaultDelayTime = 4f;
    [SerializeField] private IEnumerator Routine = null;

    public void SetMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;  
        currentHealth.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health){
        if (fade != null){
            fade.alpha = 1f;
            if(Routine != null){
                StopCoroutine(Routine);
            }
            Routine = Fade( 1f,0f,5f);  
        }
     
        slider.value = health;
        currentHealth.color = gradient.Evaluate(slider.normalizedValue);

        if(fade != null)
            StartCoroutine(Routine);
    }

    IEnumerator Fade(float start, float end, float speed)
    {
        float delayTime = 0f;
        while (delayTime <= defaultDelayTime)
        {  
            delayTime += Time.deltaTime;
            yield return null;
        }
        fade.alpha = start;
        while (Mathf.Abs(fade.alpha - end) > 0.001f)
        {
            fade.alpha = Mathf.Lerp(fade.alpha, end, speed * Time.deltaTime);
            yield return null;
        }
        Routine = null;
        yield break;
    }
 
}
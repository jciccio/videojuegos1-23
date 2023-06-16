using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextFader : MonoBehaviour
{

    TMP_Text Renderer;
    Color ObjectColor;

    [SerializeField] [Range(0f,1f)] public float AlphaValue;
    [SerializeField] bool FadeIn;
    [SerializeField] bool FadeOut;
    [SerializeField] public float FadeSpeed = 0.1f;
    [SerializeField] public float MinFade = 0;
    [SerializeField] public float MaxFade =1;


    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<TMP_Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        float Alpha = AlphaValue;
        float newValue = 0;
        if(FadeIn){
            newValue = (FadeSpeed * Time.deltaTime);
            FadeIn = AlphaValue + newValue < MaxFade;
        }

        if(FadeOut){
            newValue = -(FadeSpeed * Time.deltaTime);
            FadeOut = AlphaValue + newValue > MinFade;
        }


        AlphaValue =  Mathf.Clamp(AlphaValue + newValue,MinFade,MaxFade);
        Renderer.color = new Color(ObjectColor.r, ObjectColor.g, ObjectColor.g, AlphaValue);

    }

    public void DoFadeIn(){
        FadeIn = true;
        FadeOut = false;
    }

    public void DoFadeOut(){
        FadeOut = true;
        FadeIn = false;
    }


}

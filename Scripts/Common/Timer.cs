using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI  timeText;

    [SerializeField] private float _elapsed;

    [SerializeField] public bool pause;

    [SerializeField] public float totalTime;

    // Start is called before the first frame update
    void Start()
    {
      
       _elapsed = 0;
       timeText.color = Color.white;
       timeText.fontSize = 26;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsed = pause ? _elapsed : _elapsed + Time.deltaTime;
        if (_elapsed < totalTime - 6 ){
            float remaining = totalTime - _elapsed;
            string minutes = ((int) remaining / 60).ToString("D2");
            string seconds = ((int) remaining % 60).ToString("D2");
            timeText.text = minutes + ":" + seconds;
        }
        else if (_elapsed < totalTime-1 && _elapsed > totalTime - 6){
            float remaining = totalTime - _elapsed;
            string seconds = ((int) remaining % 60).ToString();
            timeText.text = seconds;
            timeText.color = Color.yellow;
            timeText.fontSize = 75;
        }
        else{
            timeText.text = "Time!";
            timeText.color = Color.red;
        }
      
    }

    public bool IsFinished(){
        return _elapsed >= totalTime;
    }

    public void Pause(){
        pause = true;
    }

    public void Resume(){
        pause = false;
    }

    public void Reset(){
       _elapsed = 0;
       timeText.color = Color.white;
       timeText.fontSize = 26;
    }

    public void Start(float time){
       totalTime = time;
       Reset();
    }

}
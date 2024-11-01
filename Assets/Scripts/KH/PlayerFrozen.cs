using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerFrozen : MonoBehaviour
{
    [SerializeField] float maxTime = 960.0f;
    [SerializeField] float waitSecond = 3.0f;

    private float currentTime;
    private IEnumerator coroutine;
    // Start is called before the first frame update

    void Awake(){
        currentTime = 0;
    }
    void Start()
    {
        coroutine = progress();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator progress(){
        while(currentTime<maxTime){
            currentTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log(currentTime.ToString());
        }
        StartCoroutine(GameOver());
    }


    public IEnumerator GameOver(){
        yield return new WaitForSeconds(waitSecond);
        SceneManager.LoadScene("TempMain");
    }
}

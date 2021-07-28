using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public int scene;
    public float timer;
    public bool canSkip;
    // Start is called before the first frame update
    void Start()
    {
                Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0){
            SceneManager.LoadScene(scene);
        }
        if(canSkip){
             if(Input.GetMouseButtonDown(0)){
                SceneManager.LoadScene(scene);
             }
        }
    }
}

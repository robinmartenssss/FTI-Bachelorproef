using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{

    public static GameBehaviour  Instance;


    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneToMoveTo()
    {
        SceneManager.LoadScene("BasketCodeGame");
    }

     public void sceneToMoveBackTo()
    {
        SceneManager.LoadScene("MainAreaScene");
    }
}

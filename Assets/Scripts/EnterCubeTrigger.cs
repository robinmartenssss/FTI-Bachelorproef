using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCubeTrigger : MonoBehaviour
{
    public GameObject MakeWallVisible;
    // Start is called before the first frame update
    private void OnTriggerEnter(){
        MakeWallVisible.SetActive(true);
    }
}

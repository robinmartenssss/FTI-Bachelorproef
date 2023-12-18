using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{

    void Awake()
{
    Debug.Log("GameAssets Awake");
}

void Start()
{
    Debug.Log("GameAssets Start");
    // Your initialization code
}

   private static GameAssets _i;

   public static GameAssets i {
    get  {
        if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
        return _i;
    }
   }

   public Transform pfChatBubble;
}

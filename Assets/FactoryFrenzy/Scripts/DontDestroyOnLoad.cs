using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    //RelayExample relayExample;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(relayExample._joinCodeText);
        
    }
}

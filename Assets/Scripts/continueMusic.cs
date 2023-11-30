using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class continueMusic : MonoBehaviour
{
    //script to not desstroy music through game non stop
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

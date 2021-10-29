using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BsoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManager.PlayMusic("menu_ost");
    }
}

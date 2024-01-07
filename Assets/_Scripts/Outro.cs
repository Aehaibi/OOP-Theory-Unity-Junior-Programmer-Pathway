using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string whichLevel) 
    {
        SceneManager.LoadScene(whichLevel);
    }
}

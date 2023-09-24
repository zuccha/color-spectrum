using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    public string _sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(_sceneName);
    }
}

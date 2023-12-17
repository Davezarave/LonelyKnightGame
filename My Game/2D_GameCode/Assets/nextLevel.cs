using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    [SerializeField] private int levelInBuild;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(levelInBuild);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectLoad : MonoBehaviour
{
    [SerializeField] private int levelInBuild;
    public void StartLevel()
    {
        SceneManager.LoadScene(levelInBuild);

    }
}

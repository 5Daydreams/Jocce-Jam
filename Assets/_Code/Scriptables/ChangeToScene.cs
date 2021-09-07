using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SceneChange")]
public class ChangeToScene : ScriptableObject
{
    [SerializeField] private string _sceneToChange;

    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneToChange);
    }
}

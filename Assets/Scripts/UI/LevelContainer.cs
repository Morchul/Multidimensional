using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Levels", menuName = "Multidimensional/LevelContainer")]
public class LevelContainer : ScriptableObject
{
    [Scene]
    public string[] levels;
}

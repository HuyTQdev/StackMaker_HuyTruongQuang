using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObject/DataLevel")]
[System.Serializable]
public class DataLevel:ScriptableObject
{
    public string pathMap;
    public Vector2[] gemPos;

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObject/DataLevel")]
[System.Serializable]
public class DataLevel : ScriptableObject
{
    [System.Serializable]
    public class DataGem
    {
        [SerializeField] private Vector2 position;
        [SerializeField] private int value;
        [SerializeField] private bool isBonus;

        // Optionally, you can add properties to access these fields
        public Vector2 Position { get => position; set => position = value; }
        public int Value { get => value; set => value = value; }
        public bool IsBonus { get => isBonus; set => isBonus = value; }
    }

    public string pathMap;
    public DataGem[] gems;
}

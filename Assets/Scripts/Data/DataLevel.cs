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
        public Vector2 Position { get => position; set => this.position = Position; }
        public int Value { get => value; set => this.value = value; }
        public bool IsBonus { get => isBonus; set => this.isBonus = IsBonus; }
    }

    public string pathMap;
    public DataGem[] gems;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MapGenerator: Singleton<MapGenerator>
{
    [SerializeField] string[] nameObject;
    [SerializeField] float[] offset;
    [SerializeField] Vector2 baseBlock;
    [SerializeField] DataLevel[] levels;
    [SerializeField] Transform player;
    public Vector3 playerPos;
    List<List<int>> matrix;
    Vector3 tmpPos;
    public int curMap;

    /*private void Start()
    {
        StartGenerate();
    }*/
    public void StartGenerate()
    {
        if (PlayerPrefs.HasKey("CurLevel"))
        {
            curMap = PlayerPrefs.GetInt("CurLevel");
        }
        else curMap = 0;
        matrix = CSVReader.LoadCSV(levels[curMap].pathMap);
        for (int i = 0; i < matrix.Count; i++)
        {
            for(int j = 0; j < matrix[i].Count; j++)
            {
                int val = matrix[i][j];
                if (val != -1)
                {
                    tmpPos = Vector3.forward * baseBlock.y * i + Vector3.left * baseBlock.x * j 
                        + offset[val] * Vector3.up;
                    if (nameObject[val] == "StartPos")
                    {
                        player.position = tmpPos + 3 * Vector3.up;
                        playerPos = tmpPos + 3 * Vector3.up;
                    }
                    ObjectPool.instance.GetObject(nameObject[val], tmpPos);
                }
            }
        }
        for (int i = 0; i < levels[curMap].gems.Length; i++)
        {
            tmpPos = Vector3.forward * baseBlock.y * levels[curMap].gems[i].Position.y
            + Vector3.left * baseBlock.x * levels[curMap].gems[i].Position.x + 3.5f * Vector3.up;
            GameObject go = ObjectPool.instance.GetObject("Gem", tmpPos);
            go.GetComponent<GemScript>().Init(levels[curMap].gems[i].Value, levels[curMap].gems[i].IsBonus);
        }
    }
    
}

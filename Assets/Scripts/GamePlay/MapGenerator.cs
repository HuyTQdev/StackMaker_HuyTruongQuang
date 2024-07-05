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
    [SerializeField] Transform winPos;
    public Vector3 playerPos;
    List<List<int>> matrix;
    Vector3 tmpPos;

    public void StartGenerate()
    {
        int curMap = 0;
        matrix = CSVReader.LoadCSV(levels[curMap].pathMap, levels[curMap].mapSize);
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
        tmpPos = Vector3.forward * baseBlock.y * levels[curMap].winPos.y
            + Vector3.left * baseBlock.x * levels[curMap].winPos.x + 3.5f * Vector3.up;
        winPos.position = tmpPos;
        for (int i = 0; i < levels[curMap].gemPos.Length; i++)
        {
            tmpPos = Vector3.forward * baseBlock.y * levels[curMap].gemPos[i].y
            + Vector3.left * baseBlock.x * levels[curMap].gemPos[i].x + 3.5f * Vector3.up;
            ObjectPool.instance.GetObject("Gem", tmpPos);
        }
    }
    
}

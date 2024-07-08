using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public string filePath = "Data/Map/Map_1.csv"; // Path to the CSV file

    void Start()
    {
        List<List<int>> matrix = LoadCSV(filePath);
        // Output to debug the matrix content
        foreach (List<int> row in matrix)
        {
            Debug.Log(string.Join(", ", row));
        }
    }

    public static List<List<int>> LoadCSV(string relativePath)
    {
        // Combine the relative path with the data path
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        if (!File.Exists(fullPath))
        {
            Debug.LogError("File not found: " + fullPath);
            return new List<List<int>>(); // Return an empty list to avoid further errors
        }

        List<List<int>> matrix = new List<List<int>>();
        string[] lines = File.ReadAllLines(fullPath);

        foreach (string line in lines)
        {
            List<int> row = new List<int>();
            string[] entries = line.Split(',');
            foreach (string entry in entries)
            {
                if (int.TryParse(entry.Trim(), out int value))
                {
                    row.Add(value);
                }
                else
                {
                    row.Add(-1); // Add -1 for missing or invalid data
                }
            }
            matrix.Add(row);
        }

        return matrix;
    }
}

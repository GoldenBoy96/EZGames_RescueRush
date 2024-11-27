using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    [Header("Level Infor")]
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private int width = 100;
    [Tooltip("Use to generate level if random attribute is true")]
    [SerializeField] private string seed;

    [Header("Phase 0")]
    [SerializeField] private Spawner heroSpawnPoint = new(new Vector3(50, 0, 0), new Vector3(5, 0 , 5));
    [SerializeField] private int tsunamiStartDistance;
    [SerializeField] private int tsunamiSpeed_0;

    [Header("Phase 1")]
    [SerializeField] private int finishLineDistance_1; //From hero spawn point y
    [SerializeField] private bool isRandomCatSpawn_1;
    [SerializeField] private List<Spawner> catFixedSpawnPoint_1;
    [SerializeField] private int numberCatRandomSpawn_1 = 5;
    [SerializeField] private bool isRandomObstacleSpawn_1;
    [SerializeField] private List<Spawner> obstacleFixedSpawnPoint_1;
    [SerializeField] private int numberObstacleRandomSpawn_1 = 5;

    [Header("Phase 2")]
    [SerializeField] private int finishLineDistance_2; // From finish line 1
    [SerializeField] private bool isRandomCatSpawn_2;
    [SerializeField] private List<Spawner> catFixedSpawnPoint_2;
    [SerializeField] private int numberCatRandomSpawn_2 = 5;
    [SerializeField] private bool isRandomObstacleSpawn_2;
    [SerializeField] private List<Spawner> obstacleFixedSpawnPoint_2;
    [SerializeField] private int numberObstacleRandomSpawn_2 = 5;

    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    public int Width { get => width; set => width = value; }
    public string Seed { get => seed; set => seed = value; }
    public Spawner HeroSpawnPoint { get => heroSpawnPoint; set => heroSpawnPoint = value; }
    public int TsunamiStartDistance { get => tsunamiStartDistance; set => tsunamiStartDistance = value; }
    public int TsunamiSpeed_0 { get => tsunamiSpeed_0; set => tsunamiSpeed_0 = value; }
    public int FinishLineDistance_1 { get => finishLineDistance_1; set => finishLineDistance_1 = value; }
    public bool IsRandomCatSpawn_1 { get => isRandomCatSpawn_1; set => isRandomCatSpawn_1 = value; }
    public List<Spawner> CatFixedSpawnPoint_1 { get => catFixedSpawnPoint_1; set => catFixedSpawnPoint_1 = value; }
    public int NumberCatRandomSpawn_1 { get => numberCatRandomSpawn_1; set => numberCatRandomSpawn_1 = value; }
    public int FinishLineDistance_2 { get => finishLineDistance_2; set => finishLineDistance_2 = value; }
    public bool IsRandomCatSpawn_2 { get => isRandomCatSpawn_2; set => isRandomCatSpawn_2 = value; }
    public List<Spawner> CatFixedSpawnPoint_2 { get => catFixedSpawnPoint_2; set => catFixedSpawnPoint_2 = value; }
    public int NumberCatRandomSpawn_2 { get => numberCatRandomSpawn_2; set => numberCatRandomSpawn_2 = value; }

    public List<Spawner> GetCatSpawnerList()
    {
        GenerateCatRandomSpawnPoint();
        return CatFixedSpawnPoint_1.Concat(CatFixedSpawnPoint_2).ToList();
    }
    private void GenerateCatRandomSpawnPoint()
    {
        if (IsRandomCatSpawn_1)
        {
            CatFixedSpawnPoint_1.Clear();
            if (NumberCatRandomSpawn_1 <= 0)
            {
                NumberCatRandomSpawn_1 = 5;
            }
            for (int i = 0; i < NumberCatRandomSpawn_1; i++)
            {
                //Generate spawn point coordinate for phase 1
            }
        }

        //Same same for phase 2
    }

    public int GetLength()
    {
        return finishLineDistance_1 + finishLineDistance_2;
    }

    internal List<Spawner> GetObstacleSpawnerList()
    {
        GenerateObstacleRandomSpawnPoint();
        return obstacleFixedSpawnPoint_1.Concat(obstacleFixedSpawnPoint_2).ToList();
    }

    public void GenerateObstacleRandomSpawnPoint()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    [Header("Level Infor")]
    [SerializeField] private int levelIndex = 0;
    [SerializeField] private int width = 100;
    [Tooltip("Use to generate level if random attribute is true")]
    [SerializeField] private string seed;
    [SerializeField] private float boostSpeedAmount = 1;
    [SerializeField] private float reduceSpeedAmount = 1;
    [SerializeField] private float reduceSpeedDeltaTime = 1;

    [Header("Phase prepare")]
    [SerializeField] private int prepareTime = 0;

    [Header("Phase 0")]
    [SerializeField] private Spawner heroSpawnPoint = new(new Vector3(50, 0, 0), 5);
    [SerializeField] private int tsunamiStartDistance;

    [Header("Phase 1")]
    [SerializeField] private int finishLineDistance_1; //From hero spawn point 
    [SerializeField] private bool isRandomCatSpawn_1;
    [SerializeField] private int numberCatRandomSpawn_1 = 5;
    [SerializeField] private List<Spawner> catFixedSpawnPoint_1;
    [SerializeField] private bool isRandomObstacleSpawn_1;
    [SerializeField] private int numberObstacleRandomSpawn_1 = 5;
    [SerializeField] private List<Spawner> obstacleFixedSpawnPoint_1;

    [Header("Phase 2")]
    [SerializeField] private int finishLineDistance_2; // From finish line 1
    [SerializeField] private bool isRandomCatSpawn_2;
    [SerializeField] private int numberCatRandomSpawn_2 = 5;
    [SerializeField] private List<Spawner> catFixedSpawnPoint_2;
    [SerializeField] private bool isRandomObstacleSpawn_2;
    [SerializeField] private int numberObstacleRandomSpawn_2 = 5;
    [SerializeField] private List<Spawner> obstacleFixedSpawnPoint_2;

    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    public int Width { get => width; set => width = value; }
    public string Seed { get => seed; set => seed = value; }
    public float BoostSpeedAmount { get => boostSpeedAmount; set => boostSpeedAmount = value; }
    public float ReduceSpeedAmount { get => reduceSpeedAmount; set => reduceSpeedAmount = value; }
    public float ReduceSpeedDeltaTime { get => reduceSpeedDeltaTime; set => reduceSpeedDeltaTime = value; }
    public int PrepareTime { get => prepareTime; set => prepareTime = value; }
    public Spawner HeroSpawnPoint { get => heroSpawnPoint; set => heroSpawnPoint = value; }
    public int TsunamiStartDistance { get => tsunamiStartDistance; set => tsunamiStartDistance = value; }
    public int FinishLineDistance_1 { get => finishLineDistance_1; set => finishLineDistance_1 = value; }
    public bool IsRandomCatSpawn_1 { get => isRandomCatSpawn_1; set => isRandomCatSpawn_1 = value; }
    public List<Spawner> CatFixedSpawnPoint_1 { get => catFixedSpawnPoint_1; set => catFixedSpawnPoint_1 = value; }
    public int NumberCatRandomSpawn_1 { get => numberCatRandomSpawn_1; set => numberCatRandomSpawn_1 = value; }
    public bool IsRandomObstacleSpawn_1 { get => isRandomObstacleSpawn_1; set => isRandomObstacleSpawn_1 = value; }
    public List<Spawner> ObstacleFixedSpawnPoint_1 { get => obstacleFixedSpawnPoint_1; set => obstacleFixedSpawnPoint_1 = value; }
    public int NumberObstacleRandomSpawn_1 { get => numberObstacleRandomSpawn_1; set => numberObstacleRandomSpawn_1 = value; }
    public int FinishLineDistance_2 { get => finishLineDistance_2; set => finishLineDistance_2 = value; }
    public bool IsRandomCatSpawn_2 { get => isRandomCatSpawn_2; set => isRandomCatSpawn_2 = value; }
    public List<Spawner> CatFixedSpawnPoint_2 { get => catFixedSpawnPoint_2; set => catFixedSpawnPoint_2 = value; }
    public int NumberCatRandomSpawn_2 { get => numberCatRandomSpawn_2; set => numberCatRandomSpawn_2 = value; }
    public bool IsRandomObstacleSpawn_2 { get => isRandomObstacleSpawn_2; set => isRandomObstacleSpawn_2 = value; }
    public List<Spawner> ObstacleFixedSpawnPoint_2 { get => obstacleFixedSpawnPoint_2; set => obstacleFixedSpawnPoint_2 = value; }
    public int NumberObstacleRandomSpawn_2 { get => numberObstacleRandomSpawn_2; set => numberObstacleRandomSpawn_2 = value; }

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

            float distance = finishLineDistance_1 / numberCatRandomSpawn_1;
            float z = 0;
            for (int i = 0; i < NumberCatRandomSpawn_1; i++)
            {
                //Generate spawn point coordinate for phase 1
                z += distance;
                int randomX = Random.Range(0, width);
                CatFixedSpawnPoint_1.Add(new(
                        new Vector3(randomX, 0, z),
                        10
                    ));
            }
        }

        if (IsRandomCatSpawn_2)
        {
            CatFixedSpawnPoint_2.Clear();
            if (NumberCatRandomSpawn_2 <= 0)
            {
                NumberCatRandomSpawn_2 = 5;
            }

            float distance = finishLineDistance_2 / numberCatRandomSpawn_2;
            float z = finishLineDistance_1;
            for (int i = 0; i < NumberCatRandomSpawn_2; i++)
            {
                //Generate spawn point coordinate for phase 1
                z += distance;
                int randomX = Random.Range(0, width);
                CatFixedSpawnPoint_2.Add(new(
                        new Vector3(randomX, 0, z),
                        10
                    ));
            }
        }
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
        if (IsRandomObstacleSpawn_1)
        {
            ObstacleFixedSpawnPoint_1.Clear();
            if (NumberObstacleRandomSpawn_1 <= 0)
            {
                NumberObstacleRandomSpawn_1 = 5;
            }

            float distance = finishLineDistance_1 / numberObstacleRandomSpawn_1;
            float z = 0;
            for (int i = 0; i < NumberObstacleRandomSpawn_1; i++)
            {
                //Generate spawn point coordinate for phase 1
                z += distance;
                int randomX = Random.Range(0, width);
                ObstacleFixedSpawnPoint_1.Add(new(
                        new Vector3(randomX, 0, z),
                        10
                    ));
            }
        }
        if (IsRandomObstacleSpawn_2)
        {
            ObstacleFixedSpawnPoint_2.Clear();
            if (NumberObstacleRandomSpawn_2 <= 0)
            {
                NumberObstacleRandomSpawn_2 = 5;
            }

            float distance = finishLineDistance_2 / numberObstacleRandomSpawn_2;
            float z = finishLineDistance_1;
            for (int i = 0; i < NumberObstacleRandomSpawn_2; i++)
            {
                //Generate spawn point coordinate for phase 1
                z += distance;
                int randomX = Random.Range(0, width);
                ObstacleFixedSpawnPoint_1.Add(new(
                        new Vector3(randomX, 0, z),
                        10
                    ));
            }
        }
    }
}

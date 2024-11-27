using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] Level level;

    [Header("Prefab")]
    [SerializeField] GameObject Hero;
    [SerializeField] GameObject Cat;
    [SerializeField] GameObject Obstacle;
    [SerializeField] GameObject Ground;
    [SerializeField] GameObject Tsunami;

    [Header("Game Data")]
    [SerializeField] List<GameObject> CatList;
    [SerializeField] List<GameObject> ObstacleList;

    [Header("Parent")]
    [SerializeField] Transform Parent;



    private void Start()
    {
        GameController.Instance.RegisterController(this);

        GenerateLevel();
        //if (Parent == null) Parent = transform.root;
    }

    public void GenerateLevel()
    {
        GenerateGround();
        GenerateHero();
        GenerateCat();
        GenerateObstacle();
        GenerateTsunami();
    }

    private void GenerateHero()
    {
        Hero = Instantiate(Hero, Parent);
        Hero.transform.position = level.HeroSpawnPoint.SpawnCoordination;
        //Hero.transform.parent = Parent;
        //Debug.Log(Hero.transform.parent + " | " + Parent);
        //Hero.transform.position = level.HeroSpawnPoint.SpawnCoordination;
    }

    private void GenerateCat()
    {
        CatList.Clear();
        List<Spawner> catSpawners = level.GetCatSpawnerList();
        foreach (Spawner catSpawner in catSpawners)
        {
            int randomX = (int)Random.Range(-catSpawner.SpawnCoordination.x, catSpawner.SpawnCoordination.x + 1);
            int randomY = (int)Random.Range(-catSpawner.SpawnCoordination.y, catSpawner.SpawnCoordination.y + 1);
            int randomZ = (int)Random.Range(-catSpawner.SpawnCoordination.z, catSpawner.SpawnCoordination.z + 1);
            int x = (int)(catSpawner.SpawnCoordination.x + randomX);
            if (x < 0) { x = 0; }
            if (x > level.Width) { x = level.Width; }
            int y = (int)catSpawner.SpawnCoordination.y + randomY;
            int z = (int)catSpawner.SpawnCoordination.z + randomZ;
            if (z < 0) { z = 0; }
            if (z > level.GetLength()) { z = level.GetLength(); }
            Vector3 spawnCoordination = new Vector3(x, y, z);
            GameObject catObject = Instantiate(Cat, spawnCoordination, Quaternion.identity);
            catObject.transform.parent = Parent;
            CatList.Add(catObject);
        }
    }

    private void GenerateObstacle()
    {
        ObstacleList.Clear();
        List<Spawner> obstacleSpawners = level.GetObstacleSpawnerList();
        foreach (Spawner obstancleSpawner in obstacleSpawners)
        {
            int randomX = (int)Random.Range(-obstancleSpawner.SpawnCoordination.x, obstancleSpawner.SpawnCoordination.x + 1);
            int randomY = (int)Random.Range(-obstancleSpawner.SpawnCoordination.y, obstancleSpawner.SpawnCoordination.y + 1);
            int randomZ = (int)Random.Range(-obstancleSpawner.SpawnCoordination.z, obstancleSpawner.SpawnCoordination.z + 1);
            int x = (int)(obstancleSpawner.SpawnCoordination.x + randomX);
            if (x < 0) { x = 0; }
            if (x > level.Width) { x = level.Width; }
            int y = (int)obstancleSpawner.SpawnCoordination.y + randomY;
            int z = (int)obstancleSpawner.SpawnCoordination.z + randomZ;
            if (z < 0) { z = 0; }
            if (z > level.GetLength()) { z = level.GetLength(); }
            Vector3 spawnCoordination = new Vector3(x, y, z);
            GameObject catObject = Instantiate(Cat, spawnCoordination, Quaternion.identity);
            catObject.transform.parent = Parent;
            ObstacleList.Add(catObject);
        }
    }

    private void GenerateGround()
    {
        GameObject ground = Instantiate(Ground);
        ground.transform.parent = Parent;
        ground.transform.localScale = new Vector3(level.Width, 1, level.GetLength());
        ground.transform.position = new Vector3(level.Width / 2, -1, level.GetLength() / 2);
    }

    private void GenerateTsunami()
    {
        GameObject tsunami = Instantiate(Tsunami);
        tsunami.transform.parent = Parent;
        tsunami.TryGetComponent(out TsunamiController tsunamiController);
        Debug.Log(tsunamiController.gameObject.name);
        tsunami.transform.localScale = new Vector3(tsunamiController.Tsunami.Width, tsunamiController.Tsunami.Height, 10);
        tsunami.transform.position = new Vector3(level.Width / 2, 0, -tsunamiController.Tsunami.StartDistance);
    }
}

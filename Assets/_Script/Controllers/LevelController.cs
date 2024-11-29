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
    [SerializeField] GameObject Border;

    [Header("Game Data")]
    [SerializeField] List<GameObject> CatList;
    [SerializeField] List<GameObject> ObstacleList;

    [Header("Parent")]
    [SerializeField] Transform Parent;

    public Level Level { get => level; set => level = value; }

    private void Start()
    {
        GameController.Instance.RegisterController(this);
        StartCoroutine(GenerateLevel());
        //if (Parent == null) Parent = transform.root;
    }

    public IEnumerator GenerateLevel()
    {
        yield return new WaitUntil(() => Hero != null);
        GenerateGround();
        GenerateCat();
        GenerateObstacle();
        GenerateTsunami();
        GenerateHero();
    }

    private void GenerateHero()
    {
        Hero = Instantiate(Hero, Parent);
        Hero.transform.position = Level.HeroSpawnPoint.SpawnCoordination;
        //Debug.Log("Hero Spawn Coordination: " + Level.HeroSpawnPoint.SpawnCoordination);
        //Hero.transform.parent = Parent;
        //Debug.Log(Hero.transform.parent + " | " + Parent);
        //Hero.transform.position = level.HeroSpawnPoint.SpawnCoordination;
    }

    private void GenerateCat()
    {
        CatList.Clear();
        List<Spawner> catSpawners = Level.GetCatSpawnerList();
        foreach (Spawner catSpawner in catSpawners)
        {
            int randomX = (int)Random.Range(-catSpawner.Radius, catSpawner.Radius + 1);
            int randomY = 0;
            int randomZ = (int)Random.Range(-catSpawner.Radius, catSpawner.Radius + 1);
            int x = (int)(catSpawner.SpawnCoordination.x + randomX);
            if (x < 0) { x = 0; }
            if (x > Level.Width) { x = Level.Width; }
            int y = (int)catSpawner.SpawnCoordination.y + randomY;
            int z = (int)catSpawner.SpawnCoordination.z + randomZ;
            if (z < 0) { z = 0; }
            if (z > Level.GetLength()) { z = Level.GetLength(); }
            Vector3 spawnCoordination = new Vector3(x, y, z);
            GameObject catObject = Instantiate(Cat, spawnCoordination, Quaternion.identity);
            catObject.transform.parent = Parent;
            CatList.Add(catObject);
        }
    }

    private void GenerateObstacle()
    {
        ObstacleList.Clear();
        List<Spawner> obstacleSpawners = Level.GetObstacleSpawnerList();
        foreach (Spawner obstacleSpawner in obstacleSpawners)
        {
            int randomX = (int)Random.Range(-obstacleSpawner.Radius, obstacleSpawner.Radius + 1);
            int randomY = 0;
            int randomZ = (int)Random.Range(-obstacleSpawner.Radius, obstacleSpawner.Radius + 1);
            int x = (int)(obstacleSpawner.SpawnCoordination.x + randomX);
            if (x < 0) { x = 0; }
            if (x > Level.Width) { x = Level.Width; }
            int y = (int)obstacleSpawner.SpawnCoordination.y + randomY;
            int z = (int)obstacleSpawner.SpawnCoordination.z + randomZ;
            if (z < 0) { z = 0; }
            if (z > Level.GetLength()) { z = Level.GetLength(); }
            Vector3 spawnCoordination = new Vector3(x, y, z);
            GameObject obstacleObject = Instantiate(Obstacle, spawnCoordination, Quaternion.identity);
            obstacleObject.transform.parent = Parent;
            //obstacleObject.TryGetComponent(out ObstacleController obstacleController);
            //Debug.Log(obstacleController.ToString());
            ObstacleList.Add(obstacleObject);
        }
    }

    private void GenerateGround()
    {
        GameObject ground = Instantiate(Ground);
        ground.transform.parent = Parent;
        ground.transform.localScale = new Vector3(Level.Width, 1, Level.GetLength());
        ground.transform.position = new Vector3(Level.Width / 2, -1, Level.GetLength() / 2);

        GameObject border_1 = Instantiate(Border);
        border_1.transform.parent = Parent;
        border_1.transform.localScale = new Vector3(1, 100, Level.GetLength());
        border_1.transform.position = new Vector3(-1, 0, Level.GetLength() / 2);

        GameObject border_2 = Instantiate(Border);
        border_2.transform.parent = Parent;
        border_2.transform.localScale = new Vector3(1, 100, Level.GetLength());
        border_2.transform.position = new Vector3(Level.Width + 1, 0, Level.GetLength() / 2);

        GameObject border_3 = Instantiate(Border);
        border_3.transform.parent = Parent;
        border_3.transform.localScale = new Vector3(Level.Width, 100, 1);
        border_3.transform.position = new Vector3(Level.Width / 2, 0, -1);

        GameObject border_4 = Instantiate(Border);
        border_4.transform.parent = Parent;
        border_4.transform.localScale = new Vector3(Level.Width, 100, 1);
        border_4.transform.position = new Vector3(Level.Width / 2, 0, Level.GetLength() + 1);
    }


    private void GenerateTsunami()
    {
        GameObject tsunami = Instantiate(Tsunami);
        tsunami.transform.parent = Parent;
        tsunami.TryGetComponent(out TsunamiController tsunamiController);
        tsunami.transform.localScale = new Vector3(tsunamiController.Tsunami.Width, tsunamiController.Tsunami.Height, 500);
        tsunami.transform.position = new Vector3(Level.Width / 2, 0, -tsunamiController.Tsunami.StartDistance - 500);
    }

    public float GetFinishLineDistance()
    {
        return level.FinishLineDistance_1 + level.FinishLineDistance_2;
    }
}

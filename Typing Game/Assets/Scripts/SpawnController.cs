using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    public enum EnemyTypes
    {
        melee,
        ranged,
        Boss
    }
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject bossEnemy;

    public GameObject spawn1;
    public GameObject spawn2;

    private int SpawnId;
    public int spawnLocId;
    public bool Spawn = true;
    private int numEnemies;
    public int maxEnemiesOnScreen;

    public float spawnTimer;
    public float timeTillSpawn;

    //tracks what level we are on so it knows what enemies to release
    private int levelNumber;

    public GameObject [] enemies;

    public TextAsset dictionaryTextFile;
    private string theWholeFileAsOneLongString;
    private List<string> eachLine;

    public Input playerInput;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0;
        maxEnemiesOnScreen = 4;
        spawnTimer = 2f;
        timeTillSpawn = 9.0f;
        levelNumber = 0;


        SpawnId = Random.Range(1, 500);
        theWholeFileAsOneLongString = dictionaryTextFile.text;

        eachLine = new List<string>();
        eachLine.AddRange(theWholeFileAsOneLongString.Split("\n"[0]));
    }

    // Update is called once per frame
    void Update()
    {
        spawnLocId = Random.Range(1, 10) % 2;
        if (Spawn)
        {
            timeTillSpawn += Time.deltaTime;
            if (timeTillSpawn >= spawnTimer)
            {
                // checks to see if the number of spawned enemies is less than the max num of enemies
                if (numEnemies < maxEnemiesOnScreen)
                {
                    print(maxEnemiesOnScreen);
                    // spawns an enemy
                    spawnEnemy(spawnLocId);
                    numEnemies++;
                    timeTillSpawn = 0.0f;
                }
                else
                {
                    Spawn = false;
                }
            }
        }
    }

    void spawnEnemy(int spawnLocId)
    {
        GameObject spawner;
        bool flip = true;
        if (spawnLocId == 1)
        {
            spawner = spawn1;
        }
        else
        {
            flip = false;
            spawner = spawn2;
        }
        if (levelNumber == 0)
        {
            GameObject Enemy = (GameObject) Instantiate(meleeEnemy, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
    public string ReturnWord(int level)
    {
        bool rightWord = false;
        string w = null;
        while (!rightWord)
        {
            if (level == 1)
            {
                int wordId = Random.Range(4, 466000);
                try
                {

                    if (eachLine[wordId].Length-1 < 5 && eachLine[wordId].Length-1 > 3)
                    {
                        rightWord = true;
                        w = eachLine[wordId];
                        w = w.Replace("\n", "").Replace("\r", "");
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
        }
        return w;
    }

    public void KillEnemy()
    {
        numEnemies--;
    }
    public void AttemptedKill(string arg0)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int x = 0; x <enemies.Length; x++)
        {
           
            EnemyControll enemy = enemies[x].GetComponent<EnemyControll>();
            if (arg0.Equals(enemy.ReturnWord()))
            {
                enemy.Die();
                KillEnemy();

            }
        }
    }
}

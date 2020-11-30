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
    public GameObject wraith;
    public GameObject skeleton;
    public GameObject bossEnemy;

    public GameObject numEnemiesObj;
    public GameObject levelNumObj;


    public GameObject spawn1;
    public GameObject spawn2;

    private int SpawnId;
    public int spawnLocId;
    public bool Spawn = true;
    private int numEnemies;
    public int maxEnemiesOnScreen;
    public int enemiesKilled;
    public int enemiesLeft;

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
        enemiesKilled = 0;
        numEnemies = 0;
        maxEnemiesOnScreen = 7;
        spawnTimer = 2f;
        timeTillSpawn = 9.0f;
        levelNumber = 1;
        enemiesLeft = maxEnemiesOnScreen - enemiesKilled;

        SpawnId = Random.Range(1, 500);
        theWholeFileAsOneLongString = dictionaryTextFile.text;

        eachLine = new List<string>();
        eachLine.AddRange(theWholeFileAsOneLongString.Split("\n"[0]));

        numEnemiesObj.GetComponent<UnityEngine.UI.Text>().text = enemiesLeft.ToString() + " Enemies left";
        levelNumObj.GetComponent<UnityEngine.UI.Text>().text = "Level "+levelNumber.ToString() ;
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
        enemiesLeft = maxEnemiesOnScreen - enemiesKilled;
        numEnemiesObj.GetComponent<UnityEngine.UI.Text>().text = enemiesLeft.ToString() + " Enemies left";
        levelNumObj.GetComponent<UnityEngine.UI.Text>().text = "Level " + levelNumber.ToString();
        if (enemiesLeft == 0)
        {
            levelNumber++;
            enemiesKilled = 0;
            numEnemies = 0;

            //this means maxEnemies will always increase by 5 when a level is finished, regardless of level. Probably just temporary for now given we may want more control over it. 
            maxEnemiesOnScreen += 5;
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
        if (levelNumber == 1)
        {
            var ranNum = Random.Range(0, 2);
            GameObject Enemy = ranNum == 0
                ? (GameObject)Instantiate(wraith, spawner.transform.position, Quaternion.identity)
                : (GameObject)Instantiate(skeleton, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        if (levelNumber == 2)
        {
            var ranNum = Random.Range(0, 2);
            GameObject Enemy = ranNum == 0
                ? (GameObject)Instantiate(wraith, spawner.transform.position, Quaternion.identity)
                : (GameObject)Instantiate(skeleton, spawner.transform.position, Quaternion.identity);
            if (flip)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
    public string ReturnWord()
    {
        bool rightWord = false;
        string w = null;
        while (!rightWord)
        {
            if (levelNumber == 1)
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
            else if (levelNumber == 2)
            {
                int wordId = Random.Range(4, 466000);
                try
                {

                    if (eachLine[wordId].Length - 1 < 8 && eachLine[wordId].Length - 1 > 4)
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
        enemiesKilled++;
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

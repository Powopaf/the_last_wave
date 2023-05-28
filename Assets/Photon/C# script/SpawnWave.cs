using System;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Photon.C__script
{
    public class SpawnWave : MonoBehaviour
    {
        public List<Enemy> Enemies = new List<Enemy>();
        private int currentWave;
        private int waveValue;
        private int currentValue;
        private int waveValueScale;
        public List<string> enemiesToSpawn = new List<string>();
        public List<Transform> SpawnPossition = new List<Transform>();
        public int waveDuration;
        private float waveTimer;
        private float spawnInterval;
        private float spawnTimer;
        private int playerCount;
        private int bossNumber;

        public void Start()
        {
            currentWave = 1;
            bossNumber = 1;
            playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            switch (playerCount)
            {
                case 2:
                    waveValueScale = 13;
                    break;
                case 3:
                    waveValueScale = 16;
                    break;
                case 4:
                    waveValueScale = 20;
                    break;
                default:
                    waveValueScale = 10;
                    break;
            }
            waveValue = waveValueScale;
            currentValue = waveValueScale;
            GenerateWave();
        }

        private void FixedUpdate()
        {
            if (spawnTimer <= 0)
            {
                if (enemiesToSpawn.Count > 0)
                {
                    PhotonNetwork.Instantiate(enemiesToSpawn[0], SpawnPossition[Random.Range(0,8)].position , quaternion.identity);
                    enemiesToSpawn.RemoveAt(0);
                    spawnTimer = spawnInterval;
                }
                else
                {
                    waveTimer = 0;
                }
            }
            else
            {
                spawnTimer -= Time.fixedDeltaTime;
                waveTimer -= Time.fixedDeltaTime;
            }
        }

        public void GenerateWave()
        {
            currentValue = waveValue;
            ///waveValue = curreentWave * 10;
            GenerateEnemies();
            currentWave += 1;
            if (currentWave >= 10)
            {
                waveValue *= 15 * waveValueScale / 100;
            }
            else
            {
                waveValue = (waveValue + 5) * waveValueScale / 10;
            }

            spawnInterval = waveDuration / enemiesToSpawn.Count;
            waveTimer = waveDuration;
        }

        public void GenerateEnemies()
        {
            List<string> generateEnemies = new List<string>();
            while (currentValue > 0)
            {
                int randEnemyID = Random.Range(0, Enemies.Count);
                int randEnemyCost = Enemies[randEnemyID].cost;

                if (currentValue - randEnemyCost >= 0)
                {
                    generateEnemies.Add(Enemies[randEnemyID].enemyPrefab.name);
                    currentValue -= randEnemyCost;
                }
                else if (currentValue <= 0)
                {
                    break;
                }
            }
            enemiesToSpawn.Clear();
            enemiesToSpawn = generateEnemies;
            if (currentWave % 3 == 0 && currentWave != 1)
            {
                for (int i = 0; i < bossNumber; i++)
                {
                    enemiesToSpawn.Add("Zombie4");
                }
                bossNumber += 1;
            }
        }
    }
}

[Serializable]public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
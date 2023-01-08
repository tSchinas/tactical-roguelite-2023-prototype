//using System.Collections;
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using Random = UnityEngine.Random;
//namespace Completed
//{
//    public class BoardController : MonoBehaviour
//    {
//        [Serializable]
//        public class Counter
//        {
//            public int minimum;
//            public int maximum;

//            public Counter (int min, int max)
//            {
//                minimum = min;
//                maximum = max;
//            }
//        }

//        public int columns = 10;
//        public int rows = 10;
//        public Counter gapCounter = new Counter(4, 9);
//        public GameObject[] floorTiles;
//        public GameObject[] enemyTiles;
//        public GameObject[] allyUnits;
//        public GameObject[] enemyUnits;

//        private int enemyCount;
//        private int allyCount;
//        private Transform boardHolder;
//        private Transform floorHolder;
//        private List<Vector3> gridPositions = new();
//        private List<Vector3> enemyPositions = new();
//        private List<Vector3> allyPositions = new();

//        void InitializeList()//clears list gridPositions and prepares it to generate new board
//        {
//            gridPositions.Clear();

//            for(int x = 1; x < columns - 1; x++)
//            {
//                for(int z = 1; z < rows - 1; z++)
//                {
//                    gridPositions.Add(new Vector3(x, 0f, z));
//                }
//            }
//            for (int x = 1; x < columns - 1; x++)
//            {
//                for(int z = 1; z < rows-1;z++)
//                {
//                    enemyPositions.Add(new Vector3(x, 0f, z));
//                }
//            }
//        }
//        public void SetupScene(int level)
//        {
//            BoardSetup();
//            InitializeList();
//            LayoutAlliesAtRandom(allyUnits, allyCount, allyCount);
//            LayoutEnemyAtRandom(enemyUnits, enemyCount, enemyCount);
//        }
//        void BoardSetup()
//        {
//            boardHolder = new GameObject("Board").transform;
//            floorHolder = new GameObject("Floor").transform;

//            floorHolder.transform.SetParent(boardHolder);

//            for (int x=-1;x<columns+1;x++)
//            {
//                for (int z = -1; z<columns+1;z++)
//                {
//                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
//                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
//                    instance.transform.SetParent(floorHolder);
//                }
//            }
//        }

//        Vector3 RandomPosition()
//        {
//            int randomIndex = Random.Range(0, gridPositions.Count);
//            Vector3 randomPosition = gridPositions[randomIndex];
//            gridPositions.RemoveAt(randomIndex);
//            return randomPosition;
//        }

//        Vector3 EnemyRandomPosition()
//        {
//            int randomIndex = Random.Range(0, enemyPositions.Count);
//            Vector3 randomPosition = enemyPositions[randomIndex];
//            enemyPositions.RemoveAt(randomIndex);
//            return randomPosition;
//        }
//        Vector3 AllyRandomPosition()
//        {
//            int randomIndex = Random.Range(0, allyPositions.Count);
//            Vector3 randomPosition = allyPositions[randomIndex];
//            allyPositions.RemoveAt(randomIndex);
//            return randomPosition;
//        }

//        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
//        {
//            int objectCount = Random.Range(minimum, maximum + 1);
//            for(int i = 0;i<objectCount;++i)
//            {
//                Vector3 randomPosition = RandomPosition();
//                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
//                GameObject instance = Instantiate(tileChoice, randomPosition, Quaternion.identity) as GameObject;
                
//            }
//        }

//        void LayoutEnemyAtRandom(GameObject[] enemyArray, int minimum, int maximum)
//        {
//            //enemyCount = (int)MathF.Log(level, 3f);
//            enemyCount = 3;
//            for (int i = 0; i<enemyCount;i++)
//            {
//                Vector3 randomPosition = EnemyRandomPosition();
//                GameObject enemyChoice = enemyArray[Random.Range(0, enemyArray.Length)];
//                Instantiate(enemyChoice, randomPosition, Quaternion.identity);
//            }
//        }

//        void LayoutAlliesAtRandom(GameObject[] allyArray, int minimum, int maximum)
//        {
//            allyCount = 2;
//            for (int i=0;i<allyCount;i++)
//            {
//                Vector3 randomPosition = AllyRandomPosition();
//                GameObject allyChoice = allyArray[Random.Range(0, allyArray.Length)];
//                Instantiate(allyChoice, randomPosition, Quaternion.identity);
//            }
//        }
        
//    }
//}

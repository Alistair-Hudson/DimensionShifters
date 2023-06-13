using DimensionShifters.Enemies;
using DimensionShifters.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.SpawnSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _warpInPrefab = null;

        private WorldEnemyList _enemyList = null;

        private void Awake()
        {
            _enemyList = Resources.Load<WorldEnemyList>("WorldEnemyLists/ScifiWorld");
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            while (true)
            {
                int difficultyLevel = PlayerScoreDisplay.Score / 100 + 2;
                Vector3 spawnPoint = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                WorldEnemyList.EnemyData enemyData = _enemyList.SpawnEnemy(difficultyLevel);
                yield return Spawn(enemyData, spawnPoint);
                yield return new WaitForSeconds(5f);
            }
        }

        private IEnumerator Spawn(WorldEnemyList.EnemyData enemyData, Vector3 spawnPoint)
        {
            var newWarp = Instantiate(_warpInPrefab, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(1);

            //var animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 2);
            //float time = 0;
            //while(time < 1)
            //{
            //    var t = animationCurve.Evaluate(time);
            //    newWarp.transform.position = new Vector3(newWarp.transform.position.x, t, newWarp.transform.position.z);
            //    yield return null;
            //    time += Time.deltaTime;
            //}
            EnemyAI newEnemy = Instantiate(enemyData.EnemyBasePrefab, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = transform;
            newEnemy.Setup(enemyData);
            yield return null;
            Destroy(newWarp.gameObject);
        }

        IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
            filePath = System.IO.Path.Combine(filePath, assetBundleName);

            //Load "animals" AssetBundle
            var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
            yield return assetBundleCreateRequest;

            AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

            //Load the "dog" Asset (Use Texture2D since it's a Texture. Use GameObject if prefab)
            AssetBundleRequest asset = asseBundle.LoadAssetAsync<GameObject>(objectNameToLoad);
            yield return asset;

            //Retrieve the object (Use Texture2D since it's a Texture. Use GameObject if prefab)
            GameObject loadedAsset = asset.asset as GameObject;

            //Do something with the loaded loadedAsset  object (Load to RawImage for example) 
            Instantiate(loadedAsset, Vector3.forward * 10, Quaternion.identity);
        }
    }
}
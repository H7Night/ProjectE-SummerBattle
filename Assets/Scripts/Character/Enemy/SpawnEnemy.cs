using System;
using System.Collections;
using API;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject enemyPrefab; //获取敌人预制体

    #region Wave

    public Wave[] Waves; //自定义类的数组 - 波数
    private Wave currentWave; //当前波数
    private int currentIndex; //当前波数在数组集合种的【索引index】
    public int waitSpawnNum; //这一波还剩下多少敌人没有生成
    public int spawnAliveNum; //这一波的敌人还存活多少个
    public float nextSpawnTime;

    #endregion

    private bool _isDisable; //敌人生成开关
    private LivingEntity playerEntity; //获取玩家脚本

    public event Action<int> onNewWave; //事件，在NextWave内部逻辑触发，因为每波序号不同，所以用Action<int>

    void Start() {
        playerEntity = FindObjectOfType<PlayerController>();

        playerEntity.onDeath += PlayerDeath;

        NextWave();
    }

    //生成敌人
    void Update() {
        if (!_isDisable &&
            GameManager.Instance.gameMode == GameManager.GameMode.GameProtect &&
            GameManager.Instance.gameMode != GameManager.GameMode.GameWin) {
            Starte();
        }
    }

    void NextWave() {
        currentIndex++;
        //最后一步，这次currentIndex是从1开始的，所以第三波的时候，index实际上等于3已经超过了Length的范围，所以再下一波的时候报错，可以限定范围
        if (currentIndex - 1 < Waves.Length) {
            currentWave = Waves[currentIndex - 1]; //一开始index = 1
            waitSpawnNum = currentWave.enemyNum;
            spawnAliveNum = currentWave.enemyNum;

            if (onNewWave != null) //如果事件为空
            {
                onNewWave(currentIndex); //FIXME
            }
        }
    }

    IEnumerator Spawn() //生成敌人
    {
        GameObject spawnEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnEnemy.GetComponent<EnemyController>().onDeath += EnemyDeath;
        yield return null;
    }

    private void EnemyDeath() {
        spawnAliveNum--;
        //敌人全部阵亡则下一波
        if (spawnAliveNum <= 0) {
            NextWave();
        }
    }

    private void PlayerDeath() {
        //玩家死亡
        _isDisable = true;
    }

    void Starte() {
        //索引归0
        currentIndex = 0;
        spawnAliveNum = 0;
        waitSpawnNum = currentWave.enemyNum;
        if ((waitSpawnNum > 0 || currentWave.infinite) && Time.time > nextSpawnTime) //当前波敌人未召唤完 且 游戏运行时间大于下次召唤时间
        {
            waitSpawnNum--; //当前波敌人数量--
            nextSpawnTime = Time.time + currentWave.timeBtwSpawn; //下一次生成时间间隔timeBtwSpawn秒
            StartCoroutine(Spawn()); //敌人生成改成使用协程调用
        }
    }
}
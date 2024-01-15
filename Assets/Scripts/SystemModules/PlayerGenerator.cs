using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : Singleton<PlayerGenerator>
{
    [Header("Spawn Info")]
    [SerializeField] GameObject playerSprite;

    [SerializeField] GameObject player;

    [SerializeField] Transform spawnPos;    //生成場合

    [SerializeField] GameObject appearVFX;

    [SerializeField] bool spawnPlayer;//生成制御する

    [Header("Coroutine Info")]
    [SerializeField] float deathEffectDelay;
    WaitForSeconds waitForEffect;

    /* ==  Spawn Info  ==*/
    private int spawnIndex;

    private bool firstSpawn = true;

    private void Start()
    {
        waitForEffect = new WaitForSeconds(deathEffectDelay);
    }


    private void OnEnable()
    {
        EventCenter.Subscribe(EventNames.SpawnPlayer,SpawnPlayer);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EventNames.SpawnPlayer, SpawnPlayer);
    }

    private void SpawnPlayer()
    {
        GameManager.GameState = GameState.Respawn;
        if (player != null　&& spawnPlayer)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    IEnumerator SpawnCoroutine()
    {
        

        if (firstSpawn)
        {
            //StartCoroutine(BlackMaskController.Instance.ScaleInOut(playerSprite, 145f, 2f));
            yield return StartCoroutine(BlackMaskController.Instance.ScaleInOut(145f, 1f,playerSprite));
            firstSpawn = false;


            player.transform.position = playerSprite.transform.position;
            playerSprite.SetActive(false);
            player.SetActive(true);
        }
        else
        {
            //死亡アニメーションを待つ
            yield return waitForEffect;
            player.transform.position = spawnPos.position;

            StartCoroutine(BlackMaskController.Instance.ScaleInOut(1,0.1f));
            //マスクの途中でエフェクトを生成させる
            yield return waitForEffect;

            PoolManager.Release(appearVFX, player.transform.position);
            //生成アニメーションを待つ
            yield return waitForEffect;
            
            player.SetActive(true);
        }
        


        GameManager.GameState = GameState.Playing;

        EventCenter.TriggerEvent(EventNames.Playing);

    }

    public void SetSpawnPos(Transform transform)
    {
        spawnPos = transform;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : Singleton<PlayerGenerator>
{
    [Header("Spawn Info")]
    [SerializeField] GameObject playerSprite;

    [SerializeField] GameObject player;

    [SerializeField] GameObject[] spawnPos;

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
            //アニメーションを待つ
            yield return waitForEffect;
            player.transform.position = spawnPos[spawnIndex].transform.position;

            StartCoroutine(BlackMaskController.Instance.ScaleInOut(0,0));

            PoolManager.Release(appearVFX, player.transform.position);

            yield return waitForEffect;

            player.SetActive(true);
        }
        


        GameManager.GameState = GameState.Playing;

        EventCenter.TriggerEvent(EventNames.Playing);

    }

    public void SetSpawnPos(int index) => spawnIndex = (index < 0) ? 0 : index;
}

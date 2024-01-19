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

    [Header("Audio Info")]
    [SerializeField] private AudioData appear;

    /* ==  Spawn Info  ==*/
    private int spawnIndex;

    private bool firstSpawn = true;

    protected override void Awake()
    {
        base.Awake();
        waitForEffect = new WaitForSeconds(deathEffectDelay);
    }

    private IEnumerator Start()
    {
        yield return GameManager.GameState == GameState.Respawn;
        SpawnPlayer();
    }

    private void OnEnable()
    {
        EventCenter.Subscribe(StateEvents.SpawnPlayer,SpawnPlayer);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(StateEvents.SpawnPlayer, SpawnPlayer);
    }
    
    /// <summary>
    /// プレイヤーを生成する
    /// </summary>
    private void SpawnPlayer()
    {
        //GameManager.GameState = GameState.Respawn;
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
            AudioManager.Instance.PlaySFX(appear);
            //生成アニメーションを待つ
            yield return waitForEffect;
            
            player.SetActive(true);
        }
        
        GameManager.GameState = GameState.Playing;

        //ゲーム入力を有効化にする
        EventCenter.TriggerEvent(InputEvents.EnableGameInput);
    }

    public void SetSpawnPos(Transform transform)
    {
        spawnPos = transform;
    }

}

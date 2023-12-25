using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : Singleton<PlayerGenerator>
{
    [SerializeField] GameObject playerSprite;

    [SerializeField] GameObject player;

    [SerializeField] GameObject[] spawnPos;

    [SerializeField] GameObject appearVFX;

    private int spawnIndex;

    private bool firstSpawn = true;



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
        if (player != null)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    IEnumerator SpawnCoroutine()
    {
        

        if (firstSpawn)
        {
            //StartCoroutine(BlackMaskController.Instance.ScaleInOut(playerSprite, 145f, 2f));
            yield return StartCoroutine(BlackMaskController.Instance.ScaleInOut(playerSprite, 145f, 1f));
            firstSpawn = false;


            player.transform.position = playerSprite.transform.position;
            playerSprite.SetActive(false);
            player.SetActive(true);
        }
        else
        {
            player.transform.position = spawnPos[spawnIndex].transform.position;

            StartCoroutine(BlackMaskController.Instance.ScaleInOut(0,0));
            yield return new WaitForSeconds(0.2f);

            PoolManager.Release(appearVFX, player.transform.position);

            yield return new WaitForSeconds(0.2f);

            player.SetActive(true);
        }
        


        GameManager.GameState = GameState.Playing;

        EventCenter.TriggerEvent(EventNames.Playing);

    }

    public void SetSpawnPos(int index) => spawnIndex = (index < 0) ? 0 : index;
}

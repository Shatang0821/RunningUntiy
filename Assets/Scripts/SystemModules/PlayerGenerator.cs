using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : Singleton<PlayerGenerator>
{
    [SerializeField] GameObject player;

    [SerializeField] Transform spawnPos;

    [SerializeField] GameObject appearVFX;

    private bool firstSpawn = true;

    private void OnEnable()
    {
        EventCenter.Subscribe(EventNames.Respawn,SpawnPlayer);
    }

    private void OnDisable()
    {
        EventCenter.Unsubscribe(EventNames.Respawn, SpawnPlayer);
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
        player.transform.position = spawnPos.position;
        
        yield return new WaitForSeconds(0.5f);
        if (firstSpawn)
        {
            StartCoroutine(BlackMaskController.Instance.ScaleInOut(player.transform.position, 0));
            yield return new WaitForSeconds(0.2f);
            firstSpawn = false;
        }
        else
        {
            StartCoroutine(BlackMaskController.Instance.ScaleInOut(player.transform.position, 0));
            yield return new WaitForSeconds(0.2f);
        }
        PoolManager.Release(appearVFX, player.transform.position);

        yield return new WaitForSeconds(0.2f);
        player.SetActive(true);

        GameManager.GameState = GameState.Playing;

        EventCenter.TriggerEvent(EventNames.Playing);

    }
}

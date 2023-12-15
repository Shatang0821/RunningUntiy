using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : Singleton<PlayerGenerator>
{
    [SerializeField] GameObject player;

    [SerializeField] Transform spawnPos;

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
        player.SetActive(true);

    }
}

using UnityEngine;
using DG.Tweening;

public class AirplaneObject : MonoBehaviour
{
    [SerializeField] float flightDuration;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;
    [SerializeField] AirplaneMissle misslePrefab;
    [SerializeField] float missleCooldown;

    bool isFlying = false;
    float spawnCounter = 0;

    public void StartFlying()
    {
        isFlying = true;
        transform.position = startPosition.position + PlayerManager.player.transform.position;
        transform.DOMove(endPosition.position + PlayerManager.player.transform.position, flightDuration).onComplete += () =>
         {
             isFlying = false;
             transform.position = startPosition.position;
         };
    }

    private void FixedUpdate()
    {
        //transform.position = startPosition.position;

        if (!isFlying)
        {
            return;
        }

        if (spawnCounter <= 0)
        {
            SpawnMissle();
            spawnCounter = missleCooldown;
        }
        else
        {
            spawnCounter -= Time.deltaTime;
        }
    }

    void SpawnMissle()
    {
        AirplaneMissle _missle = Instantiate(misslePrefab, transform.position, Quaternion.identity);
    }
}

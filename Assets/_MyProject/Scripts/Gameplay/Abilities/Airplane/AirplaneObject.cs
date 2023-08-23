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
        transform.position = startPosition.position;
        transform.DOMove(endPosition.position, flightDuration).onComplete += () =>
         {
             isFlying = false;
             transform.position = startPosition.position;
         };
    }

    private void FixedUpdate()
    {
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
        AirplaneMissle _missle = Instantiate(misslePrefab);
        _missle.transform.position = transform.position;
    }
}

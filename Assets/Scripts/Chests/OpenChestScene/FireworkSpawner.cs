using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkSpawner : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private ParticleSystem[] _fireworks;

    private void OnValidate()
    {
        _minDelay = Mathf.Clamp(_minDelay, _minDelay, _maxDelay);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        float delay;

        while (true)
        {
            delay = Random.Range(_minDelay, _maxDelay);
            yield return new WaitForSeconds(delay);

            var firework = _fireworks[Random.Range(0, _fireworks.Length)];
            Instantiate(firework, transform.position, Quaternion.identity);
        }
    }
}

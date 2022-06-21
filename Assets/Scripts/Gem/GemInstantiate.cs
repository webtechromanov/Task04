using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInstantiate : MonoBehaviour
{
    [SerializeField] private Gem _gem;
    [SerializeField] private Vector3[] _spawnPositions;

    private Coroutine _creation;

    private void Start()
    {
        _creation = StartCoroutine(SpawnGem());
    }

    private void OnDisable()
    {
        StopCoroutine(_creation);
    }

    private IEnumerator SpawnGem()
    {
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            CreateGem(_spawnPositions[i]);
            yield return null;
        }
    }

    private void CreateGem(Vector3 position)
    {
        Instantiate(_gem, position, Quaternion.identity);
    }
}

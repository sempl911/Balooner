using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftBaloon : MonoBehaviour
{
    [SerializeField] private GameObject _playerCabin;

    private void Update()
    {
        transform.position = new Vector3(_playerCabin.transform.position.x + 1, _playerCabin.transform.position.y + 1);
    }
}

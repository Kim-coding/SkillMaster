using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private GameObject[] player;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }


}

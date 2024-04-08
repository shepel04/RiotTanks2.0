using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetDestoyedToCanvas : MonoBehaviour
{
    public TextMeshProUGUI LightTanks;
    public TextMeshProUGUI HeavyTanks;

    private GameObject _counter;
    private DestroyedEnemyCounter _inst;

    private void Start()
    {
        _counter = GameObject.Find("DestroyedEnemyCounter");
        _inst = _counter.GetComponent<DestroyedEnemyCounter>();

        int newVal1 = _inst.Enemy / 2; 
        int newVal2 = _inst.BigEnemy / 2;

        LightTanks.text = newVal1.ToString();
        HeavyTanks.text = newVal2.ToString();
    }
}

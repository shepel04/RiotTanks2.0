using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MineSpawner : MonoBehaviour
{
    public GameObject MinePrefab; 
    public Transform PlayerTransform;
    public TextMeshProUGUI MinesCounterText;
    
    private int _minesPlaced;
    private int _maxMines = 4;
    private int _minesLeft = 4;
    private MineSliderAnimator _animInstance;

    private void Start()
    {
        _animInstance = GetComponent<MineSliderAnimator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (_minesPlaced < _maxMines)
            {
                _animInstance.AnimateMineSlider();
                SpawnMine();
                UpdateMinesCounter();
            }
            else
            {
                Debug.Log("You have already placed all mines ;)");
            }
        }
    }

    void SpawnMine()
    {
        if (MinePrefab != null && PlayerTransform != null)
        {
            Instantiate(MinePrefab, PlayerTransform.position, PlayerTransform.rotation);
            _minesPlaced++;
        }
        else
        {
            Debug.LogWarning("Mine prefab or player transform is not assigned.");
        }
    }
    
    void UpdateMinesCounter()
    {
        if (MinesCounterText != null)
        {
            _minesLeft--;
            MinesCounterText.text = _minesLeft.ToString();
        }
    }
}
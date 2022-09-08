using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    private List<GameObject> _buildingParts = new();

    private Coroutine _openPartCoroutine;
    private bool _isFull = false;

    private int _percentage;
    [Range(0f, 1f)] [SerializeField] private float completePercentage = .9f;
    [SerializeField] private TextMeshProUGUI percentageText;

    [SerializeField] private int brickCountPerBuildingPart = 5;

    private Stack _stack;


    private void Awake()
    {
        SetParts();
    }

    private void SetParts()
    {
        var savedParts = PlayerPrefs.GetInt(gameObject.name);
        if (savedParts != 0)
        {
            for (int i = 0; i < savedParts; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                _buildingParts.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    public bool CheckFull()
    {
        if (_buildingParts.Count == 0)
        {
            percentageText.color = Color.green;
            return _isFull = true;
        }

        return default;
    }
    

    private IEnumerator OpenPartCoroutine()
    {
        var completeCount = (int) (_buildingParts.Count * (1f - completePercentage));
        var brickBuildingPart = _buildingParts.Count;
        var buildingPartsCount = (_buildingParts.Count - (completeCount));
        
        for (int i = 0; i < buildingPartsCount; i += brickCountPerBuildingPart)
        {

            if (!CheckFull())
            {
                _stack.RemoveBrick(_openPartCoroutine);
                var maxIndex = i + Mathf.Min(brickCountPerBuildingPart, (brickBuildingPart - i) - completeCount);
                
                for (int j = i; j < maxIndex; j++)
                {
                    var buildingPart = _buildingParts[0];
                    _buildingParts.Remove(buildingPart);
                    buildingPart.SetActive(true);
                    buildingPart.transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero);
                }
            }

            yield return BetterWaitForSeconds.Wait(0.02f);
        }

        //_stack.StopBuildingCoroutine();
        for (int i = 0; i < completeCount; i++)
        {
            var buildingPart = _buildingParts[0];
            _buildingParts.Remove(buildingPart);
            buildingPart.SetActive(true);
            buildingPart.transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero);
            if (CheckFull())
            {
                GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void OpenPart()
    {
        if (_openPartCoroutine == null)
        {
            _openPartCoroutine = StartCoroutine(OpenPartCoroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player)
        {
            return;
        }

        var stack = other.GetComponentInChildren<Stack>();
        if (!stack) return;
        _stack = stack;

        if (!_isFull)
        {
            OpenPart();
            //stack.FillBuilding(_openPartCoroutine, other.GetComponent<Collider>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player)
        {
            return;
        }

        if (_openPartCoroutine != null)
        {
            StopCoroutine(_openPartCoroutine);
        }

        _openPartCoroutine = null;
    }

    private void SaveBuildingData(bool eventData)
    {
        var filledParts = transform.childCount - _buildingParts.Count;
        PlayerPrefs.SetInt(gameObject.name, filledParts);
    }

    public void ResetBuilding()
    {
        PlayerPrefs.SetInt(gameObject.name, 0);
    }

    private void SetPercentageText()
    {
        percentageText.text = $"% {CalculatePercentage()}";
    }

    private int CalculatePercentage()
    {
        var allParts = transform.childCount - 1;
        var filledParts = allParts - _buildingParts.Count;
        _percentage = (int) (((float) filledParts / allParts) * 100f);
        return _percentage;
    }

    private void Update()
    {
        SetPercentageText();
    }

    private void OnEnable()
    {
        GameManager.Instance.GameOverEvent += SaveBuildingData;
        //GameManager.Instance.GameOverEvent += KillCoroutines;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameOverEvent -= SaveBuildingData;
        //GameManager.Instance.GameOverEvent -= KillCoroutines;
    }

    private void KillCoroutines(bool data)
    {
        StopAllCoroutines();
    }
}
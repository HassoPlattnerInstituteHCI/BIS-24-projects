using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DualPantoToolkit;

public class BillardGameManager : MonoBehaviour
{
    [System.Serializable]
    public struct IntroductionObjects
    {
        public List<GameObject> balls;
        public Transform playerSpawn;
    }

    public GameObject panto;
    public GameObject player;
    
    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    public IntroductionObjects introductionObjects;

    private float _lastRotation = 0f;
    private int _currentBall = 0;
    
    PantoCollider[] _pantoColliders;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = panto.GetComponent<UpperHandle>();
        _lowerHandle = panto.GetComponent<LowerHandle>();
        _lastRotation = _lowerHandle.GetRotation();
        _lowerHandle.Freeze();
        _lowerHandle.FreeRotation();
        Introduction();
    }

    private void Update()
    {
        float nextRotation = _lowerHandle.GetRotation();
        if (nextRotation > _lastRotation + 2)
        {
            _currentBall++;
            if (_currentBall >= introductionObjects.balls.Count) _currentBall = 0;
            _lowerHandle.MoveToPosition(introductionObjects.balls[_currentBall].transform.position, 5f);
        } else if (nextRotation < _lastRotation - 2)
        {
            _currentBall--;
            if (_currentBall < 0) _currentBall = introductionObjects.balls.Count - 1;
            _lowerHandle.MoveToPosition(introductionObjects.balls[_currentBall].transform.position, 5f);
        }

        _lastRotation = nextRotation;
    }

    async void Introduction()
    {
        await _upperHandle.MoveToPosition(introductionObjects.playerSpawn.position);
        await RenderObstacle();
    }
    
    async Task RenderObstacle()
    {
        _pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in _pantoColliders)
        {
            Debug.Log("Added a Wall lol");
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DualPantoToolkit;
using UnityEngine.XR;
using SpeechIO;

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

    private GameObject _upperHandleObject;
    private GameObject _lowerHandleObject;

    private float _upperRot = 0f;
    private float _lowerRot = 0f;
    
    private bool _introDone = false;
    private int _currentBall = 0;
    
    PantoCollider[] _pantoColliders;
    private float[] handleRotations= {60f, 180f, 300f};

    private int _ballIndex = 0;
    private float _rotPos;

    private SpeechOut _so;
    
    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = panto.GetComponent<UpperHandle>();
        _lowerHandle = panto.GetComponent<LowerHandle>();
        _upperHandleObject = GameObject.Find("MeHandle");
        _lowerHandleObject = GameObject.Find("ItHandle");
        _rotPos = handleRotations[0];
        _so = new SpeechOut();
        Introduction();
    }

    private void Update()
    {
        _upperRot = _upperHandleObject.transform.rotation.eulerAngles.y;
        _lowerRot = _lowerHandleObject.transform.rotation.eulerAngles.y;
        // Debug.Log(_upperRot + " " + _lowerRot);
        if (!_introDone) return;
        if (!_lowerHandle.inTransition)
        {
            _lowerHandle.Rotate(_rotPos);
        }
        
        BallSelection();
    }

    async void BallSelection()
    {
        float nextRotation = _lowerRot;

        float closest = 361f;
        int closestIndex = 0;
        for (int i = 0; i < handleRotations.Length; i++)
        {
            float diff = Math.Abs(handleRotations[i] - nextRotation);
            if (diff < closest)
            {
                closest = diff;
                closestIndex = i;
            }
        }
        
        if (_ballIndex != closestIndex)
        {
            if (closestIndex == handleRotations.Length-1 && _ballIndex == 0) _currentBall--;
            else if (closestIndex == 0 && _ballIndex == handleRotations.Length-1) _currentBall++;
            else if (closestIndex > _ballIndex) _currentBall++;
            else if (closestIndex < _ballIndex) _currentBall--;

            if (_currentBall >= introductionObjects.balls.Count) _currentBall = 0;
            else if (_currentBall < 0) _currentBall = introductionObjects.balls.Count - 1;
            
            _ballIndex = closestIndex;
            _rotPos = handleRotations[_ballIndex];
            introductionObjects.balls[_currentBall].transform.Rotate(new Vector3(0f, _rotPos, 0f));
            _lowerHandle.SwitchTo(introductionObjects.balls[_currentBall]);
            _so.Speak("Keep rotating to select the next ball. Rotate backwards to select the previous ball.");
        }
    }

    async void Introduction()
    {
        _so.Speak("Rotate the lower handle to select a ball.");
        await _upperHandle.MoveToPosition(introductionObjects.playerSpawn.position, 2f);
        await _lowerHandle.SwitchTo(introductionObjects.balls[_currentBall], 2f);
        await RenderObstacle();
        _introDone = true;
    }
    
    async Task RenderObstacle()
    {
        _pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in _pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}

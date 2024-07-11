using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class Level2Manager : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();
    public GameObject fruit;
    public GameObject knife;

    public Transform fruitSpawn;
    public Transform knifeSpawn;

    private UpperHandle _upperHandle;
    private LowerHandle _lowerHandle;

    PantoCollider[] pantoColliders;

    // Start is called before the first frame update
    void Start()
    {
        _upperHandle = GetComponent<UpperHandle>();
        _lowerHandle = GetComponent<LowerHandle>();

        Introduction();
    }

    async void Introduction()
    {
        Level level = GetComponent<Level>();
        await level.PlayIntroduction(0.2f, 3000);
        await speechOut.Speak("Try to slice the fruit before it falls down");

        await StartGame();
    }

    async Task StartGame()
    {
        await RenderObstacle();

        Instantiate(knife, knifeSpawn);
        GameObject sb = Instantiate(fruit, fruitSpawn);
        //fruit wieder aktivieren
        //sb.SetActive(true);

        // TODO 3:
        await _lowerHandle.SwitchTo(sb, 50.0f);
        _upperHandle.Free();
    }

    async Task RenderObstacle()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
        }
    }
}
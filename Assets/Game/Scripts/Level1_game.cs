using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks;
using System.Globalization;
using DualPantoToolkit;

public class Level1_game : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();
    [TextArea(5, 10)]
    public string IntroductionText;

    public GameObject Panto;
    public GameObject Middle;
    UpperHandle upperHandle;
    // Start is called before the first frame update
    async void Start()
    {
        await Task.Delay(5000);
        await speechOut.Speak(IntroductionText);
        await Task.Delay(1000);
        upperHandle = Panto.GetComponent<UpperHandle>();
        await upperHandle.SwitchTo(Middle);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

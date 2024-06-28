using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks;

public class Level1_pa : MonoBehaviour
{
    public string[] commands = new string[] { "place", "delete" };
    SpeechOut speechOut = new SpeechOut();
    SpeechIn speechIn;

    public GameObject ActiveSelection;
    public GameObject Pixel;
    GameObject currentPixel;

    [TextArea(5, 10)]
    public string IntroductionText;
    [TextArea(5, 10)]
    public string SuccessText_0;
    [TextArea(5, 10)]
    public string SuccessText_1;

    // Start is called before the first frame update
    async void Start()
    {
       
        ActiveSelection.transform.position = new Vector3(0, 0, 0);
        await Task.Delay(5000);
        await speechOut.Speak(IntroductionText);

        speechIn = new SpeechIn(onRecognized, commands);
        await speech_place();
        await Task.Delay(1000);
        await speechOut.Speak(SuccessText_0);
        await speech_delete();
        await speechOut.Speak(SuccessText_1);
    }
    void onRecognized(string message)
    {
       // Debug.Log("[MyScript]: " + message);
    }

    private async Task speech_place()
    {
        string speechInput = await speechIn.Listen(new string[] { "place"});
        if (speechInput == "place")
        {
            currentPixel = Instantiate(Pixel, ActiveSelection.transform.position, Quaternion.identity);
        }
    }
    private async Task speech_delete()
    {
        string speechInput = await speechIn.Listen(new string[] { "delete" });
        if (speechInput == "delete")
        {
            Destroy(currentPixel);
        }
    }

}

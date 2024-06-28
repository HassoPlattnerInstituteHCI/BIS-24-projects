using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
namespace DualPantoToolkit{
    public class PManager : MonoBehaviour
    {
        public static int mode=0; //0.. Explanation, 1..
        public static ArrayList detections=new ArrayList();
        //Privates

        private static SpeechOut speechIO= new SpeechOut();
        //private GameObject TopBound;
        private LowerHandle l;
        private UpperHandle u;
        void Start()
        {
            //TopBound = GameObject.Find("UpperBound");
            l = GameObject.Find("Panto").GetComponent<LowerHandle>();
            u = GameObject.Find("Panto").GetComponent<UpperHandle>();
            GameObject.Find("MeHandleGodObject").AddComponent(typeof(CornerDetector));
            StartCoroutine(DelayedStart());
        }
        IEnumerator DelayedStart(){
            yield return new WaitForSeconds(2);
            //TopBound.SetActive(false);
            //Initial Position
            speechIO.Speak("Grab both handles to start the productivity app");
            u.MoveToPosition(new Vector3(0,0,-9), 0.5f, true);
            yield return new WaitForSeconds(4);
            l.MoveToPosition(new Vector3(0,0,-9), 0.5f, true);
            yield return new WaitForSeconds(5);
            speechIO.Speak("Follow the movement of the lower handle");
            mode=1;
            yield return new WaitForSeconds(5);
            l.SwitchTo(GameObject.Find("TopRight"),1f);
            yield return new WaitForSeconds(4);
            l.SwitchTo(GameObject.Find("BottomRight"),1f);
            yield return new WaitForSeconds(4);
            l.SwitchTo(GameObject.Find("BottomLeft"),1f);
            yield return new WaitForSeconds(4);
            l.SwitchTo(GameObject.Find("TopLeft"),1f);
            yield return new WaitForSeconds(4);
        }
        public static void task_finished(){
            for(int i=0;i<4;i++){
                if((int)detections[i]!=i) {
                    speechIO.Speak("You have failed the tutorial");
                    return;
                }
            }
            speechIO.Speak("You have completed the tutorial");
        } 
    }
}
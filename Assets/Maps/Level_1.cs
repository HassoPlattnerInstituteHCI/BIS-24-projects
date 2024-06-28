using System;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;

namespace DualPantoToolkit
{
    /// <summary>
    /// A level that can be introduced to the player. You could use one of these for each scene, or for each room in a scene.
    /// </summary>
    public class Level_1 : PantoBehaviour
    {
        AudioSource audioSource;
        SpeechOut speechOut = new SpeechOut();
        float speed;
        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Introduce all objects of interest in order of their priority. Free both handles afterwards.
        /// </summary>
        async public Task PlayIntroduction(float introductionSpeed = 10f, int msDelayBetweenObjects = 200)
        {
            speed = introductionSpeed;
            ObjectOfInterest[] gos = UnityEngine.Object.FindObjectsOfType<ObjectOfInterest>();
            Array.Sort(gos, ((go1, go2) => go2.priority.CompareTo(go1.priority)));

            for (int index = 0; index < gos.Length; index++)
            {
                await IntroduceObject(gos[index], msDelayBetweenObjects);
            }
            GetPantoGameObject().GetComponent<LowerHandle>().Free();
            GetPantoGameObject().GetComponent<UpperHandle>().Free();
        }

        async private Task IntroduceObject(ObjectOfInterest objectOfInterest, int msDelay)
        {
            Task[] tasks = new Task[2];
            tasks[0] = speechOut.Speak(objectOfInterest.description);

            PantoHandle pantoHandle = objectOfInterest.isOnUpper
                ? (PantoHandle)GetPantoGameObject().GetComponent<UpperHandle>()
                : (PantoHandle)GetPantoGameObject().GetComponent<LowerHandle>();

            if (objectOfInterest.traceShape)
            {
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in objectOfInterest.transform)
                {
                    children.Add(child.gameObject);
                }
                children.Sort((GameObject g1, GameObject g2) => g1.name.CompareTo(g2.name));
                tasks[1] = pantoHandle.TraceObjectByPoints(children, speed);
            }
            else
            {
                tasks[1] = pantoHandle.SwitchTo(objectOfInterest.gameObject, speed);
            }
            await Task.WhenAll(tasks);
            await Task.Delay(msDelay);
        }
        void OnApplicationQuit()
        {
            speechOut.Stop();
        }
    }
}

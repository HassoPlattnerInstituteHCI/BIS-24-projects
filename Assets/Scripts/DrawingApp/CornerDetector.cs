using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DualPantoToolkit{
    public class CornerDetector : MonoBehaviour
    {
        void OnTriggerEnter(Collider other){
            if(PManager.mode==1){
                if(other.gameObject.name.Equals("TopRight")&&(PManager.detections.Count==0||(int)PManager.detections[PManager.detections.Count-1]!=0)) {
                    PManager.detections.Add(0);
                    if((int)PManager.detections.Count>=4) PManager.task_finished();
                    Debug.Log("0");
                }
                else if(other.gameObject.name.Equals("BottomRight")&&(PManager.detections.Count==0||(int)PManager.detections[PManager.detections.Count-1]!=1)) {
                    PManager.detections.Add(1);
                    if((int)PManager.detections.Count>=4) PManager.task_finished();
                    Debug.Log("1");
                }
                else if(other.gameObject.name.Equals("BottomLeft")&&(PManager.detections.Count==0||(int)PManager.detections[PManager.detections.Count-1]!=2)) {
                    PManager.detections.Add(2);
                    if((int)PManager.detections.Count>=4) PManager.task_finished();
                    Debug.Log("2");
                }
                else if(other.gameObject.name.Equals("TopLeft")&(PManager.detections.Count==0||(int)PManager.detections[PManager.detections.Count-1]!=3)) {
                    PManager.detections.Add(3);
                    if((int)PManager.detections.Count>=4) PManager.task_finished();
                    Debug.Log("3");
                }
                else Debug.Log("Other: "+other.gameObject.name);
            }
        }
    }
}

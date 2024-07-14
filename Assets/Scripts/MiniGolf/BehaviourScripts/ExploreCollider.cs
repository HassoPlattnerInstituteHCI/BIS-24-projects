using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DualPantoToolkit{
    public class ExploreCollider : MonoBehaviour
    {
        Vector3 direction;
        DualPantoSync dps;
        public void Awake()
        {
            direction = new Vector3(1,0,0); 
            dps=GameObject.Find("Panto").GetComponent<DualPantoSync>();
        }
        public void OnTriggerEnter(Collider other){
            if(other.gameObject.name.Equals("Water")){
                GameManager.speechIO.Speak("Enter Water");
            }
        }

        public void OnTriggerStay(Collider collider)
        {
            if(collider.gameObject.name.Equals("Water"))
            {
                GameObject.Find("GameMaster").GetComponent<GameManagerClass>().wiggle.wiggle_wiggle_wiggle(true);
                /* Vector3 direction=new Vector3(1,0,0);
                dps.ApplyForce(true,direction,1);
                dps.FreeHandle(true);
                direction*=-1; */
            }
        }
        public void OnTriggerExit(Collider other){
            if(other.gameObject.name.Equals("Water")){
                GameManager.speechIO.Speak("Exit Water");
                dps.FreeHandle(true);
            }
        }

    }
}    
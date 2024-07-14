using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DualPantoToolkit{
    public class GoalCollider : MonoBehaviour
    {
        GameManagerClass g;
        void Awake(){
            g=GameObject.Find("GameMaster").GetComponent<GameManagerClass>();
        }
        public void OnTriggerEnter(Collider other){
            if(other.gameObject.name.Equals("Goal")){
                g.mode=5;
                GameManager.speechIO.Speak("Congratulations! You hit a hole in" + g.amount_hits +  " and completed level "+g.currentlvl);
                GameObject.Find("Ball").GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
                g.NextLevel();
            }
            else if(other.gameObject.name.Equals("Water")){
                GameManager.speechIO.Speak("You hit your ball into the water! You have to retry the level.");
                GameObject.Find("Ball").GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
                SceneManager.LoadScene (sceneName:name + g.currentlvl.ToString());
            }
        }
    }    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DualPantoToolkit{
    public class GoalCollider : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other){
            if(other.gameObject.name.Equals("Goal")){
                GameManager.speechIO.Speak("Congratulations! You hit a hole in" + amount_hits +  " and completed level one.");
                GameObject.Find("Ball").GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
                SceneManager.LoadScene (sceneName:"MiniGolfLvl2");
            }
            else if(other.gameObject.name.Equals("Water")){
                GameManager.speechIO.Speak("You hit your ball into the water! You have to retry the level.");
                GameObject.Find("Ball").GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
                SceneManager.LoadScene (sceneName:"MiniGolfLvl3");
            }
        }
    }   
}
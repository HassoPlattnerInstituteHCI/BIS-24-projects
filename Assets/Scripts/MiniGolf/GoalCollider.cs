using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DualPantoToolkit{
    public class GoalCollider : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other){
            if(other.gameObject.name.Equals("Goal")){
                GameManager.speechIO.Speak("Congratulations! You hit a hole in One and completed level one.");
                GameObject.Find("Ball").GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
                SceneManager.LoadScene (sceneName:"MiniGolfLvl2");
            }
        }
    }   
}
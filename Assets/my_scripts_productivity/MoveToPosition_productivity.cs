using UnityEngine;
using DualPantoToolkit;
using System.Threading.Tasks;
using SpeechIO;
using System; 

public class MoveToPosition_productivity : MonoBehaviour
{
    //public bool isUpper;
    public bool shouldFreeHandle;
    public float speed = 10f;
    PantoHandle upper_handle;
    PantoHandle lower_handle; 
    SpeechOut sp;

    public GameObject drawn_objectPrefab;
    private Vector3[] spawnBox = {new Vector3(-3, 0, 2), new Vector3(3, 0, -2)};

    //public Transform drawn_objectPrefab;
    public bool hasEncountered = false; 

    async void Start()
    {
        await Task.Delay(500);

        sp = new SpeechOut();

        //upper_handle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        //await upper_handle.MoveToPosition(new Vector3(-3, 0, -13), speed, shouldFreeHandle);

        //Vector3 spawnPosition = new Vector3(Random.Range(spawnBox[0].x, spawnBox[1].x), 0, Random.Range(spawnBox[0].z, spawnBox[1].z));
        //transform.position = (drawn_objectPrefab.ObjectPosition(transform.position));
        //await lower_handle.MoveToPosition(transform.position, speed, shouldFreeHandle);
        //await lower_handle.MoveToPosition(drawn_objectPrefab, speed, shouldFreeHandle);

        lower_handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        await lower_handle.SwitchTo(drawn_objectPrefab, 20f);
        //movementStarted = true;

        sp.Speak("Move your upper handle to the position of the lower handle");

        
    }

    Vector3 get_upper_position(){
        upper_handle = (PantoHandle)GameObject.Find("Panto").GetComponent<UpperHandle>();
        Vector3 upper_handle_pos = (upper_handle.HandlePosition(transform.position));
        return upper_handle_pos;
    }

    Vector3 get_lower_position(){
        lower_handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>(); 
        Vector3 lower_handle_pos = (lower_handle.HandlePosition(transform.position));
        return lower_handle_pos;
    }

    void Update(){

        //Vector3 pos_diff = new Vector3( (get_upper_position().x - get_lower_position().x), (get_upper_position().y - get_lower_position().y), (get_upper_position().z - get_lower_position().z) );
        //Vector3 pos_diff = new Vector3( (get_upper_position().x - transform.position.x), (get_upper_position().y - transform.position.y), (get_upper_position().z - transform.position.z) );

        float vector_distance = Vector3.Distance(transform.position, get_upper_position());

        Debug.Log(transform.position);
        Debug.Log(get_upper_position());
        Debug.Log(vector_distance);
        //Debug.Log(pos_diff);


        //if( (Math.Abs(pos_diff.x) == 0) && (Math.Abs(pos_diff.y) == 0) && (Math.Abs(pos_diff.z) == 0))
        //if( (pos_diff.x <= 0.1 || pos_diff.x >= -0.1) && (pos_diff.y <= 0.1 || pos_diff.y >= -0.1) && (pos_diff.z <= 0.1 || pos_diff.z >= -0.1))
        if (vector_distance < 1)
        {
            if(!hasEncountered)
            {
            SpawnDrawnObject();
            sp.Speak("Congratulations, you drew your first dot");
            }
            hasEncountered = true; 
        }
    }

    void SpawnDrawnObject() {
        Vector3 spawnPosition = get_lower_position();
        Instantiate(drawn_objectPrefab, spawnPosition, Quaternion.identity);
    }
}

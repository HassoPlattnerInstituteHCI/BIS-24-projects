using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class ME : MonoBehaviour {

    SpeechOut sp;
    public static bool player;
    Dictionary <string, (int, int)> dic = new Dictionary<string, (int, int)>();
    Dictionary <(int, int), GameObject> dicr = new Dictionary<(int, int), GameObject>();
    static PantoHandle upperHandle;
    static PantoHandle lowerHandle;
    static private float lastRotationU;
    static private float lastRotationL;
    private float timer = 0f;
    private float interval = 0.2f;
    Collider currentCollider = null;
    Collider selected = null;
    
    void Start() {
        // OPTIONAL TODO: 
        // speechIn = new SpeechIn(onRecognized); 	
        // speechIn.StartListening();
        // SpeedUpListener();
        sp = new SpeechOut();
        player = true;
        sp.Speak("Find the pieces on the board with both handles and pick them up and place them by twisting the Me handle, after each move it's the other players turn.");
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        lastRotationU = upperHandle.GetRotation();
        lastRotationL = lowerHandle.GetRotation();
        //handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Black")) {
            string name = go.name;
            int pos = name[2] - '0';
            
            int row = name[1] - '0';
            (int, int) v2 = (pos, row);
            dic[name] = v2;
            dicr[v2] = go;
            
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("White")) {
            string name = go.name;
            int pos = name[2] - '0';
            
            int row = name[1] - '0';
            (int, int) v2 = (pos, row);
            dic[name] = v2;
            dicr[v2] = go;
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Green")) {
            string name = go.name;
            int pos = name[2] - '0';
            
            int row = name[1] - '0';
            (int, int) v2 = (pos, row);
            dic[name] = v2;
            dicr[v2] = go;
        }
        

        //Reset();
    }

    public void Update() {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            if (rotatedHandle(upperHandle))
            {
                string outp = "";
                if(currentCollider != null && ((currentCollider.CompareTag("White") && player ) || (currentCollider.CompareTag("Black") && !player))) {
                    selected = currentCollider;
                    outp += "Selected";
                    //sp.Speak("Selected");
                } else if(selected != null && currentCollider != null && currentCollider.CompareTag("Green")) {
                    int pgx = dic[currentCollider.gameObject.name].Item1;
                    int ppx = dic[selected.gameObject.name].Item1;
                    int pgy = dic[currentCollider.gameObject.name].Item2;
                    int ppy = dic[selected.gameObject.name].Item2;
                    bool b = ((player && (pgy == ppy - 1)) || (!player && (pgy == ppy + 1))) && (pgx == ppx - 1 || pgx == ppx + 1);
                    int tx = Math.Abs((pgx - ppx));
                    int ty = (pgy - ppy);
                    if ((ty == -2 || ty == 2) && tx == 2) {
                        (int, int) tupelT = ((ppx + pgx)/2, (ppy + pgy)/2);
                        if (player && ty == -2) {
                            if (dicr[tupelT].CompareTag("Black")) {
                                b = true;
                                dicr[tupelT].tag = "Green";
                                dicr[tupelT].GetComponent<MeshRenderer> ().material = currentCollider.GetComponent<MeshRenderer>().material;
                                outp += "Captured ";
                            } 
                        } else if (!player && ty == 2){
                            if (dicr[tupelT].CompareTag("White")) {
                                b  = true;
                                dicr[tupelT].tag = "Green";
                                dicr[tupelT].GetComponent<MeshRenderer> ().material = currentCollider.GetComponent<MeshRenderer>().material;
                                outp += "Captured ";
                            }
                        }
                    }
                    GameObject toRemove = null;
                    if(selected.gameObject.name[0] == 'D') {
                        int xv = (pgx - ppx) / Math.Abs(ppx - pgx);
                        int yv = (pgy - ppy) / Math.Abs(ppy - pgy);
                        if(tx == Math.Abs(ty)) {
                            b = true;
                            int countWhite = 0, countBlack = 0;
                            for(int i = 1; i < tx; i++) {
                                (int, int) tup = (ppx + xv * i, ppy + yv * i);
                                GameObject g = dicr[tup];
                                if(g.CompareTag("White")) {
                                    countWhite++;
                                    toRemove = g;
                                } else if(g.CompareTag("Black")) {
                                    countBlack++;
                                    toRemove = g;
                                }
                            }
                            if(player && (countWhite > 0 || countBlack > 1)) {
                                b = false;
                            } else if(!player && (countBlack > 0 || countWhite > 1)) {
                                b = false;
                            }
                        }
                    }
                    if(b) {
                        swapGameObjects(selected.gameObject, currentCollider.gameObject);
                        if(player && dic[selected.gameObject.name].Item2 == 1 && selected.gameObject.name[0] != 'D') {
                            selected.gameObject.GetComponent<MeshRenderer>().material = GameObject.Find("ME").GetComponent<MeshRenderer>().material;
                            string tempName = selected.gameObject.name;
                            selected.gameObject.name = selected.gameObject.name.Replace('W', 'D');
                            dic[selected.gameObject.name] = dic[tempName];
                            outp += "Promoted";
                        } else if(!player && dic[selected.gameObject.name].Item2 == 8 && selected.gameObject.name[0] != 'D') {
                            selected.gameObject.name = selected.gameObject.name.Replace('B', 'D');
                            string tempName = selected.gameObject.name;
                            selected.gameObject.GetComponent<MeshRenderer>().material = GameObject.Find("IT").GetComponent<MeshRenderer>().material;
                            dic[selected.gameObject.name] = dic[tempName];
                            outp += "Promoted";
                        }
                        if(toRemove != null) {
                            toRemove.tag = "Green";
                            toRemove.GetComponent<MeshRenderer> ().material = currentCollider.GetComponent<MeshRenderer>().material;
                            outp += " Captured";
                        }
                        selected = null;
                        sp.Stop();
                        sp.Speak("Placed");
                        player = !player;
                        if(GameObject.FindGameObjectsWithTag("Black").Length == 0) {
                            sp.Stop();
                            sp.Speak("White wins");
                        } else if(GameObject.FindGameObjectsWithTag("White").Length == 0) {
                            sp.Stop();
                            sp.Speak("Black wins");
                        }
                    }
                }
                sp.Speak(outp);
            }
            timer -= interval;
        }
    }

    void OnTriggerEnter(Collider other) {
        string outp = " ";
        if(other.CompareTag("White") && player) {
            outp += "White";
            currentCollider = other;
            wiggle(upperHandle, other.gameObject);
        } else if(other.CompareTag("Black") && !player) {
            outp += "Black";
            currentCollider = other;
            wiggle(upperHandle, other.gameObject);
        } else if(other.CompareTag("Green") && selected != null) {
            outp += "Empty";
            currentCollider = other;
            //wiggle(upperHandle, other.gameObject);
        }
        if(other.gameObject.name[0] == 'D') {
            outp += " Queen";
        }
        if(outp != "") {
            sp.Stop();
            sp.Speak(outp);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other == currentCollider) currentCollider = null;
    }

    bool rotatedHandle(PantoHandle handle)
    {
        float currentRotation = handle.GetRotation();
        if (Math.Abs(currentRotation - lastRotationU) > 60)
        {
            if(handle == upperHandle) {
                lastRotationU = currentRotation;
            } else if(handle == lowerHandle) {
                lastRotationL = currentRotation;
            }
            
            return true;
        }
        if(handle == upperHandle) {
                lastRotationU = currentRotation;
            } else if(handle == lowerHandle) {
                lastRotationL = currentRotation;
            }
        return false;
    }

    void swapGameObjects(GameObject g1, GameObject g2) {
        Vector3 p = g1.transform.position;

        g1.transform.position = g2.transform.position;
        g2.transform.position = p;
        (int, int) ptemp = dic[g1.name];
        dic[g1.name] = dic[g2.name];
        dic[g2.name] = ptemp;
        dicr[dic[g1.name]] = g1;
        dicr[dic[g2.name]] = g2;

    }

    public static async void wiggle(PantoHandle ph, GameObject g1) {
        GameObject go = GameObject.Find("ME");
        Vector3 v = go.transform.position;
        v += new Vector3(0.05f, 0, 0.05f);
        g1.transform.Rotate(ph.transform.rotation.eulerAngles);
        await ph.SwitchTo(g1, 5f);
        //await ph.MoveToPosition(g1.transform.position, 5f);
        if(ph == upperHandle) {
            lastRotationU = 0; //ph.GetRotation();
        } else {
            lastRotationL = 0; //ph.GetRotation();
        }
        
        //await ph.MoveToPosition(go.transform.position, 1f);
        ph.Free();
    }

    public static void ItWiggle(GameObject go) {
        wiggle(lowerHandle, go);
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseState : MonoBehaviour
{
    
    void Start()
    {
             
    }

    
    void Update()
    {
        if (GameObject.Find("Attack_Team").GetComponent<AttackTeam>().attackSucces ==true) 
        {
            print("ав╬З╬Н©╛..");
            
        }

        return;
    }
}

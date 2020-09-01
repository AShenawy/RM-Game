using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//namespace
namespace Methodyca.Minigames.SortGame
{
    public class Levi : MonoBehaviour
    {
       public float speed;
       public float amplitude;
       private float temp;
       public float originalY;
       private Vector3 tempPos = new Vector3();
       public Vector3 off = new Vector3();
       private RectTransform used;
       public RectTransform lick; 
       public int points;

       void Start ()
       {
           used = this.gameObject.GetComponent<RectTransform>();
           temp = used.position.y;
           //off.Set(used.position.x, used.position.y, used.position.z);
       }

       

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.B))
            {
                points++;
                
            }
                if(points >= 1)
                {
                    //Shenway();
                }

            //Le();    
        }
        public void Le()
        {
           
            tempPos = lick.position;
            tempPos.y = temp + amplitude * Mathf.Sin(speed* Time.time);
            lick.position = tempPos; //removing this makes the position stay the same.
            //used.position = tempPos; 
            used.position = lick.position;
            //tempPos = off;
            off = lick.position;

        }
        
        
        public void Float()
        {
            //off = lick.position;
            //lick.position = tempPos;

            //Math function  
            //tempPos.y = amplitude * Mathf.Sin(speed*Time.time);

            //tempPos.y = balance;
            //lick.transfrom.Translate(amplitude * Mathf.Sin(speed*Time.time));
            //lick.position = tempPos;
            //lick.transform.Translate(Vector3.up*amplitude*Mathf.Sin(speed*Time.time));
            //off.y = originalY + tempPos.y;
            used.position.Set(off.x, off.y + (amplitude * Mathf.Sin(speed * Time.time)), off.z);
            //Debug.Log("Lies");
        }
        
        public void Shenway()
        {
            used.transform.Translate(Vector3.up * amplitude * Mathf.Sin(Time.fixedTime * speed));
            //used.transform.position = Vector3.up * Time.deltaTime * speed;
            lick.position = used.position;
            off = lick.position;
            originalY = off.y / 30f;
        } 
    }

}


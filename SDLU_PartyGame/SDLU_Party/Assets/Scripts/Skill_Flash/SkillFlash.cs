using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlash : MonoBehaviour
{
    private Camera camera; 
    private bool isMove; 
    private Vector3 destination;
    [SerializeField]
    private float maxFlashDistance = 7f;

    private void Awake() 
    { 
        camera = Camera.main; 
    }

    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            RaycastHit hit; 
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit)) 
            {
                if (hit.collider.tag != "Floor") return;
                Flash(hit.point);
            }
            
        }

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.tag != "Floor") return;
                SetDestination(hit.point);
            }
        }

        Move();
    }

    private void SetDestination(Vector3 dest) 
    { 
        destination = dest; 
        isMove = true; 
    }
    
    private void Move() 
    { 
        if (isMove) 
        {
            if (Vector3.Distance(destination, transform.position) <= 0.1f)
            {
                isMove = false; 
                return; 
            } 
            var dir = destination - transform.position;
            transform.position += dir.normalized * Time.deltaTime * 10f;
        }
    }

    private void Flash(Vector3 dest)
    {
        isMove = false;
        //Debug.Log(Vector3.Distance(dest, transform.position));
        if(Vector3.Distance(dest, transform.position) < 7f)
        {
            transform.position = dest;
        }
        else
        {
            var dir = dest - transform.position;
            transform.position += dir.normalized * maxFlashDistance;
        }
    }
}

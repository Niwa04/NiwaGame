using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerNew : MonoBehaviour
{

    public Camera camera;

    public float normalSpeed;
    public float fastSpeed;

    public float mouvementSpeed;
    public float mouvementTime;

    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    public Vector3 rotadeStartPosition;
    public Vector3 rotadeCurrentPosition;

    public bool canDeplace;
    public bool canRotade;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = camera.transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandelMouvementImput();
        HandelMouseImput();
    }
    void HandelMouseImput(){
        if(Input.mouseScrollDelta.y < 0 && newZoom.y > 2){
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

         if(Input.mouseScrollDelta.y > 0 && newZoom.y < 15){
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }


        if(Input.GetMouseButtonDown(0) && canDeplace){
            Plane plane = new Plane(Vector3.up, Vector3.zero );
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry)){
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if(Input.GetMouseButton(0) && canDeplace){
            Plane plane = new Plane(Vector3.up, Vector3.zero );

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry)){
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }


        if(Input.GetMouseButtonDown(2)){
            rotadeStartPosition = Input.mousePosition;
      }
        if(Input.GetMouseButton(2)){
            rotadeCurrentPosition = Input.mousePosition;

            Vector3 difference = rotadeStartPosition - rotadeCurrentPosition;
            rotadeStartPosition = rotadeCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x/5f));
      }


    }
    void HandelMouvementImput(){
        if(Input.GetKey(KeyCode.LeftShift)){
            mouvementSpeed = fastSpeed;
        }
        else{
           mouvementSpeed = normalSpeed;
        }

      /*  if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow)){
            newPosition += (transform.forward*-mouvementSpeed);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            newPosition += (transform.forward* mouvementSpeed);
        }
          if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            newPosition += (transform.right* -mouvementSpeed);
        }
          if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)){
            newPosition += (transform.right* mouvementSpeed);
        }*/

        if(Input.GetKey(KeyCode.A)){
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if(Input.GetKey(KeyCode.E)){
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if(Input.GetKey(KeyCode.R)){
            newZoom += zoomAmount;
        }
        if(Input.GetKey(KeyCode.F)){
            newZoom -= zoomAmount;
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime*mouvementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime*mouvementTime); 
        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, newZoom, Time.deltaTime*mouvementTime);
    }
}

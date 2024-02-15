using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    //Buttons
    private Button button;
    public Sprite imageUnlock;
    public Sprite imageLock;

    private UserInputManager userInputManagerScript;
    private MousePosition mousePositionScript;

    // Start is called before the first frame update
    void Start()
    {
        //Wyswietl stan obiektu
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager>();
        button = GetComponent<Button>();
        button.image.sprite = imageUnlock;
        button.onClick.AddListener(LockMovement);
    }

    private void Update()
    {
        if (userInputManagerScript.objectDetected && mousePositionScript.selectedObject != null && !mousePositionScript.mouseDragsObject && !mousePositionScript.terrainHItted)
        {
            DisplayStateOfSelectedObject();
        }
        else if (!userInputManagerScript.objectDetected)
        {
            button.image.sprite = imageUnlock;

        }
    }

    //Unlock object movement
    public void LockMovement()
    {
        if (mousePositionScript.objectID == 0) //WallX
        {
            mousePositionScript.selectedObject.GetComponent<CreateWallX>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<CreateWallX>().isFrozeen;
            
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<CreateWallX>().isFrozeen)
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 1) //WallZ
        {
            mousePositionScript.selectedObject.GetComponent<CreateWallZ>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<CreateWallZ>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<CreateWallZ>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 2) //Floor Y
        {
            mousePositionScript.selectedObject.GetComponent<CreateFloor>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<CreateFloor>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<CreateFloor>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else
            {
                button.image.sprite = imageUnlock;
            }
           
        }
        else if (mousePositionScript.objectID == 3) //Ceiling Obstacle
        {
            mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 4) //Door Obstacle
        {
            mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 5) //Ventilation Obstacle
        {
            mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 6) //Lighting Obstacle
        {
            mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 7) //Window Obstacle
        {
            mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 8) //Panel
        {
            mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen = !mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen;
            //Change lock button image
            if (mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen)
            {

                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = true;
                button.image.sprite = imageLock;
            }
            else
            {
                mousePositionScript.selectedObject.GetComponent<Collider>().isTrigger = false;
                button.image.sprite = imageUnlock;
            }
        }

    }

    private void DisplayStateOfSelectedObject()
    {
       
        if (mousePositionScript.objectID == 0) //WallX
        {
            if (mousePositionScript.selectedObject.GetComponent<CreateWallX>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<CreateWallX>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 1) //WallZ
        {
            if (mousePositionScript.selectedObject.GetComponent<CreateWallZ>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<CreateWallZ>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 2) //Floor Y
        {
            if (mousePositionScript.selectedObject.GetComponent<CreateFloor>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<CreateFloor>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 3) //Ceiling Obstacle
        {
            if (mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 4) //Wall Obstacle
        {
            if (mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 5) //Ventilation Obstacle
        {
            if (mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<CeilingObstacle>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 6) //Lighting Obstacle
        {
            if (mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 7) //Window Obstacle
        {
            if (mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<WallObstacle>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
        else if (mousePositionScript.objectID == 8) //Panel
        {
            if (mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen)
            {
                button.image.sprite = imageLock;
            }
            else if (!mousePositionScript.selectedObject.GetComponent<HangingObstacle>().isFrozeen)
            {
                button.image.sprite = imageUnlock;
            }
        }
    }


}

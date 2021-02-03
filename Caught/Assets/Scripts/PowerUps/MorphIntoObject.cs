using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class MorphIntoObject : PowerUp
{
    private Mirror.NetworkManager nm;
    private GameObject objClicked;
    private bool powerupUsed = false;
    Transform target;

    ControllablePlayer player;

    public override void Start()
    {
        setName("Morph Into Object");
        setTime(10); //lasts for 10 seconds

        //COMMENT OUT THIS IF NOT TESTING:

        //GameObject powerUpBtn = GameObject.FindGameObjectWithTag("Button");
        //powerUpBtn.GetComponent<ClickableButton>().powerUp = this;

        //Task.addPowerUp(this);
    }

    void FixedUpdate()
    {
        /*if (Input.GetMouseButtonDown(0) && powerupUsed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D rayHit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(rayHit.collider != null)
            {
                objClicked = rayHit.collider.gameObject;
                Debug.Log("Clicked on "+objClicked);
                objClicked.GetComponent<Objects>().isPlayer = true;
                GameObject currPlayer = GameObject.FindGameObjectWithTag("Player");
                currPlayer.GetComponent<Renderer>().enabled = false;
                Vector2 targetPos = objClicked.transform.position;
                currPlayer.transform.position = objClicked.transform.position;
                powerupUsed = false;
            }
        }*/

        /*if (Input.GetMouseButtonDown(0) && powerupUsed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, 100.0f))
            {
                if (rayHit.collider != null)
                {
                    objClicked = rayHit.collider.gameObject;
                    Debug.Log("Clicked on " + objClicked);
                    objClicked.GetComponent<Objects>().isPlayer = true;
                    GameObject currPlayer = GameObject.FindGameObjectWithTag("Player");
                    currPlayer.GetComponent<Renderer>().enabled = false;
                    Vector2 tasrgetPos = objClicked.transform.position;
                    currPlayer.transform.position = objClicked.transform.position;
                    powerupUsed = false;
                }
            }
            
        }*/

        if (isActive())
        { 
            Vector3 tasrgetPos = player.transform.position;
            objClicked.transform.position = tasrgetPos;
        }


    }

    public override void usePowerUp(ControllablePlayer p)
    {
        //maths here:
        //
        /*Escapee e = (Escapee)p;
        if (e.getMorph() != null)
        {
            Debug.Log("usePowerUp in Morph");
            powerupUsed = true;
            player = p;
            player.GetComponent<SpriteRenderer>().enabled = false;
            objClicked = e.getMorph();
            objClicked.GetComponent<Rigidbody>().isKinematic = false;
        }

        //don't change:
        setActive(true);*/
    }

    public override void reset(ControllablePlayer p)
    {
        objClicked.GetComponent<Objects>().isPlayer = false;
        //GameObject currPlayer = GameObject.FindGameObjectWithTag("Player");
        //currPlayer.GetComponent<Renderer>().enabled = true;
        p.GetComponent<SpriteRenderer>().enabled = true;
        setActive(false);
        p.setMorphNull();
        
    }
}

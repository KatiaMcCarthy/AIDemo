using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehavior : MonoBehaviour{

public GameObject player;
[HideInInspector]
public float rotationSpeed;

public GameObject patrolWayPts;
public Transform[] childWayPoints;
public GameObject pWP;
private UnityEngine.AI.NavMeshAgent agent;
private int destPoint;


    //body parts
    public GameObject headVisual;
    public GameObject torsoVisual;
    public GameObject lArmVisual;
    public GameObject rArmVisual;
    public GameObject wheelVisual;

    private FPSPlayer fpsPlayer;


public string playerName;
	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        fpsPlayer = player.GetComponent<FPSPlayer>();
		agent = GetComponent<NavMeshAgent> ();
		agent.autoBraking = false;

        Debug.Log(player.name);
	}


    void GoToNextPoint()
	{
		//Part that moves the AI
		if (childWayPoints.Length == 0) {
			return;
		}
		//turns the AI unit to look at the next point
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, childWayPoints[destPoint].transform.rotation, Time.deltaTime * rotationSpeed);

		agent.destination = childWayPoints [destPoint].position;

		destPoint = (destPoint + 1) % childWayPoints.Length;
	}

    void Update()
    {
        if (GameObject.FindWithTag ("PatrolPoints") == null) {
				Instantiate (patrolWayPts, patrolWayPts.transform.position, Quaternion.identity);
			}

			pWP = GameObject.FindWithTag ("PatrolPoints");
			if (pWP != null) {
			//	Debug.Log (pWP.name);
			}
			childWayPoints = new Transform[pWP.transform.childCount];

			int i = 0;
			foreach (Transform child in pWP.transform ) {
				childWayPoints[i] = child;
				i++;
			//	Debug.Log (child.name);
			}

            if (agent.remainingDistance < 0.5f) 
			{
				GoToNextPoint ();
			}
    }

    	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) {
			Debug.Log ("Hit Player");
			
		}

	}


    public void TakeDamage(GameObject go)
    {
        //read in the game object that is hit, disable the corrosponding visual

        if(go.tag == "Head")
        {
            headVisual.SetActive(false);
            fpsPlayer.headNum++;
            fpsPlayer.UpdateScoreUI();
            Destroy(go);
        }else if (go.tag == "Torso")
        {
            torsoVisual.SetActive(false);
            fpsPlayer.torsoNum++;
            fpsPlayer.UpdateScoreUI();
            Destroy(go);
        }
        else if (go.tag == "LArm")
        {
            lArmVisual.SetActive(false);
            fpsPlayer.armNum++;
            fpsPlayer.UpdateScoreUI();
            Destroy(go);
        }
        else if(go.tag == "RArm")
        {
            rArmVisual.SetActive(false);
            fpsPlayer.armNum++;
            fpsPlayer.UpdateScoreUI();
            Destroy(go);
        }
        else if(go.tag == "Wheel")
        {
            wheelVisual.SetActive(false);
            fpsPlayer.wheelNum++;
            fpsPlayer.UpdateScoreUI();
            Destroy(go);
        }
        
    }
}
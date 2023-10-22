using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
   public float ghostDelay;
   private float ghostDelayTime;
   public GameObject ghost;
   [SerializeField] private float timeGhostDestroy;
    public bool MakeGhost=false;
    void Start()
    {
        ghostDelayTime = ghostDelay;     
    }

    // Update is called once per frame
    void Update()
    {
        if(MakeGhost)
        {
            if(ghostDelayTime > 0)
        {
            ghostDelayTime -= Time.deltaTime;
        }
            else
            {
                StartCoroutine(CreateGhost());
            }   
        }
    }
    private IEnumerator CreateGhost()
    {
    GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
    Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
    currentGhost.transform.localScale = this.transform.localScale;
    currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
    yield return new WaitForSeconds(timeGhostDestroy);
    Destroy(currentGhost);
    }
}


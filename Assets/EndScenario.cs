using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScenario : MonoBehaviour
{
    public CharacterMovement characterMovement;
    
    Animator myAnimator;
    public SpriteRenderer FrogSpriteRenderer;
    public GameObject Frog;   
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter (Collider col){
         
        if(col.gameObject.tag == "Player"){
            Frog.SetActive(false);
             characterMovement.enabled =false; 
            FrogSpriteRenderer.sprite = null;
            myAnimator.SetBool("HasReachedEnd",true);
            StartCoroutine(WaitforWinScreen());  
             

        }
    }
 public void PlayTongueAnimation() {
    
  }
  private IEnumerator WaitforWinScreen() {
    yield return new WaitForSeconds(10f);
    characterMovement.Win();
    
  }

}


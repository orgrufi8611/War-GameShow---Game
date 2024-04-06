using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSlotrScript : MonoBehaviour
{
    [SerializeField] float maxY = 6, minY = -6;
    [SerializeField] float spinVelocity;
    public bool spin,spinDone;
    [SerializeField] float spinTime;
    [SerializeField] float initialY;
    [SerializeField] GameObject numbers;
    public int numberResult;
    float time;
    [SerializeField] bool win;
    [SerializeField] Sprite[] expressions = new Sprite[5];
    [SerializeField] SpriteRenderer face;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip slotSpin, slotSpinDone,winEffect;
    public bool spinable;
    // Start is called before the first frame update
    void Start()
    {
        spinable = true;
        spinDone = false;
        spin = false;
        time = 0;
        numberResult = 0;
        spinVelocity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            time += Time.deltaTime;
            float y = numbers.transform.localPosition.y;
            if (y >= maxY) 
            {
                y = minY;
            }
            y = Mathf.Clamp(y + spinVelocity*Time.deltaTime, minY, maxY);
            numbers.transform.localPosition = new Vector3(0,y,-0.1f);
            if(time > spinTime)
            {
                audioS.Stop();
                y = numbers.transform.localPosition.y;
                numbers.transform.localPosition = new Vector3(0, (int)y, -0.1f);
                numberResult = -(int)y + 7;
                spin = false;
                spinDone = true;
                audioS.PlayOneShot(slotSpinDone);
            }
        }       
    }
    
    public void NumberSpin()
    {
        if(spinable)
        {
            face.sprite = expressions[4];
            spinTime = Random.Range(3, 5);
            spinVelocity = Random.Range(2, 5);
            initialY = Random.Range(minY, maxY);
            numbers.transform.localPosition = new Vector3(0, initialY, -0.1f);
            spin = true;
            spinable = false;
            time = 0;
            audioS.PlayOneShot(slotSpin);
        }
    }
    
    public void RoundWon()
    {
        //smile expression
        face.sprite = expressions[0];
        audioS.PlayOneShot(winEffect);
    }
    public void RoundLost()
    {
        //sad expression
        face.sprite = expressions[1];
    }

    public void GameWon()
    {
        face.sprite = expressions[2];
        audioS.PlayOneShot(winEffect);
    }

    public void GameLost()
    {
        face.sprite = expressions[3];
    }
}

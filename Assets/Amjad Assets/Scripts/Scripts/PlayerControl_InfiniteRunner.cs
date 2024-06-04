using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl_InfiniteRunner : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true; // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false; // Condition for whether the player should jump.
    public float moveForce = 365f; // Amount of force added to move the player left and right.
    public float maxSpeed = 5f; // The fastest the player can travel in the x axis.
    public float maxJumpSpeed = 5f;
    public AudioClip[] jumpClips; // Array of clips for when the player jumps.
    public AudioClip[] PunchClips; // Array of clips for when the player jumps.
    public float jumpForce = 1000f; // Amount of force added when the player jumps.
    public float SecondJumpForce = 700f;
    public AudioClip[] taunts; // Array of clips for when the player taunts.
    public float tauntProbability = 50f; // Chance of a taunt happening.
    public float tauntDelay = 1f; // Delay for when the taunt should happen.
    public bool FlipAtAwake = false;
    public LayerMask WhatIsGround;
    private int tauntIndex; // The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck; // A position marking where to check if the player is grounded.
    public bool grounded = false; // Whether or not the player is grounded.
    private Animator anim; // Reference to the player's animator component.
    public Transform Bat;
    private Animator BatAnim;
    private bool LastPressed = false; //check the last status of the button press
    private bool doJump = false;
    private bool SecondJump = true;
    private float TopV = 0f;
    private float BV = 0f;
    public bool UseParallaxScrolling = false;
    public Transform BG1;
    public Transform BG2;
    public Transform BG3;
    private float PrevCamPos = 0;
    private bool JumpButton = false;
    public Transform BoostButton;
    private TouchBtnScript BoostBtnScript;
    private Image BoostBtnImage;
    private Image PauseBtn;
    public float BoostMaxSpeed = 7f;
    private float CurrentSpeed;
    private float TopSpeed;

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        BatAnim = Bat.GetComponent<Animator>();
        if (FlipAtAwake)
        {
            Flip();
            facingRight = true;
        }
        PauseBtn = GameObject.Find("Pausebtn").GetComponent<Image>();
        if (BoostButton)
        {
            BoostBtnImage = BoostButton.GetComponent<Image>();
            BoostBtnScript = BoostButton.GetComponent<TouchBtnScript>();
        }
        audio.Play();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (audio.isPlaying)
                audio.Stop();

            return;
        }

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, WhatIsGround);
        if (rigidbody2D.velocity.x == 0 || !grounded)
        {
            audio.Stop();
            transform.Find("trail1").gameObject.SetActive(false);
            transform.Find("trail2").gameObject.SetActive(false);

        }
        else if (rigidbody2D.velocity.x != 0 && !audio.isPlaying && grounded)
        {
            audio.Play();
            transform.Find("trail1").gameObject.SetActive(true);
            transform.Find("trail2").gameObject.SetActive(true);

        }
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        if (grounded)
        {
            anim.SetBool("AirAttack", false);

        }

        if (rigidbody2D.velocity.y > TopV)
            TopV = rigidbody2D.velocity.y;

        if (rigidbody2D.velocity.y < BV)
            BV = rigidbody2D.velocity.y;

        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !PauseBtn.HitTest(Input.GetTouch(0).position)) || Input.GetMouseButtonDown(0) && !PauseBtn.HitTest(Input.mousePosition) || Input.GetButtonDown("Jump")) && grounded)
        {

            jump = true;
            SecondJump = true;

        }
        else if (!grounded && SecondJump && ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")))
        {

            jump = true;
            SecondJump = false;


        }

        if (BoostButton)
        {

            if (BoostBtnTxture.HitTest(Input.mousePosition) && jump == true)
            {
                jump = false;
                SecondJump = !SecondJump;
            }
            else if (Input.touchCount > 0)
            {
                if (BoostBtnTxture.HitTest(Input.GetTouch(0).position) && jump == true)
                {
                    jump = false;
                    SecondJump = !SecondJump;
                }

            }


        }



        float h = 1f;

        if ((BoostButton != null && BoostBtnScript.longPressDetected) || Input.GetButton("Punch"))
        {
            TopSpeed = BoostMaxSpeed;
            //h = h * 2f;

        }
        else
        {
            TopSpeed = maxSpeed;
        }
        // Cache the horizontal input.


        anim.SetFloat("Speed", Mathf.Abs(h));
        if (h * rigidbody2D.velocity.x < TopSpeed)
            rigidbody2D.AddForce(Vector2.right * h * moveForce);


        if (Mathf.Abs(rigidbody2D.velocity.x) > TopSpeed)
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);


        if (h == 0f)
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
        }
        if (UseParallaxScrolling == true && PrevCamPos != Camera.main.transform.position.x)
        {
            if (BG1 != null)
                BG1.GetComponent<ScrollScript_Auto>().Scroll(rigidbody2D.velocity.x);
            if (BG2 != null)
                BG2.GetComponent<ScrollScript_Auto>().Scroll(rigidbody2D.velocity.x);
            if (BG3 != null)
                BG3.GetComponent<ScrollScript_Auto>().Scroll(rigidbody2D.velocity.x);
        }


        if (Mathf.Abs(rigidbody2D.velocity.y) > maxJumpSpeed && rigidbody2D.velocity.y > 0f)
            // ... set the player's velocity to the maxJumpSpeed in the y axis.
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sign(rigidbody2D.velocity.y) * maxJumpSpeed);
        // If the input is moving the player right and the player is facing left...

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            int i = Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position, 0.5f);
            anim.SetTrigger("Jump");
            if (SecondJump == false)
            {
                rigidbody2D.AddForce(new Vector2(0f, SecondJumpForce));
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
            jump = false;
        }
      
        PrevCamPos = Camera.main.transform.position.x;

    }

    //This will fire by an event in the animation "Hit"
    public void PlayAudioPunch()
    {

        //int i = Random.Range (0, PunchClips.Length);
        AudioSource.PlayClipAtPoint(PunchClips[1], transform.position);
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public IEnumerator Taunt()
    {
        // Check the random chance of taunting.
        float tauntChance = Random.Range(0f, 100f);
        if (tauntChance > tauntProbability)
        {
            // Wait for tauntDelay number of seconds.
            yield return new WaitForSeconds(tauntDelay);

            // If there is no clip currently playing.
            if (!audio.isPlaying)
            {
                // Choose a random, but different taunt.
                tauntIndex = TauntRandom();

                // Play the new taunt.
                audio.clip = taunts[tauntIndex];
                audio.Play();
            }
        }
    }

    int TauntRandom()
    {
        // Choose a random index of the taunts array.
        int i = Random.Range(0, taunts.Length);

        // If it's the same as the previous taunt...
        if (i == tauntIndex)
            // ... try another random taunt.
            return TauntRandom();
        else
            // Otherwise return this index.
            return i;
    }
}

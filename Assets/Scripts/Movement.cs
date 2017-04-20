using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
  public float forward_speed;
  public float side_speed;
  public bool move_left = false;
  public bool move_right = false;
  public float new_location = 0.0f;
  public bool moving = false;
  bool touching_floor = false;
  bool already_swiped_down = false;
  bool isDead = false;
  float last_platform_y = 0.0f;
  bool front_contact = false;
  int platforms_touched = 0;

  int speed_level = 1;
  float jumpSpeed = 10f;
  float fulcrum_decrement = 90f;
  float max_height;
  float max_height_increment = 3.0f;
  float extra_gravity = 50f;

  bool jumping = false;

  //public AudioClip music;

  public float minSwipeDistY = 150.0f;
  public float minSwipeDistX = 150.0f;
  private Vector2 startPos;

  // Use this for initialization
  void Start ()
  {
    //audio.PlayOneShot (music, 0.2f);
    //forward_speed = 0.25f; //0.15f
    //side_speed = 12.25f;
  }

  // Update is called once per frame
  void Update () {
    switch(platforms_touched) //increase level speed when certain platforms are reached
    {
      case 10:
        speed_level = 2;
        break;
      case 20:
        speed_level = 3;
        break;
      case 30:
        speed_level = 4;
        break;
      case 40:
        speed_level = 5;
        break;
      default:
        break;
    }
    switch(speed_level) //settings for speed_level
    {
      case 1:
        jumpSpeed = 450f;
        max_height_increment = 3f;
        fulcrum_decrement = 50f;
        extra_gravity = 50f;
        forward_speed = 0.26f; //0.15f
        side_speed = 12.25f;
        break;
      case 2:
        jumpSpeed = 500f;
        max_height_increment = 3.0f;
        fulcrum_decrement = 50f;
        extra_gravity = 50f;
        forward_speed = 0.29f; //0.15f
        side_speed = 13.25f;
        break;
      case 3:
        jumpSpeed = 500f;
        max_height_increment = 3.0f;
        fulcrum_decrement = 90f;
        extra_gravity = 50f;
        forward_speed = 0.32f; //0.15f
        side_speed = 14.25f;
        break;
      case 4:
        jumpSpeed = 500f;
        max_height_increment = 3.0f;
        fulcrum_decrement = 90f;
        extra_gravity = 50f;
        forward_speed = 0.35f; //0.15f
        side_speed = 14.25f;
        break;
      case 5:
        jumpSpeed = 500f;
        max_height_increment = 3.0f;
        fulcrum_decrement = 100f;
        extra_gravity = 50f;
        forward_speed = 0.38f; //0.15f
        side_speed = 15.45f;
        break;
    }
    max_height_increment *= 1.30f;

    if(jumping && this.gameObject.transform.position.y > max_height)
    {
      GetComponent<Rigidbody>().AddForce(Vector3.down * fulcrum_decrement);
    }

    if(front_contact)
    {
      forward_speed = 0.0f;
    }

    if (Input.touchCount > 0)
    {
      Touch touch = Input.touches[0];
      switch (touch.phase)
      {
        case TouchPhase.Began:
          startPos = touch.position;
          break;
        case TouchPhase.Ended: //Checks screen swiping direction, and performs a specific corresponding action
          float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
          if (swipeDistVertical > minSwipeDistY)
          {
            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
            if (swipeValue > 0) //up swipe
            {
              if (touching_floor == true) //can only jump if touching a platform
              {
                max_height = this.gameObject.transform.position.y + max_height_increment;
                jumping = true;

                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
              }
            }
            else if (swipeValue < 0) //down swipe
            {
              if (touching_floor == false && !already_swiped_down)
              {
                already_swiped_down = true;
                GetComponent<Rigidbody>().AddForce(Vector3.down * 800f);
              }
            }
          }
          float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

          if (swipeDistHorizontal > minSwipeDistX)
          {
            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

            if (swipeValue > 0) //right swipe
            {
              if(!moving){
                new_location = transform.position.x + 10f;
                move_right = true;
                moving = true;
              }
            }
            else if (swipeValue < 0) //left swipe
            {
              if(!moving){
                new_location = transform.position.x - 10f;
                move_left = true;
                moving = true;
              }
            }
          }
          break;
      }
    }

    if(transform.position.y < last_platform_y - 10.0f)
    {
      isDead = true;
    }

    transform.Translate (new Vector3 (0, 0, 1.0f) * forward_speed);
    if (Input.GetKeyDown ("up") && touching_floor == true) //keyboard controls
    {
      max_height = this.gameObject.transform.position.y + max_height_increment;
      jumping = true;

      GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
    }
    if (Input.GetKeyDown ("down") && touching_floor == false && !already_swiped_down)
    {
      already_swiped_down = true;
      GetComponent<Rigidbody>().AddForce(Vector3.down * 800f);
    }
    if (Input.GetKeyDown("left") && !moving)
    {
      new_location = transform.position.x - 10f;
      move_left = true;
      moving = true;
    }
    if (Input.GetKeyDown("right") && !moving)
    {
      new_location = transform.position.x + 10f;
      move_right = true;
      moving = true;
    }

    if (move_left)
    {
      float step = side_speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(new_location, transform.position.y, transform.position.z), step);
      if(transform.position.x == new_location)
      {
        transform.position = new Vector3(Mathf.RoundToInt(new_location), transform.position.y, transform.position.z);
        moving = false;
      }
    }

    if (move_right)
    {
      float step = side_speed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(new_location, transform.position.y, transform.position.z), step);
      if(transform.position.x == new_location)
      {
        transform.position = new Vector3(Mathf.RoundToInt(new_location), transform.position.y, transform.position.z);
        moving = false;
      }
    }
  }

  void OnCollisionEnter(Collision col)
  {
    last_platform_y = col.transform.position.y;
    touching_floor = true;
    jumping = false;
    already_swiped_down = false;
    platforms_touched++;
  }

  void OnCollisionExit(Collision col)
  {
    touching_floor = false;
    if(!jumping) //collision exit and not jumping, so falling...
    {
      GetComponent<Rigidbody>().AddForce(Vector3.down * extra_gravity);
    }
  }
  //collision detection doesn't work for front on back of cube objects. Have to add a new trigger object to dect front side collisions
  void OnTriggerEnter(Collider col)
  {
    front_contact = true;
  }

  void OnGUI()
  {
    if (isDead) //show Game Over menu
    {
      GUI.Box(new Rect(Screen.width * (0.10f),Screen.height * (0.10f),Screen.width * (0.8f), Screen.height * (0.8f)), "Game Over");
      if (GUI.Button(new Rect(Screen.width * (0.2f),Screen.height * (0.45f), Screen.width * (0.6f), Screen.height * (0.125f)), "Retry")) {
        Application.LoadLevel (Application.loadedLevelName);
      }

      if (GUI.Button(new Rect(Screen.width * (0.2f),Screen.height * (0.6f), Screen.width * (0.6f), Screen.height * (0.125f)), "Quit")) {
        Application.LoadLevel ("MainMenu");
      }
    }
  }
}

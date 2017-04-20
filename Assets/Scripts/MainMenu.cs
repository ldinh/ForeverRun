using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

  bool plain = true;
  bool challenge_1 = false;
  bool challenge_2 = false;
  bool challenge_3 = false;
  bool unlockables = false;
  bool credits = false;
  public Texture2D lock_image;
  public Texture2D next_arrow;
  public Texture2D prev_arrow;
  public Vector2 scrollPosition;
  public AudioClip music;

  public int levelSelect = 0;
  public int costumeSelect = 0;
  public int specialSelect = 0;

  //public GUIStyle style;

  void Start()
  {

  }

  void Update ()
  {

  }


  void OnGUI()
  {
    int w = Screen.width;
    int h = Screen.height;
    GUI.skin.box.fontSize = Screen.width / 20;
    GUI.skin.button.fontSize = Screen.width / 20;
    GUI.skin.label.fontSize = Screen.width / 20;

    if (plain) //plain menu active
    {
      GUI.Label (new Rect(Screen.width * (1f/2f) - ((Screen.width * (1.3f/5f))/2),Screen.height * (1f/7f),Screen.width * (3f/5f),Screen.height * (1f/9f)), "Forever Run");
      if(GUI.Button(new Rect(Screen.width * (1f/13f),Screen.height * (4f/7f),Screen.width * (2f/5f),Screen.height * (1f/9f)), "Play!")) {
        Application.LoadLevel("Runner");
      }
      if(GUI.Button(new Rect(Screen.width * (6f/11f),Screen.height * (4f/7f),Screen.width * (2f/5f),Screen.height * (1f/9f)), "Credits")) {
        plain = false;
        credits = true;
      }
    }

    if(unlockables) //unlockable menu active
    {

    }

    if(credits) //credits menu active
    {
      GUI.Box(new Rect(0,0,Screen.width , Screen.height ), "Credits");

      GUI.Label (new Rect(Screen.width * (1f/2f) - ((Screen.width * (3f/5f))/2),Screen.height * (1f/13f),Screen.width * (3f/5f),Screen.height * (1f/9f)), "Programming:");
      GUI.Label (new Rect(Screen.width * (1f/2f) - ((Screen.width * (3f/5f))/2),Screen.height * (2f/13f),Screen.width * (3f/5f),Screen.height * (1f/9f)), "TSG_Falsetto");

      if(GUI.Button(new Rect(Screen.width * (1f/7.5f),Screen.height * (8f/9f),Screen.width * (2f/7f),Screen.height * (1f/11f)), prev_arrow))
      {
        plain = true;
        credits = false;
      }
    }
  }
}

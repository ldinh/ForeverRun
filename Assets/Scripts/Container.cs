using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour {
  public GameObject player = null;

  // Use this for initialization
  void Start ()
  {
    player = GameObject.Find("First Person Controller");
  }

  // Update is called once per frame
  void Update ()
  {
    foreach (Transform child in transform){
      // do what you want with the transform
      if(player.transform.position.z > child.position.z + 15)
      {
        //child = null;
        Destroy(child.gameObject);
      }
    }
  }
}

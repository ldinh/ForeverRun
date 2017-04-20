using UnityEngine;
using System.Collections;

public class RandSpawn : MonoBehaviour {

  public GameObject platform;
  public GameObject prev;
  public int forward_inc = 0;
  public float side_inc = 0;
  public int vert_inc = 0;
  public GameObject spawn = null;
  public GameObject container = null;

  public Random rand = new Random();
  public int rand_int;

  void Start () {
    container = GameObject.Find("Platform Container");
  }

  // Update is called once per frame
  void Update() {
    if(container.transform.childCount < 10)
    {
      rand_int = Random.Range(0, 80);
      forward_inc += 23;

      if (rand_int > 0 && rand_int < 20){
        side_inc += 10f;
      }
      if (rand_int > 20 && rand_int < 40){
        side_inc -= 10f;
      }
      if (rand_int > 40 && rand_int < 60){
        vert_inc -= 2;
      }
      if (rand_int > 60 && rand_int < 80){
        vert_inc += 2;
      }

      spawn = (GameObject)Instantiate(platform, new Vector3(side_inc , vert_inc, forward_inc), Quaternion.identity);
      spawn.GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Diffuse");
      spawn.transform.parent = container.transform;
    }
  }
}

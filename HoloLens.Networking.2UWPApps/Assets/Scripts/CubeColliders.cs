using System.Collections.Generic;
using UnityEngine;

public class CubeColliders : MonoBehaviour {

    public static Dictionary<string, CubeColliders> CubeColliderList { get; set; }

    public string actionsConfiguration;

	// Use this for initialization
	void Start () {
        if (CubeColliderList == null)
            CubeColliderList = new Dictionary<string, global::CubeColliders>();
        CubeColliderList.Add(this.name, this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //private bool isCollider = false;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (isCollider)
    //    {
    //        isCollider = true;
    //        print("Collider enter with " + this.name + ". Actions : " + actionsConfiguration);
    //        TCPClient.Instance.SendMessage("Collider enter with " + this.name + ". Actions : " + actionsConfiguration);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    isCollider = false;
    //    print("Collider exit with " + this.name + ". Actions : " + actionsConfiguration);
    //    TCPClient.Instance.SendMessage("Collider exit with " + this.name + ". Actions : " + actionsConfiguration);
    //}
}

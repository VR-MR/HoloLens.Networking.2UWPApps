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
}

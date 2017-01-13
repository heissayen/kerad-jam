using UnityEngine;
using System.Collections;

public class TestNetworkEvents : MonoBehaviour {
	public PhotonView photonView;

	string txt;
	int i;

	// Use this for initialization
	void Start () {
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.W))
		{
			SendEvent();
		}

	}

	void SendEvent()
	{
		photonView.RPC("Test", PhotonTargets.All);
	}

	[PunRPC]
	void Test()
	{
		i++;
	}

}

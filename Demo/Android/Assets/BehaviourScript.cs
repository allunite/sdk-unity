using UnityEngine;
using System.Collections;

public class BehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.service.BTService");
		AndroidJavaObject currentActivity = getCurrentActivity();

		allUniteSdk.CallStatic ("startService", currentActivity);
	}

	private AndroidJavaObject getCurrentActivity()
	{
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		return jc.GetStatic<AndroidJavaObject>("currentActivity");
	}
}

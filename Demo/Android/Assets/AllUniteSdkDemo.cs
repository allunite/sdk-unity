using System.Collections;
using UnityEngine;

public class AllUniteSdkDemo : MonoBehaviour {

    public void onStart()
    {
		
    }

    public void requestPermission() {
    	UniAndroidPermission.RequestPremission (AndroidPermission.ACCESS_COARSE_LOCATION, () => {
    		// add permit action
		}, () => {
    		// add not permit action
		});
    }

	public void init() 
	{
		AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
		AndroidJavaObject currentActivity = getCurrentActivity();

		allUniteSdk.CallStatic("init", currentActivity, "Ardas test", "287708C2BE7048A3B4D8518D84E642B3");
	}

	public void bindDevice() 
	{
		AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
		AndroidJavaObject currentActivity = getCurrentActivity();

		allUniteSdk.CallStatic ("bindDevice", currentActivity, "allunite://main");
	}

	public void bindFBProfile() 
	{
		AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
		AndroidJavaObject currentActivity = getCurrentActivity();

		allUniteSdk.CallStatic (
			"bindFBProfile", 
			"{\"id\": \"12345678\", \"birthday\": \"1/1/1950\", \"first_name\": \"Chris\", \"gender\": \"male\", \"last_name\": \"Colm\", \"link\": \"http://www.facebook.com/12345678\", \"location\": { \"id\": \"110843418940484\", \"name\": \"Seattle, Washington\" }, \"locale\": \"en_US\", \"name\": \"Chris Colm\", \"timezone\": -8, \"updated_time\": \"2010-01-01T16:40:43+0000\", \"verified\": true}", 
			"whateverFacebookTokenExample");
	}

	public void tracking()
	{
		AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
		AndroidJavaObject currentActivity = getCurrentActivity();

		currentActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => 
			{
				allUniteSdk.CallStatic ("track", currentActivity, "test", "id");
			}));

	}

    private AndroidJavaObject getCurrentActivity()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        return jc.GetStatic<AndroidJavaObject>("currentActivity");
    }
	
}

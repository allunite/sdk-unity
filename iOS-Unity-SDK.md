IOS Unity SDK - Quick Start Guide
===============================

#### 1. To use AllUniteSdk you need to add dependencies to your Unity app.
```
 * /libAlluniteSdk.a (ios static library)
 * /libBlueCatsSDK.a (ios static library)
 * /AllUniteSdk.cs (c# wrapper AlluniteSdk api)

 Copy libAlluniteSdk.a to Unity project (Assets/Plugins/iOS folder).
 Copy libBlueCatsSDK.a to Unity project (Assets/Plugins/iOS folder)
 Copy AllUniteSdk.cs  to Unity project (Assets folder)
```

#### 2. Build and Run Unity project (Generate IOS Platform)


#### 3. Open Unity XCode project and add to build settings:

Add the value of the Other Linker Flags build setting to the -ObjC to your project

For more details see there: https://developer.apple.com/library/content/qa/qa1490/_index.html


#### 4. Open Unity XCode project and add to info.plist:
```
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
...
	<key>CFBundleURLTypes</key>
	<array>
		...
		<dict>
			<key>CFBundleURLName</key>
			<string>AllUniteSdk</string>
			<key>CFBundleURLSchemes</key>
			<array>
				<string>allunite-sdk</string>
			</array>
		</dict>
		...
	</array>
...
</dict>
</plist>

```
For location and beacon tracking add to info.plist :
```
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
...
	<key>UIBackgroundModes</key>
	<array>
		...
		<string>bluetooth-central</string>
		...
	</array>
	<key>NSBluetoothPeripheralUsageDescription</key>
	<string>Bluetooth Peripheral Usage Description</string>
	<key>NSLocationAlwaysUsageDescription</key>
	<string>description to user - location always usage</string>
	<key>NSLocationWhenInUseUsageDescription</key>
	<string>description to user - location when usage</string>
...
</dict>
</plist>
```

#### 4. Init AllUniteSdk
```
public class AlluniteSdk : MonoBehaviour {

	#if UNITY_IPHONE

	[DllImport("__Internal")]
	private static extern int AllUnite_InitSDK(string accountId, string accountKey);

	#endif

	public void initSdk(){
		String accountId = YOUR_ACCOUNT_ID;
		String accountKey = YOUR_ACCOUNT_KEY;
		int res = AllUnite_InitSDK (accountId, accountKey);
		if (res == 0) {
			print ("Init SDK. Success");
		} else {
			print ("Init SDK. Failed network request");
		}
	}
}
```

Make sure that you've replaced "YOUR_ACCOUNT_ID" and "YOUR_ACCOUNT_KEY" with your customer account and key, and run your app.

#### 5. Add code to track device location and add code to track beacons.
```
public class AlluniteSdk : MonoBehaviour {

	#if UNITY_IPHONE

	[DllImport("__Internal")]
	private static extern void AllUnite_Track(string actionCategory, string actionId);
	[DllImport("__Internal")]
	private static extern int AllUnite_SendBeaconTrack();
	[DllImport("__Internal")]
	private static extern void AllUnite_StopBeaconTrack();
	[DllImport("__Internal")]
	private static extern void AllUnite_RequestAlwaysAuthorization();

	#endif

	public void track(){
		// before should CLLocationManager requestAlwaysAuthorization
		AllUnite_RequestAlwaysAuthorization ();
		
		String actionCategory = "showCampaign";
		String actionId = "blackFriday2016";
		AllUnite_Track (actionCategory , actionId);
	}
	
	public void sendTrack(){
		// before should CLLocationManager requestAlwaysAuthorization
		AllUnite_RequestAlwaysAuthorization ();
		
		int res = AllUnite_SendBeaconTrack ();
		if (res == 0) {
			print ("Beacon track begin. Success");
			AllUnite_StopBeaconTrack ();
		} else {
			print ("Beacon track begin. Failed");
		}
	}
}
```

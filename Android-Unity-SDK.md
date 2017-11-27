Android Unity SDK - Quick Start Guide
===============================

[![](https://jitpack.io/v/allunite/mobile-unity-sdk.svg)](https://jitpack.io/#allunite/mobile-unity-sdk)

### To use AllUniteSdk:
1. Get AllUniteSdk.jar from /app/release folder or build it using 'gradle exportJar'
2. Put jar library into /Assets/Plugins/Android.
3. Call library methods in Unity project:
```csharp
    public void useAllUniteSdk()
    {
        AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
        AndroidJavaObject currentActivity = getCurrentActivity();

        allUniteSdk.CallStatic("init", currentActivity, <your_account_id>, <your_account_key>);
        allUniteSdk.CallStatic ("bindDevice", currentActivity, "android-app://<your_app_package_name>");
    }

    private AndroidJavaObject getCurrentActivity()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        return jc.GetStatic<AndroidJavaObject>("currentActivity");
    }
```
---------------------------------

### Add [deep link](https://developer.android.com/training/app-links/index.html) to your application:

1. Add AndroidManifest.xml to your Unity project using [Build Buddy plugin](https://www.assetstore.unity3d.com/en/#!/content/20041) or manually.
2. Add these permissions:
```xml
    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
```
3. Add an &lt;intent-filter&gt; to __UnityPlayerNativeActivity__. For example:
```xml
    <intent-filter>
        <action android:name="android.intent.action.VIEW" />

        <category android:name="android.intent.category.DEFAULT" />
        <category android:name="android.intent.category.BROWSABLE" />

        <data
            android:host="test"
            android:pathPrefix="sdk"
            android:scheme="allunite" />
    </intent-filter>
```
Now you can use it by calling AllUniteSdk.bindDevice method as described above.

---------------------------------

### Tracking
Tracking differs from other calls and must be executed in UI thread:
```csharp
    	AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
		AndroidJavaObject currentActivity = getCurrentActivity();

		currentActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => 
			{
				allUniteSdk.CallStatic ("track", currentActivity, <action_category>, <action_id>);
			}));
```

And add new permissions to your AndroidManifest.xml file:

```xml
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
```

Otherwise GPS-coords always will be (0.0, 0.0).

### Beacons Tracking
Beacons tracking works in service that starts on device reboot or charger on/off.
Add new permissions to your AndroidManifest.xml file:

```xml
<uses-permission android:name="android.permission.BLUETOOTH" />
<uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />
<uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
```

And add Android services required for tracking and receiver for starting them:

```xml
<receiver android:name="com.allunite.sdk.startup.StartupBroadcastReceiver">
            <intent-filter>
                <action android:name="android.intent.action.BOOT_COMPLETED" />
                <action android:name="android.intent.action.ACTION_POWER_CONNECTED" />
                <action android:name="android.intent.action.ACTION_POWER_DISCONNECTED" />
		<action android:name="com.allunite.sdk.startup.start" />
            </intent-filter>
        </receiver>

        <service
            android:name="com.allunite.sdk.service.BTService"
            android:enabled="true"
            android:exported="false"/>

        <service
            android:name="org.altbeacon.beacon.service.BeaconService"
            android:enabled="true"
            android:exported="false"
            android:isolatedProcess="false"
            android:label="beacon" />

        <service
            android:name="org.altbeacon.beacon.BeaconIntentProcessor"
            android:enabled="true"
            android:exported="false" />
```

Enjoy!

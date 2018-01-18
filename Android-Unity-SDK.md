Android Unity SDK - Quick Start Guide
===============================

[![](https://jitpack.io/v/allunite/mobile-unity-sdk.svg)](https://jitpack.io/#allunite/mobile-unity-sdk)

### To use AllUniteSdk:
1. Enable Gradle build system as described in Unity Documentation. https://docs.unity3d.com/Manual/android-gradle-overview.html

2. Add files to Assets/Plugins/Android/

mainTemplate.gradle:
```gradle
buildscript {
	repositories {
		jcenter()
	}

	dependencies {
		classpath 'com.android.tools.build:gradle:2.1.0'
	}
}

allprojects {
   repositories {
   		jcenter()
    	mavenCentral()
    	maven { url "https://jitpack.io" }
      flatDir {
        dirs 'libs'
      }
   }
}

apply plugin: 'com.android.application'

dependencies {
	compile fileTree(dir: 'libs', include: ['*.jar'])

    compile 'com.github.allunite:mobile-unity-sdk:1.2.18'
    compile 'com.bluecats:bluecats-android-sdk:2.0.7'

**DEPS**}

android {
	compileSdkVersion **APIVERSION**
	buildToolsVersion '**BUILDTOOLS**'

	defaultConfig {
		targetSdkVersion **TARGETSDKVERSION**
		applicationId '**APPLICATIONID**'
	}

	lintOptions {
		abortOnError false
	}

	aaptOptions {
		noCompress '.unity3d', '.ress', '.resource', '.obb'
	}

**SIGN**
	buildTypes {
  		debug {
 			minifyEnabled **MINIFY_DEBUG**
 			useProguard **PROGUARD_DEBUG**
 			proguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-unity.txt'**USER_PROGUARD**
  			jniDebuggable true
  		}
  		release {
 			minifyEnabled **MINIFY_RELEASE**
 			useProguard **PROGUARD_RELEASE**
  			proguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-unity.txt'**USER_PROGUARD**
  			**SIGNCONFIG**
  		}
	}
}
```
AndroidManifest.xml using your AllUnite application credentials (AllUniteId, AllUniteKey and deeplink scheme):
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package=""
    android:installLocation="preferExternal" android:theme="@android:style/Theme.NoTitleBar"
    android:versionCode="1" android:versionName="1.0">
    <supports-screens android:anyDensity="true" android:largeScreens="true"
        android:normalScreens="true" android:smallScreens="true" android:xlargeScreens="true" />
    <application android:debuggable="false" android:icon="@drawable/app_icon"
        android:label="@string/app_name">

	<meta-data
            android:name="AllUniteId"
            android:value="**ALL_UNITE_ID**" />
        <meta-data
            android:name="AllUniteKey"
            android:value="**ALL_UNITE_KEY**" />

        <activity android:name="com.unity3d.player.UnityPlayerNativeActivity"
            android:configChanges="mcc|mnc|locale|touchscreen|keyboard|keyboardHidden|navigation|orientation|screenLayout|uiMode|screenSize|smallestScreenSize|fontScale" android:label="@string/app_name"
            android:launchMode="singleTask"
            android:screenOrientation="landscape">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>

			<intent-filter>
                <action android:name="android.intent.action.VIEW" />
                <category android:name="android.intent.category.DEFAULT" />
                <category android:name="android.intent.category.BROWSABLE" />

                <data
                    android:host="main"
                    android:scheme="**DEEPLINK_SCHEME**" />
	
            </intent-filter>
        </activity>

        <receiver android:name="com.allunite.sdk.startup.StartupBroadcastReceiver">
            <intent-filter>
                <action android:name="android.intent.action.BOOT_COMPLETED" />
                <action android:name="android.intent.action.ACTION_POWER_CONNECTED" />
                <action android:name="android.intent.action.ACTION_POWER_DISCONNECTED" />
                <action android:name="com.allunite.sdk.startup.start" />
            </intent-filter>
        </receiver>

        <service android:name="com.allunite.sdk.service.BCService" />

    </application>
    <uses-sdk android:minSdkVersion="9" android:targetSdkVersion="19" />
    <uses-feature android:glEsVersion="0x00020000" />
    <uses-permission android:name="android.permission.INTERNET" />

    <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
    <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />

    <uses-permission android:name="android.permission.BLUETOOTH" />
    <uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />
    <uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
</manifest>
```
3. Call library methods in Unity project:
```csharp
    public void useAllUniteSdk()
    {
        AndroidJavaObject allUniteSdk = new AndroidJavaObject("com.allunite.sdk.AllUniteSdk");
        AndroidJavaObject currentActivity = getCurrentActivity();

        allUniteSdk.CallStatic("init");
        allUniteSdk.CallStatic ("bindDevice", currentActivity, "android-app://<your_app_package_name>");
    }

    private AndroidJavaObject getCurrentActivity()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        return jc.GetStatic<AndroidJavaObject>("currentActivity");
    }
```
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

Enjoy!

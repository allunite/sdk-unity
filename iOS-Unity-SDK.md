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

#### 5. Add code to bind device to customer's web profile and bind device to customer's facebook profile
```
public class AlluniteSdk : MonoBehaviour {

	#if UNITY_IPHONE

	[DllImport("__Internal")]
	private static extern void AllUnite_BindDevice();
	[DllImport("__Internal")]
	private static extern void AllUnite_BindFbProfile(string profileToken, string profileData);
	
	#endif

	public void bindDevice(){
		AllUnite_BindDevice ();
	}
	
	public void bindFbProfile(){
		String profileToken = getFacebookToken();
		String profileData = getFacebookProfileJson();
		AllUnite_BindFbProfile (profileToken, profileData);
	}
}
```

#### 6. Add code to track device location and add code to track beacons.
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


IOS native SDK - Quick Start Guide
===============================

#### 1. To use AllUniteSdk you need to add dependencies to your XCode project.
```
 * /libAlluniteSdk.a (ios static library)
 * /AllUniteSdk.h
 * /AllUniteSdkManager.h (optional, objective-c wrapper for AllUniteSdk)

 Copy libAlluniteSdk.a to XCode project. 
 Copy AllUniteSdk.h to XCode project
 Copy AllUniteSdkManager.h to XCode project (optional)
 
 Add missing dependencies:
 1. Install CocoaPods. CocoaPods is an Objective-C library dependency manager. You can learn more about CocoaPods from cocoapods.org.
 2. Add the BlueCatsSDK static library dependency to your Podfile:
 	pod 'BlueCatsSDK', :git => 'https://github.com/bluecats/bluecats-ios-sdk.git', :tag => '2.0.0'
 3. To update installed BlueCatsSDK dependency in your project run the following command:
 	pod update
 4. Open XCode project using *.xcworkspace
```

#### 2. Add to info.plist:

Custom Url scheme:
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
For location and beacon tracking add this to info.plist :
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
#### 3. Init AllUniteSdk
```
	#import "AllUniteSdk.h"

	+(void) initSdk
	{
		const char* accountId = YOUR_ACCOUNT_ID;
		const char* accountKey = YOUR_ACCOUNT_KEY;
		int res = AllUnite_InitSDK (accountId, accountKey);
		if (res == 0) {
			NSLog ("Init SDK. Success");
		} else {
			NSLog ("Init SDK. Failed network request");
		}
	}
```

Make sure that you've replaced "YOUR_ACCOUNT_ID" and "YOUR_ACCOUNT_KEY" with your customer account and key, and run your app.

#### 4. Add code to bind device to customer's web profile and bind device to customer's facebook profile
```
	#import "AllUniteSdk.h"

	+(void) bindDevice() {
		AllUnite_BindDevice ();
	}
	
	+ (void) bindFbProfile(){
		const char* profileToken = getFacebookToken();
		const char* profileData = getFacebookProfileJson();
		AllUnite_BindFbProfile (profileToken, profileData);
	}
```

#### 5. Add code to track device location
```
	#import "AllUniteSdk.h"

	+ (void) track(){
		
		... // once
		// before should CLLocationManager requestAlwaysAuthorization
		AllUnite_RequestAlwaysAuthorization();
		...
		
		const char* actionCategory = "showCampaign";
		const char* actionId = "blackFriday2016";
		AllUnite_Track (actionCategory , actionId);
	}
}
```

#### 6. Add code to track beacons.
```
	#import "AllUniteSdk.h"
	
	+ (void) sendBeaconTrack(){
	
		... // once
		// before should CLLocationManager requestAlwaysAuthorization
		AllUnite_RequestAlwaysAuthorization();
		...
		
		int res = AllUnite_SendBeaconTrack ();
		if (res == 0) {
			NSLog ("Success");
			AllUnite_StopBeaconTrack ();
		} else {
			NSLog ("Failed");
		}
	}
```


AllUniteSdkManager class - Quick Start Guide
===============================

AllUniteSdkManager is alternative way of working with SDK API for IOS developer

#### 1. To use AllUniteSdk you need to add dependencies to your XCode project (as descripted above).

#### 2. Add to info.plist (as descripted above)

#### 3. Init AllUniteSdk in application delegate

```
@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {
    
    let alluniteSdk = AllUniteSdkManager.sharedInstance()
    
    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?) -> Bool {
    
    	let accountId = YOUR_ACCOUNT_ID;
	let accountKey = YOUR_ACCOUNT_KEY;
        alluniteSdk.initializeAllUniteSdk(withAccountId: accountId, accountKey: accountKey, launchOptions: launchOptions)
        return true
    }
    
    public func application(_ app: UIApplication, open url: URL, options: [UIApplicationOpenURLOptionsKey : Any] = [:]) -> Bool {
        if (alluniteSdk.open(url, options: options)){
            return true;
        }
	// handle yours url
        
        return true
    }
}
```

#### 4. Now to call AllUniteSdk Api you can use AllUniteSdkManager

```

class YouViewController: UIViewController {
    
    let alluniteSdk = AllUniteSdkManager.sharedInstance()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        bindAndTrackDevice()
    }
    
    
    func detectStateSdk(){
        let enabled:Bool = alluniteSdk.isSdkEnabled()
        print("SdkDisabled \(enabled)");
        if(enabled){
            print("All api functions are available")
        } else {
            print("Only func initializeAllUniteSdk is enabled")
        }
    }
    
    func changeStateSdk(){
        print("You can enable sdk for current device")
        alluniteSdk.setSdkEnabled(true)
        
        print("You can disable sdk for current device")
        alluniteSdk.setSdkEnabled(false)
    }
    
    func bindAndTrackDevice() {
        
        alluniteSdk.requestAutorizationStatus { (status: CLAuthorizationStatus) in
            if status != CLAuthorizationStatus.authorizedWhenInUse
                && status != CLAuthorizationStatus.authorizedAlways {
                print("App don't have permission using CoreLocation")
                return
            }
            
            self.alluniteSdk.reinitilize({ (error) in
                if let err = error {
                    print("Reinitilize failed. Reason: \(err.localizedDescription)")
                    return
                }
                
                self.alluniteSdk.startTrackingBeacon({ (error) in
                    if let _ = error {
                        print("startTrackingBeacon failed")
                        return
                    }
                    print("startTrackingBeacon success")
                }) { (beaconinfo) in
                    print("Beacon detected")
                }
                
                self.alluniteSdk.bindDevice({ (error) in
                    if let _ = error {
                        print("bindDevice failed.")
                        return
                    }
                    print("bindDevice success")
                })
            })
        }
    }
    
    func bindFacebookProfile() {
        
        let token = getFacebookToken()
        let profileJsonData = getFacebookProfileJson()
        
        alluniteSdk.bindFacebook(token, profileJson: profileJsonData) { (error) in
            if let _ = error {
                print("fail")
                return
            }
            print("success")
        }
    }
    
    func sendCampaignTrack() {
        
        let actionCategory = "showCampaign";
        let actionId = "blackFriday2016";
        alluniteSdk.track(withCategory: actionCategory, actionId: actionId) { (error) in
            if let _ = error {
                print("fail")
                return
            }
            print("success")
        }
    }
    
    
    func getFacebookToken() -> String {
        return "facebook user token"
    }
    
    func getFacebookProfileJson() -> String {
        return "facebook profile json for user token"
    }

}
```

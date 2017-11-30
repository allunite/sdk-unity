Unity 2017.02.of3
===============================

Demo account:
```
account_id = UnityDemo
account_key = 2414863EEE4C41EAAE505983A9F2CD23
allunite_scheme = allunite-unity-demo
```

### 1. Open unity demo project

### 2. Build IOS platform

### 3. (Required) Open generated Unity XCode project and add to build settings:

Add the value of the Other Linker Flags build setting to the -ObjC to your project

For more details see there: https://developer.apple.com/library/content/qa/qa1490/_index.html

### 4. (Required) Open generated Unity XCode project and add to info.plist settings:
```
      <key>CFBundleURLTypes</key>
      <array>
          <dict>
              <key>CFBundleURLName</key>
              <string>AllUniteSdk</string>
              <key>CFBundleURLSchemes</key>
              <array>
                  <string>allunite-unity-demo</string>
              </array>
          </dict>
      </array>
      <key>UIBackgroundModes</key>
      <array>
          <string>bluetooth-central</string>
      </array>
      <key>NSBluetoothPeripheralUsageDescription</key>
      <string>Bluetooth Peripheral Usage Description</string>
      <key>NSLocationAlwaysUsageDescription</key>
      <string>description to user - location always usage</string>
      <key>NSLocationWhenInUseUsageDescription</key>
      <string>description to user - location when usage</string>
      <key>NSLocationAlwaysAndWhenInUseUsageDescription</key>
      <string>IOS 11+. description to user - location when always and when usage</string>
```
### 5. Rebuild and run Unity demo app.

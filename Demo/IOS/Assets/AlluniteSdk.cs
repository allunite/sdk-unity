using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;

public class AlluniteSdk : MonoBehaviour {

	#if UNITY_IPHONE

		[DllImport("__Internal")]
		private static extern int AllUnite_enableDebugLog();

		[DllImport("__Internal")]
		private static extern int AllUnite_InitSDK(string accountId, string accountKey);
		[DllImport("__Internal")]
		private static extern void AllUnite_InitSDK_Async(string accountId, string accountKey, Callback callback);
		[DllImport("__Internal")]
		private static extern void AllUnite_Track(string actionCategory, string actionId);
		
		[DllImport("__Internal")]
		private static extern bool AllUnite_isBeaconTracking();
		[DllImport("__Internal")]
		private static extern void AllUnite_SendBeaconTrack();
		[DllImport("__Internal")]
		private static extern void AllUnite_StopBeaconTrack();

		[DllImport("__Internal")]
		private static extern bool AllUnite_isDeviceBounded();
		[DllImport("__Internal")]
		private static extern void AllUnite_BindDevice();
		[DllImport("__Internal")]
		private static extern void AllUnite_BindFbProfile(string profileToken, string profileData);
		[DllImport("__Internal")]
		private static extern void AllUnite_RequestAlwaysAuthorization(Callback callback);


		public delegate void Callback(int error);
		
		[MonoPInvokeCallback(typeof(Callback))]
		private static void asyncInitResult(int res) {
		Debug.Log("Init result: " + res);
			if (res == 0) {
				print ("Init SDK. Success");
			} else {
				print ("Init SDK. Failed network request");
			}
		}

		[MonoPInvokeCallback(typeof(Callback))]
		private static void authorizationPermissionChangedCallback(int res) {
			Debug.Log("Autorization. User changed the autorization status");
			if (res == 0) {
				print ("Autorization. App have permission use Location Service");
			} else {
				print ("Autorization. App don't have permission use Location Service");
			}
		}
	#endif

	public void onClickInitSdk(){

		#if UNITY_IPHONE

		AllUnite_enableDebugLog();

		string accountId = "UnityDemo"; // YOUR_ACCOUNT_ID
		string accountKey = "2414863EEE4C41EAAE505983A9F2CD23"; // YOUR_ACCOUNT_KEY
		int res = AllUnite_InitSDK (accountId, accountKey);
		if (res == 0) {
			print ("Init SDK. Success");
		} else {
			print ("Init SDK. Failed network request");
		}
		#endif
	}

	public void onClickInitSdkAsync(){
		#if UNITY_IPHONE
		Debug.Log("AllUnite_InitSDK_Async");

		AllUnite_enableDebugLog();

		string accountId = "UnityDemo"; // YOUR_ACCOUNT_ID
		string accountKey = "2414863EEE4C41EAAE505983A9F2CD23"; // YOUR_ACCOUNT_KEY
		AllUnite_InitSDK_Async (accountId, accountKey, asyncInitResult);
		#endif
	}

	public void onClickRequestAutorizationStatus() {
		#if UNITY_IPHONE
		AllUnite_RequestAlwaysAuthorization(authorizationPermissionChangedCallback);
		#endif
	}

	public void onClickBindDevice(){
		#if UNITY_IPHONE
		AllUnite_enableDebugLog();
		if (!AllUnite_isDeviceBounded ()) {
			Debug.Log("AllUnite_BindDevice");
			AllUnite_BindDevice ();
		} else {
			Debug.Log ("Device already bounded");
		}
		#endif
	}

	public void onClickTrack(){
		#if UNITY_IPHONE
		Debug.Log("AllUnite_Track");

		string actionCategory = "track";
		string actionId = "test";

		AllUnite_Track (actionCategory, actionId);
		#endif
	}

	public void onClickStartTrack(){
		#if UNITY_IPHONE
		if (!AllUnite_isBeaconTracking ()) {
			Debug.Log("AllUnite_SendBeaconTrack");
			AllUnite_SendBeaconTrack ();
		} else {
			Debug.Log("Device already tracking beacon");
		}
		#endif
	}

	public void onClickStopTrack(){
		#if UNITY_IPHONE
		Debug.Log("AllUnite_StopBeaconTrack");
		AllUnite_StopBeaconTrack();
		#endif
	}

	public void onClickBindFbProfile(){
		#if UNITY_IPHONE
		Debug.Log("AllUnite_StopBeaconTrack");
		string profileToken = "ADlwLwlsdj42j";
		string profileJsonData = "{\"id\": \"12345678\", \"birthday\": \"1/1/1950\", \"first_name\": \"Chris\", \"gender\": \"male\", \"last_name\": \"Colm\", \"link\": \"http://www.facebook.com/12345678\", \"location\": { \"id\": \"110843418940484\", \"name\": \"Seattle, Washington\" }, \"locale\": \"en_US\", \"name\": \"Chris Colm\", \"timezone\": -8, \"updated_time\": \"2010-01-01T16:40:43+0000\", \"verified\": true}"; 
		AllUnite_BindFbProfile (profileToken, profileJsonData);
		#endif
	}

	public void onEnableDebugLog(){
		#if UNITY_IPHONE
		Debug.Log("AllUnite_enableDebugLog");
		AllUnite_enableDebugLog();
		#endif
	}

}

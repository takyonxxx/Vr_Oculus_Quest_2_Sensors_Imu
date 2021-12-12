using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace tbiliyor.bluetooth
{

public class BluetoothAdapter
{

    static AndroidJavaClass _JavaClass = null;
    static AndroidJavaClass JavaClass
    {
        get
        {
            if (_JavaClass == null)
            {
                _JavaClass = new AndroidJavaClass("android.bluetooth.BluetoothAdapter");
            }

            return _JavaClass;
        }
    }

    AndroidJavaObject JavaObject;

    BluetoothAdapter(AndroidJavaObject obj)
    {
        JavaObject = obj;
    }

    public static BluetoothAdapter getDefaultAdapter()
    {
        var obj = JavaClass.CallStatic<AndroidJavaObject>("getDefaultAdapter");

        return new BluetoothAdapter(obj);
    }

    public bool isEnabled()
    {
        return JavaObject.Call<bool>("isEnabled");
    }

    public string getName()
    {
        try
        {
            return JavaObject.Call<string>("getName");
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }

    public ICollection<BluetoothDevice> getBondedDevices()
    {
        var devices = new Collection<BluetoothDevice>();

        var DevsSet = JavaObject.Call<AndroidJavaObject>("getBondedDevices");
        var DevsIter = DevsSet.Call<AndroidJavaObject>("iterator");

        while (DevsIter.Call<bool>("hasNext"))
        {
            var dev = DevsIter.Call<AndroidJavaObject>("next");
            devices.Add(new BluetoothDevice(dev));
        }

        return devices;
    }

        /*using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
        {
            try
            {
            AndroidJavaClass bluetooth = new AndroidJavaClass("android.bluetooth.BluetoothAdapter");
            AndroidJavaObject bluetoothAdapter = bluetooth.CallStatic<AndroidJavaObject>("getDefaultAdapter");

            textInfo.text = "Ble connected " + bluetoothAdapter.Call<string>("getName");

                if (!bluetoothAdapter.Call<bool>("isEnabled"))
                {
                    bluetoothAdapter.Call<bool>("enable");
                }

                AndroidJavaObject mBluetoothLeScanner = bluetoothAdapter.Call<AndroidJavaObject>("getBluetoothLeScanner");
                ScanCallback scanCallback = new ScanCallback();

                mBluetoothLeScanner.Call("startScan", scanCallback);
        catch (Exception e)
        {
            textInfo.text = e.ToString();
        }
        }*/

        /*private static bool IsBluetoothEnabled()
    {
        using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
        {
            try
            {
                Debug.Log("Requesting android bluetooth system service");
                using (var BluetoothManager = activity.Call<AndroidJavaObject>("getSystemService", "bluetooth"))
                {
                    var BluetoothAdapter = BluetoothManager.Call<AndroidJavaObject>("getAdapter");
                    if (BluetoothAdapter == null)
                    {
                        return false;
                    }                   

                   return BluetoothAdapter.Call<bool>("isEnabled");
                }
            }
            catch (Exception)
            {
                Debug.LogError("Failed to query Bluetooth sensor state");
            }
        }
        return false;
    }*/
    }
}

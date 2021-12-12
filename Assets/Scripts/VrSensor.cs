using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using XRStats = Unity.XR.Oculus.Stats;
using tbiliyor.bluetooth;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class VrSensor : MonoBehaviour
{
    private TextMeshProUGUI textInfo;
    private Button buttonExit;

    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice deviceCenterEye;
    private InputDevice deviceLeftController;
    private InputDevice deviceRightController;

    private Vector3 EyePos;
    private Quaternion EyeRotation;

    private Vector3 RightHandPos;
    private float RightHandTrigger;

    private Vector3 LeftHandPos;
    private float LeftHandTrigger;

    static int LogLines = 0;
    private bool IsConnected;

    // Start is called before the first frame update
    void Start()
    {       
        textInfo = GameObject.Find("textInfo").GetComponent<TextMeshProUGUI>();
        textInfo.text = "..." + "\n";
        
        buttonExit = GameObject.Find("buttonExit").GetComponent<Button>();
        buttonExit.onClick.AddListener(ExitApp);

        IsConnected = false;

        var ba = BluetoothAdapter.getDefaultAdapter();
        textInfo.text += ba.getName();
    }
   
    public void Log(string format, params object[] args)
    {
        var msg = string.Format(format, args);
        textInfo.text += msg + "\n";
        Debug.Log(msg);
        if ((LogLines += 1) > 32)
        {/* trim away first line */
            var log = textInfo.text;
            textInfo.text = log.Substring(log.IndexOf('\n') + 1);
            LogLines -= 1;
        }
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.CenterEye, devices);
        deviceCenterEye = devices[devices.Count - 1];

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);
        deviceLeftController = devices[devices.Count - 1];

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        deviceRightController = devices[devices.Count - 1];
    }

    void OnEnable()
    {
        if (!deviceCenterEye.isValid)
        {
            GetDevice();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        //Get device if not valid
        /* if (!deviceCenterEye.isValid)
        {
            GetDevice();
        }

        if (deviceCenterEye.isValid)
        {
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyePosition, out EyePos);
            deviceCenterEye.TryGetFeatureValue(CommonUsages.centerEyeRotation, out EyeRotation);
        }

        if (deviceLeftController.isValid)
        {
            deviceLeftController.TryGetFeatureValue(CommonUsages.devicePosition, out LeftHandPos);
            deviceLeftController.TryGetFeatureValue(CommonUsages.trigger, out LeftHandTrigger);
        }

        if (deviceRightController.isValid)
        {
            deviceRightController.TryGetFeatureValue(CommonUsages.devicePosition, out RightHandPos);
            deviceRightController.TryGetFeatureValue(CommonUsages.trigger, out RightHandTrigger);
        }

        string info = "";

        info = "Device Name : " + SystemInfo.deviceName + "\n";      
        info += "\n";
        info += "Pos:   " + EyePos.ToString() + "\n";
        info += "IMU X: " + EyeRotation.x.ToString("F2") + " Y: " + EyeRotation.y.ToString("F2") + " Z: " + EyeRotation.z.ToString("F2") + "\n";
        info += "\n";
        info += "Left  TRig: " + LeftHandTrigger.ToString("F2") +"\n";
        info += "Right TRig: " + RightHandTrigger.ToString("F2");

        textInfo.text = info;*/
    }
  
    public void ExitApp()
    {    	
        Application.Quit();
    }
}

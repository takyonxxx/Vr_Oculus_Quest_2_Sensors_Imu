using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using XRStats = Unity.XR.Oculus.Stats;

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


    // Start is called before the first frame update
    void Start()
    {       
        textInfo = GameObject.Find("textInfo").GetComponent<TextMeshProUGUI>();
        textInfo.text = "---";
        
        buttonExit = GameObject.Find("buttonExit").GetComponent<Button>();
        buttonExit.onClick.AddListener(ExitApp);
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
        if (!deviceCenterEye.isValid || !deviceLeftController.isValid || !deviceRightController.isValid)
        {
            GetDevice();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get device if not valid
        if (!deviceCenterEye.isValid || !deviceLeftController.isValid || !deviceRightController.isValid)
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
        /*info += "maxTextureSize : " + SystemInfo.maxTextureSize.ToString() + "\n";
        info += "operatingSystem : " + SystemInfo.operatingSystem + "\n";
        info += "processorFrequency : " + SystemInfo.processorFrequency.ToString() + "\n";
        info += "systemMemorySize : " + SystemInfo.systemMemorySize.ToString() + "\n";
        info += "PluginVersion : " + XRStats.PluginVersion + "\n";
        info += "BatteryLevel : " + SystemInfo.batteryLevel.ToString("F2") + "\n";*/
        info += "\n";
        info += "Pos:   " + EyePos.ToString() + "\n";
        info += "IMU X: " + EyeRotation.x.ToString("F2") + " Y: " + EyeRotation.y.ToString("F2") + " Z: " + EyeRotation.z.ToString("F2") + "\n";
        info += "\n";
        info += "Left  TRig: " + LeftHandTrigger.ToString("F2") +"\n";
        info += "Right TRig: " + RightHandTrigger.ToString("F2");

        textInfo.text = info;
    }
  
    public void ExitApp()
    {    	
        Application.Quit();
    }
}

using UnityEngine;


/// <summary>
/// 华清远见
/// 基于Unity内置陀螺仪方法的控制文件
/// 时间：2016年11月18日10:42:00
/// </summary>


public class HQYJ_Gyroscope_controller : MonoBehaviour
{
    #region [私有属性]
    private bool gyroEnabled = true;
    private const float lowPassFilterFactor = 0.2f;
    private readonly Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);
    private readonly Quaternion landscapeRight = Quaternion.Euler(0, 0, 90);
    private readonly Quaternion landscapeLeft = Quaternion.Euler(0, 0, -90);
    private readonly Quaternion upsideDown = Quaternion.Euler(0, 0, 180);
    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion calibration = Quaternion.identity;
    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion baseOrientationRotationFix = Quaternion.identity;
    private Quaternion referanceRotation = Quaternion.identity;
    private bool debug = true;
    #endregion

    #region [Unity内置方法]
    protected void Start()
    {
        AttachGyro();//载入对应参数
        Input.gyro.enabled = true;//打开陀螺仪
    }

    protected void Update()
    {
        if (!gyroEnabled)
            return;
        //每帧变化摄像头角度
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraBase * (ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()), lowPassFilterFactor);

    }
    /// <summary>
    /// 主要是做Debug使用的方法
    /// </summary>
    //protected void OnGUI()
    //{


    //    if (!debug)
    //        return;

    //    GUILayout.Label("Orientation: " + Screen.orientation);
    //    GUILayout.Label("Calibration: " + calibration);
    //    GUILayout.Label("Camera base: " + cameraBase);
    //    GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
    //    GUILayout.Label("transform.rotation: " + transform.rotation);

    //    if (GUILayout.Button("On/off gyro: " + Input.gyro.enabled, GUILayout.Height(100)))
    //    {
    //        Input.gyro.enabled = !Input.gyro.enabled;
    //    }

    //    if (GUILayout.Button("On/off gyro controller: " + gyroEnabled, GUILayout.Height(100)))
    //    {
    //        if (gyroEnabled)
    //        {
    //            DetachGyro();
    //        }
    //        else
    //        {
    //            AttachGyro();
    //        }
    //    }

    //    if (GUILayout.Button("Update gyro calibration (Horizontal only)", GUILayout.Height(80)))
    //    {
    //        UpdateCalibration(true);
    //    }

    //    if (GUILayout.Button("Update camera base rotation (Horizontal only)", GUILayout.Height(80)))
    //    {


    //        UpdateCameraBaseRotation(true);


    //    }






    //    if (GUILayout.Button("Reset base orientation", GUILayout.Height(80)))
    //    {


    //        ResetBaseOrientation();


    //    }






    //    if (GUILayout.Button("Reset camera rotation", GUILayout.Height(80)))
    //    {


    //        transform.rotation = Quaternion.identity;


    //    }


    //}
    #endregion

    #region [公共方法]

    /// <summary>
    /// Attaches gyro controller to the transform.
    /// </summary>
    private void AttachGyro()
    {
        //如果开始默认不开启陀螺仪则更改此值为true
        gyroEnabled = true;
        ResetBaseOrientation();
        UpdateCalibration(true);
        UpdateCameraBaseRotation(true);
        RecalculateReferenceRotation();
    }

    /// <summary>
    /// Detaches gyro controller from the transform
    /// </summary>
    private void DetachGyro()
    {
        gyroEnabled = false;
    }
    #endregion

    #region [私有方法]
    /// <summary>
    /// 更新陀螺仪
    /// </summary>
    private void UpdateCalibration(bool onlyHorizontal)
    {
        if (onlyHorizontal)
        {
            var fw = (Input.gyro.attitude) * (-Vector3.forward);
            fw.z = 0;
            if (fw == Vector3.zero)
            {
                calibration = Quaternion.identity;
            }
            else
            {
                calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
            }
        }
        else
        {
            calibration = Input.gyro.attitude;
        }
    }

    /// <summary>
    /// Update the camera base rotation.
    /// </summary>
    /// <param name='onlyHorizontal'>
    /// Only y rotation.
    /// </param>
    private void UpdateCameraBaseRotation(bool onlyHorizontal)
    {
        if (onlyHorizontal)
        {
            var fw = transform.forward;
            fw.y = 0;
            if (fw == Vector3.zero)
            {
                cameraBase = Quaternion.identity;
            }
            else
            {
                cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
            }
        }
        else
        {
            cameraBase = transform.rotation;
        }
    }

    /// <summary>
    /// Converts the rotation from right handed to left handed.
    /// </summary>
    /// <returns>
    /// The result rotation.
    /// </returns>
    /// <param name='q'>
    /// The rotation to convert.
    /// </param>
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    /// <summary>
    /// Gets the rot fix for different orientations.
    /// </summary>
    /// <returns>
    /// The rot fix.
    /// </returns>
    private Quaternion GetRotFix()
    {
        return Quaternion.identity;
    }

    /// <summary>
    /// Recalculates reference system.
    /// </summary>
    private void ResetBaseOrientation()
    {
        baseOrientationRotationFix = GetRotFix();
        baseOrientation = baseOrientationRotationFix * baseIdentity;
    }

    /// <summary>
    /// Recalculates reference rotation.
    /// </summary>
    private void RecalculateReferenceRotation()
    {
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }
    #endregion
}


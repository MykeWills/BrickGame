using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Rewired;
using System;

public class OptimizeSystem : MonoBehaviour
{
    public static BinaryFormatter _BinaryFormatter = new BinaryFormatter();
    public static Color _colorAlpha = new Color();
    public static Player _InputPlayer;
    public static bool _Active = false;
    public static Vector3 _Vector3 = new Vector3();
    public static Vector2 _Vector2 = new Vector2();
    public static Quaternion _Quaternion = new Quaternion();
    public static IEnumerator routine = null;
    public static Func<bool> func;
    public static WaitUntil _waitUntil = new WaitUntil(func);
    static Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(100);
    static Dictionary<float, WaitForSecondsRealtime> _realTimeInterval = new Dictionary<float, WaitForSecondsRealtime>(100);
    public static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
    public static WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    public void Awake() { _InputPlayer = ReInput.players.GetPlayer(0); }
    public static Player playerController { get { return _InputPlayer; } }
    public static WaitForEndOfFrame EndOfFrame { get { return _endOfFrame; } }
    public static WaitUntil WaitFor(bool active) { func = () => active; return _waitUntil; }
    public static WaitForFixedUpdate FixedUpdate { get { return _fixedUpdate; } }
    public static WaitForSeconds Wait(float seconds)
    {
        if (!_timeInterval.ContainsKey(seconds))
            _timeInterval.Add(seconds, new WaitForSeconds(seconds));
        return _timeInterval[seconds];
    }
    public static WaitForSecondsRealtime WaitRealtime(float seconds)
    {
        if (!_realTimeInterval.ContainsKey(seconds))
            _realTimeInterval.Add(seconds, new WaitForSecondsRealtime(seconds));
        return _realTimeInterval[seconds];
    }
    public static BinaryFormatter ChangeBinaryFormatter
    {
        get { return _BinaryFormatter; }
    }
    public static Color ChangeColor(float r, float g, float b, float a)
    {
        _colorAlpha.r = r;
        _colorAlpha.g = g;
        _colorAlpha.b = b;
        _colorAlpha.a = a;
        return _colorAlpha;
    }

    public static Vector3 ChangeVector3(float x, float y, float z)
    {
        _Vector3.x = x;
        _Vector3.y = y;
        _Vector3.z = z;
        return _Vector3;
    }
    public static Vector2 ChangeVector2(float x, float y)
    {
        _Vector2.x = x;
        _Vector2.y = y;
        return _Vector2;
    }
    public static Quaternion ChangeQuaternion(float x, float y, float z, float w)
    {
        _Quaternion.x = x;
        _Quaternion.y = y;
        _Quaternion.y = z;
        _Quaternion.y = w;
        return _Quaternion;
    }
    public static void ClearOutRenderTexture(RenderTexture renderTexture)
    {
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }
    public static void GameMouseActive(bool active, CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = active;
    }
}

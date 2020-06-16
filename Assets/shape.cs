
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 形狀:基底類別
/// </summary>
public class shape : MonoBehaviour
{
    [Header("長寬高")]
    public float length;
    public float width;
    public float height;

    /// <summary>
    /// 體積
    /// </summary>
    public float volume;


    /// <summary>
    /// 取得體積
    /// </summary>
    /// <return>體積</return>
    public virtual float GetVolume()
    {
    volume= length* width * height;
        return volume;
}
   
}

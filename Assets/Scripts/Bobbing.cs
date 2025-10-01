using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public Vector3 bobDirection;
    public Vector3 bobSpeed;
    public WaveType xType, yType, zType;

    public enum WaveType
    {
        Sin,
        Cos,
        AbsoluteSin,
        AbsoluteCos
    }

    float transitionSpeed = 17f;
    Vector3 restPos, camPos, startPos;
    float newDir;

    void Start()
    {
        startPos = transform.localPosition;
        restPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(
            Mathf.Lerp(camPos.x, restPos.x + GetDir(bobSpeed.x, bobDirection.x, xType), transitionSpeed * Time.deltaTime),
            Mathf.Lerp(camPos.y, restPos.y + GetDir(bobSpeed.y, bobDirection.y, yType), transitionSpeed * Time.deltaTime),
            Mathf.Lerp(camPos.z, restPos.z + GetDir(bobSpeed.z, bobDirection.z, zType), transitionSpeed * Time.deltaTime));
        transform.localPosition = newPosition;
        camPos = newPosition;
    }

    float GetDir(float bobSpeed, float bobDir, WaveType wavetype)
    {
        switch(wavetype)
        {
            case WaveType.Sin:
                newDir = Mathf.Sin(Time.time * (bobSpeed * 2)) * bobDir;
                break;

            case WaveType.Cos:
                newDir = Mathf.Cos(Time.time * (bobSpeed * 2)) * bobDir;
                break;

            case WaveType.AbsoluteSin:
                newDir = Mathf.Abs(Mathf.Sin(Time.time * (bobSpeed * 2))) * bobDir;
                break;

            case WaveType.AbsoluteCos:
                newDir = Mathf.Abs(Mathf.Cos(Time.time * (bobSpeed * 2))) * bobDir;
                break;
        }

        return newDir;
    }
}
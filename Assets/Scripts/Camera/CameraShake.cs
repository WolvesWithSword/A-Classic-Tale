using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region SINGLETON
    private static CameraShake instance;
    public static CameraShake Instance { get { return instance; } } // Accessor
    #endregion


    public float testRotation = 40f;
    public float testTime = 0.5f;
    public float testPower = 0.1f;

    private float shakeTimeRemaining;
    private float shakePower;
    private float rotationMultiplier;
    private float shakeFadeTime;
    private float shakeRotation;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShaking)
        {
            if (Input.GetKey(KeyCode.K))
            {
                StartShake(testTime, testPower, testRotation);
            }

            if(Vector3.Distance(transform.position, startPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, 8 * Time.deltaTime);
            }
            else
            {
                transform.position = startPosition;//To reset completely
            }
        }
        

    }

    private void LateUpdate()
    {
        if (!isShaking) return; 

        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
        }
        else
        {
            isShaking = false;
            transform.rotation = startRotation;
            // Can do Vector3.MoveTowards here because can't finish to replace
        }
    }

    public void StartShake(float time, float power,float rotation)
    {
        // If spam StartShake can change camera position 
        // and here it's fixed so don't bother too much
        // startPosition = transform.position;
        // startRotation = transform.rotation;
        rotationMultiplier = rotation;
        shakeTimeRemaining = time;
        shakePower = power;
        shakeFadeTime = power / time;
        shakeRotation = power * rotationMultiplier;
        isShaking = true;

    }
}

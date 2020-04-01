using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public Transform player;
    Vector3 target, mousePos, refVel, shakeOffset;
    float cameraDist = 3.5f;
    public float smoothTime = 0.4f;
    float zStart;
    float shakeMag, shakeTimeEnd;
    Vector3 shakeVector;
    bool shaking;
    // Start is called before the first frame update
    void Start() {
        target = player.position;
        zStart = transform.position.z;
    }

    // Update is called once per frame
    void Update() {
        mousePos = CaptureMousePos();
        shakeOffset = UpdateShake();
        target = UpdateTargetPos();
        UpdateCameraPosition();


        Vector3 CaptureMousePos() {
            Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            ret *= 2;
            ret -= Vector2.one;
            float max = 0.9f;
            if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max) {
                ret = ret.normalized;
            }
            return ret;
        }

        Vector3 UpdateTargetPos() {
            Vector3 mouseOffset = mousePos * cameraDist;
            Vector3 ret = player.position + mouseOffset;
            ret += shakeOffset;
            ret.z = zStart;
            return ret;
        }

        Vector3 UpdateShake() {
            if (!shaking || Time.time > shakeTimeEnd) {
                shaking = false;
                return Vector3.zero;
            }
            Vector3 tempOffset = shakeVector;
            tempOffset *= shakeMag;
            return tempOffset;
        }
    }
    void UpdateCameraPosition() {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }

    public void Shake(Vector3 direction, float magnitude, float length) {
        shaking = true;
        shakeVector = direction;
        shakeMag = magnitude;
        shakeTimeEnd = Time.time + length;
    }


}

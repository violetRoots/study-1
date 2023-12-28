using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private float destroyForce = 50.0f;
    [SerializeField] private float destroyTorque = 50.0f;

    [SerializeField] private Rigidbody leftWall;
    [SerializeField] private Rigidbody rightWall;
    [SerializeField] private Rigidbody upWall;
    [SerializeField] private Rigidbody downWall;
    [SerializeField] private Rigidbody frontWall;
    [SerializeField] private Rigidbody backWall;

    private void Awake()
    {
        leftWall.isKinematic = true;
        rightWall.isKinematic = true;
        upWall.isKinematic = true;
        downWall.isKinematic = true;
        frontWall.isKinematic = true;
        backWall.isKinematic = true;
    }

    public void DestroyCage()
    {
        leftWall.isKinematic = false;
        leftWall.AddForce(Vector3.left * destroyForce);
        leftWall.AddTorque(Random.insideUnitSphere * destroyTorque);

        rightWall.isKinematic = false;
        rightWall.AddForce(Vector3.right * destroyForce);
        rightWall.AddTorque(Random.insideUnitSphere * destroyTorque);

        upWall.isKinematic = false;
        upWall.AddForce(Vector3.up * destroyForce);
        upWall.AddTorque(Random.insideUnitSphere * destroyTorque);

        downWall.isKinematic = false;
        downWall.AddForce(Vector3.down * destroyForce);
        downWall.AddTorque(Random.insideUnitSphere * destroyTorque);

        frontWall.isKinematic = false;
        frontWall.AddForce(Vector3.back * destroyForce);
        frontWall.AddTorque(Random.insideUnitSphere * destroyTorque);

        backWall.isKinematic = false;
        backWall.AddForce(Vector3.forward * destroyForce);
        backWall.AddTorque(Random.insideUnitSphere * destroyTorque);
    }
}

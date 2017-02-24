using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];

    private Rigidbody rigidBody;
    private GameManager gameManager;


    // Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody> ();
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.recording) {
            Record ();
        } else {
            Playback ();
        }
    }

    private void Playback () {
        rigidBody.isKinematic = true;

        if (Time.frameCount < bufferFrames) {   // buffer underflow
            int frame = Time.frameCount % bufferFrames;
            //print ("Reading frame: " + frame);

            transform.position = keyFrames[frame].position;
            transform.rotation = keyFrames[frame].rotation;

            // Debug.LogWarning ("Underflow");
        } else {
            int frame = Time.frameCount % bufferFrames;
            //print ("Reading frame: " + frame);

            transform.position = keyFrames[frame].position;
            transform.rotation = keyFrames[frame].rotation;
        }
    }

    private void Record () {
        rigidBody.isKinematic = false;

        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        //print ("Writing frame: " + frame);

        keyFrames[frame] = new MyKeyFrame (time, transform.position, transform.rotation);
    }

    /// <summary>
    /// A structure for storing time, position and rotation
    /// </summary>
    public struct MyKeyFrame {
        public float frameTime;
        public Vector3 position;
        public Quaternion rotation;

        public MyKeyFrame (float aFrameTime, Vector3 aPosition, Quaternion aRotation) {
            frameTime = aFrameTime;
            position = aPosition;
            rotation = aRotation;

        }
    }
}

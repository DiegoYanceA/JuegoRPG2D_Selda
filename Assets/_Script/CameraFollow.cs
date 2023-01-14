    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        public GameObject target;
        private Vector3 targetPosition;
        public float cameraSpeed = 3.0f;

        private Camera theCamera;
        private Vector3 minLimits;
        private Vector3 maxLimits;

        public float halfHeight;
        public float halfWidth;


        public void ChangeScene(BoxCollider2D cameraLimits)
        {
            minLimits = cameraLimits.bounds.min;
            maxLimits = cameraLimits.bounds.max;

            theCamera = GetComponent<Camera>();

            halfHeight = theCamera.orthographicSize;
            halfWidth = halfHeight * Screen.width / Screen.height;
        }

        // Update is called once per frame
        void Update()
        {
            float posX = Mathf.Clamp(this.target.transform.position.x, minLimits.x + halfWidth, maxLimits.x - halfWidth);
            float posY = Mathf.Clamp(this.target.transform.position.y, minLimits.y + halfHeight, maxLimits.y - halfHeight);
            targetPosition = new Vector3(posX, posY , this.transform.position.z);
        
        }

        private void LateUpdate()
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        }
    }

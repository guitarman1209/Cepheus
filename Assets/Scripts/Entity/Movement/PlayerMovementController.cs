using UnityEngine;

namespace Entity.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        public float verticalInputAcceleration = 1;
        public float horizontalInputAcceleration = 20;
 
        public float maxSpeed = 100;
        public float maxRotationSpeed = 110;
        public float rotationStopSpeed = 2;
 
        public float velocityDrag = 0.2f;
        public float rotationDrag = 0.1f;
 
        private Vector3 velocity;
        private float zRotationVelocity;
        private float rotationStopCutoff = 2;
 
        private void Update()
        {
            // apply forward input
            var acceleration = Input.GetAxis("Vertical") * verticalInputAcceleration * transform.up;
            velocity += acceleration * Time.deltaTime;
 
            // apply turn input
            var zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration;
            zRotationVelocity += zTurnAcceleration * Time.deltaTime;
        }
 
        private void FixedUpdate()
        {
            // apply velocity drag
            velocity *= (1 - Time.deltaTime * velocityDrag);
 
            // clamp to maxSpeed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            // only apply rotation if turn key is active
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                // apply rotation drag
                zRotationVelocity *= (1 - Time.deltaTime * rotationDrag);

                // clamp to maxRotationSpeed
                zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);
            }
            // slowly but steadily stop the ship from continuously spinning if no turn input is pressed
            else
            {
                // still need to slow
                if (zRotationVelocity != 0)
                    zRotationVelocity = Mathf.Lerp(zRotationVelocity, 0, rotationStopSpeed * Time.deltaTime);
            
                // reached minimum cutoff - set at 0 - stop rotation
                if (Mathf.Abs(zRotationVelocity) <= rotationStopCutoff)
                    zRotationVelocity = 0;
            }

            // update transform
            transform.position += velocity * Time.deltaTime;
            transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
            //Debug.Log($"v:{velocity} zr:{zRotationVelocity}");
        }
    }
}

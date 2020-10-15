using CameraUtil;
using Entity.Movement;
using UnityEngine;

namespace Entity.Ship
{
    public class ShipTransformController : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject shipPrefab;
        public Sprite[] frigates,
            destroyers,
            cruisers,
            carriers;
        
        private void Start()
        {
            // spawn the player prefab
            var player = SpawnPlayer();
            
            // assign it a ship sprite
            player.GetComponent<SpriteRenderer>().sprite = frigates[0];

            // test frigate settings
            var settings = player.GetComponent<PlayerMovementController>();
            settings.horizontalInputAcceleration = 40;
            settings.verticalInputAcceleration = 15;
            settings.maxSpeed = 150;//100
            settings.maxRotationSpeed = 80;//110
            //settings.rotationStopSpeed = 4;//2
            settings.velocityDrag = 0.4f;//0.2
            settings.rotationDrag = 0.05f;//0.1
            
            // set initial cam target to fresh player spawn
            GetComponent<CameraController>().target = player;
        }

        public GameObject SpawnPlayer()
        {
            // basic spawn method
            return Instantiate(playerPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        }
    }
}
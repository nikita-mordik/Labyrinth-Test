﻿using UnityEngine;

namespace FreedLOW._Maze.Scripts.Common
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform following;
        [SerializeField] private float rotationAngleX;
        [SerializeField] private float distance;
        [SerializeField] private float offsetY;
        
        private void LateUpdate()
        {
            if (following == null) return;

            var rotation = Quaternion.Euler(rotationAngleX, 90f, 0f);
            var position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject follower)
        {
            following = follower.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
    }
}
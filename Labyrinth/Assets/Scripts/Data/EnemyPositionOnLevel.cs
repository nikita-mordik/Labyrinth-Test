using System;

namespace Data
{
    [Serializable]
    public class EnemyPositionOnLevel
    {
        public string Id;
        public Vector3Data Position;
        public Vector3Data Rotation;

        public EnemyPositionOnLevel() { }

        public EnemyPositionOnLevel(string id, Vector3Data position, Vector3Data rotation)
        {
            Id = id;
            Position = position;
            Rotation = rotation;
        }
    }
}
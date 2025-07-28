using UnityEngine;
using Unity.Entities;

public class CubeSpawnerAuthoring : MonoBehaviour
{
    public GameObject cubeprefab;
    public float spawnRate;

    class CubeSpawnerBaker : Baker<CubeSpawnerAuthoring> 
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new CubeSpawnerComponent
            {
                prefab = GetEntity(authoring.cubeprefab, TransformUsageFlags.Dynamic),
                spawnPOS = authoring.transform.position,
                nextTimeSpawn = 0.0f,
                spawnRate = authoring.spawnRate
            }) ;
        }
    }

}

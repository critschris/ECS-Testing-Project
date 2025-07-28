using Unity.Entities;
using Unity.Mathematics;
public struct CubeSpawnerComponent : IComponentData 
{
    public Entity prefab;
    public float3 spawnPOS;
    public float nextTimeSpawn;
    public float spawnRate;
}

using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

[BurstCompile]
public partial struct CubeSpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.TryGetSingletonEntity<CubeSpawnerComponent>(out Entity spawnerEntity))
        {
            return;
        }
        RefRW<CubeSpawnerComponent> spawner = SystemAPI.GetComponentRW<CubeSpawnerComponent>(spawnerEntity);

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        if (spawner.ValueRO.nextTimeSpawn < SystemAPI.Time.ElapsedTime)
        {
            Entity newEntity = ecb.Instantiate(spawner.ValueRO.prefab);

            ecb.AddComponent(newEntity, new CubeMovementComponent
            {
                moveDirection = Random.CreateFromIndex((uint)(SystemAPI.Time.ElapsedTime / SystemAPI.Time.DeltaTime)).NextFloat3(),
                moveSpeed = 10
            }) ;

            spawner.ValueRW.nextTimeSpawn = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

            //To execute all commands in the ecb and import the entity manager
            ecb.Playback(state.EntityManager);
        }
    }
}

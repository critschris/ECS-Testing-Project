using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Transforms;

namespace ECS
{
    public partial struct CubeSystem : ISystem
    {
        
        public void OnUpdate(ref SystemState state)
        {
            EntityManager entityManager = state.EntityManager;

            NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);

            //get all and loop through them all
            foreach (Entity entity in entities)
            {
                if (entityManager.HasComponent<CubeMovementComponent>(entity))
                {
                    CubeMovementComponent cube = entityManager.GetComponentData<CubeMovementComponent>(entity);
                    LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);

                    float3 moveDir = cube.moveDirection * SystemAPI.Time.DeltaTime * cube.moveSpeed;

                    localTransform.Position = localTransform.Position + moveDir;
                    //Need to set changes back to entity after changing variables
                    entityManager.SetComponentData<LocalTransform>(entity, localTransform);

                    if (cube.moveSpeed > 0)
                    {
                        cube.moveSpeed -= SystemAPI.Time.DeltaTime;
                    }
                    else
                    {
                        cube.moveSpeed = 0;
                    }

                    //Need to set changes
                    entityManager.SetComponentData<CubeMovementComponent>(entity, cube);
                }
            }
        }
    }
}



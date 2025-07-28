using Unity.Entities;
using Unity.Mathematics;

public class CubeMovementComponent : IComponentData
{
    public float3 moveDirection;
    public float moveSpeed;
}

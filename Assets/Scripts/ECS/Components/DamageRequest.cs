using ECS.Services;

namespace ECS.Components
{
    public struct DamageRequest
    {
        public Entity Target;
        public float Amount;
    }

    public struct Health
    {
        public float Value;
    }

    public struct DeathRequest { }
}
using System;

namespace ECS.Services
{
    [AttributeUsage(AttributeTargets.Struct)]
    public sealed class OneFrameAttribute : Attribute { }
}
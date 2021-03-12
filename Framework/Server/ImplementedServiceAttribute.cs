﻿// (C) KAL ATM Software GmbH, 2021

using System;
using XFS4IoT;

namespace XFS4IoTServer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImplementedServiceAttribute : Attribute
    {
        public ImplementedServiceAttribute(Type ServiceClass)
        {
            ServiceClass.IsNotNull($"Invalid type of service class received in the {nameof(ImplementedServiceAttribute)} constructor. {nameof(ServiceClass)}");
            this.ServiceClass = ServiceClass;
        }

        public Type ServiceClass { get; }
    }
}
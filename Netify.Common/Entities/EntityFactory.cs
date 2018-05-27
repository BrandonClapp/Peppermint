using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Netify.Common.Entities
{
    public class EntityFactory
    {
        private IServiceProvider _container;
        public EntityFactory(IServiceProvider container)
        {
            _container = container;
        }

        public T Make<T>() where T : DataEntity
        {
            T shell = _container.GetService<T>();
            return shell;
        }

        public T Make<T>(dynamic sourceItem) where T : DataEntity
        {
            T shell = _container.GetService<T>();

            // I was hoping that automapper would just copy properties from sourceItem
            // over to the mapped... Unfortunately, it tries to construct a new object.
            // todo: write an implementation to copy each property from sourceItem and
            // set the value on shell to the property value.
            var mapped = (T)Mapper.Map<T>(sourceItem);
            return mapped;
        }
    }
}

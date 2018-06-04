using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace Peppermint.Core.Entities
{
    public class EntityFactory
    {
        private IServiceProvider _container;
        public EntityFactory(IServiceProvider container)
        {
            _container = container;
        }

        public T Make<T>()
        {
            T shell = _container.GetService<T>();
            return shell;
        }

        public T Make<T>(dynamic sourceItem)
        {
            if (sourceItem == null)
                return default(T);

            T shell = _container.GetService<T>();
            var copied = CopyProperties<T>(sourceItem, shell);

            return copied;
        }

        private T CopyProperties<T>(dynamic fromObj, T toObj)
        { 
            var fromProps = (IDictionary<string, object>)fromObj;
            var toProps = toObj.GetType().GetProperties().ToList();

            foreach (var fromProp in fromProps)
            {
                var targetProp = toProps.FirstOrDefault(p => p.Name == fromProp.Key);
                if (targetProp != null)
                {
                    if (CanCast(fromProp.Value, targetProp.PropertyType))
                    {
                        var value = fromProp.Value;
                        targetProp.SetValue(toObj, value, null);
                    }                   
                }
            }

            return toObj;
        }

        private bool CanCast(object value, Type toType)
        {
            try
            {
                var result = Convert.ChangeType(value, toType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

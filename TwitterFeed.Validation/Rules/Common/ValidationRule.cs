using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using CCS.Common.Notifications;
 
using CCS.Common;
using System.Diagnostics.CodeAnalysis;
using CCS.Core.Entities;

namespace TwitterFeed.Validation
{
    public abstract class ValidationRule : IRule
    {
        private bool getPropertyValueCalled;
        private static Type stringType = typeof(string);
        private static Type enumerableType = typeof(IEnumerable);
        private object propertyValue;
        private PropertyInfo propertyType;
        private Type propertyValueType;
        private bool? propertyHasValue; 

        #region IRule Members

        public object Context { get; set; }

        public object Owner { get; set; }

        public bool IsPropertyRule { get; set; }

        public string PropertyName { get; set; }

        public bool IsDependant { get; set; }

        public string Name { get; set; }

        public string ReadOnlyMessage { get; set; }

        public Notification Validate()
        {
            return OnValidate();
        }

        public bool IsApplicabilityRule { get; set; }

        public SeverityType Severity { get; set; }

        public bool MustValidate()
        {
            return OnMustValidate();
        }

        public string DisplayName { get; set; }

        #endregion

        #region Properties

        protected Type PropertyValueType
        {
            get
            {
                if (propertyValueType.IsNull() && PropertyValue.IsNotNull())
                {
                    propertyValueType = PropertyValue.GetType();
                }

                return propertyValueType;
            }
        }

        protected PropertyInfo PropertyType
        {
            get
            {
                if (propertyType.IsNull())
                {
                    var instanceType = Owner.GetType();
                    if (PropertyName.Contains("."))
                    {
                        propertyType = GetPropertyInfo(Owner, instanceType, PropertyName.Split('.'), 0);
                    }
                    
                    Invariant.IsFalse(propertyType.IsNull(), () => "No property {0} exists on {1}".FormatInvariantCulture(PropertyName, instanceType.FullName));
                }

                return propertyType;
            }
        }

        private PropertyInfo GetPropertyInfo(object parentObject, Type parentObjectType, string[] properties, int index)
        {
            var propertyInfo = parentObjectType.GetTypeInfo().GetProperty(properties[index]);

            if (properties.Length - 1 > index)
            {
                var propertyObject = propertyInfo.GetValue(parentObject);
                return GetPropertyInfo(propertyObject, propertyObject.GetType(), PropertyName.Split('.'), index + 1);
            }

            return propertyInfo;
        }

        protected virtual object PropertyValue
        {
            get
            {
                if (!getPropertyValueCalled)
                {
                    getPropertyValueCalled = true;
                    propertyValue = GetPropertyObject(Owner, PropertyName.Split('.'), 0);
                    
                }

                return propertyValue;
            }
        }

        private object GetPropertyObject(object parentObject, string[] properties, int index)
        {
            var propertyInfo = parentObject.GetType().GetTypeInfo().GetProperty(properties[index]);

            propertyValue = propertyInfo.GetValue(parentObject, null);

            if (properties.Length - 1 > index)
            {
                return GetPropertyObject(propertyValue, properties, index + 1);
            }

            return propertyValue;
        }

        #endregion

        #region Methods

        protected bool PropertyHasValue()
        {
            if (!propertyHasValue.HasValue)
            {
                propertyHasValue = false;

                if (PropertyValue.IsNotNull())
                {
                    if (PropertyType.PropertyType.Equals(stringType))
                    {
                        propertyHasValue = PropertyValue.AsString().IsNotNullOrEmpty();
                    }
                    else if (enumerableType.GetTypeInfo().IsAssignableFrom(PropertyType.PropertyType))
                    {
                        var list = PropertyValue as IList;
                        if (list.IsNotNull())
                        {
                            propertyHasValue = list.Count > 0;
                        }
                    }
                    else
                    {
                        propertyHasValue = true;
                    }
                }
            }

            return propertyHasValue.Value;
        }

        protected Message CreateMessage(string message, params object[] values)
        {
            var ruleMessage = ValidationHelper.CreateMessage(this, GenerateMessagePropertyName(), message, string.Empty, values);
            if (ReadOnlyMessage.IsNotNullOrEmpty())
            {
                ruleMessage.Text = ReadOnlyMessage;
                ruleMessage.MayOverrideMessage = false;
            }
            return ruleMessage;
        }

        protected Message CreateReadOnlyMessage(string message, params object[] values)
        {
            var ruleMessage = CreateMessage(message, values);
            ruleMessage.MayOverrideMessage = false;
            return ruleMessage;
        }

        protected bool IsInContext(JobEntityBase entity, ValidationContext context)
        {
            return entity.EnterpriseId == context.EnterpriseEntityId && entity.OrganisationId == context.OrganisationEntityId && entity.JobEntityId == context.JobEntityId;
        }

        #endregion

        #region Virtual Methods

        protected virtual bool OnMustValidate()
        {
            return true;
        }

        protected virtual Notification OnValidate()
        {
            return Notification.CreateEmpty();
        }

        protected virtual string GenerateMessagePropertyName()
        {
            return PropertyName;
        }





        #endregion
    }

    public abstract class ValidationRule<T> : ValidationRule
    {
        #region Properties

        protected T Instance
        {
            get
            {
                return (T)Owner;
            }
        }

        #endregion
    }

    public abstract class ValidationRule<T, C> : ValidationRule<T> 
        where T : class
        where C : class
    {
        #region Methods

        public C GetContext()
        {
            return (C)Context;
        }

        #endregion 
    }
}

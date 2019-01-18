using System;
namespace TwitterFeed.Core.Entities
{
	public abstract class TwitterEntityBase
	{
		private Guid _entityId;
		public Guid EntityId
		{
			get
			{
				return _entityId;
			}
			set
			{
				_entityId = value;
			}
		}

		//private IValidationContext GetValidationContext()
		//{
		//	var validationcontextAttribute = GetType().GetTypeInfo().GetCustomAttributes(typeof(ValidationContextAttribute), true).Cast<ValidationContextAttribute>();
		//	if (validationcontextAttribute != null)
		//	{
		//		Contract.Assert(validationcontextAttribute.Count() <= 1, "There can only be zero or one ValidationContextAttribute on an Entity");

		//		if (validationcontextAttribute.Count() == 1)
		//		{
		//			return DomainContext.Instance.Container.Resolve<IValidationContext>(validationcontextAttribute.First().ValidationContextName);
		//		}
		//	}

		//	return null;
		//}
	}
}
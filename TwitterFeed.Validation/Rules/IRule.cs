using System;
using System.Collections.Generic;
using System.Reflection;
using TwitterFeed.Common.Notifications;

namespace TwitterFeed.Validation
{
	public interface IRule
	{
		/// <summary>
		/// The severity for the context
		/// </summary>
		SeverityType Severity { get; set; }

		/// <summary>
		/// The context in which the rule is running
		/// </summary>
		object Context { get; set; }

		/// <summary>
		/// The owner for the rule
		/// </summary>
		object Owner { get; set; }

		/// <summary>
		/// The name of the rule
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Execute the rule
		/// </summary>
		Notification Validate();

		/// <summary>
		/// Indicates whether the rule is for a property
		/// </summary>
		bool IsPropertyRule { get; set; }

		/// <summary>
		/// Specifies the property name if the rule is for a property
		/// </summary>
		string PropertyName { get; set; }

	}
}
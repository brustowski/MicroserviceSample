using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Apollo.Bp.Net.Card.Core.Constants;

namespace Apollo.Bp.Net.Card.Core.DTOs.Validation
{
	public class NotEmptyAttribute : RequiredAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is Guid guid && guid != Guid.Empty)
			{
				return ValidationResult.Success;
			}

			if (value is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ErrorConstants.NullParameter, validationContext.DisplayName.ToLower()));
		}
	}
}
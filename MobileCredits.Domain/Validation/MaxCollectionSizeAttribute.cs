using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MobileCredits.Domain.Validation;

public class MaxCollectionSizeAttribute : ValidationAttribute
{
    private readonly int _maxSize;

    public MaxCollectionSizeAttribute(int maxSize)
    {
        _maxSize = maxSize;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is ICollection collection)
        {
            if (collection.Count > _maxSize)
            {
                return new ValidationResult($"The collection cannot contain more than {_maxSize} elements.");
            }
        }
        return ValidationResult.Success;
    }
}
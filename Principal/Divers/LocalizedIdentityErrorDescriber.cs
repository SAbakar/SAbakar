using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Text;

namespace Principal.Divers
{
    public static class LocalizedIdentityErrorMessages
    {
        public static IdentityErrorMessages GetErrorMessageInJson()
        {
                
            var jsonText = File.ReadAllText("IdentityErrorMessages_fr.json", Encoding.GetEncoding("iso-8859-1"));
            return JsonConvert.DeserializeObject<IdentityErrorMessages>(jsonText,new JsonSerializerSettings { Culture = new CultureInfo("fr-FR") });
        }
    }

    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().DuplicateEmail, email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().DuplicateUserName, userName)
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().InvalidEmail, email)
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().DuplicateRoleName, role)
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().InvalidRoleName, role)
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().InvalidToken
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().InvalidUserName, userName)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().LoginAlreadyAssociated
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordMismatch
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordRequiresDigit
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordRequiresLower
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordRequiresNonAlphanumeric
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordRequiresUniqueChars, uniqueChars)
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordRequiresUpper
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().PasswordTooShort, length)
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().UserAlreadyHasPassword
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().UserAlreadyInRole, role)
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                Description = string.Format(LocalizedIdentityErrorMessages.GetErrorMessageInJson().UserNotInRole, role)
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().UserLockoutNotEnabled
            };
        }

        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return new IdentityError
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().RecoveryCodeRedemptionFailed
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError
            {
                Code = nameof(ConcurrencyFailure),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().ConcurrencyFailure
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description  = LocalizedIdentityErrorMessages.GetErrorMessageInJson().DefaultIdentityError
            };
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class IdentityErrorMessages
    {
        public string CompareAttribute_MustMatch { get; set; }
        public string CreditCardAttribute_Invalid { get; set; }
        public string CustomValidationAttribute_ValidationError { get; set; }
        public string DataTypeAttribute_EmptyDataTypeString { get; set; }
        public string EmailAddressAttribute_Invalid { get; set; }
        public string FileExtensionsAttribute_Invalid { get; set; }
        public string MaxLengthAttribute_ValidationError { get; set; }
        public string MinLengthAttribute_ValidationError { get; set; }
        public string PhoneAttribute_Invalid { get; set; }
        public string RangeAttribute_ValidationError { get; set; }
        public string RegexAttribute_ValidationError { get; set; }
        public string RequiredAttribute_ValidationError { get; set; }
        public string StringLengthAttribute_ValidationError { get; set; }
        public string StringLengthAttribute_ValidationErrorIncludingMinimum { get; set; }
        public string UrlAttribute_Invalid { get; set; }
        public string ValidationAttribute_ValidationError { get; set; }
        public string DuplicateEmail { get; set; }
        public string DuplicateUserName { get; set; }
        public string InvalidEmail { get; set; }
        public string DuplicateRoleName { get; set; }
        public string InvalidRoleName { get; set; }
        public string InvalidToken { get; set; }
        public string InvalidUserName { get; set; }
        public string LoginAlreadyAssociated { get; set; }
        public string PasswordMismatch { get; set; }
        public string PasswordRequiresDigit { get; set; }
        public string PasswordRequiresLower { get; set; }
        public string PasswordRequiresNonAlphanumeric { get; set; }
        public string PasswordRequiresUniqueChars { get; set; }
        public string PasswordRequiresUpper { get; set; }
        public string PasswordTooShort { get; set; }
        public string UserAlreadyHasPassword { get; set; }
        public string UserAlreadyInRole { get; set; }
        public string UserNotInRole { get; set; }
        public string UserLockoutNotEnabled { get; set; }
        public string RecoveryCodeRedemptionFailed { get; set; }
        public string ConcurrencyFailure { get; set; }
        public string DefaultError { get; set; }
        public string AttemptedValueIsInvalidAccessor { get; set; }
        public string MissingBindRequiredValueAccessor { get; set; }
        public string MissingKeyOrValueAccessor { get; set; }
        public string MissingRequestBodyRequiredValueAccessor { get; set; }
        public string NonPropertyAttemptedValueIsInvalidAccessor { get; set; }
        public string NonPropertyUnknownValueIsInvalidAccessor { get; set; }
        public string NonPropertyValueMustBeANumberAccessor { get; set; }
        public string UnknownValueIsInvalidAccessor { get; set; }
        public string ValueIsInvalidAccessor { get; set; }
        public string ValueMustBeANumberAccessor { get; set; }
        public string ValueMustNotBeNullAccessor { get; set; }
        public string DefaultIdentityError { get; set; }
    }



}

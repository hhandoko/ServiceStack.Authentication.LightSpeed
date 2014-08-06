using System;

using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Validation;
using Mindscape.LightSpeed.Linq;

namespace ServiceStack.Authentication.LightSpeed
{
  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class UserAuth : Entity<int>
  {
    #region Fields
  
    #pragma warning disable 169
    [ValidateLength(0, 8000)]
    private string _userName;
    [ValidateEmailAddress]
    [ValidateLength(0, 8000)]
    private string _email;
    [ValidateLength(0, 8000)]
    private string _primaryEmail;
    [ValidateLength(0, 8000)]
    private string _phoneNumber;
    [ValidateLength(0, 8000)]
    private string _firstName;
    [ValidateLength(0, 8000)]
    private string _lastName;
    [ValidateLength(0, 8000)]
    private string _displayName;
    [ValidateLength(0, 8000)]
    private string _company;
    private System.Nullable<System.DateTime> _birthDate;
    [ValidateLength(0, 8000)]
    private string _birthDateRaw;
    [ValidateLength(0, 8000)]
    private string _address;
    [ValidateLength(0, 8000)]
    private string _address2;
    [ValidateLength(0, 8000)]
    private string _city;
    [ValidateLength(0, 8000)]
    private string _state;
    [ValidateLength(0, 8000)]
    private string _country;
    [ValidateLength(0, 8000)]
    private string _culture;
    [ValidateLength(0, 8000)]
    private string _fullName;
    [ValidateLength(0, 8000)]
    private string _gender;
    [ValidateLength(0, 8000)]
    private string _language;
    [ValidateLength(0, 8000)]
    private string _mailAddress;
    [ValidateLength(0, 8000)]
    private string _nickname;
    [ValidateLength(0, 8000)]
    private string _postalCode;
    [ValidateLength(0, 8000)]
    private string _timeZone;
    [ValidateLength(0, 8000)]
    private string _salt;
    [ValidateLength(0, 8000)]
    private string _passwordHash;
    [ValidateLength(0, 8000)]
    private string _digestHa1Hash;
    [ValidateLength(0, 8000)]
    private string _roles;
    [ValidateLength(0, 8000)]
    private string _permissions;
    private System.DateTime _createdDate;
    private System.DateTime _modifiedDate;
    private int _invalidLoginAttempts;
    private System.Nullable<System.DateTime> _lastLoginAttempt;
    private System.Nullable<System.DateTime> _lockedDate;
    [ValidateLength(0, 8000)]
    private string _recoveryToken;
    private System.Nullable<int> _refId;
    [ValidateLength(0, 8000)]
    private string _refIdStr;
    [ValidateLength(0, 8000)]
    private string _meta;
    #pragma warning restore 169

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the UserName entity attribute.</summary>
    public const string UserNameField = "UserName";
    /// <summary>Identifies the Email entity attribute.</summary>
    public const string EmailField = "Email";
    /// <summary>Identifies the PrimaryEmail entity attribute.</summary>
    public const string PrimaryEmailField = "PrimaryEmail";
    /// <summary>Identifies the PhoneNumber entity attribute.</summary>
    public const string PhoneNumberField = "PhoneNumber";
    /// <summary>Identifies the FirstName entity attribute.</summary>
    public const string FirstNameField = "FirstName";
    /// <summary>Identifies the LastName entity attribute.</summary>
    public const string LastNameField = "LastName";
    /// <summary>Identifies the DisplayName entity attribute.</summary>
    public const string DisplayNameField = "DisplayName";
    /// <summary>Identifies the Company entity attribute.</summary>
    public const string CompanyField = "Company";
    /// <summary>Identifies the BirthDate entity attribute.</summary>
    public const string BirthDateField = "BirthDate";
    /// <summary>Identifies the BirthDateRaw entity attribute.</summary>
    public const string BirthDateRawField = "BirthDateRaw";
    /// <summary>Identifies the Address entity attribute.</summary>
    public const string AddressField = "Address";
    /// <summary>Identifies the Address2 entity attribute.</summary>
    public const string Address2Field = "Address2";
    /// <summary>Identifies the City entity attribute.</summary>
    public const string CityField = "City";
    /// <summary>Identifies the State entity attribute.</summary>
    public const string StateField = "State";
    /// <summary>Identifies the Country entity attribute.</summary>
    public const string CountryField = "Country";
    /// <summary>Identifies the Culture entity attribute.</summary>
    public const string CultureField = "Culture";
    /// <summary>Identifies the FullName entity attribute.</summary>
    public const string FullNameField = "FullName";
    /// <summary>Identifies the Gender entity attribute.</summary>
    public const string GenderField = "Gender";
    /// <summary>Identifies the Language entity attribute.</summary>
    public const string LanguageField = "Language";
    /// <summary>Identifies the MailAddress entity attribute.</summary>
    public const string MailAddressField = "MailAddress";
    /// <summary>Identifies the Nickname entity attribute.</summary>
    public const string NicknameField = "Nickname";
    /// <summary>Identifies the PostalCode entity attribute.</summary>
    public const string PostalCodeField = "PostalCode";
    /// <summary>Identifies the TimeZone entity attribute.</summary>
    public const string TimeZoneField = "TimeZone";
    /// <summary>Identifies the Salt entity attribute.</summary>
    public const string SaltField = "Salt";
    /// <summary>Identifies the PasswordHash entity attribute.</summary>
    public const string PasswordHashField = "PasswordHash";
    /// <summary>Identifies the DigestHa1Hash entity attribute.</summary>
    public const string DigestHa1HashField = "DigestHa1Hash";
    /// <summary>Identifies the Roles entity attribute.</summary>
    public const string RolesField = "Roles";
    /// <summary>Identifies the Permissions entity attribute.</summary>
    public const string PermissionsField = "Permissions";
    /// <summary>Identifies the CreatedDate entity attribute.</summary>
    public const string CreatedDateField = "CreatedDate";
    /// <summary>Identifies the ModifiedDate entity attribute.</summary>
    public const string ModifiedDateField = "ModifiedDate";
    /// <summary>Identifies the InvalidLoginAttempts entity attribute.</summary>
    public const string InvalidLoginAttemptsField = "InvalidLoginAttempts";
    /// <summary>Identifies the LastLoginAttempt entity attribute.</summary>
    public const string LastLoginAttemptField = "LastLoginAttempt";
    /// <summary>Identifies the LockedDate entity attribute.</summary>
    public const string LockedDateField = "LockedDate";
    /// <summary>Identifies the RecoveryToken entity attribute.</summary>
    public const string RecoveryTokenField = "RecoveryToken";
    /// <summary>Identifies the RefId entity attribute.</summary>
    public const string RefIdField = "RefId";
    /// <summary>Identifies the RefIdStr entity attribute.</summary>
    public const string RefIdStrField = "RefIdStr";
    /// <summary>Identifies the Meta entity attribute.</summary>
    public const string MetaField = "Meta";


    #endregion
    
    #region Relationships

    [ReverseAssociation("UserAuth")]
    private readonly EntityCollection<UserAuthDetail> _userAuthDetails = new EntityCollection<UserAuthDetail>();
    [ReverseAssociation("UserAuth")]
    private readonly EntityCollection<UserAuthRole> _userAuthRoles = new EntityCollection<UserAuthRole>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<UserAuthDetail> UserAuthDetails
    {
      get { return Get(_userAuthDetails); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public EntityCollection<UserAuthRole> UserAuthRoles
    {
      get { return Get(_userAuthRoles); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string UserName
    {
      get { return Get(ref _userName, "UserName"); }
      set { Set(ref _userName, value, "UserName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Email
    {
      get { return Get(ref _email, "Email"); }
      set { Set(ref _email, value, "Email"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string PrimaryEmail
    {
      get { return Get(ref _primaryEmail, "PrimaryEmail"); }
      set { Set(ref _primaryEmail, value, "PrimaryEmail"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string PhoneNumber
    {
      get { return Get(ref _phoneNumber, "PhoneNumber"); }
      set { Set(ref _phoneNumber, value, "PhoneNumber"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string FirstName
    {
      get { return Get(ref _firstName, "FirstName"); }
      set { Set(ref _firstName, value, "FirstName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string LastName
    {
      get { return Get(ref _lastName, "LastName"); }
      set { Set(ref _lastName, value, "LastName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string DisplayName
    {
      get { return Get(ref _displayName, "DisplayName"); }
      set { Set(ref _displayName, value, "DisplayName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Company
    {
      get { return Get(ref _company, "Company"); }
      set { Set(ref _company, value, "Company"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<System.DateTime> BirthDate
    {
      get { return Get(ref _birthDate, "BirthDate"); }
      set { Set(ref _birthDate, value, "BirthDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string BirthDateRaw
    {
      get { return Get(ref _birthDateRaw, "BirthDateRaw"); }
      set { Set(ref _birthDateRaw, value, "BirthDateRaw"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Address
    {
      get { return Get(ref _address, "Address"); }
      set { Set(ref _address, value, "Address"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Address2
    {
      get { return Get(ref _address2, "Address2"); }
      set { Set(ref _address2, value, "Address2"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string City
    {
      get { return Get(ref _city, "City"); }
      set { Set(ref _city, value, "City"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string State
    {
      get { return Get(ref _state, "State"); }
      set { Set(ref _state, value, "State"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Country
    {
      get { return Get(ref _country, "Country"); }
      set { Set(ref _country, value, "Country"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Culture
    {
      get { return Get(ref _culture, "Culture"); }
      set { Set(ref _culture, value, "Culture"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string FullName
    {
      get { return Get(ref _fullName, "FullName"); }
      set { Set(ref _fullName, value, "FullName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Gender
    {
      get { return Get(ref _gender, "Gender"); }
      set { Set(ref _gender, value, "Gender"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Language
    {
      get { return Get(ref _language, "Language"); }
      set { Set(ref _language, value, "Language"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string MailAddress
    {
      get { return Get(ref _mailAddress, "MailAddress"); }
      set { Set(ref _mailAddress, value, "MailAddress"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Nickname
    {
      get { return Get(ref _nickname, "Nickname"); }
      set { Set(ref _nickname, value, "Nickname"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string PostalCode
    {
      get { return Get(ref _postalCode, "PostalCode"); }
      set { Set(ref _postalCode, value, "PostalCode"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string TimeZone
    {
      get { return Get(ref _timeZone, "TimeZone"); }
      set { Set(ref _timeZone, value, "TimeZone"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Salt
    {
      get { return Get(ref _salt, "Salt"); }
      set { Set(ref _salt, value, "Salt"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string PasswordHash
    {
      get { return Get(ref _passwordHash, "PasswordHash"); }
      set { Set(ref _passwordHash, value, "PasswordHash"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string DigestHa1Hash
    {
      get { return Get(ref _digestHa1Hash, "DigestHa1Hash"); }
      set { Set(ref _digestHa1Hash, value, "DigestHa1Hash"); }
    }



    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime CreatedDate
    {
      get { return Get(ref _createdDate, "CreatedDate"); }
      set { Set(ref _createdDate, value, "CreatedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime ModifiedDate
    {
      get { return Get(ref _modifiedDate, "ModifiedDate"); }
      set { Set(ref _modifiedDate, value, "ModifiedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public int InvalidLoginAttempts
    {
      get { return Get(ref _invalidLoginAttempts, "InvalidLoginAttempts"); }
      set { Set(ref _invalidLoginAttempts, value, "InvalidLoginAttempts"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<System.DateTime> LastLoginAttempt
    {
      get { return Get(ref _lastLoginAttempt, "LastLoginAttempt"); }
      set { Set(ref _lastLoginAttempt, value, "LastLoginAttempt"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<System.DateTime> LockedDate
    {
      get { return Get(ref _lockedDate, "LockedDate"); }
      set { Set(ref _lockedDate, value, "LockedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RecoveryToken
    {
      get { return Get(ref _recoveryToken, "RecoveryToken"); }
      set { Set(ref _recoveryToken, value, "RecoveryToken"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<int> RefId
    {
      get { return Get(ref _refId, "RefId"); }
      set { Set(ref _refId, value, "RefId"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RefIdStr
    {
      get { return Get(ref _refIdStr, "RefIdStr"); }
      set { Set(ref _refIdStr, value, "RefIdStr"); }
    }


    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table(IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class UserAuthRole : Entity<int>
  {
    #region Fields
  
    #pragma warning disable 169
    [ValidateLength(0, 8000)]
    private string _role;
    [ValidateLength(0, 8000)]
    private string _permission;
    private System.DateTime _createdDate;
    private System.DateTime _modifiedDate;
    private System.Nullable<int> _refId;
    [ValidateLength(0, 8000)]
    private string _refIdStr;
    [ValidateLength(0, 8000)]
    private string _meta;
    private int _userAuthId;
    #pragma warning restore 169

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Role entity attribute.</summary>
    public const string RoleField = "Role";
    /// <summary>Identifies the Permission entity attribute.</summary>
    public const string PermissionField = "Permission";
    /// <summary>Identifies the CreatedDate entity attribute.</summary>
    public const string CreatedDateField = "CreatedDate";
    /// <summary>Identifies the ModifiedDate entity attribute.</summary>
    public const string ModifiedDateField = "ModifiedDate";
    /// <summary>Identifies the RefId entity attribute.</summary>
    public const string RefIdField = "RefId";
    /// <summary>Identifies the RefIdStr entity attribute.</summary>
    public const string RefIdStrField = "RefIdStr";
    /// <summary>Identifies the Meta entity attribute.</summary>
    public const string MetaField = "Meta";
    /// <summary>Identifies the UserAuthId entity attribute.</summary>
    public const string UserAuthIdField = "UserAuthId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("UserAuthRoles")]
    private readonly EntityHolder<UserAuth> _userAuth = new EntityHolder<UserAuth>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public UserAuth UserAuth
    {
      get { return Get(_userAuth); }
      set { Set(_userAuth, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string Role
    {
      get { return Get(ref _role, "Role"); }
      set { Set(ref _role, value, "Role"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Permission
    {
      get { return Get(ref _permission, "Permission"); }
      set { Set(ref _permission, value, "Permission"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime CreatedDate
    {
      get { return Get(ref _createdDate, "CreatedDate"); }
      set { Set(ref _createdDate, value, "CreatedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime ModifiedDate
    {
      get { return Get(ref _modifiedDate, "ModifiedDate"); }
      set { Set(ref _modifiedDate, value, "ModifiedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<int> RefId
    {
      get { return Get(ref _refId, "RefId"); }
      set { Set(ref _refId, value, "RefId"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RefIdStr
    {
      get { return Get(ref _refIdStr, "RefIdStr"); }
      set { Set(ref _refIdStr, value, "RefIdStr"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Meta
    {
      get { return Get(ref _meta, "Meta"); }
      set { Set(ref _meta, value, "Meta"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="UserAuth" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int UserAuthId
    {
      get { return Get(ref _userAuthId, "UserAuthId"); }
      set { Set(ref _userAuthId, value, "UserAuthId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [System.ComponentModel.DataObject]
  [Table("UserAuthDetails", IdentityMethod=IdentityMethod.IdentityColumn)]
  public partial class UserAuthDetail : Entity<int>
  {
    #region Fields
  
    #pragma warning disable 169
    [ValidateLength(0, 8000)]
    private string _provider;
    [ValidateLength(0, 8000)]
    private string _userId;
    [ValidateLength(0, 8000)]
    private string _userName;
    [ValidateLength(0, 8000)]
    private string _fullName;
    [ValidateLength(0, 8000)]
    private string _displayName;
    [ValidateLength(0, 8000)]
    private string _firstName;
    [ValidateLength(0, 8000)]
    private string _lastName;
    [ValidateLength(0, 8000)]
    private string _company;
    [ValidateEmailAddress]
    [ValidateLength(0, 8000)]
    private string _email;
    [ValidateLength(0, 8000)]
    private string _phoneNumber;
    private System.Nullable<System.DateTime> _birthDate;
    [ValidateLength(0, 8000)]
    private string _birthDateRaw;
    [ValidateLength(0, 8000)]
    private string _address;
    [ValidateLength(0, 8000)]
    private string _address2;
    [ValidateLength(0, 8000)]
    private string _city;
    [ValidateLength(0, 8000)]
    private string _state;
    [ValidateLength(0, 8000)]
    private string _country;
    [ValidateLength(0, 8000)]
    private string _culture;
    [ValidateLength(0, 8000)]
    private string _gender;
    [ValidateLength(0, 8000)]
    private string _language;
    [ValidateLength(0, 8000)]
    private string _mailAddress;
    [ValidateLength(0, 8000)]
    private string _nickname;
    [ValidateLength(0, 8000)]
    private string _postalCode;
    [ValidateLength(0, 8000)]
    private string _timeZone;
    [ValidateLength(0, 8000)]
    private string _refreshToken;
    private System.Nullable<System.DateTime> _refreshTokenExpiry;
    [ValidateLength(0, 8000)]
    private string _requestToken;
    [ValidateLength(0, 8000)]
    private string _requestTokenSecret;
    [ValidateLength(0, 8000)]
    private string _items;
    [ValidateLength(0, 8000)]
    private string _accessToken;
    [ValidateLength(0, 8000)]
    private string _accessTokenSecret;
    private System.DateTime _createdDate;
    private System.DateTime _modifiedDate;
    private System.Nullable<int> _refId;
    [ValidateLength(0, 8000)]
    private string _refIdStr;
    [ValidateLength(0, 8000)]
    private string _meta;
    private int _userAuthId;
    #pragma warning restore 169

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the Provider entity attribute.</summary>
    public const string ProviderField = "Provider";
    /// <summary>Identifies the UserId entity attribute.</summary>
    public const string UserIdField = "UserId";
    /// <summary>Identifies the UserName entity attribute.</summary>
    public const string UserNameField = "UserName";
    /// <summary>Identifies the FullName entity attribute.</summary>
    public const string FullNameField = "FullName";
    /// <summary>Identifies the DisplayName entity attribute.</summary>
    public const string DisplayNameField = "DisplayName";
    /// <summary>Identifies the FirstName entity attribute.</summary>
    public const string FirstNameField = "FirstName";
    /// <summary>Identifies the LastName entity attribute.</summary>
    public const string LastNameField = "LastName";
    /// <summary>Identifies the Company entity attribute.</summary>
    public const string CompanyField = "Company";
    /// <summary>Identifies the Email entity attribute.</summary>
    public const string EmailField = "Email";
    /// <summary>Identifies the PhoneNumber entity attribute.</summary>
    public const string PhoneNumberField = "PhoneNumber";
    /// <summary>Identifies the BirthDate entity attribute.</summary>
    public const string BirthDateField = "BirthDate";
    /// <summary>Identifies the BirthDateRaw entity attribute.</summary>
    public const string BirthDateRawField = "BirthDateRaw";
    /// <summary>Identifies the Address entity attribute.</summary>
    public const string AddressField = "Address";
    /// <summary>Identifies the Address2 entity attribute.</summary>
    public const string Address2Field = "Address2";
    /// <summary>Identifies the City entity attribute.</summary>
    public const string CityField = "City";
    /// <summary>Identifies the State entity attribute.</summary>
    public const string StateField = "State";
    /// <summary>Identifies the Country entity attribute.</summary>
    public const string CountryField = "Country";
    /// <summary>Identifies the Culture entity attribute.</summary>
    public const string CultureField = "Culture";
    /// <summary>Identifies the Gender entity attribute.</summary>
    public const string GenderField = "Gender";
    /// <summary>Identifies the Language entity attribute.</summary>
    public const string LanguageField = "Language";
    /// <summary>Identifies the MailAddress entity attribute.</summary>
    public const string MailAddressField = "MailAddress";
    /// <summary>Identifies the Nickname entity attribute.</summary>
    public const string NicknameField = "Nickname";
    /// <summary>Identifies the PostalCode entity attribute.</summary>
    public const string PostalCodeField = "PostalCode";
    /// <summary>Identifies the TimeZone entity attribute.</summary>
    public const string TimeZoneField = "TimeZone";
    /// <summary>Identifies the RefreshToken entity attribute.</summary>
    public const string RefreshTokenField = "RefreshToken";
    /// <summary>Identifies the RefreshTokenExpiry entity attribute.</summary>
    public const string RefreshTokenExpiryField = "RefreshTokenExpiry";
    /// <summary>Identifies the RequestToken entity attribute.</summary>
    public const string RequestTokenField = "RequestToken";
    /// <summary>Identifies the RequestTokenSecret entity attribute.</summary>
    public const string RequestTokenSecretField = "RequestTokenSecret";
    /// <summary>Identifies the Items entity attribute.</summary>
    public const string ItemsField = "Items";
    /// <summary>Identifies the AccessToken entity attribute.</summary>
    public const string AccessTokenField = "AccessToken";
    /// <summary>Identifies the AccessTokenSecret entity attribute.</summary>
    public const string AccessTokenSecretField = "AccessTokenSecret";
    /// <summary>Identifies the CreatedDate entity attribute.</summary>
    public const string CreatedDateField = "CreatedDate";
    /// <summary>Identifies the ModifiedDate entity attribute.</summary>
    public const string ModifiedDateField = "ModifiedDate";
    /// <summary>Identifies the RefId entity attribute.</summary>
    public const string RefIdField = "RefId";
    /// <summary>Identifies the RefIdStr entity attribute.</summary>
    public const string RefIdStrField = "RefIdStr";
    /// <summary>Identifies the Meta entity attribute.</summary>
    public const string MetaField = "Meta";
    /// <summary>Identifies the UserAuthId entity attribute.</summary>
    public const string UserAuthIdField = "UserAuthId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("UserAuthDetails")]
    private readonly EntityHolder<UserAuth> _userAuth = new EntityHolder<UserAuth>();


    #endregion
    
    #region Properties

    [System.Diagnostics.DebuggerNonUserCode]
    public UserAuth UserAuth
    {
      get { return Get(_userAuth); }
      set { Set(_userAuth, value); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string Provider
    {
      get { return Get(ref _provider, "Provider"); }
      set { Set(ref _provider, value, "Provider"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string UserId
    {
      get { return Get(ref _userId, "UserId"); }
      set { Set(ref _userId, value, "UserId"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string UserName
    {
      get { return Get(ref _userName, "UserName"); }
      set { Set(ref _userName, value, "UserName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string FullName
    {
      get { return Get(ref _fullName, "FullName"); }
      set { Set(ref _fullName, value, "FullName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string DisplayName
    {
      get { return Get(ref _displayName, "DisplayName"); }
      set { Set(ref _displayName, value, "DisplayName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string FirstName
    {
      get { return Get(ref _firstName, "FirstName"); }
      set { Set(ref _firstName, value, "FirstName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string LastName
    {
      get { return Get(ref _lastName, "LastName"); }
      set { Set(ref _lastName, value, "LastName"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Company
    {
      get { return Get(ref _company, "Company"); }
      set { Set(ref _company, value, "Company"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Email
    {
      get { return Get(ref _email, "Email"); }
      set { Set(ref _email, value, "Email"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string PhoneNumber
    {
      get { return Get(ref _phoneNumber, "PhoneNumber"); }
      set { Set(ref _phoneNumber, value, "PhoneNumber"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<System.DateTime> BirthDate
    {
      get { return Get(ref _birthDate, "BirthDate"); }
      set { Set(ref _birthDate, value, "BirthDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string BirthDateRaw
    {
      get { return Get(ref _birthDateRaw, "BirthDateRaw"); }
      set { Set(ref _birthDateRaw, value, "BirthDateRaw"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Address
    {
      get { return Get(ref _address, "Address"); }
      set { Set(ref _address, value, "Address"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Address2
    {
      get { return Get(ref _address2, "Address2"); }
      set { Set(ref _address2, value, "Address2"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string City
    {
      get { return Get(ref _city, "City"); }
      set { Set(ref _city, value, "City"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string State
    {
      get { return Get(ref _state, "State"); }
      set { Set(ref _state, value, "State"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Country
    {
      get { return Get(ref _country, "Country"); }
      set { Set(ref _country, value, "Country"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Culture
    {
      get { return Get(ref _culture, "Culture"); }
      set { Set(ref _culture, value, "Culture"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Gender
    {
      get { return Get(ref _gender, "Gender"); }
      set { Set(ref _gender, value, "Gender"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Language
    {
      get { return Get(ref _language, "Language"); }
      set { Set(ref _language, value, "Language"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string MailAddress
    {
      get { return Get(ref _mailAddress, "MailAddress"); }
      set { Set(ref _mailAddress, value, "MailAddress"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string Nickname
    {
      get { return Get(ref _nickname, "Nickname"); }
      set { Set(ref _nickname, value, "Nickname"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string PostalCode
    {
      get { return Get(ref _postalCode, "PostalCode"); }
      set { Set(ref _postalCode, value, "PostalCode"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string TimeZone
    {
      get { return Get(ref _timeZone, "TimeZone"); }
      set { Set(ref _timeZone, value, "TimeZone"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RefreshToken
    {
      get { return Get(ref _refreshToken, "RefreshToken"); }
      set { Set(ref _refreshToken, value, "RefreshToken"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<System.DateTime> RefreshTokenExpiry
    {
      get { return Get(ref _refreshTokenExpiry, "RefreshTokenExpiry"); }
      set { Set(ref _refreshTokenExpiry, value, "RefreshTokenExpiry"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RequestToken
    {
      get { return Get(ref _requestToken, "RequestToken"); }
      set { Set(ref _requestToken, value, "RequestToken"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RequestTokenSecret
    {
      get { return Get(ref _requestTokenSecret, "RequestTokenSecret"); }
      set { Set(ref _requestTokenSecret, value, "RequestTokenSecret"); }
    }


    [System.Diagnostics.DebuggerNonUserCode]
    public string AccessToken
    {
      get { return Get(ref _accessToken, "AccessToken"); }
      set { Set(ref _accessToken, value, "AccessToken"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string AccessTokenSecret
    {
      get { return Get(ref _accessTokenSecret, "AccessTokenSecret"); }
      set { Set(ref _accessTokenSecret, value, "AccessTokenSecret"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime CreatedDate
    {
      get { return Get(ref _createdDate, "CreatedDate"); }
      set { Set(ref _createdDate, value, "CreatedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.DateTime ModifiedDate
    {
      get { return Get(ref _modifiedDate, "ModifiedDate"); }
      set { Set(ref _modifiedDate, value, "ModifiedDate"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public System.Nullable<int> RefId
    {
      get { return Get(ref _refId, "RefId"); }
      set { Set(ref _refId, value, "RefId"); }
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public string RefIdStr
    {
      get { return Get(ref _refIdStr, "RefIdStr"); }
      set { Set(ref _refIdStr, value, "RefIdStr"); }
    }


    /// <summary>Gets or sets the ID for the <see cref="UserAuth" /> property.</summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public int UserAuthId
    {
      get { return Get(ref _userAuthId, "UserAuthId"); }
      set { Set(ref _userAuthId, value, "UserAuthId"); }
    }

    #endregion
  }




  /// <summary>
  /// Provides a strong-typed unit of work for working with the UserAuthModel model.
  /// </summary>
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  public partial class UserAuthModelUnitOfWork : Mindscape.LightSpeed.UnitOfWork
  {

    public System.Linq.IQueryable<UserAuth> UserAuths
    {
      get { return this.Query<UserAuth>(); }
    }
    
    public System.Linq.IQueryable<UserAuthRole> UserAuthRoles
    {
      get { return this.Query<UserAuthRole>(); }
    }
    
    public System.Linq.IQueryable<UserAuthDetail> UserAuthDetails
    {
      get { return this.Query<UserAuthDetail>(); }
    }
    
  }

}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedUserAuthRepository.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Mindscape.LightSpeed;

    using ServiceStack.Auth;

    /// <summary>
    /// The LightSpeed ORM user authentication repository.
    /// </summary>
    public partial class LightSpeedUserAuthRepository
        : IUserAuthRepository
    {
        /// <summary>
        /// The username validation regex.
        /// Source: http://stackoverflow.com/questions/3588623/c-sharp-regex-for-a-username-with-a-few-restrictions
        /// </summary>
        public Regex ValidUserNameRegEx = new Regex(@"^(?=.{3,15}$)([A-Za-z0-9][._-]?)*$", RegexOptions.Compiled);

        /// <summary>
        /// The Unit of Work.
        /// </summary>
        private readonly UserAuthModelUnitOfWork unitOfWork;

        /// <summary>
        /// The password hasher.
        /// </summary>
        private readonly IHashProvider passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightSpeedUserAuthRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public LightSpeedUserAuthRepository(IUnitOfWork unitOfWork)
            : this(unitOfWork, new SaltedHash())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LightSpeedUserAuthRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="passwordHasher">The password hasher.</param>
        public LightSpeedUserAuthRepository(IUnitOfWork unitOfWork, IHashProvider passwordHasher)
        {
            this.passwordHasher = passwordHasher;
            this.unitOfWork = (UserAuthModelUnitOfWork)unitOfWork;
        }

        /// <summary>
        /// Gets or sets the max login attempts.
        /// </summary>
        public int? MaxLoginAttempts { get; set; }

        /// <summary>
        /// Get a UserAuth by its id.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <returns>The <see cref="IUserAuth"/>.</returns>
        public IUserAuth GetUserAuth(string userAuthId)
        {
            var authId = int.Parse(userAuthId);

            return this.GetUserAuth(authId);
        }

        /// <summary>
        /// Get a UserAuth by its session.
        /// </summary>
        /// <param name="authSession">The auth session.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The <see cref="IUserAuth"/>.</returns>
        public IUserAuth GetUserAuth(IAuthSession authSession, IAuthTokens tokens)
        {
            // Try and get by its authentication id
            if (!string.IsNullOrEmpty(authSession.UserAuthId))
            {
                var userAuth = this.GetUserAuth(authSession.UserAuthId);
                if (userAuth != null)
                {
                    return userAuth;
                }
            }
            
            // Try and get by its user auth name
            if (!string.IsNullOrEmpty(authSession.UserAuthName))
            {
                var userAuth = this.GetUserAuthByUserName(authSession.UserAuthName);
                if (userAuth != null)
                {
                    return userAuth;
                }
            }

            // Check for null OAuth session tokens
            if (tokens == null
                || string.IsNullOrEmpty(tokens.Provider)
                || string.IsNullOrEmpty(tokens.UserId))
            {
                return null;
            }

            // Try and get from the OAuth table
            var oauthProvider = this.GetUserAuthDetailsByProvider(tokens.UserId, tokens.Provider);

            return
                oauthProvider != null
                    ? this.GetUserAuth(oauthProvider.UserAuthId)
                    : null;
        }

        /// <summary>
        /// Get a UserAuth by its username.
        /// </summary>
        /// <param name="userNameOrEmail">The username or email.</param>
        /// <returns>The <see cref="IUserAuth"/>.</returns>
        public IUserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            return
                userNameOrEmail.Contains("@")
                    ? this.unitOfWork.UserAuths
                            .FirstOrDefault(x =>
                                x.Email == userNameOrEmail)
                    : this.unitOfWork.UserAuths
                            .FirstOrDefault(x =>
                                x.UserName == userNameOrEmail);
        }

        /// <summary>
        /// Get a UserAuthDetail by its UserAuth id.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <returns>The <see cref="List{IUserAuthDetails}"/>.</returns>
        public List<IUserAuthDetails> GetUserAuthDetails(string userAuthId)
        {
            var authId = int.Parse(userAuthId);

            return
                this.unitOfWork.UserAuthDetails
                    .Where(x => x.UserAuthId == authId)
                    .OrderBy(x => x.ModifiedDate)
                    .ToList()
                    .ConvertAll(x => (IUserAuthDetails)x);
        }

        /// <summary>
        /// Get a UserAuthDetail by its user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="provider">The OAuth provider.</param>
        /// <returns>The <see cref="List{IUserAuthDetails}"/>.</returns>
        public IUserAuthDetails GetUserAuthDetailsByProvider(string userId, string provider)
        {
            return
                this.unitOfWork.UserAuthDetails
                    .FirstOrDefault(x =>
                        x.Provider == provider
                        && x.UserId == userId);
        }

        /// <summary>
        /// Create a new UserAuth record.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="IUserAuth"/>.</returns>
        public IUserAuth CreateUserAuth(IUserAuth newUser, string password)
        {
            this.ValidateNewUser(newUser, password);
            this.AssertNoExistingUser(newUser);

            string salt;
            string hash;
            this.passwordHasher.GetHashAndSaltString(password, out hash, out salt);

            newUser.PasswordHash = hash;
            newUser.Salt = salt;
            newUser.CreatedDate = DateTime.UtcNow;
            newUser.ModifiedDate = newUser.CreatedDate;

            var record = new UserAuth();
            record.PopulateWith(newUser);

            this.unitOfWork.Add(record);
            this.unitOfWork.SaveChanges();

            return newUser;
        }

        /// <summary>
        /// Create or merge auth session.
        /// </summary>
        /// <param name="authSession">The auth session.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string CreateOrMergeAuthSession(IAuthSession authSession, IAuthTokens tokens)
        {
            // Try and get from the UserAuth table
            var userAuth = this.GetUserAuth(authSession, tokens) as LightSpeed.UserAuth;
            if (userAuth == null)
            {
                userAuth = new LightSpeed.UserAuth();
                this.unitOfWork.Add(userAuth);
            }

            // Try and get from the OAuth table
            var oauthProvider = this.GetUserAuthDetailsByProvider(tokens.UserId, tokens.Provider) as UserAuthDetail;
            if (oauthProvider == null)
            {
                oauthProvider =
                    new LightSpeed.UserAuthDetail
                        {
                            UserId = tokens.UserId,
                            Provider = tokens.Provider
                        };
                this.unitOfWork.Add(oauthProvider);
            }

            oauthProvider.PopulateMissing(tokens);

            // Update UserAuth
            userAuth.PopulateMissingExtended(oauthProvider);
            userAuth.ModifiedDate = DateTime.UtcNow;
            if (userAuth.CreatedDate == default(DateTime))
            {
                userAuth.CreatedDate = userAuth.ModifiedDate;
            }

            // Update UserAuthDetail
            oauthProvider.ModifiedDate = userAuth.ModifiedDate;
            if (oauthProvider.CreatedDate == default(DateTime))
            {
                oauthProvider.CreatedDate = userAuth.ModifiedDate;
            }

            this.unitOfWork.SaveChanges();

            return oauthProvider.UserAuthId.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Update the UserAuth.
        /// </summary>
        /// <param name="existingUser">The existing UserAuth.</param>
        /// <param name="newUser">The new or updated UserAuth.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="IUserAuth"/>.</returns>
        public IUserAuth UpdateUserAuth(IUserAuth existingUser, IUserAuth newUser, string password)
        {
            this.ValidateNewUser(newUser, password);
            this.AssertNoExistingUser(newUser);

            var hash = existingUser.PasswordHash;
            var salt = existingUser.Salt;
            if (password != null)
            {
                this.passwordHasher.GetHashAndSaltString(password, out hash, out salt);
            }

            newUser.Id = existingUser.Id;
            newUser.PasswordHash = hash;
            newUser.Salt = salt;
            newUser.CreatedDate = existingUser.CreatedDate;
            newUser.ModifiedDate = DateTime.UtcNow;

            this.unitOfWork.SaveChanges();

            return newUser;
        }

        /// <summary>
        /// Save the UserAuth.
        /// </summary>
        /// <param name="userAuth">The UserAuth.</param>
        public void SaveUserAuth(IUserAuth userAuth)
        {
            userAuth.ModifiedDate = DateTime.UtcNow;
            if (userAuth.CreatedDate == default(DateTime))
            {
                userAuth.CreatedDate = userAuth.ModifiedDate;
            }

            this.unitOfWork.SaveChanges(true);
        }

        /// <summary>
        /// Save the UserAuth from the session.
        /// </summary>
        /// <param name="authSession">The auth session.</param>
        public void SaveUserAuth(IAuthSession authSession)
        {
            var userAuth =
                !string.IsNullOrEmpty(authSession.UserAuthId)
                    ? this.GetUserAuth(authSession.UserAuthId)
                    : authSession.ConvertTo<LightSpeed.UserAuth>();

            if (userAuth.Id == default(int)
                && !string.IsNullOrEmpty(authSession.UserAuthId))
            {
                userAuth.Id = int.Parse(authSession.UserAuthId);
            }

            this.SaveUserAuth(userAuth);
        }

        /// <summary>
        /// Try to authenticate the user.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="userAuth">The UserAuth.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool TryAuthenticate(string userName, string password, out IUserAuth userAuth)
        {
            userAuth = this.GetUserAuthByUserName(userName);
            if (userAuth == null)
            {
                return false;
            }

            if (this.passwordHasher.VerifyHashString(password, userAuth.PasswordHash, userAuth.Salt))
            {
                this.RecordSuccessfulLogin(userAuth);

                return true;
            }
            else
            {
                this.RecordInvalidLoginAttempt(userAuth);

                userAuth = null;

                return false;
            }
        }

        /// <summary>
        /// Try to authenticate the user.
        /// </summary>
        /// <param name="digestHeaders">The digest headers.</param>
        /// <param name="privateKey">The private key.</param>
        /// <param name="nonceTimeOut">The nonce time out.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="userAuth">The user auth.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool TryAuthenticate(
            Dictionary<string, string> digestHeaders,
            string privateKey,
            int nonceTimeOut,
            string sequence,
            out IUserAuth userAuth)
        {
            userAuth = this.GetUserAuthByUserName(digestHeaders["username"]);
            if (userAuth == null)
            {
                return false;
            }

            var digestHelper = new DigestAuthFunctions();
            if (digestHelper.ValidateResponse(digestHeaders, privateKey, nonceTimeOut, userAuth.DigestHa1Hash, sequence))
            {
                this.RecordSuccessfulLogin(userAuth);

                return true;
            }
            else
            {
                this.RecordInvalidLoginAttempt(userAuth);

                userAuth = null;

                return false;
            }
        }

        /// <summary>
        /// Load the user auth based on the session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tokens">The tokens.</param>
        /// <exception cref="ArgumentException">
        /// Exception thrown if session is null.
        /// </exception>
        public void LoadUserAuth(IAuthSession session, IAuthTokens tokens)
        {
            session.ThrowIfNull("session");

            var userAuth = this.GetUserAuth(session, tokens) as LightSpeed.UserAuth;
            if (userAuth == null)
            {
                return;
            }

            // Re-populate the session
            var sessionId = session.Id;
            session.PopulateWith(userAuth);
            session.Id = sessionId;
            session.UserAuthId = userAuth.Id.ToString(CultureInfo.InvariantCulture);
            session.ProviderOAuthAccess =
                this.GetUserAuthDetails(session.UserAuthId)
                    .ConvertAll(x => (IAuthTokens)x);
        }

        /// <summary>
        /// Record successful login.
        /// </summary>
        /// <param name="userAuth">The UserAuth.</param>
        protected virtual void RecordSuccessfulLogin(IUserAuth userAuth)
        {
            if (this.MaxLoginAttempts == null)
            {
                return;
            }
            
            userAuth.InvalidLoginAttempts = 0;
            userAuth.LastLoginAttempt = userAuth.ModifiedDate = DateTime.UtcNow;
            
            this.SaveUserAuth(userAuth);
        }

        /// <summary>
        /// Record invalid login attempts.
        /// </summary>
        /// <param name="userAuth">The UserAuth.</param>
        protected virtual void RecordInvalidLoginAttempt(IUserAuth userAuth)
        {
            if (this.MaxLoginAttempts == null)
            {
                return;
            }

            userAuth.InvalidLoginAttempts += 1;
            userAuth.LastLoginAttempt = userAuth.ModifiedDate = DateTime.UtcNow;

            if (userAuth.InvalidLoginAttempts >= this.MaxLoginAttempts.Value)
            {
                userAuth.LockedDate = userAuth.LastLoginAttempt;
            }

            this.SaveUserAuth(userAuth);
        }

        /// <summary>
        /// Get a UserAuth by its id.
        /// </summary>
        /// <param name="authId">The UserAuth id.</param>
        /// <returns>The <see cref="IUserAuth"/>.</returns>
        private IUserAuth GetUserAuth(int authId)
        {
            return
                this.unitOfWork.UserAuths
                    .FirstOrDefault(x => x.Id == authId);
        }

        /// <summary>
        /// Validate new user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if newUser is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Exception thrown if password is null or empty.
        /// </exception>
        private void ValidateNewUser(IUserAuth newUser, string password)
        {
            newUser.ThrowIfNull("newUser");
            password.ThrowIfNullOrEmpty("password");

            // Check for username and email fields existence
            if (newUser.UserName.IsNullOrEmpty()
                && newUser.Email.IsNullOrEmpty())
            {
                throw new ArgumentNullException("UserName or Email is required");
            }

            // Check for username validity based on Regex rule
            if (!newUser.UserName.IsNullOrEmpty())
            {
                if (!this.ValidUserNameRegEx.IsMatch(newUser.UserName))
                {
                    throw new ArgumentException("UserName contains invalid characters", "UserName");
                }
            }
        }

        /// <summary>
        /// Assert no user exist with given username and email of the new user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <param name="exceptForExistingUser">The existing user to except.</param>
        /// <exception cref="ArgumentException">
        /// Exception thrown if username or email already exists.
        /// </exception>
        private void AssertNoExistingUser(IUserAuth newUser, IUserAuth exceptForExistingUser = null)
        {
            // Check for existing username
            if (newUser.UserName != null)
            {
                var existingUser = this.GetUserAuthByUserName(newUser.UserName);
                if (existingUser != null
                    && (exceptForExistingUser == null
                        || existingUser.Id != exceptForExistingUser.Id))
                {
                    throw new ArgumentException(string.Format("User {0} already exists", newUser.UserName));
                }
            }
            
            // Check for existing email
            if (newUser.Email != null)
            {
                var existingUser = this.GetUserAuthByUserName(newUser.Email);
                if (existingUser != null
                    && (exceptForExistingUser == null
                        || existingUser.Id != exceptForExistingUser.Id))
                {
                    throw new ArgumentException(string.Format("Email {0} already exists", newUser.Email));
                }
            }
        }
    }
}

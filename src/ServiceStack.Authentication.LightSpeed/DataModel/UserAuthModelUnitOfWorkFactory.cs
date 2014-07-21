// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAuthModelUnitOfWorkFactory.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using Mindscape.LightSpeed;

    using ServiceStack.Text;

    /// <summary>
    /// The UserAuthModelUnitOfWork factory.
    /// </summary>
    public class UserAuthModelUnitOfWorkFactory
        : IUnitOfWorkFactory
    {
        /// <summary>
        /// The string serializer.
        /// </summary>
        private static IStringSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthModelUnitOfWorkFactory"/> class.
        /// </summary>
        public UserAuthModelUnitOfWorkFactory()
            : this(new JsvStringSerializer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthModelUnitOfWorkFactory"/> class.
        /// </summary>
        /// <param name="stringSerializer">The string serializer.</param>
        public UserAuthModelUnitOfWorkFactory(IStringSerializer stringSerializer)
        {
            serializer = stringSerializer;
        }

        /// <summary>
        /// Create a new UserAuth model unit of work.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="IUnitOfWork"/>.</returns>
        public IUnitOfWork Create(LightSpeedContext context)
        {
            return
                new UserAuthModelUnitOfWork
                    {
                        Context = context,
                        Serializer = serializer
                    };
        }
    }
}

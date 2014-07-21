/* ************************************************** *
 * DROP TABLES IF EXISTS
 *
 * ************************************************** */
IF OBJECT_ID('UserAuthRole', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuthRole];

IF OBJECT_ID('UserAuthDetails', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuthDetails];

IF OBJECT_ID('UserAuth', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuth];
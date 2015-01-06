## 0.5 - In Progress
  * *TBA*

## 0.4 - <small>2015/01/06</small>
  * Updated `CreateOrMergeAuthSession` implementation for ServiceStack v4.0.35 compatibility.
  * Updated `IManageRoles` unit tests for ServiceStack v4.0.35 compatibility.

## 0.3 - <small>2014/08/06</small> 
  * Added `IManageRoles` unit tests.
  * Added LightSpeed's `UserAuth` unit tests.
  * Implemented `IClearable` in LightSpeedUserAuthRepository.
  * Fixed various roles management-related method persistence issues.

## 0.2 - <small>2014/07/21</small>
  * Added write compatibility unit tests.
  * Implemented `IManageRoles` in LightSpeedUserAuthRepository.
  * Fixed `CreateUserAuth` and `CreateOrMergeUserAuth` persistence methods.
  * Reverted to LightSpeed's own `IUnitOfWork` from custom implementation.

## 0.1 - <small>2014/06/17</small>
  * Initial release.
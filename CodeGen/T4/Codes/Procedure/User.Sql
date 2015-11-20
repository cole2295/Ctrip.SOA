/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/


DROP PROCEDURE [dbo].[spA_User_u]
GO

DROP PROCEDURE [dbo].[spA_User_i]
GO

DROP PROCEDURE [dbo].[spA_User_d]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[spA_User_d]  
  
                  @UserId bigint  
                  AS  
                  DECLARE @retcode int, @rowcount int   
                  SET LOCK_TIMEOUT 1000  
                  DELETE [User] WHERE [UserId]=@UserId                
  
                    
                  SELECT @retcode = @@ERROR, @rowcount = @@ROWCOUNT  
                  IF @retcode = 0 AND @rowcount = 0  
                  BEGIN   
                    SELECT 100  
                    RETURN 100  
                  END   
                  ELSE  
                  BEGIN   
       SELECT @retcode  
       RETURN @retcode  
                  END   

GO

  CREATE PROCEDURE [dbo].[spA_User_i]  

    
  
                         	    
		@UserId bigint output,

	               
                         	      @UserName nvarchar (48)   , 
	               
                         	      @CreateTime datetime    , 
	               
                         	      @UpdateTime datetime   
	               			   AS  
           DECLARE @retcode int, @rowcount int ,@identity bigint
           SET LOCK_TIMEOUT 1000  
          INSERT INTO [dbo].[User]
           (
		                                                                  		[UserName]  , 	
	                                        		[CreateTime]  , 	
	                                        		[UpdateTime] 	
	               
			)
			VALUES
			(
			                             	 
                                     		@UserName  , 	   	 
                                     		@CreateTime  , 	   	 
                                     		@UpdateTime 	   	 
            			)

           SELECT @retcode = @@ERROR, @rowcount = @@ROWCOUNT  ,@identity = @@IDENTITY
           IF @retcode = 0 AND @rowcount = 0  
            BEGIN   
                    SELECT 100  
                    RETURN 100  
            END   
            ELSE  
            BEGIN   
			set @UserId = @identity
			SELECT @retcode  
			RETURN @retcode  

            END  


	
GO

   CREATE PROCEDURE [dbo].[spA_User_u]  
   
		  @UserId bigint    , 
            
		  @UserName nvarchar (48)   , 
            
		  @CreateTime datetime    , 
            
		  @UpdateTime datetime   
                                
              AS  
			  DECLARE @retcode int, @rowcount int  
              SET LOCK_TIMEOUT 1000  
             UPDATE [dbo].[User]
             SET 

			    			            						[UserName] = ISNULL(@UserName ,[UserName]) , 		
		                 						[CreateTime] = ISNULL(@CreateTime ,[CreateTime]) , 		
		                 						[UpdateTime] = ISNULL(@UpdateTime ,[UpdateTime])		
		                 
			WHERE UserId=@UserId

			  SELECT @retcode = @@ERROR, @rowcount = @@ROWCOUNT  
              IF @retcode = 0 AND @rowcount = 0  
              BEGIN   
     SELECT 100  
                    RETURN 100  
              END   
              ELSE  
              BEGIN   
     SELECT @retcode  
     RETURN @retcode  
              END  
GO

/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/


DROP PROCEDURE [dbo].[spA_Product_u]
GO

DROP PROCEDURE [dbo].[spA_Product_i]
GO

DROP PROCEDURE [dbo].[spA_Product_d]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[spA_Product_d]  
  
                  @ProductId bigint  
                  AS  
                  DECLARE @retcode int, @rowcount int   
                  SET LOCK_TIMEOUT 1000  
                  DELETE [Product] WHERE [ProductId]=@ProductId                
  
                    
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

  CREATE PROCEDURE [dbo].[spA_Product_i]  

    
  
                         	    
		@ProductId bigint output,

	               
                         	      @ProductName nvarchar (128)   , 
	               
                         	      @CreateTime datetime    , 
	               
                         	      @UpdateTime datetime   
	               			   AS  
           DECLARE @retcode int, @rowcount int ,@identity bigint
           SET LOCK_TIMEOUT 1000  
          INSERT INTO [dbo].[Product]
           (
		                                                                  		[ProductName]  , 	
	                                        		[CreateTime]  , 	
	                                        		[UpdateTime] 	
	               
			)
			VALUES
			(
			                             	 
                                     		@ProductName  , 	   	 
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
			set @ProductId = @identity
			SELECT @retcode  
			RETURN @retcode  

            END  


	
GO

   CREATE PROCEDURE [dbo].[spA_Product_u]  
   
		  @ProductId bigint    , 
            
		  @ProductName nvarchar (128)   , 
            
		  @CreateTime datetime    , 
            
		  @UpdateTime datetime   
                                
              AS  
			  DECLARE @retcode int, @rowcount int  
              SET LOCK_TIMEOUT 1000  
             UPDATE [dbo].[Product]
             SET 

			    			            						[ProductName] = ISNULL(@ProductName ,[ProductName]) , 		
		                 						[CreateTime] = ISNULL(@CreateTime ,[CreateTime]) , 		
		                 						[UpdateTime] = ISNULL(@UpdateTime ,[UpdateTime])		
		                 
			WHERE ProductId=@ProductId

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

/* ->
* -> 该代码使用工具生成，请勿手动修改 
* -> 生成时间： 2015-10-26 17:33:18 
*/


DROP PROCEDURE [dbo].[spA_Order_u]
GO

DROP PROCEDURE [dbo].[spA_Order_i]
GO

DROP PROCEDURE [dbo].[spA_Order_d]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[spA_Order_d]  
  
                  @OrderId bigint  
                  AS  
                  DECLARE @retcode int, @rowcount int   
                  SET LOCK_TIMEOUT 1000  
                  DELETE [Order] WHERE [OrderId]=@OrderId                
  
                    
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

  CREATE PROCEDURE [dbo].[spA_Order_i]  

    
  
                         	    
		@OrderId bigint output,

	               
                         	      @UserId bigint    , 
	               
                         	      @Name nvarchar (128)   , 
	               
                         	      @Price decimal    , 
	               
                         	      @ProductId bigint    , 
	               
                         	      @CreateTime datetime    , 
	               
                         	      @UpdateTime datetime   
	               			   AS  
           DECLARE @retcode int, @rowcount int ,@identity bigint
           SET LOCK_TIMEOUT 1000  
          INSERT INTO [dbo].[Order]
           (
		                                                                  		[UserId]  , 	
	                                        		[Name]  , 	
	                                        		[Price]  , 	
	                                        		[ProductId]  , 	
	                                        		[CreateTime]  , 	
	                                        		[UpdateTime] 	
	               
			)
			VALUES
			(
			                             	 
                                     		@UserId  , 	   	 
                                     		@Name  , 	   	 
                                     		@Price  , 	   	 
                                     		@ProductId  , 	   	 
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
			set @OrderId = @identity
			SELECT @retcode  
			RETURN @retcode  

            END  


	
GO

   CREATE PROCEDURE [dbo].[spA_Order_u]  
   
		  @OrderId bigint    , 
            
		  @UserId bigint    , 
            
		  @Name nvarchar (128)   , 
            
		  @Price decimal    , 
            
		  @ProductId bigint    , 
            
		  @CreateTime datetime    , 
            
		  @UpdateTime datetime   
                                
              AS  
			  DECLARE @retcode int, @rowcount int  
              SET LOCK_TIMEOUT 1000  
             UPDATE [dbo].[Order]
             SET 

			    			            						[UserId] = ISNULL(@UserId ,[UserId]) , 		
		                 						[Name] = ISNULL(@Name ,[Name]) , 		
		                 						[Price] = ISNULL(@Price ,[Price]) , 		
		                 						[ProductId] = ISNULL(@ProductId ,[ProductId]) , 		
		                 						[CreateTime] = ISNULL(@CreateTime ,[CreateTime]) , 		
		                 						[UpdateTime] = ISNULL(@UpdateTime ,[UpdateTime])		
		                 
			WHERE OrderId=@OrderId

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

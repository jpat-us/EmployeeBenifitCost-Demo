USE [HRMSDB]
GO

/****** Object:  UserDefinedFunction [dbo].[udfNetBenifitCost]    Script Date: 8/31/2020 10:42:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION  [dbo].[udfNetBenifitCost](
    @empID INT
	,@empYearlyCost decimal(10,2) 
    ,@depYearlyCost decimal(10,2) 
	)
RETURNS decimal(10,2)
AS 
BEGIN
    DECLARE @employeeNetCost decimal(10,2) =  0
	        ,@depCostWithA   decimal(10,2) =  (@depYearlyCost - (@depYearlyCost *.10))
			,@depCostWithoutA  decimal(10,2)
			,@empOnlyCost     decimal(10,2)
			,@depCountWithA		 INT = 0
			,@depCountWithoutA	  INT = 0
			;			
    --Dependent count where name starts with 'A'
	SELECT   @depCountWithA = count(*) 
	FROM	 dbo.Dependents 
	WHERE   employeeID = @empID
	AND    dep_firstname  LIKE 'A%'
	GROUP BY employeeID
	
	--depenents count with firstname starting without 'A'
	SELECT   @depCountWithoutA = count(*) 
	FROM	dbo.Dependents 
	WHERE	employeeID = @empID
	AND		dep_firstname NOT LIKE 'A%'
	GROUP BY employeeID
	

	
	SELECT  @depCostWithA = CASE @depCountWithA 
								WHEN NULL THEN 0
								ELSE @depCountWithA * @depCostWithA
							END
							
		
	SELECT  @depCostWithoutA = CASE @depCountWithoutA 
									WHEN NULL THEN 0
									ELSE @depCountWithoutA * @depYearlyCost
								END
			--PRINT @depCostWithoutA				

	--Check emp name starts with 'a'
	SELECT	@empOnlyCost = CASE WHEN fullName LIKE 'A%'
	                                 THEN (@empYearlyCost-(@empYearlyCost*.10))
									 ELSE @empYearlyCost
							END
	FROM    dbo.employees  
	where   employees.employeeID = @empID;

	SET    @employeeNetCost = (@empOnlyCost + @depCostWithoutA + @depCostWithA)/26;

    RETURN  @employeeNetCost;
END
GO


/****** Object:  StoredProcedure [dbo].[GetEmployeeCostDetails]    Script Date: 8/31/2020 10:58:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[GetEmployeeCostDetails]
AS
BEGIN
     DECLARE  @empCost decimal(10,2) 
			 ,@depCost decimal(10,2)
			 ,@dependent INT = 0;
	 
	 --As provided 
	 
     SELECT  @empCost  = 1000
	        ,@depCost  = 500;

     SELECT  e.employeeID, 
			 FullName,
			 Department,
			 Gender,
			 Company, 
			  (SELECT  CASE  	COUNT(ID)
			       
					   when   NULL THEN 0
					   ELSE  COUNT(ID)
					   END 
			  FROM dbo.Dependents WHERE employeeID = e.employeeID  GROUP BY employeeID) AS dependentsCount,
			 
			 dbo.udfNetBenifitCost(e.employeeID, @empCost, @depCost) AS BenifitCost
	 FROM    dbo.Employees  e 
	
	 ORDER BY employeeID

END
GO




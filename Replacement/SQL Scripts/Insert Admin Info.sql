INSERT INTO [RIS_DB].[dbo].[UserParticulars]
           ([UserId]
           ,[NRIC]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[Gender]
           ,[Prefix]
           ,[Suffix]
           ,[DateOfBirth]
           ,[Address]
           ,[PostalCode]
           ,[ContactNumber]
           ,[CountryOfResidence]
           ,[Nationality])
     VALUES
           (CONVERT(uniqueidentifier,'527F3AE4-25D3-44A8-80A4-4773314ED979')
           ,'S1234567A'
           ,'Desmond'
           ,null
           ,'Poh'
           ,'M'
           ,'Mr.'
           ,null
           ,'18-Aug-1986'
           ,'My Address'
           ,'478192'
           ,'6192998'
           ,199
           ,'Singaporean'
           )
GO





Begin Transaction UpdateTypeId 

Begin Try

	Update Tests
	set TypeId = (Select top 1 [Id] from TestTypes where ShortName = N'ฬสม')

	Commit Transaction UpdateTypeId
End Try
Begin Catch
	Rollback Transaction UpdateTypeId

	Select
		ERROR_MESSAGE() as Message,
		ERROR_LINE() as Line,
		ERROR_NUMBER() as Number
End Catch

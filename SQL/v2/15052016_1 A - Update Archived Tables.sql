
Begin Transaction addFieldsToArchive

Begin Try
	
	Alter Table ArchivedProductTests
	Add
		TestType nvarchar(150) Not Null Constraint DefaultTestType Default(''),
		TestTypeShortName nvarchar(4) Not Null Constraint DefaultShortName Default(''),
		MethodValue nvarchar(400) Not Null Constraint DefaultMethodValue Default(''),
		Remark nvarchar(400) Null

	Alter Table ArchivedProtocolResults
	Drop Column MethodValue

	Alter Table ArchivedProductTests
	Drop Constraint DefaultTestType

	Alter Table ArchivedProductTests
	Drop Constraint DefaultShortName

	Alter Table ArchivedProductTests
	Drop Constraint DefaultMethodValue

	Commit Transaction addFieldsToArchive
End Try
Begin Catch
	Rollback Transaction addFieldsToArchive

	Select
		ERROR_LINE() as Line,
		ERROR_NUMBER() as Number,
		ERROR_MESSAGE() as Message
End Catch
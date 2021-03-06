USE [DB_9BCA69_RedDb]
GO
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'5696d246-25db-4d59-bcf6-139cd303f2f4', N'Обработка на Администратори и Роли')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'a896caa3-43eb-452a-a0ce-4691290f2a19', N'Обработка на Клиенти')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'93b1ccf0-c462-464a-9294-524e5088b93b', N'Визуализация на Протоколи')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'4177b39a-ddce-46ad-812b-55d5935012ed', N'Визуализация на Клиенти')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'b93941bf-aa40-490e-9764-5aea1841de32', N'Визуализация на Зявки')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'fd53f97f-8bec-42ae-b17a-80cc7fee522f', N'Визуализация на Дневник')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'77d2cc10-dc68-4fbf-8c3d-9128df7c1a09', N'Визуализация на Потребителски действия')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'0e161082-3d84-4887-8bef-968e1ca53256', N'Визуализация на Изследвания')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a', N'Обработка на Дневник')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'e8d6d039-d94d-4465-9302-c2f6fde5d330', N'Обработка на Изследнвания')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'4a6fd1e4-7720-4385-841a-d33a58c3130a', N'Обработка на Заявки')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'95342ca3-d105-4e5e-9b37-d7205afd463e', N'Обработка на Забележки')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'9d9bc1f3-1d9a-41bd-8e03-d95b651909fc', N'Обработка на Потребителски действия')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'132fb592-e0de-4f7b-89dd-e11b4aacc4ff', N'Визуализация на Администратори и Роли')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'54471a7f-866f-4ccd-8501-ec6e08c7f052', N'Визуализация на Забележки')
INSERT [dbo].[Features] ([Id], [DisplayName]) VALUES (N'b3a0ca2d-428d-4f12-8b93-fc227350fc2c', N'Обработка на Протоколи')
INSERT [dbo].[Roles] ([Id], [DisplayName]) VALUES (N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'Администратор')

SET IDENTITY_INSERT [dbo].[RolesFeatures] ON 

INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1095, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'132fb592-e0de-4f7b-89dd-e11b4aacc4ff')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1096, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'fd53f97f-8bec-42ae-b17a-80cc7fee522f')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1097, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'54471a7f-866f-4ccd-8501-ec6e08c7f052')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1098, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'b93941bf-aa40-490e-9764-5aea1841de32')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1099, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'0e161082-3d84-4887-8bef-968e1ca53256')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1100, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'4177b39a-ddce-46ad-812b-55d5935012ed')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1101, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'77d2cc10-dc68-4fbf-8c3d-9128df7c1a09')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1102, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'93b1ccf0-c462-464a-9294-524e5088b93b')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1103, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'5696d246-25db-4d59-bcf6-139cd303f2f4')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1104, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'6b1b671c-0e4b-49fe-a3ac-9f3de4ae7e8a')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1105, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'95342ca3-d105-4e5e-9b37-d7205afd463e')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1106, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'4a6fd1e4-7720-4385-841a-d33a58c3130a')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1107, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'e8d6d039-d94d-4465-9302-c2f6fde5d330')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1108, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'a896caa3-43eb-452a-a0ce-4691290f2a19')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1109, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'9d9bc1f3-1d9a-41bd-8e03-d95b651909fc')
INSERT [dbo].[RolesFeatures] ([Id], [RoleId], [FeatureId]) VALUES (1110, N'9bbcfa41-e24f-467e-b5c9-8517105e472d', N'b3a0ca2d-428d-4f12-8b93-fc227350fc2c')

SET IDENTITY_INSERT [dbo].[RolesFeatures] OFF

INSERT [dbo].[Users] ([Id], [Username], [Password], [FirstName], [MiddleName], [LastName], [Position], [RoleId]) VALUES (N'613b0faa-8828-44a9-8bbe-09ba68cc33ae', N'master', N'master', N'Master', N'of the known', N'Universe', N'Omnipotent', N'9bbcfa41-e24f-467e-b5c9-8517105e472d')
INSERT [dbo].[Users] ([Id], [Username], [Password], [FirstName], [MiddleName], [LastName], [Position], [RoleId]) VALUES (N'0f68da69-5c82-480b-9474-54c133439b0c', N'administrator', N'QW12erty!', N'Администратор', N'Админ', N'Админ', N'Администратор', N'9bbcfa41-e24f-467e-b5c9-8517105e472d')
INSERT [dbo].[Users] ([Id], [Username], [Password], [FirstName], [MiddleName], [LastName], [Position], [RoleId]) VALUES (N'fc1934d1-ea5a-4c83-a45b-ba27bd47b5e4', N'rvsrvs', N'rvsrvs', N'rvs', N'test', N'test', N'test acc', N'9bbcfa41-e24f-467e-b5c9-8517105e472d')
INSERT [dbo].[AcredetationLevels] ([Id], [Level], [Description]) VALUES (N'3998c0aa-84fb-4d17-ba63-a410932a443a', N'B', N'Неакредитиран')
INSERT [dbo].[AcredetationLevels] ([Id], [Level], [Description]) VALUES (N'ac2405b5-26e6-483f-9655-beaa713a4896', N'A', N'Акредитиран')
SET IDENTITY_INSERT [dbo].[ActionTypes] ON 

INSERT [dbo].[ActionTypes] ([Id], [TypeName], [TypeNameBg]) VALUES (1, N'Add                 ', N'Добави              ')
INSERT [dbo].[ActionTypes] ([Id], [TypeName], [TypeNameBg]) VALUES (2, N'Edit                ', N'Промени             ')
INSERT [dbo].[ActionTypes] ([Id], [TypeName], [TypeNameBg]) VALUES (3, N'Delete              ', N'Изтри               ')
SET IDENTITY_INSERT [dbo].[ActionTypes] OFF

USE [PersonDB]
GO
/****** Object:  UserDefinedTableType [dbo].[BasicUDT]    Script Date: 28.03.2022 17:47:50 ******/
CREATE TYPE [dbo].[BasicUDT] AS TABLE(
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL
)
GO
/****** Object:  Table [dbo].[People]    Script Date: 28.03.2022 17:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[PhoneId] [int] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 28.03.2022 17:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (1, N'Ferdy', N'Hamilton', 1)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (2, N'Bethney', N'Gaila', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (3, N'Rowena', N'Chantal', NULL)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (4, N'Tabatha', N'Quin', 2)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (5, N'Abbey', N'Hubert', 3)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (1002, N'Ainsley', N'Gaila', 1002)
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [PhoneId]) VALUES (1003, N'Sherill', N'Herman', NULL)
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[Phones] ON 

INSERT [dbo].[Phones] ([Id], [PhoneNumber]) VALUES (1, N'209-200-2791')
INSERT [dbo].[Phones] ([Id], [PhoneNumber]) VALUES (2, N'209-202-6713')
INSERT [dbo].[Phones] ([Id], [PhoneNumber]) VALUES (3, N'209-205-7326')
INSERT [dbo].[Phones] ([Id], [PhoneNumber]) VALUES (1002, N'209-205-1416')
SET IDENTITY_INSERT [dbo].[Phones] OFF
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Phones_PhoneId] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phones] ([Id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Phones_PhoneId]
GO
/****** Object:  StoredProcedure [dbo].[spPerson_InsertSet]    Script Date: 28.03.2022 17:47:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spPerson_InsertSet]
	@people BasicUDT readonly
AS
BEGIN
	INSERT INTO dbo.People(FirstName, LastName)
	SELECT [FirstName], [LastName]
	FROM @people;
END
GO

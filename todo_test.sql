USE [todo_test]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 8/2/2016 10:56:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[categories_tasks]    Script Date: 8/2/2016 10:56:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories_tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NULL,
	[task_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tasks]    Script Date: 8/2/2016 10:56:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL,
	[due_date] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name]) VALUES (60, N'Home Stuff')
INSERT [dbo].[categories] ([id], [name]) VALUES (61, N'Work Stuff')
INSERT [dbo].[categories] ([id], [name]) VALUES (62, N'Home stuff')
INSERT [dbo].[categories] ([id], [name]) VALUES (63, N'Home stuff')
SET IDENTITY_INSERT [dbo].[categories] OFF
SET IDENTITY_INSERT [dbo].[categories_tasks] ON 

INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (2, 26, 16)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (3, 28, 18)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (4, 28, 19)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (5, 34, 23)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (6, 36, 24)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (9, 39, 27)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (12, 47, 34)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (13, 49, 35)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (16, 52, 38)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (17, 54, 40)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (18, 54, 41)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (19, 60, 45)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (20, 62, 46)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (10, 41, 29)
INSERT [dbo].[categories_tasks] ([id], [category_id], [task_id]) VALUES (11, 41, 30)
SET IDENTITY_INSERT [dbo].[categories_tasks] OFF

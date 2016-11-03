USE [MLog]
GO
/****** Object:  Table [dbo].[tblLog]    Script Date: 11/3/2016 12:16:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLog](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Parent] [bigint] NULL,
	[Application] [varchar](255) NULL,
	[Title] [varchar](255) NULL,
	[Description] [varchar](max) NULL,
	[IpAddress] [varchar](255) NULL,
	[JsonData] [varchar](max) NULL,
	[XmlData] [xml] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

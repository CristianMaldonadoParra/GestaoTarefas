
CREATE TABLE [dbo].[Comentario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TarefaId] [int] NOT NULL,
	[Autor] [nvarchar](100) NOT NULL,
	[Texto] [nvarchar](max) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoricoAtualizacao]    Script Date: 07/06/2025 15:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoricoAtualizacao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TarefaId] [int] NOT NULL,
	[Autor] [nvarchar](100) NOT NULL,
	[CampoAlterado] [nvarchar](100) NOT NULL,
	[ValorAntigo] [nvarchar](max) NULL,
	[ValorNovo] [nvarchar](max) NULL,
	[DataAlteracao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prioridades]    Script Date: 07/06/2025 15:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prioridades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projeto]    Script Date: 07/06/2025 15:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projeto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](200) NOT NULL,
	[Descricao] [nvarchar](max) NULL,
	[DataCriacao] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusTarefa]    Script Date: 07/06/2025 15:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusTarefa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarefa]    Script Date: 07/06/2025 15:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarefa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjetoId] [int] NOT NULL,
	[Titulo] [nvarchar](200) NOT NULL,
	[Descricao] [nvarchar](max) NULL,
	[DataVencimento] [date] NULL,
	[StatusId] [int] NOT NULL,
	[PrioridadeId] [int] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[Autor] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comentario] ADD  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[HistoricoAtualizacao] ADD  DEFAULT (getdate()) FOR [DataAlteracao]
GO
ALTER TABLE [dbo].[Projeto] ADD  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[Tarefa] ADD  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[Comentario]  WITH CHECK ADD FOREIGN KEY([TarefaId])
REFERENCES [dbo].[Tarefa] ([Id])
GO
ALTER TABLE [dbo].[HistoricoAtualizacao]  WITH CHECK ADD FOREIGN KEY([TarefaId])
REFERENCES [dbo].[Tarefa] ([Id])
GO
ALTER TABLE [dbo].[Tarefa]  WITH CHECK ADD FOREIGN KEY([PrioridadeId])
REFERENCES [dbo].[Prioridades] ([Id])
GO
ALTER TABLE [dbo].[Tarefa]  WITH CHECK ADD FOREIGN KEY([ProjetoId])
REFERENCES [dbo].[Projeto] ([Id])
GO
ALTER TABLE [dbo].[Tarefa]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[StatusTarefa] ([Id])
GO



INSERT INTO StatusTarefa (Id, Nome) VALUES
(3, 'concluída'),
(2, 'em andamento'),
(1, 'pendente');


INSERT INTO Prioridades (Id, Nome) VALUES
(3, 'alta'),
(1, 'baixa'),
(2, 'média');
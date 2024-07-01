USE [Test-Candidate]
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 30/06/2024 01:02:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[kategori_id] [int] IDENTITY(1,1) NOT NULL,
	[nama_kategori] [varchar](50) NULL,
 CONSTRAINT [PK_Kategori] PRIMARY KEY CLUSTERED 
(
	[kategori_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PengadaanDetail]    Script Date: 30/06/2024 01:02:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PengadaanDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pengadaan_no] [varchar](20) NULL,
	[nama_item] [varchar](50) NULL,
	[spesifikasi_item] [varchar](50) NULL,
	[jumlah] [int] NULL,
 CONSTRAINT [PK_PengadaanDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PengadaanHeader]    Script Date: 30/06/2024 01:02:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PengadaanHeader](
	[pengadaan_no] [varchar](20) NOT NULL,
	[pengadaan_name] [varchar](50) NULL,
	[pengadaan_kategori] [int] NULL,
	[tanggal_butuh] [datetime] NULL,
	[tanggal_buat] [datetime] NULL,
	[tanggal_update] [datetime] NULL,
 CONSTRAINT [PK_PengadaanHeader] PRIMARY KEY CLUSTERED 
(
	[pengadaan_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

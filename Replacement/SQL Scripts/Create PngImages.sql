use [RIS_DB]
go

create table PngImages
(
	[ImageId] integer primary key,
	[PngUID] [uniqueidentifier] rowguidcol not null unique default newid(),
	[PngImage] varbinary(max) filestream not null,
	constraint FK_PngImages_Images foreign key (ImageId) references [dbo].[Images](ImageId) on update cascade
)
go
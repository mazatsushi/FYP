use [RIS_DB]
go

create table DicomImages
(
	[ImageId] integer not null primary key,
	[DicomUID] [uniqueidentifier] rowguidcol not null unique default newid(),
	[DicomImage] varbinary(max) filestream not null,
	constraint FK_DicomImages_Images foreign key (ImageId) references [dbo].[Images](ImageId) on update cascade
)
go
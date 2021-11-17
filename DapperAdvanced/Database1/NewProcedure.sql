use BikeDB
go

create table Bikes
(
	Id int primary key identity(1,1) not null,
	Name nvarchar(300) not null,
	Type nvarchar(300) not null,
	Price int not null,
	Company nvarchar(300) not null, 
	
)
go

create proc Bikes_Create
	@Id int,
	@Name nvarchar(300),
	@Type nvarchar(300),
	@Price int ,
	@Company nvarchar(300)	 
as begin
	insert into Bikes
	(
		Name,
		Type,
		Price,
		Company
	)
    values
	(
		@Name,
		@Type,
		@Price,
		@Company
	)

	select SCOPE_IDENTITY() InsertedId
end
go

create proc [dbo].[Bikes_GetAll]
	@Id int = null,
	@Search nvarchar(50) = null,
	@OrderBy varchar(100) = 'name',
	@IsDescending bit = 0
as begin
	select 
		Id,
		Name,
		Type,
		Price,
		Company
	from 
		Bikes
	where
		Id = coalesce(@Id, Id)
		and
		(
			(@Search is null or @Search = '')
			or
			(
				@Search is not null
				and
				(
					Name like '%' + @Search + '%'
					or
					Type like '%' + @Search + '%'
					or
					Price like '%' + @Search + '%'
					or
					Company like '%' + @Search + '%'
				)
			)
		)
order by
		case when @OrderBy = 'name' and @IsDescending = 0 then Name end asc
		, case when @OrderBy = 'name' and @IsDescending = 1 then Name end desc
		, case when @OrderBy = 'type' and @IsDescending = 0 then type end asc
		, case when @OrderBy = 'type' and @IsDescending = 1 then type end desc
		, case when @OrderBy = 'price' and @IsDescending = 0 then price end asc
		, case when @OrderBy = 'price' and @IsDescending = 1 then price end desc
		, case when @OrderBy = 'company' and @IsDescending = 0 then Company end asc
		, case when @OrderBy = 'company' and @IsDescending = 1 then Company end desc
		
end
go

create proc [dbo].[Bikes_Get]
	@Id int
as begin
	select 
		Id,
		Name,
		Type,
		Price,
		Company
		
	from 
	Bikes
	where 
		Id = @Id
end
go

create proc [dbo].[Bikes_Update]
	@Id int,
	@Name nvarchar(300),
	@Type nvarchar(300),
	@Price int ,
	@Company nvarchar(300)
as begin
	update 
		Bikes
	set
		Name = @Name,
		Type = @Type,
		Price = @Price,
		Company = @Company
		
	where 
		Id=@Id
end
go

create proc [dbo].[Bikes_Delete]
	@Id int
as begin
	delete 
	from 
		Bikes
	where 
		Id = @Id
end
go
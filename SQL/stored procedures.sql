use ImageGallery
go


create procedure [dbo].[insert_new_password]
@Login nvarchar(50), @Password nvarchar(100)
as
begin transaction
set transaction isolation level serializable
	if((select COUNT(*) from [dbo].[PASSWORD] 
		where [Login]= @Login and [Password] = @Password) 
		= 0)
		begin try
			insert into [dbo].[PASSWORD]
			([Login],[Password])
			values(@Login,@Password)
		commit transaction
		end try
		begin catch
			rollback transaction
		end catch
go

create procedure [insert_new_user]
@Name nvarchar(100), @Surname nvarchar (100), @Id_Password int
as 
begin transaction
set transaction isolation level serializable
	if((select COUNT(*) from [dbo].[USER] 
		where [Name] = @Name and [Surname] = @Surname) = 0)
		begin try
			if((select COUNT(*) from [dbo].[PASSWORD]
				where [Id] = @Id_Password) = 1)
					INSERT INTO [dbo].[USER]
					([Name],[Surname],[Id_Password])
					VALUES(@Name,@Surname,@Id_Password)
			commit transaction
		end try
		begin catch
			rollback transaction
		end catch
go
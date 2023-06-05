using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FefuHobbies.Domain.Entities;

namespace FefuHobbies.Domain
{
	public class AppDbContex: IdentityDbContext<IdentityUser>
	{
		public AppDbContex(DbContextOptions<AppDbContex> options): base(options) { }
		//Установка новой таблицы в БД
		public DbSet<Card> Cards { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "c2cc70ec-dec2-11ed-b5ea-0242ac120002",
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
			{
				Id = "812517a2-dec2-11ed-b5ea-0242ac120002",
				Name = "admin",
				NormalizedName = "ADMIN"
            });

			builder.Entity<IdentityUser>().HasData(new IdentityUser
			{
				Id = "23587d20-dec3-11ed-b5ea-0242ac120002",
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "my@email.com",
				NormalizedEmail = "MY@EMAIL.COM",
				EmailConfirmed = true,
				PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "5090"),
				SecurityStamp = String.Empty
            });

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = "812517a2-dec2-11ed-b5ea-0242ac120002",
				UserId = "23587d20-dec3-11ed-b5ea-0242ac120002"
            });

		}
	}
}

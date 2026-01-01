using e_CommerceApi.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace e_CommerceApi.Models.Context
{
    public static class SeedDatabase
    {
        public static async void Initialize(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices
                              .CreateScope()
                              .ServiceProvider
                              .GetRequiredService<UserManager<AppUser>>();

            var roleManager = app.ApplicationServices
                              .CreateScope()
                              .ServiceProvider
                              .GetRequiredService<RoleManager<AppRole>>();

            if (!roleManager.Roles.Any())
            {
                var customer = new AppRole { Name = "Customer" };
                var admin = new AppRole { Name = "Admin" };

                await roleManager.CreateAsync(customer); //bu metodla dbye eklemiş oluyoruz
                await roleManager.CreateAsync(admin);
            }

            if (!userManager.Users.Any())
            {
                var customer = new AppUser { Name = "Anıl Torun", UserName = "torunanil", Email = "torunanil@gmail.com" };
                var admin = new AppUser { Name = "Ömer Torun", UserName = "torunomer", Email = "torunomer@gmail.com" };

                await userManager.CreateAsync(customer, "Customer_123"); //parola veriyoruz
                await userManager.AddToRoleAsync(customer, "Customer"); //kullanıcıyı role atıyoruz

                await userManager.CreateAsync(admin, "Admin_123");
                await userManager.AddToRolesAsync(admin, ["Admin","Customer"]);
            }
        }
    }
}

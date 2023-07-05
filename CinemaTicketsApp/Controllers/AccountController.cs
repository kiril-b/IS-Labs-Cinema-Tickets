using System.Security.Claims;
using CinemaTicketsDomain.DomainModels;
using CinemaTicketsDomain.DTOs;
using CinemaTicketsDomain.Identity;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaTicketsApp.Controllers;

public class AccountController : Controller {
    private readonly UserManager<CustomUser> userManager;
    private readonly SignInManager<CustomUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AccountController(UserManager<CustomUser> userManager,
        SignInManager<CustomUser> signInManager, RoleManager<IdentityRole> roleManager) {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [HttpGet, AllowAnonymous]
    public IActionResult Register() {
        UserRegistrationDto model = new UserRegistrationDto();
        return View(model);
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Register(UserRegistrationDto request) {
        await InitRoles();

        if (ModelState.IsValid) {
            var userCheck = await userManager.FindByEmailAsync(request.Email);
            if (userCheck == null) {
                var user = new CustomUser {
                    UserName = request.Email,
                    NormalizedUserName = request.Email,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    ShoppingCart = new ShoppingCart()
                };
                var result = await userManager.CreateAsync(user, request.Password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, "Customer");
                    return RedirectToAction("Login");
                }
                else {
                    if (result.Errors.Count() > 0) {
                        foreach (var error in result.Errors) {
                            ModelState.AddModelError("message", error.Description);
                        }
                    }

                    return View(request);
                }
            }
            else {
                ModelState.AddModelError("message", "Email already exists.");
                return View(request);
            }
        }

        return View(request);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterUsersFromFile(IFormFile file) {
        await InitRoles();

        string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";
        using (FileStream fileStream = System.IO.File.Create(pathToUpload)) {
            file.CopyTo(fileStream);
            fileStream.Flush();
        }

        List<UserExcelImportDto> usersDtos = getAllUsersFromFile(file.FileName);

        foreach (var userDto in usersDtos) {
            var userCheck = userManager.FindByEmailAsync(userDto.Email).Result;
            if (userCheck == null) {
                var newUser = new CustomUser() {
                    UserName = userDto.Email,
                    NormalizedUserName = userDto.Email,
                    Email = userDto.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    ShoppingCart = new ShoppingCart()
                };
                var result = userManager.CreateAsync(newUser, userDto.Password).Result;
                if (result.Succeeded) {
                    await roleManager.CreateAsync(new IdentityRole(userDto.Role));
                    await userManager.AddToRoleAsync(newUser, userDto.Role);
                }
            }
        }

        return RedirectToAction("Index", "Home");
    }

    private async Task InitRoles() {
        if (!await roleManager.RoleExistsAsync("Customer") &&
            !await roleManager.RoleExistsAsync("Admin")) {
            await roleManager.CreateAsync(new IdentityRole("Customer"));
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }

    private List<UserExcelImportDto> getAllUsersFromFile(string fileName) {
        List<UserExcelImportDto> users = new List<UserExcelImportDto>();
        string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read)) {
            using (var reader = ExcelReaderFactory.CreateReader(stream)) {
                while (reader.Read()) {
                    var newUser = new UserExcelImportDto {
                        Email = reader.GetValue(0).ToString(),
                        Password = reader.GetValue(1).ToString(),
                        Role = reader.GetValue(2).ToString()
                    };
                    users.Add(newUser);
                }
            }
        }

        return users;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() {
        UserLoginDto model = new UserLoginDto();
        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto model) {
        if (ModelState.IsValid) {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && !user.EmailConfirmed) {
                ModelState.AddModelError("message", "Email not confirmed yet");
                return View(model);
            }

            if (await userManager.CheckPasswordAsync(user, model.Password) == false) {
                ModelState.AddModelError("message", "Invalid credentials");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

            if (result.Succeeded) {
                await userManager.AddClaimAsync(user, new Claim("UserRole", "Standard"));
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut) {
                // return View("AccountLocked");
                return RedirectToAction("Index", "Home");
            }
            else {
                ModelState.AddModelError("message", "Invalid login attempt");
                return View(model);
            }
        }

        return View(model);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AddUserToRole() {
        var users = userManager.Users.ToList();
        var roles = roleManager.Roles.ToList();

        var model = new AddUserToRoleDto {
            Users = new SelectList(users, "Id", "UserName"),
            Roles = new SelectList(roles, "Name", "Name")
        };

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleDto model) {
        if (ModelState.IsValid) {
            var user = await userManager.FindByIdAsync(model.SelectedUserId);

            if (user != null) {
                var existingRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, existingRoles);

                await userManager.AddToRoleAsync(user, model.SelectedRoleName);

                return RedirectToAction("Index", "Home");
            }
        }

        var users = userManager.Users.ToList();
        var roles = roleManager.Roles.ToList();

        model.Users = new SelectList(users, "Id", "UserName");
        model.Roles = new SelectList(roles, "Name", "Name");

        return View(model);
    }
}
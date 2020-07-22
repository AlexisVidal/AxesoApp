using Axeso_BE;
using Axeso_BL;
using AxesoWeb.App_Start;
using AxesoWeb.Helpers;
using AxesoWeb.Models;
using Facebook;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vereyon.Web;

namespace AxesoWeb.Controllers
{
    public class AccountController : Controller
    {
        public static string faceappid = System.Configuration.ConfigurationManager.AppSettings["FaceAppId"];
        public static string faceappsecret = System.Configuration.ConfigurationManager.AppSettings["FaceAppSecret"];

        private ModelsBL usuarioBL = new ModelsBL();
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.IsConnected = "";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(Usuarios model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await PasswordSignInAsyncCustomAsync(model.Usuario, model.Token, true, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = true });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Credenciales inválidas.");
                    return View(model);
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            // First we clean the authentication ticket like always
            if (Session["IdUsuario"] != null)
            {
            }

            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();

            // Second we clear the principal to ensure the user does not retain any authentication
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToLocal2();
        }
        private ActionResult RedirectToLocal2(string returnUrl = "")
        {
            if (!returnUrl.IsNullOrWhiteSpace() && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Usuario");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Usuario");
        }

        public virtual async Task<SignInStatus> PasswordSignInAsyncCustomAsync(string Usuario, string Token, bool rememberMe, bool shouldLockout)
        {
            var retur = SignInStatus.Failure;

            Usuarios usuariom = null;
            string claveEncrip = "";


            claveEncrip = EncriptaClave(Token);
            var entidad = await usuarioBL.Login(Usuario);

            if (entidad != null)
            {
                if (entidad.Token.Equals(claveEncrip))
                {
                    FormsAuthentication.SetAuthCookie(entidad.UsuarioID.ToString(), false);
                    Session["IdUsuario"] = entidad.UsuarioID.ToString();
                    Session["Usuario"] = entidad.Usuario.ToString();
                    Session["Nombres"] = entidad.Descripcion;
                    Session["Email"] = entidad.Email;
                    retur = SignInStatus.Success;
                }
            }
            return retur;
        }

        private string EncriptaClave(string token)
        {
            string claveencrip = "";
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] inputBytes = (new System.Text.UnicodeEncoding()).GetBytes(token);
                byte[] hash = sha1.ComputeHash(inputBytes);

                claveencrip = Convert.ToBase64String(hash);
            }
            catch (Exception)
            {

            }
            return claveencrip;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(Usuarios model)
        {
            int contador = 0;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!model.Activo)
            {
                return View(model);
            }
            var existe = await CheckIfExistUser(model.Usuario);
            
                if (existe != null)
                {
                    //valido = 1;
                    ModelState.AddModelError("", "Correo electronico ya registrado.");
                    FlashMessage.Confirmation("Correo electronico ya registrado");
                    return View(model);
                }
            string clavetoken = EncriptaClave(model.Token);
            UsuarioInput usuarionew = new UsuarioInput()
            {
                UsuarioID = contador + 1,
                TipoUsuarioID = "1",
                Usuario = model.Usuario,
                Descripcion = model.Descripcion,
                Email = model.Usuario,
                Telefono = model.Telefono,
                Activo = true,
                Bloqueado = false,
                Token = clavetoken,
                FchHraCreacion = DateTime.Now,
                FchHraActualizacion = DateTime.Now,
                FchHraBloqueo = DateTime.Now,
                FchHraDesbloqueo = DateTime.Now
            };
            var registro = await usuarioBL.CreateUsuario(usuarionew);
            if (registro != null)
            {
                FlashMessage.Confirmation("Registro exitoso");
                return RedirectToAction("Index", "Account");
            }
            else
            {
                FlashMessage.Confirmation("Error en el registro");
            }
            return View();
        }

        private async Task<Usuarios> CheckIfExistUser(string usuario)
        {
            int contador = 0;
            try
            {
                var usuarios = await usuarioBL.Listar();
                if (usuarios.Any())
                {
                    contador = usuarios.Count();
                    var existe = usuarios.Where(x => x.Usuario.ToUpper().Equals(usuario.ToUpper())).FirstOrDefault();
                    return existe;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region Google
        //public void GoogleLogin(string email, string name, string gender, string lastname, string location)
        //{
        //    //Write your code here to access these paramerters
        //    var a = "a";
        //}
        public void SignIn(string ReturnUrl = "/", string type = "")
        {
            if (!Request.IsAuthenticated)
            {
                if (type == "Google")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Account/GoogleLoginCallback" }, "Google");
                }
            }
            else
            {
                if (type == "Google")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Account/GoogleLoginCallback" }, "Google");
                }
            }
        }
        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback()
        {
            var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;

            var loginInfo = GoogleLoginViewModel.GetLoginInfo(claimsPrincipal);
            if (loginInfo == null)
            {
                return RedirectToAction("Index");
            }
            var user = await CheckIfExistUser(loginInfo.emailaddress);
            int idgenerado = 0;
            if (user == null)
            {
                var todos = await usuarioBL.Listar();
                int contador = todos.Count();
                idgenerado = contador + 1;
                UsuarioInput usuarionew = new UsuarioInput()
                {
                    UsuarioID = idgenerado,
                    TipoUsuarioID = "1",
                    Usuario = loginInfo.emailaddress,
                    Descripcion = loginInfo.givenname + " " +loginInfo.surname,
                    Email = loginInfo.emailaddress,
                    Telefono = "",
                    Activo = true,
                    Bloqueado = false,
                    Token = "" ,
                    FchHraCreacion = DateTime.Now,
                    FchHraActualizacion = DateTime.Now,
                    FchHraBloqueo = DateTime.Now,
                    FchHraDesbloqueo = DateTime.Now
                };
                var registro = await usuarioBL.CreateUsuario(usuarionew);
                if (registro != null)
                {
                    user = registro;
                }
            }

            var ident = new ClaimsIdentity(
                    new[] { 
								// adding following 2 claim just for supporting default antiforgery provider
								new Claim(ClaimTypes.NameIdentifier, user.Email),
                                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                                new Claim(ClaimTypes.Name, user.Descripcion),
                                new Claim(ClaimTypes.Email, user.Email),
								// optionally you could add roles if any
								new Claim(ClaimTypes.Role, "User")
                    },
                    CookieAuthenticationDefaults.AuthenticationType);


            HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
            
            FormsAuthentication.SetAuthCookie(idgenerado.ToString(), false);
            Session["IdUsuario"] = idgenerado.ToString();
            Session["Usuario"] = user.Email.ToString();
            Session["Nombres"] = user.Descripcion;
            Session["Email"] = user.Email.ToString();

            return RedirectToAction("Index", "Usuario");

        }

        #endregion

        #region Facebook
        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }




        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = faceappid,
                client_secret = faceappsecret,
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public async Task<ActionResult> FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            int idgenerado = 0;
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = faceappid,
                client_secret = faceappsecret,
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            //TempData["email"] = me.email;
            //TempData["first_name"] = me.first_name;
            //TempData["lastname"] = me.last_name;
            //TempData["picture"] = me.picture.data.url;
            //FormsAuthentication.SetAuthCookie(email, false);

            var existe = await CheckIfExistUser(me.email);

            if (existe == null)
            {
                var todos = await usuarioBL.Listar();
                int contador = todos.Count();
                idgenerado = contador + 1;
                UsuarioInput usuarionew = new UsuarioInput()
                {
                    UsuarioID = idgenerado,
                    TipoUsuarioID = "1",
                    Usuario = me.email,
                    Descripcion = me.first_name + " " + me.last_name,
                    Email = me.email,
                    Telefono = "",
                    Activo = true,
                    Bloqueado = false,
                    Token = "",
                    FchHraCreacion = DateTime.Now,
                    FchHraActualizacion = DateTime.Now,
                    FchHraBloqueo = DateTime.Now,
                    FchHraDesbloqueo = DateTime.Now
                };
                var registro = await usuarioBL.CreateUsuario(usuarionew);
                if (registro != null)
                {
                   
                }
            }


            FormsAuthentication.SetAuthCookie(idgenerado.ToString(), false);
            Session["IdUsuario"] = idgenerado.ToString();
            Session["Usuario"] = me.email.ToString();
            Session["Nombres"] = me.first_name + " " + me.last_name;
            Session["Email"] = me.email;

            return RedirectToAction("Index", "Usuario");

        }
        #endregion

        #region test 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        //{
        //    ManageMessageId? message = null;
        //    IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        //    if (result.Succeeded)
        //    {
        //        message = ManageMessageId.RemoveLoginSuccess;
        //    }
        //    else
        //    {
        //        message = ManageMessageId.Error;
        //    }
        //    return RedirectToAction("Manage", new { Message = message });
        //}

        ////
        //// GET: /Account/Manage
        //public ActionResult Manage(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
        //        : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
        //        : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : "";
        //    ViewBag.HasLocalPassword = HasPassword();
        //    ViewBag.ReturnUrl = Url.Action("Manage");
        //    return View();
        //}
        //// GET: Account/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Account/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Account/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Account/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Account/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Account/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Account/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var user = await UserManager.FindAsync(loginInfo.Login);
        //    if (user != null)
        //    {
        //        await SignInAsync(user, isPersistent: false);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then prompt the user to create an account
        //        ViewBag.ReturnUrl = returnUrl;
        //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        //}

        ////
        //// GET: /Account/LinkLoginCallback
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Manage");
        //    }
        //    return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser() { UserName = model.UserName };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        ////
        //// POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut();
        //    return RedirectToAction("Index", "Usuario");
        //}

        ////
        //// GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //[ChildActionOnly]
        //public ActionResult RemoveAccountList()
        //{
        //    var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
        //    ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
        //    return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && UserManager != null)
        //    {
        //        UserManager.Dispose();
        //        UserManager = null;
        //    }
        //    base.Dispose(disposing);
        //}

        //#region Helpers
        //// Used for XSRF protection when adding external logins
        //private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private bool HasPassword()
        //{
        //    //var user = UserManager.FindById(User.Identity.GetUserId());
        //    //if (user != null)
        //    //{
        //    //    return user.PasswordHash != null;
        //    //}
        //    return false;
        //}

        //public enum ManageMessageId
        //{
        //    ChangePasswordSuccess,
        //    SetPasswordSuccess,
        //    RemoveLoginSuccess,
        //    Error
        //}



        //private class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
        //#endregion
        #endregion
    }
}

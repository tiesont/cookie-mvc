using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//
using CookieMVC.ApplicationServices;
using CookieMVC.Helpers;
using CookieMVC.Web.Security;
//
using Postal;

namespace CookieMVC.Web.Controllers
{
    public class AccountController : BaseController
    {
        internal IMembershipService Membership { get; private set; }
        internal IAuthenticator AuthenticationManager { get; private set; }

        public AccountController(ILogger logger, IEmailService mailer, IAuthenticator manager, IMembershipService service)
            : base(logger, mailer)
        {
            Membership = service;
            AuthenticationManager = manager;
        }

        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.LoginFormModel model, string returnUrl = "")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var valid = await Task.Factory.StartNew(() => Membership.ValidateCredentials(model.Email, model.Password));
                    if (valid)
                    {
                        AuthenticationManager.SignIn(model.Email, model.CreatePersistentSession);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        var statusCode = Membership.CheckMembershipStatus(model.Email);
                        string message = string.Empty;

                        switch (statusCode)
                        {
                            case MembershipStatus.Locked:
                                message = string.Format("Maximum attempts reached. You must wait {0} hours to login.", AppSettings.LockedOutWindow);
                                break;
                            default:
                                message = "Username or password is not valid. Please try again.";
                                break;
                        }

                        TempData.Remove(Alert.Error);
                        TempData.Add(Alert.Error, message);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(Models.ForgotPasswordFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userStatus = await Task.Factory.StartNew(() => Membership.CheckMembershipStatus(model.Email));
                    if (userStatus == MembershipStatus.Active)
                    {
                        string token = await Task.Factory.StartNew(() => Membership.GeneratePasswordResetToken(model.Email));
                        var resetLink = Url.Action("resetpassword", "account", new { id = token }, protocol: Request.Url.Scheme);
                        string html = string.Format("<a href=\"{0}\">{1}</a>", resetLink, resetLink);
                        await SendEmailAsync("PasswordResetNotification", model.Email, "Reset Password", html, resetLink);
                    }

                    TempData.Remove(Alert.Success);
                    TempData.Add(Alert.Success, "<b>Thank you!</b> Please check your email for further instructions.");

                    return RedirectToAction("login", "account");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }


        public ActionResult ResetPassword(string id = "")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("login", "account");
            }

            var model = new Models.ResetPasswordFormModel()
            {
                Token = id
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(Models.ResetPasswordFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = await Task.Factory.StartNew(() => Membership.ResetPassword(model.Token, model.Password));
                    if (result)
                    {
                        TempData.Clear();
                        TempData.Add(Alert.Success, "<b>Thank you!</b> Your password has been updated. You may now login using your new password.");
                        return RedirectToAction("login", "account");
                    }
                    else
                    {
                        TempData.Remove(Alert.Error);
                        TempData.Add(Alert.Error, "<b>I'm sorry,</b> but your password could not be reset. Please try again.");

                        return View(model);
                    }
                }
            }
            catch (MinPasswordLengthException ex)
            {
                HandleException(ex, ex.Message);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }


        public ActionResult LogOut()
        {
            Session.Clear();
            TempData.Clear();

            AuthenticationManager.SignOut();
            TempData.Add(Alert.Success, "<b>Thank you!</b> You have been successfully logged out. See you soon!");

            return RedirectToAction("login", "account");
        }
    }
}
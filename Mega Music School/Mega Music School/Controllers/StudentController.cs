using Mega_Music_School.Database;
using Mega_Music_School.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mega_Music_School.IHelper;
using Mega_Music_School.Enum;
using Microsoft.AspNetCore.Http;

namespace Mega_Music_School.Controllers
{
    public class StudentController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<UserAndAdminProfile> _userManager;
        private readonly SignInManager<UserAndAdminProfile> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAccountService _accountService;

        public object VideoUpdate { get; private set; }

        // private IAccountService injectedAccountService;
        // private UserAndAdminProfile newInstanceOfUserAndAdminProfileModelAboutToBeCreated;

        public StudentController(ApplicationDbContext db, UserManager<UserAndAdminProfile> injectedUserManager, SignInManager<UserAndAdminProfile> injectedSignInManager,
            IWebHostEnvironment injectedWebHostEnvironment, IAccountService injectedAccountService)
        {
            _db = db;
            _userManager = injectedUserManager;
            _signInManager = injectedSignInManager;
            _webHostEnvironment = injectedWebHostEnvironment;
            _accountService = injectedAccountService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateStudentProfile()
        {
            return View();
        }
        
        
        [HttpPost]
        // POST || PROFILEUPDATE
        public IActionResult UpdateStudentProfile(UserAndAdminProfile UserAndAdminProfileDetailsForReg)
        {
            try
            {
                var ProfileDisplay = string.Empty;

                if (UserAndAdminProfileDetailsForReg.ProfilePicturePngJpg == null)
                {
                    UserAndAdminProfileDetailsForReg.Message = "Your Picture.";
                    UserAndAdminProfileDetailsForReg.ErrorHappened = true;
                    return View(UserAndAdminProfileDetailsForReg);
                }
                else
                {
                    ProfileDisplay = ProfilePicture(UserAndAdminProfileDetailsForReg.ProfilePicturePngJpg, "/StudentsPix/");
                }

                //ViewBag.allDepartment = _accountService.GetAllTheDepartment();
                //ViewBag.allCountry = _accountService.GetAllTheCountry();


                if (UserAndAdminProfileDetailsForReg.Address == null)
                {
                    UserAndAdminProfileDetailsForReg.Message = "Your Email was empty,please put mail.";
                    UserAndAdminProfileDetailsForReg.ErrorHappened = true;
                    return View(UserAndAdminProfileDetailsForReg);
                }

                if (UserAndAdminProfileDetailsForReg.DateOfBirth == null)
                {
                    UserAndAdminProfileDetailsForReg.Message = " Please put in your first and last name.";
                    UserAndAdminProfileDetailsForReg.ErrorHappened = true;
                    return View(UserAndAdminProfileDetailsForReg);
                }

                if (UserAndAdminProfileDetailsForReg.JoiningDate == null)
                {
                    UserAndAdminProfileDetailsForReg.Message = "Please put in your phone number.";
                    UserAndAdminProfileDetailsForReg.ErrorHappened = true;
                    return View(UserAndAdminProfileDetailsForReg);
                }

                if (UserAndAdminProfileDetailsForReg.AcademicQualification == null)
                {
                    UserAndAdminProfileDetailsForReg.Message = "Please enter your password.";
                    UserAndAdminProfileDetailsForReg.ErrorHappened = true;
                    return View(UserAndAdminProfileDetailsForReg);
                }
                if (UserAndAdminProfileDetailsForReg.ProfilePicturePngJpg == null)
                {
                    UserAndAdminProfileDetailsForReg.Message = "Please enter your password.";
                    UserAndAdminProfileDetailsForReg.ErrorHappened = true;
                    return View(UserAndAdminProfileDetailsForReg);
                }

                // Query the user details if it exists in thr Db B4 Authentication
                var existingUserDetails = _userManager.Users.Where(s => s.UserName == User.Identity.Name)?.FirstOrDefault();
                if (existingUserDetails != null)

                existingUserDetails.Address = UserAndAdminProfileDetailsForReg.Address;
                existingUserDetails.DateOfBirth = UserAndAdminProfileDetailsForReg.DateOfBirth;
                existingUserDetails.JoiningDate = UserAndAdminProfileDetailsForReg.JoiningDate;
                existingUserDetails.AcademicQualification = UserAndAdminProfileDetailsForReg.AcademicQualification;
                existingUserDetails.ProfilePictureUrl = ProfileDisplay;

                _db.UserAndAdminProfiles.Update(existingUserDetails);
                _db.SaveChanges();


                existingUserDetails.Message = "You have Successfully Updated Your Profile, Go To View";
                existingUserDetails.ErrorHappened = false;
                return View(existingUserDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //METHOD TO VIEW PERSON'S PROFILE PIC
        //To make profile pix showing
        //We collected user's profile pics and create a method which collects it, and store them in one directory 

        public string ProfilePicture(IFormFile filesUrl, string fileLocation)
        {
            string uniqueFileName = string.Empty;

            if (filesUrl != null)
            {
                var upPath = fileLocation.Trim('/');
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, upPath);

                string pathString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", upPath);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filesUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    filesUrl.CopyTo(fileStream);
                }
            }
            var generatedPictureFilePath = fileLocation + uniqueFileName;
            return generatedPictureFilePath;
        }

        //VIEW PROFILE
        [HttpGet]
        public IActionResult ViewProfile()
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
                var userDetails = _userManager.Users.Where(u => u.UserName == userName).Include(u => u.Country).Include(u => u.LocalGovernmentArea).Include(u => u.State).FirstOrDefault();
                return View(userDetails);
            }
            return RedirectToAction("ViewProfile", "Student");
        }

        //UPLOAD PROJECT
        [HttpGet]
        public IActionResult UploadVideo()
        {            
            {
                return View();
            }
        }


        [HttpPost]
        // POST || UPLOADPROJECT
        public IActionResult UploadVideo(Video videoUpdate)
        {
            try
            {
                if (videoUpdate.VideoName == null)
                {
                    videoUpdate.Message = "Input Video Name To Continue.";
                    videoUpdate.ErrorHappened = true;
                    return View(videoUpdate);
                }

                if (videoUpdate.VideoLink == null)
                {
                    videoUpdate.Message = "Input Video Link To Continue.";
                    videoUpdate.ErrorHappened = true;
                    return View(videoUpdate);
                }

                if (videoUpdate.VideoDetails == null)
                {
                    videoUpdate.Message = "Write some details about the clip 2 continue.";
                    videoUpdate.ErrorHappened = true;
                    return View(videoUpdate);
                }

                // Query the user details if it exists in thr Db B4 Authentication

                var userDetails = _db.UserAndAdminProfiles.Where(s => s.UserName == User.Identity.Name)?.FirstOrDefault();
                if (userDetails != null)
                {
                    var newVideo = new Video
                    {
                        VideoName = videoUpdate.VideoName,
                        VideoDetails = videoUpdate.VideoDetails,
                        VideoLink = videoUpdate.VideoLink,
                        StudentID = userDetails.Id,
                        VideoStatus = VideoStatus.Pending,
                        DateAdded = DateTime.Now,
                        Deleted = false,
                    };
                    _db.Add(newVideo);
                    _db.SaveChanges();
                    videoUpdate.Message = "Video Uploded Sucessfully";
                    videoUpdate.ErrorHappened = false;
                    return View(videoUpdate);
                }
                else
                {
                    videoUpdate.Message = "Invalid Credentials,please login";
                    videoUpdate.ErrorHappened = true;
                    return View(videoUpdate);
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
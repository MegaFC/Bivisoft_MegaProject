using Mega_Music_School.Database;
using Mega_Music_School.Enum;
using Mega_Music_School.IHelper;
using Mega_Music_School.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mega_Music_School.Helper
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UserAndAdminProfile> _userManager;
        private readonly SignInManager<UserAndAdminProfile> _signInManager;


        public AccountService(ApplicationDbContext db, UserManager<UserAndAdminProfile> injectedUserManager, SignInManager<UserAndAdminProfile> injectedSignInManager)
        {
            _db = db;
            _userManager = injectedUserManager;
            _signInManager = injectedSignInManager;

        }

        public List<Department> GetAllTheDepartment()
        {
            var newDepartment = new Department()
            {
                Id = 0,
                Name = "---Select Departments---"
            };
            var getAllTheDepartments = _db.Departments.Where(x => x.Active == true).ToList();
            getAllTheDepartments.Insert(0, newDepartment);

            return getAllTheDepartments;
        }

        public List<Country> GetAllTheCountry()
        {
            var newCountry = new Country()
            {
                Id = 0,
                Name = "---Select Countries---"
            };
            var getAllTheCountries = _db.Countries.Where(x => x.Active == true).ToList();
            getAllTheCountries.Insert(0, newCountry);

            return getAllTheCountries;
        }

        public List<State> GetAllTheStateAttachedToACountry(int countryId)
        {
            var newState = new State()
            {
                Id = 0,
                Name = "---Select States---"
            };
            var getAllTheStates = _db.State.Where(x => x.Active == true && x.NationalityId == countryId).ToList();
            getAllTheStates.Insert(0, newState);

            return getAllTheStates;
        }

        public List<LGA> GetAllTheLGAsAttachedToAState(int stateId)
        {
            var newLGAs = new LGA()
            {
                Id = 0,
                Name = "---Select LGAs---"
            };
            var getAllTheLGAs = _db.LGAs.Where(x => x.Active == true && x.StateId == stateId).ToList();
            getAllTheLGAs.Insert(0, newLGAs);

            return getAllTheLGAs;
        }




        public List<Department> PopulateListOfDepartmentsToTable()
        {

            var getAllTheDepartments = _db.Departments.Where(x => x.Active == true).ToList();

            return getAllTheDepartments;
        }

        public List<Country> PopulateListOfCountriesToTable()
        {

            var getAllTheCountries = _db.Countries.Where(x => x.Active == true).ToList();

            return getAllTheCountries;
        }

        public List<State> PopulateListOfStateToTable()
        {

            var getAllTheStates = _db.State.Where(x => x.Active == true).ToList();

            return getAllTheStates;
        }

        public List<LGA> PopulateListOfLGAToTable()
        {

            var getAllTheLGAs = _db.LGAs.Where(x => x.Active == true).ToList();

            return getAllTheLGAs;
        }
        //TRYING TO RETRIEVE VIDEO FROM DATABASE TO THE ADMIN PAGE
        public List<Video> PopulateListOfVideoToTable()
        {

            var getAllTheVideos = _db.Videos.Where(x => x.VideoStatus == VideoStatus.Pending).ToList();

            return getAllTheVideos;
        }

        /// <summary>
        // tRYING TO RETRIEVE VIDEO FROM DATABASE TO THE ADMIN PAGE.
        /// </summary>
        /// <returns> all approved videos</returns>
        public List<Video> ApprovedVideo()
        {

            var getAllAcceptedVideo = _db.Videos.Where(x => x.VideoStatus == VideoStatus.Accepted).ToList();

            return getAllAcceptedVideo;
        }
        
        //TRYING TO RETRIEVE COURSE SAVED IN THE DATABASE FROM THE ADMIN AND PUSHING IT TO THE LEARNING PAGE
        public List<Course> CoursesToDownload()
        {

            var getAllCourseForDownload = _db.Courses.Include(x => x.Department).ToList();

            return getAllCourseForDownload;

        }
    }
}
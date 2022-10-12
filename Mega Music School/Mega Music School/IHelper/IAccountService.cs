using Mega_Music_School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mega_Music_School.IHelper
{
    public interface IAccountService
    {
        List<Department> GetAllTheDepartment();
        List<Country> GetAllTheCountry();
        List<State> GetAllTheStateAttachedToACountry(int countryId);
        List<LGA> GetAllTheLGAsAttachedToAState(int stateId);

        //HERE AM DOING ANOTHER REFERENCING
        List<Country> PopulateListOfCountriesToTable();
        List<Department> PopulateListOfDepartmentsToTable();
        List<State> PopulateListOfStateToTable();
        List<LGA> PopulateListOfLGAToTable();

        //Population of video
        List<Video> PopulateListOfVideoToTable();

        //Approved Video
        List<Video> ApprovedVideo();

        //Video FindVideoById(Guid Id);
        List<Course> CoursesToDownload();

    }
}

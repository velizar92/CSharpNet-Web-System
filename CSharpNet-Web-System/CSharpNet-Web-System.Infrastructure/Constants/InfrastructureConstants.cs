using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNet_Web_System.Infrastructure.Constants
{
    public class InfrastructureConstants
    {

        // User
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string ProfileImageUrl = "ProfileImageUrl";

        // Roles
        public const string AdminRole = "Admin";
        public const string LearnerRole = "Learner";

        // File formats
        public const string Presentation = "PPT Presentation";
        public const string Video = "Video MP4";
        public const string PdfDocument = "PDF Document";

        // Mime types
        public const string PDF = "application/pdf";
        public const string MP4 = "video/mp4";
        public const string PPT = "pptx";

        // Validation Messages (to be extracted in res files if possible)
        public const string FIELD_REQUIRED = "Полето '{0}' е задължително.";
        public const string MIN_MAX_STRING_VALIDATION = "Полето '{0}' трябва да бъде низ с минимална дължина от {2} и максимална дължина от {1} символа.";
        public const string PASSWORDS_DONT_MATCH = "Паролата и паролата за потвърждение не съвпадат.";
        public const string PASSWORD_LENGTH_VALIDATION = "{0}та трябва да е най-малко {2} и максимално {1} символа дълга.";
    }
}

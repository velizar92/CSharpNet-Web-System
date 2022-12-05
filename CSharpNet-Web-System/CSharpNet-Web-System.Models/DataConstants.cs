namespace CSharpNet_Web_System.Models
{
    // TODO: Maybe should remove the inner classes and not using static imports for the whole class, but just for one constant. Anyway, this is a minor improvement, can even be ignored.
    public class DataConstants
    {
        public class User
        {
            public const int FirstNameMinLength = 5;
            public const int FirstNameMaxLength = 50;
            public const int LastNameMinLength = 5;
            public const int LastNameMaxLength = 50;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public class Course
        {
            public const int CourseNameMinLength = 5;
            public const int CourseNameMaxLength = 150;
            public const int CourseDescriptionMinLength = 20;
            public const int CourseDescriptionMaxLength = 2000;
        }

        public class Tutorial
        {
            public const int TutorialTitleMinLength = 5;
            public const int TutorialTitleMaxLength = 150;
            public const int TutorialDescriptionMinLength = 20;
            public const int TutorialDescriptionMaxLength = 1000;
        }

        public class Resource
        {
            public const int ResourceNameMinLength = 10;
            public const int ResourceNameMaxLength = 150;
        }

        public class ResourceType
        {
            public const int ResourceTypeNameMinLength = 5;
            public const int ResourceTypeNameMaxLength = 100;
        }

        public class Issue
        {
            public const int IssueTitleMinLength = 10;
            public const int IssueTitleMaxLength = 150;

            public const int IssueDescriptionMinLength = 20;
            public const int IssueDescriptionMaxLength = 1000;
        }

        public class Comment
        {
            public const int CommentContentMinLength = 2;
            public const int CommentContentMaxLength = 300;

            public const int IssueDescriptionMinLength = 20;
            public const int IssueDescriptionMaxLength = 1000;
        }
    }
}
